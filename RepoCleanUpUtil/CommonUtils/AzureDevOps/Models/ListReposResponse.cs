using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class ListReposResponse
  {
    public class Payload
    {
      public Value[] value { get; set; }
      public int count { get; set; }
    }

    public class Value
    {
      public string id { get; set; }
      public string name { get; set; }
      public string url { get; set; }
      public Project project { get; set; }
      public long size { get; set; }
      public string remoteUrl { get; set; }
      public string sshUrl { get; set; }
      public string webUrl { get; set; }
      public string[] validRemoteUrls { get; set; }
      public _Links _links { get; set; }
      public string defaultBranch { get; set; }
    }

    public class Project
    {
      public string id { get; set; }
      public string name { get; set; }
      public string description { get; set; }
      public string url { get; set; }
      public string state { get; set; }
      public int revision { get; set; }
      public string visibility { get; set; }
      public DateTime lastUpdateTime { get; set; }
    }

    public class _Links
    {
      public Self self { get; set; }
      public Project1 project { get; set; }
      public Web web { get; set; }
      public Ssh ssh { get; set; }
      public Commits commits { get; set; }
      public Refs refs { get; set; }
      public Pullrequests pullRequests { get; set; }
      public Items items { get; set; }
      public Pushes pushes { get; set; }
    }

    public class Self
    {
      public string href { get; set; }
    }

    public class Project1
    {
      public string href { get; set; }
    }

    public class Web
    {
      public string href { get; set; }
    }

    public class Ssh
    {
      public string href { get; set; }
    }

    public class Commits
    {
      public string href { get; set; }
    }

    public class Refs
    {
      public string href { get; set; }
    }

    public class Pullrequests
    {
      public string href { get; set; }
    }

    public class Items
    {
      public string href { get; set; }
    }

    public class Pushes
    {
      public string href { get; set; }
    }

  }
}
