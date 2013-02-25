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
		public bool IgnoreMerges { get; set; }

		public AnalysisOptions()
		{
			From = DateTime.MinValue;
			To = DateTime.MaxValue;
			IgnoreMerges = true;
		}
	}
}