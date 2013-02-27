using System;

namespace GitStatistics.Adhoc
{
	class Program
	{
		private const string repoPath = @"D:\Projects\reveal.js (GIT)";

		static void Main()
		{
			var options = new AnalysisOptions {
				IgnoreFirstCommit = false,
				IgnoreMergeCommits = true,
				IncludeDatesWithNoCommits = true
			};

			var repo = StatisticsRepo.Analyze(repoPath, options);
			
			foreach (var date in repo.RawStatistics.Values)
			{
				Console.WriteLine(date.Date.ToString() + ": " + date.TotalLinesAdded + " / " + date.TotalLinesDeleted + " (" + date.NumberOfCommits + ")");
				break;
			}

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}