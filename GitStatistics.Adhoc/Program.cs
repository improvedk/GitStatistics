using System;

namespace GitStatistics.Adhoc
{
	class Program
	{
		private const string repoPath = @"D:\Projects\reveal.js";

		static void Main()
		{
			var repo = StatisticsRepo.Analyze(repoPath);
			

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}