using System.Collections.Generic;

namespace GitMetrics
{
	public class DateMetric
	{
		public IEnumerable<Author> Authors { get; private set; }
		public IEnumerable<Committer> Committers { get; private set; }
		public int NumberOfCommits { get; internal set; }
		public int TotalLinesAdded { get; internal set; }
		public int TotalLinesDeleted { get; internal set; }

		public double LinesAddedPerCommit
		{
			get { return (double)TotalLinesAdded / NumberOfCommits; }
		}

		public double LinesDeletedPerCommit
		{
			get { return (double)TotalLinesDeleted / NumberOfCommits; }
		}

		public int TotalLinesModified
		{
			get { return TotalLinesAdded + TotalLinesDeleted; }
		}

		public double LinesModifiedPerCommit
		{
			get { return (double)TotalLinesModified / NumberOfCommits; }
		}

		internal DateMetric()
		{
			Authors = new List<Author>();
			Committers = new List<Committer>();
		}
	}
}