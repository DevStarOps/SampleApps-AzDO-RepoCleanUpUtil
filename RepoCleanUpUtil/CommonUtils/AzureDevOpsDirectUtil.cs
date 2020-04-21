using RepoCleanUpUtil.CommonUtils.AzureDevOps.Models;
using System;
using System.Linq;

namespace RepoCleanUpUtil.CommonUtils
{
  public partial class AzureDevOpsDirectUtil
  {
    public ListSubscriptionsResponse.Payload ListSubscriptions()
    {
      return Get<ListSubscriptionsResponse.Payload>($"/_apis/hooks/subscriptions?api-version=5.1");
    }

    public ListReposResponse.Payload ListRepos(string projectName, bool includeLinks = false, bool includeAllUrls = false, bool includeHidden = false)
    {
      return Get<ListReposResponse.Payload>($"/{projectName}/_apis/git/repositories?includeLinks={includeLinks}&includeAllUrls={includeAllUrls}&includeHidden={includeHidden}&api-version=4.1");
    }

    public ListReposResponse.Payload ListRepos(bool includeLinks = true, bool includeAllUrls = false, bool includeHidden = false)
    {
      return Get<ListReposResponse.Payload>($"/_apis/git/repositories?includeLinks={includeLinks}&includeAllUrls={includeAllUrls}&includeHidden={includeHidden}&api-version=4.1");
    }

    public GetRepo.Payload GetRepo(string projectName, string repositoryId, bool includeParent = true)
    {
      return Get<GetRepo.Payload>($"/{projectName}/_apis/git/repositories/{repositoryId}?includeParent={includeParent}&api-version=4.1");
    }

    public ListRefs.Payload ListRefs(string projectName, string repositoryId, string filter = "", bool includeLinks = false, bool includeStatuses = false, bool includeMyBranches = false, bool latestStatusesOnly = true, bool peelTags = false)
    {
      return Get<ListRefs.Payload>($"/{projectName}/_apis/git/repositories/{repositoryId}/refs?filter={filter}&includeLinks={includeLinks}&includeStatuses={includeStatuses}&includeMyBranches={includeMyBranches}&latestStatusesOnly={latestStatusesOnly}&peelTags={peelTags}&api-version=4.1");
    }

    public GetBranchStats.Payload GetBranchStats(string projectName, string repositoryId, string name)
    {
      if (!name.StartsWith("refs/heads/", StringComparison.InvariantCultureIgnoreCase))
      {
        throw new Exception("Only head refs can be used.");
      }
      name = name.Remove(0, 11);
      return Get<GetBranchStats.Payload>($"/{projectName}/_apis/git/repositories/{repositoryId}/stats/branches?name={name}&api-version=5.1");
    }

    public void DeleteRef(string projectName, string repositoryId, string refName, string refObjectId)
    {
      var body = new
      {
        name = refName,
        oldObjectId = refObjectId,
        newObjectId = "0000000000000000000000000000000000000000"
      };
      var bodyList = new[] { body };
      Post($"/{projectName}/_apis/git/repositories/{repositoryId}/refs?api-version=5.0", bodyList);
    }

    public void CreateWebHook(string webHookLocation, AzDOWorkItemUpdateTypes updateType, Guid teamProjectId, string systemUniqueKey)
    {
      try
      {
        var eventType = CreateWebHookSubscriptionRequest.GetEventType(updateType);
        var serviceHooks = ListSubscriptions().value
            .Where(o => o.publisherInputs.projectId == teamProjectId &&
                        o.eventType == eventType &&
                        o.consumerInputs.url.StartsWith(webHookLocation, StringComparison.InvariantCultureIgnoreCase));
        foreach (var serviceHook in serviceHooks)
        {
          DeleteWebHook(serviceHook.id);
        }
        object request;
        switch (updateType)
        {
          case AzDOWorkItemUpdateTypes.Create:
          case AzDOWorkItemUpdateTypes.Update:
            request = CreateWebHookSubscriptionRequest.CreateOrUpdateWorkItem(webHookLocation, updateType, teamProjectId, systemUniqueKey);
            break;
          case AzDOWorkItemUpdateTypes.Delete:
          case AzDOWorkItemUpdateTypes.Restore:
            request = CreateWebHookSubscriptionRequest.DeleteOrRestoreWorkItem(webHookLocation, updateType, teamProjectId, systemUniqueKey);
            break;
          default:
            throw new NotImplementedException();
        }
        Post($"/_apis/hooks/subscriptions?api-version=5.1", request);
      }
      catch (Exception ex)
      {
        string observeEx = ex.ToString();
        throw;
      }
    }

    public void DeleteWebHook(Guid subscriptionId)
    {
      Delete($"/_apis/hooks/subscriptions/{subscriptionId}?api-version=5.1");
    }

    public void CreateTeamAreaPath(Guid teamProjectId, string clientName)
    {
      var request = new NameRequest { name = clientName };
      Post($"/{teamProjectId}/_apis/wit/classificationNodes/areas?api-version=5.1", request);
    }

    public void RenameTeamAreaPath(Guid teamProjectId, string oldClientName, string newClientName)
    {
      var request = new NameRequest { name = newClientName };
      Patch($"/{teamProjectId}/_apis/wit/classificationNodes/areas/{oldClientName}?api-version=5.1", request);
    }

    public GetWorkitem.Workitem GetWorkitem(int id)
    {
      var result = Get<GetWorkitem.Workitem>($"/_apis/wit/workItems/{id}?$expand=All&api-version=5.1");
      if (result.fields.ParentWorkItemId == 0)
      {
        var parentUrl = result.relations.FirstOrDefault(o => o.attributes.name.Equals("Parent", StringComparison.InvariantCultureIgnoreCase))?.url;
        if (parentUrl != null)
        {
          result.fields.ParentWorkItemId = Convert.ToInt32(parentUrl.Remove(0, parentUrl.LastIndexOf('/') + 1));
        }
      }
      return result;
    }

    //public GetWorkitems GetWorkitems(int[] id)
    //{
    //    return Get<GetWorkitems>($"/_apis/wit/workItems/?$expand=relations&ids={string.Join(",", id)}&api-version=2.2");
    //}

    public void UpdateWorkItem(int workItemId, UpdateWorkItemRequest_Operations[] requestOperations)
    {
      Patch2($"/_apis/wit/workitems/{workItemId}?api-version=5.1&bypassRules=true", requestOperations);
    }

  }
}
