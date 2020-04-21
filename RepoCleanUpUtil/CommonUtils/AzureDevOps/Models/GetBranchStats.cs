using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class GetBranchStats
  {
    public class Payload
    {
      public Commit commit { get; set; }
      public string name { get; set; }
      public int aheadCount { get; set; }
      public int behindCount { get; set; }
      public bool isBaseVersion { get; set; }
      public bool isBase_or_master => isBaseVersion || name.Equals("master", StringComparison.InvariantCultureIgnoreCase);
    }

    public class Commit
    {
      public string treeId { get; set; }
      public string commitId { get; set; }
      public Author author { get; set; }
      public Committer committer { get; set; }
      public string comment { get; set; }
      public string[] parents { get; set; }
      public string url { get; set; }
    }

    public class Author
    {
      public string name { get; set; }
      public string email { get; set; }
      public DateTime date { get; set; }
    }

    public class Committer
    {
      public string name { get; set; }
      public string email { get; set; }
      public DateTime date { get; set; }
    }

  }
}
