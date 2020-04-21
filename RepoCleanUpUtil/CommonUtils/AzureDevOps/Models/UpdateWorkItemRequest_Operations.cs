namespace RepoCleanUpUtil.CommonUtils.AzureDevOps.Models
{
  public class UpdateWorkItemRequest_Operations
  {
    public string op { get; set; }
    public string path { get; set; }
    public object value { get; set; }
  }
}
