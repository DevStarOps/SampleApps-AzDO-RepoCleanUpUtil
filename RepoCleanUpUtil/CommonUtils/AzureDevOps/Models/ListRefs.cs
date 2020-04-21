using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class ListRefs
  {

    public class Payload
    {
      public Value[] value { get; set; }
      public int count { get; set; }
    }

    public class Value
    {
      public string name { get; set; }
      public string objectId { get; set; }
      public Creator creator { get; set; }
      public string url { get; set; }
      public Status[] statuses { get; set; }
      public _Links1 _links { get; set; }
      public bool is_branch => name.StartsWith("refs/heads/", StringComparison.InvariantCultureIgnoreCase);
    }

    public class Creator
    {
      public string displayName { get; set; }
      public string url { get; set; }
      public _Links _links { get; set; }
      public string id { get; set; }
      public string uniqueName { get; set; }
      public string imageUrl { get; set; }
      public string descriptor { get; set; }
    }

    public class _Links
    {
      public Avatar avatar { get; set; }
    }

    public class Avatar
    {
      public string href { get; set; }
    }

    public class _Links1
    {
      public Self self { get; set; }
      public Repository repository { get; set; }
    }

    public class Self
    {
      public string href { get; set; }
    }

    public class Repository
    {
      public string href { get; set; }
    }

    public class Status
    {
      public int id { get; set; }
      public string state { get; set; }
      public string description { get; set; }
      public Context context { get; set; }
      public DateTime creationDate { get; set; }
      public Createdby createdBy { get; set; }
      public string targetUrl { get; set; }
    }

    public class Context
    {
      public string name { get; set; }
      public string genre { get; set; }
    }

    public class Createdby
    {
      public string displayName { get; set; }
      public string url { get; set; }
      public _Links2 _links { get; set; }
      public string id { get; set; }
      public string uniqueName { get; set; }
      public string imageUrl { get; set; }
      public string descriptor { get; set; }
    }

    public class _Links2
    {
      public Avatar1 avatar { get; set; }
    }

    public class Avatar1
    {
      public string href { get; set; }
    }
  }
}
