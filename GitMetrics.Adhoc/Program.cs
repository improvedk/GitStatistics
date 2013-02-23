using System;

namespace GitMetrics.Adhoc
{
	class Program
	{
		private const string repoPath = @"E:\iPaperCMS";

		static void Main()
		{
			printCommitsPerMonth();

			Console.WriteLine("Done");
			Console.ReadLine();
		}

		static void printCommitsPerDay()
		{
			using (var repo = new MetricsRepo(repoPath))
			{
				var result = repo.GetCommitCountPerDay();

				foreach (var date in result.Keys)
					Console.WriteLine(date + ": " + result[date]);
			}
		}

		static void printCommitsPerMonth()
		{
			using (var repo = new MetricsRepo(repoPath))
			{
				var result = repo.GetCommitCountPerMonth();

				foreach (var date in result.Keys)
					Console.WriteLine(date + ": " + result[date]);
			}
		}
	}
}