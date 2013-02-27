using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace GitStatistics
{
	/// <summary>
	/// Used for analyzing git repositories and gathering useful statistics on code, commits and authors.
	/// </summary>
	public class StatisticsRepo : IDisposable
	{
		private Repository repo;

		public SortedDictionary<DateTime, DatePoint> RawStatistics { get; private set; }

		/// <summary>
		/// Can only be instantiated through the Analyze methods. Performs complete analysis in the constructor.
		/// </summary>
		private StatisticsRepo(string repoPath, AnalysisOptions options)
		{
			RawStatistics = new SortedDictionary<DateTime, DatePoint>();
			repo = new Repository(repoPath);
			
			// Commits within the specified date range
			var commits = repo.Commits.Where(x => x.Author.When >= options.From && x.Author.When <= options.To);

			foreach (var commit in commits)
			{
				// Get the date and a corresponding DatePoint, either new or existing
				var commitDate = commit.Committer.When.Date;
				var data = RawStatistics.ContainsKey(commitDate) ? RawStatistics[commitDate] : new DatePoint();
				data.Date = commitDate;

				// Create a committer instance based on the commit
				var committer = new Committer {
					Email = commit.Committer.Email,
					NumberOfCommits = 1
				};

				// What type of commit are we dealing with?
				TreeChanges diff;

				switch (commit.ParentsCount)
				{
					// Root commit
					case 0:
						if (options.IgnoreFirstCommit)
							continue;

						// Queue to hold child trees and files that we need to process
						var entries = new Queue<TreeEntry>();

						// Queue up first level entries
						foreach (var entry in commit.Tree)
							entries.Enqueue(entry);
							
						// Loop until we've processed everything
						while (entries.Count > 0)
						{
							var entry = entries.Dequeue();

							switch (entry.Type)
							{
								// For trees we'll queue up all children
								case GitObjectType.Tree:
									var tree = (Tree)entry.Target;

									foreach (var child in tree)
										entries.Enqueue(child);
									break;

								// For files we'll count the lines
								case GitObjectType.Blob:
									var blob = (Blob)entry.Target;

									// There are probably better ways of detecting binary files, but for now we'll use the same
									// hack as libgit and just detect if any \0 bytes are present.
									if (blob.Content.Contains((byte)0x0))
									{
										// For now we'll just ignore binary files
									}
									else
									{
										// For text files we'll define a linebreak as being = \n
										// TODO: Detect actual encoding rather than assuming it's UTF-8
										committer.TotalLinesAdded += blob.ContentAsUtf8().Split('\n').Length;
									}
									break;
							}
						}
						break;

					// Normal commit
					case 1:
						// Diff parent and note changes
						diff = repo.Diff.Compare(commit.Parents.First().Tree, commit.Tree);
						committer.TotalLinesAdded = diff.LinesAdded;
						committer.TotalLinesDeleted = diff.LinesDeleted;
						break;

					// Merge commit
					default:
						if (options.IgnoreMergeCommits)
							continue;

						// Diff each parent and sum up changes
						foreach (var parent in commit.Parents)
						{
							diff = repo.Diff.Compare(parent.Tree, commit.Tree);
							committer.TotalLinesAdded += diff.LinesAdded;
							committer.TotalLinesDeleted += diff.LinesDeleted;
						}
						break;
				}

				// Add this committers data to the aggregated total
				data.AddOrUpdateCommitter(committer);

				// Update or store DatePoint
				RawStatistics[commitDate] = data;
			}
		}

		/// <summary>
		/// Analyzes commits of a repository between from and to.
		/// </summary>
		/// <param name="path">Path of the repository to be analyzed.</param>
		public static StatisticsRepo Analyze(string path, AnalysisOptions options)
		{
			return new StatisticsRepo(path, options);
		}

		/// <summary>
		/// Analyzes all commits of a repository.
		/// </summary>
		/// <param name="path">Path of the repository to be analyzed.</param>
		public static StatisticsRepo Analyze(string path)
		{
			return new StatisticsRepo(path, new AnalysisOptions());
		}

		public void Dispose()
		{
			if (repo != null)
				repo.Dispose();
		}
	}
}