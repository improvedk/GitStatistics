using System;

namespace GitStatistics
{
	/// <summary>
	/// Default options for the repository analysis.
	/// </summary>
	public class AnalysisOptions
	{
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public bool IgnoreMergeCommits { get; set; }

		public AnalysisOptions()
		{
			From = new DateTime(1000, 1, 1);
			To = new DateTime(3000, 1, 1);
			IgnoreMergeCommits = true;
		}
	}
}