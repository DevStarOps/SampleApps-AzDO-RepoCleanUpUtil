using Newtonsoft.Json;
using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class GetWorkitem
  {
    public class WorkitemList
    {
      public int count { get; set; }
      public Workitem[] value { get; set; }
    }

    public class Workitem
    {
      public int id { get; set; }
      public int rev { get; set; }
      public Fields fields { get; set; }
      public string url { get; set; }
      public Relation[] relations { get; set; } = new Relation[0];
      public Workitem_Links _links { get; set; }
    }

    public class Relation
    {
      public string rel { get; set; }
      public string url { get; set; }
      public Attributes attributes { get; set; }
    }

    public class Attributes
    {
      public bool isLocked { get; set; }
      public string name { get; set; }
    }

    public class Fields
    {
      [JsonProperty("System.Id")]
      public int WorkItemId { get; set; }
      [JsonProperty("System.Parent")]
      public int ParentWorkItemId { get; set; }
      [JsonProperty("System.AreaPath")]
      public string AreaPath { get; set; }
      [JsonProperty("System.IterationPath")]
      public string IterationPath { get; set; }
      [JsonProperty("System.WorkItemType")]
      public string WorkItemType { get; set; }
      [JsonProperty("System.State")]
      public string State { get; set; }
      [JsonProperty("System.CreatedDate")]
      public DateTime CreatedDate { get; set; }
      [JsonProperty("System.ChangedDate")]
      public DateTime ChangedDate { get; set; }
      [JsonProperty("System.Title")]
      public string Title { get; set; }
      [JsonProperty("System.AssignedTo")]
      public UserIdentity AssignedTo { get; set; }
      [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
      public DateTime StateChangeDate { get; set; }
      [JsonProperty("Microsoft.VSTS.Scheduling.Effort")]
      public decimal Effort { get; set; }

      [JsonProperty("System.Tags")]
      public string Tags { get; set; }

      [JsonProperty("Custom.ProjectName")]
      public string ProjectName { get; set; }
      [JsonProperty("Custom.RequestName")]
      public string RequestName { get; set; }
      [JsonProperty("Custom.ClientName")]
      public string ClientName { get; set; }
      [JsonProperty("Custom.LoggedHours")]
      public decimal LoggedHours { get; set; }
      [JsonProperty("Custom.BillingNumber")]
      public int BillingNumber { get; set; }
      [JsonProperty("Custom.ClientLock")]
      public bool ClientLock { get; set; }

      [JsonProperty("Custom.OverRunReasonRequired")]
      public bool OverRunReasonRequired { get; set; }
      [JsonProperty("Custom.OverRunTheme")]
      public string OverRunTheme { get; set; }
      [JsonProperty("Custom.OverRunThemeOther")]
      public string OverRunThemeOther { get; set; }
      [JsonProperty("Custom.OverRunSpecifics")]
      public string OverRunSpecifics { get; set; }








      [JsonProperty("Microsoft.VSTS.Scheduling.StoryPoints")]
      public decimal StoryPoints { get { return Effort; } set { Effort = value; } }
      [JsonProperty("NologoStudios.DollarNumber")]
      public int NologoStudiosDollarNumber { get { return BillingNumber; } set { BillingNumber = value; } }
      [JsonProperty("NologoStudios.OverRunReasonRequired")]
      public int NologoStudiosOverRunReasonRequired { get { return OverRunReasonRequired ? 1 : 0; } set { OverRunReasonRequired = value == 1; } }
      [JsonProperty("NologoStudios.Scheduling.HoursLogged")]
      public decimal NologoStudiosSchedulingHoursLogged { get { return LoggedHours; } set { LoggedHours = value; } }
      [JsonProperty("NologoStudios.ClientName")]
      public string NologoStudiosClientName { get { return ClientName; } set { ClientName = value; } }

      [JsonProperty("NologoStudios.OverRunTheme")]
      public string NologoStudiosOverRunTheme { get { return OverRunTheme; } set { OverRunTheme = value; } }
      [JsonProperty("NologoStudios.OverRunThemeOther")]
      public string NologoStudiosOverRunThemeOther { get { return OverRunThemeOther; } set { OverRunThemeOther = value; } }
      [JsonProperty("NologoStudios.OverRunSpecifics")]
      public string NologoStudiosOverRunSpecifics { get { return OverRunSpecifics; } set { OverRunSpecifics = value; } }
    }

    public class Workitem_Links
    {
      public HrefObject self { get; set; }
      public HrefObject workItemUpdates { get; set; }
      public HrefObject workItemRevisions { get; set; }
      public HrefObject workItemComments { get; set; }
      public HrefObject html { get; set; }
      public HrefObject workItemType { get; set; }
      public HrefObject fields { get; set; }
    }

    public class UserIdentity
    {
      public string displayName { get; set; }
      public string url { get; set; }
      public UserIdentity_Links _links { get; set; }
      public string id { get; set; }
      public string uniqueName { get; set; }
      public string imageUrl { get; set; }
      public string descriptor { get; set; }
    }

    public class UserIdentity_Links
    {
      public HrefObject avatar { get; set; }
    }

    public class HrefObject
    {
      public string href { get; set; }
    }
  }
}
