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
		private StatisticsRepo(string repoPath, DateTime from, DateTime to)
		{
			RawStatistics = new SortedDictionary<DateTime, DatePoint>();
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
		public static StatisticsRepo Analyze(string path, DateTime from, DateTime to)
		{
			return new StatisticsRepo(path, from, to);
		}

		/// <summary>
		/// Analyzes all commits of a repository.
		/// </summary>
		/// <param name="path">Path of the repository to be analyzed.</param>
		public static StatisticsRepo Analyze(string path)
		{
			return Analyze(path, DateTime.MinValue, DateTime.MaxValue);
		}

		public void Dispose()
		{
			if (repo != null)
				repo.Dispose();
		}
	}
}