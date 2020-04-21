using System.Collections.Generic;

namespace RepoCleanUpUtil
{
  public class Config
  {
    public string ProjectName { get; set; }
    public string AccountName { get; set; }
    public string PatToken { get; set; }
    public List<string> DeleteBehindOnlyRefsFrom { get; set; } = new List<string>();
  }
}
