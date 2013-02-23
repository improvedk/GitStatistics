using LibGit2Sharp;
using System;
using System.Collections.Generic;

namespace GitMetrics
{
	public class MetricsRepo : IDisposable
	{
		private Repository repo;

		public MetricsRepo(string repoPath)
		{
			repo = new Repository(repoPath);
		}

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

		public void Dispose()
		{
			if (repo != null)
				repo.Dispose();
		}
	}
}