using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class ListSubscriptionsResponse
  {
    public class Payload
    {
      public int count { get; set; }
      public Value[] value { get; set; }
    }

    public class Value
    {
      public Guid id { get; set; }
      public string url { get; set; }
      public string status { get; set; }
      public string publisherId { get; set; }
      public string eventType { get; set; }
      public object subscriber { get; set; }
      public string resourceVersion { get; set; }
      public string eventDescription { get; set; }
      public string consumerId { get; set; }
      public string consumerActionId { get; set; }
      public string actionDescription { get; set; }
      public Createdby createdBy { get; set; }
      public DateTime createdDate { get; set; }
      public Modifiedby modifiedBy { get; set; }
      public DateTime modifiedDate { get; set; }
      public Publisherinputs publisherInputs { get; set; }
      public Consumerinputs consumerInputs { get; set; }
      public _Links _links { get; set; }
    }

    public class Createdby
    {
      public string displayName { get; set; }
      public Guid id { get; set; }
      public string uniqueName { get; set; }
      public string descriptor { get; set; }
    }

    public class Modifiedby
    {
      public string displayName { get; set; }
      public Guid id { get; set; }
      public string uniqueName { get; set; }
      public string descriptor { get; set; }
    }

    public class Publisherinputs
    {
      public string areaPath { get; set; }
      public string linksChanged { get; set; }
      public Guid projectId { get; set; }
      public string tfsSubscriptionId { get; set; }
      public string workItemType { get; set; }
      public string changedFields { get; set; }
    }

    public class Consumerinputs
    {
      public string detailedMessagesToSend { get; set; }
      public string messagesToSend { get; set; }
      public string resourceDetailsToSend { get; set; }
      public string url { get; set; }
    }

    public class _Links
    {
      public Self self { get; set; }
      public Consumer consumer { get; set; }
      public Actions actions { get; set; }
      public Notifications notifications { get; set; }
      public Publisher publisher { get; set; }
    }

    public class Self
    {
      public string href { get; set; }
    }

    public class Consumer
    {
      public string href { get; set; }
    }

    public class Actions
    {
      public string href { get; set; }
    }

    public class Notifications
    {
      public string href { get; set; }
    }

    public class Publisher
    {
      public string href { get; set; }
    }

  }
}
