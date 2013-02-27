using System;

namespace GitStatistics
{
	/// <summary>
	/// Default options for the repository analysis.
	/// </summary>
	public class AnalysisOptions
	{
		/// <summary>
		/// The time from which commits should be analyzed. Default = 1000-01-01
		/// </summary>
		public DateTime From { get; set; }

		/// <summary>
		/// The time until which commits should be analyzed. Default = 3000-01-01
		/// </summary>
		public DateTime To { get; set; }

		/// <summary>
		/// Ignores all merge commits - that is, commits with more than one parent. Default = true
		/// </summary>
		public bool IgnoreMergeCommits { get; set; }

		/// <summary>
		/// Ignores the very first commit. Default = true
		/// </summary>
		public bool IgnoreFirstCommit { get; set; }

		/// <summary>
		/// If set, dates with no commits will be included in the result set. Default = false
		/// </summary>
		public bool IncludeDatesWithNoCommits { get; set; }

		public AnalysisOptions()
		{
			From = new DateTime(1000, 1, 1);
			To = new DateTime(3000, 1, 1);
			IgnoreMergeCommits = true;
			IgnoreFirstCommit = true;
			IncludeDatesWithNoCommits = false;
		}
	}
}