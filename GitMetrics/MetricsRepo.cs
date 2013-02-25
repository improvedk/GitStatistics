using System.Collections.Generic;
using System.Linq;
using System;
using LibGit2Sharp;

namespace GitMetrics
{
	/// <summary>
	/// Used for analyzing git repositories and gathering useful statistics on code, commits and authors.
	/// </summary>
	public class MetricsRepo : IDisposable
	{
		private Repository repo;

		public SortedDictionary<DateTime, DateMetric> RawMetrics { get; private set; }

		/// <summary>
		/// Can only be instantiated through the Analyze methods. Performs complete analysis in the constructor.
		/// </summary>
		private MetricsRepo(string repoPath, DateTime from, DateTime to)
		{
			RawMetrics = new SortedDictionary<DateTime, DateMetric>();
			repo = new Repository(repoPath);
			
			// Commits within the specified date range
			var commits = repo.Commits.Where(x => x.Author.When >= from && x.Author.When <= to);

			foreach (var commit in commits)
			{
				
			}
		}

		/// <summary>
		/// Analyzes commits of a repository between from and to.
		/// </summary>
		/// <param name="path">Path of the repository to be analyzed.</param>
		public static MetricsRepo Analyze(string path, DateTime from, DateTime to)
		{
			return new MetricsRepo(path, from, to);
		}

		/// <summary>
		/// Analyzes all commits of a repository.
		/// </summary>
		/// <param name="path">Path of the repository to be analyzed.</param>
		public static MetricsRepo Analyze(string path)
		{
			return Analyze(path, DateTime.MinValue, DateTime.MaxValue);
		}

		public void Dispose()
		{
			if (repo != null)
				repo.Dispose();
		}

		/*
			/// <summary>
			/// Aggregates all commits and counts the number of commits per day. Days with no commits are not included.
			/// </summary>
			public SortedDictionary<DateTime, int> GetCommitCountPerDay()
			{
				var result = new SortedDictionary<DateTime, int>();

				foreach (var commit in repo.Commits)
				{
					var date = new DateTime(commit.Author.When.Year, commit.Author.When.Month, commit.Author.When.Day);
				
					if (!result.ContainsKey(date))
						result.Add(date, 0);

					result[date]++;
				}

				return result;
			}

			/// <summary>
			/// Aggregates all commits and counts the number of commits per month. Months with no commits are not included.
			/// </summary>
			/// <returns></returns>
			public SortedDictionary<DateTime, int> GetCommitCountPerMonth()
			{
				var result = new SortedDictionary<DateTime, int>();

				foreach (var commit in repo.Commits)
				{
					var date = new DateTime(commit.Author.When.Year, commit.Author.When.Month, 1);

					if (!result.ContainsKey(date))
						result.Add(date, 0);

					result[date]++;
				}

				return result;
			}
		*/
	}
}