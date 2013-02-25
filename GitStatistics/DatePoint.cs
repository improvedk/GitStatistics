using System.Linq;
using System.Collections.Generic;

namespace GitStatistics
{
	public class DatePoint
	{
		private readonly Dictionary<string, Committer> committers = new Dictionary<string, Committer>();

		public IEnumerable<Committer> Committers
		{
			get { return committers.Values; }
		}

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

		/// <summary>
		/// No public instantiations please
		/// </summary>
		internal DatePoint()
		{ }

		/// <summary>
		/// Either adds or updates an existing committers total statistics
		/// </summary>
		internal void AddOrUpdateCommitter(Committer committer)
		{
			if (committers.ContainsKey(committer.Email))
			{
				var existingCommitter = committers[committer.Email];
				existingCommitter.NumberOfCommits += committer.NumberOfCommits;
				existingCommitter.TotalLinesAdded += committer.TotalLinesAdded;
				existingCommitter.TotalLinesDeleted += committer.TotalLinesDeleted;
			}
			else
				committers[committer.Email] = committer;
		}
	}
}