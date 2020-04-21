using System;

namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class CreateWebHookSubscriptionRequest
  {
    public static PayloadCreateOrUpdate CreateOrUpdateWorkItem(string webHookLocation, AzDOWorkItemUpdateTypes updateType, Guid teamProjectId, string systemUniqueKey)
    {
      var eventType = GetEventType(updateType);
      var result = new PayloadCreateOrUpdate();
      result.eventType = eventType;
      result.publisherInputs.projectId = teamProjectId.ToString().ToLowerInvariant();
      result.consumerInputs.url = webHookLocation;
      return result;
    }

    public static PayloadDeleteOrRestore DeleteOrRestoreWorkItem(string webHookLocation, AzDOWorkItemUpdateTypes updateType, Guid teamProjectId, string systemUniqueKey)
    {
      var eventType = GetEventType(updateType);
      var result = new PayloadDeleteOrRestore();
      result.eventType = eventType;
      result.publisherInputs.projectId = teamProjectId.ToString().ToLowerInvariant();
      result.consumerInputs.url = webHookLocation;
      return result;
    }

    public static string GetEventType(AzDOWorkItemUpdateTypes updateType)
    {
      string eventType;
      switch (updateType)
      {
        case AzDOWorkItemUpdateTypes.Create:
          eventType = "workitem.created";
          break;
        case AzDOWorkItemUpdateTypes.Update:
          eventType = "workitem.updated";
          break;
        case AzDOWorkItemUpdateTypes.Delete:
          eventType = "workitem.deleted";
          break;
        case AzDOWorkItemUpdateTypes.Restore:
          eventType = "workitem.restored";
          break;
        default:
          throw new NotImplementedException();
      }

      return eventType;
    }

    public class PayloadCreateOrUpdate
    {
      public string publisherId { get; set; } = "tfs";
      public string eventType { get; set; }
      public string resourceVersion { get; set; } = "1.0";
      public string consumerId { get; set; } = "webHooks";
      public string consumerActionId { get; set; } = "httpRequest";
      public PublisherInputsCreateOrUpdate publisherInputs { get; set; } = new PublisherInputsCreateOrUpdate();
      public ConsumerInputs consumerInputs { get; set; } = new ConsumerInputs();
    }

    public class PublisherInputsCreateOrUpdate
    {
      public string areaPath { get; set; } = string.Empty;
      public string linksChanged { get; set; } = "false";
      public string projectId { get; set; }
      public string workItemType { get; set; } = string.Empty;
    }

    public class PayloadDeleteOrRestore
    {
      public string publisherId { get; set; } = "tfs";
      public string eventType { get; set; }
      public string resourceVersion { get; set; } = "1.0";
      public string consumerId { get; set; } = "webHooks";
      public string consumerActionId { get; set; } = "httpRequest";
      public PublisherInputsDeleteOrRestore publisherInputs { get; set; } = new PublisherInputsDeleteOrRestore();
      public ConsumerInputs consumerInputs { get; set; } = new ConsumerInputs();
    }

    public class PublisherInputsDeleteOrRestore
    {
      public string areaPath { get; set; } = string.Empty;
      public string projectId { get; set; }
      public string workItemType { get; set; } = string.Empty;
    }

    public class ConsumerInputs
    {
      public string detailedMessagesToSend { get; set; } = "none";
      public string messagesToSend { get; set; } = "none";
      public string resourceDetailsToSend { get; set; } = "minimal";
      public string url { get; set; }
    }
  }
}
