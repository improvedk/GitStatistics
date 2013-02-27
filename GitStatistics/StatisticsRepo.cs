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

				// Create a committer instance based on the commit
				var committer = new Committer {
					Email = commit.Committer.Email,
					NumberOfCommits = 1
				};

				// What type of commit are we dealing with?
				switch (commit.ParentsCount)
				{
					// Root commit
					case 0:
						if (options.IgnoreFirstCommit)
							continue;

						// TODO: Count lines manually
						break;

					// Normal commit
					case 1:
						var diff = repo.Diff.Compare(commit.Parents.First().Tree, commit.Tree);

						committer.TotalLinesAdded = diff.LinesAdded;
						committer.TotalLinesDeleted = diff.LinesDeleted;
						break;

					// Merge commit
					default:
						if (options.IgnoreMergeCommits)
							continue;

						// TODO: Diff according to both parents
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