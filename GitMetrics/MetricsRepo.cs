using LibGit2Sharp;
using System;

namespace GitMetrics
{
	public class MetricsRepo : IDisposable
	{
		private Repository repo;

		public MetricsRepo(string repoPath)
		{
			repo = new Repository(repoPath);
		}

		public void Dispose()
		{
			if (repo != null)
				repo.Dispose();
		}
	}
}