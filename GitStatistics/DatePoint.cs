using System.Linq;
using System.Collections.Generic;

namespace GitStatistics
{
	public class DatePoint
	{
		public IEnumerable<Committer> Committers { get; private set; }

		public int NumberOfCommits
		{
			get { return Committers.Sum(x => x.NumberOfCommits); }
		}

		public int TotalLinesAdded
		{
			get { return Committers.Sum(x => x.TotalLinesAdded); }
		}

		public int TotalLinesDeleted
		{
			get { return Committers.Sum(x => x.TotalLinesDeleted); }
		}

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

		internal DatePoint()
		{
			Committers = new List<Committer>();
		}
	}
}