using System;

namespace GitMetrics.Adhoc
{
	class Program
	{
		private const string repoPath = @"D:\Projects\reveal.js";

		static void Main()
		{
			var repo = MetricsRepo.Analyze(repoPath);
			

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}