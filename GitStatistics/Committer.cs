namespace GitStatistics
{
	public class Committer
	{
		public int NumberOfCommits { get; internal set; }
		public int TotalLinesAdded { get; internal set; }
		public int TotalLinesDeleted { get; internal set; }
		public string Email { get; internal set; }
	}
}