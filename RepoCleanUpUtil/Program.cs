using Newtonsoft.Json;
using RepoCleanUpUtil.CommonUtils;
using System;
using System.IO;
using System.Linq;

namespace RepoCleanUpUtil
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.White;

      var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(".\\config.json"));

      var azdoClient = AzureDevOpsDirectUtil.CreateInstance(config.PatToken, $"https://dev.azure.com/{config.AccountName}");

      var repos = azdoClient.ListRepos(config.ProjectName);

      foreach (var repo in repos.value.OrderBy(o => o.name))
      {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{repo.name}");
        if (!config.DeleteBehindOnlyRefsFrom.Any(o => o.Equals(repo.name, StringComparison.InvariantCultureIgnoreCase)))
        {
          Console.Write($"...");
          Console.ForegroundColor = ConsoleColor.DarkYellow;
          Console.WriteLine($"skipped");
          continue;
        }
        Console.WriteLine();
        var refs = azdoClient.ListRefs(config.ProjectName, repo.name);
        foreach (var @ref in refs.value.OrderBy(o => o.name))
        {
          if (@ref.is_branch)
          {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"- {@ref.name}...");
            var stats = azdoClient.GetBranchStats(config.ProjectName, repo.name, @ref.name);
            if (stats.isBase_or_master)
            {
              Console.ForegroundColor = ConsoleColor.Green;
              Console.WriteLine($"baseline");
            }
            else if (stats.aheadCount > 0)
            {
              Console.ForegroundColor = ConsoleColor.Blue;
              Console.WriteLine($"keep ({stats.aheadCount})");
            }
            else
            {
              Console.ForegroundColor = ConsoleColor.Red;
              Console.WriteLine($"delete ({stats.aheadCount})");
              azdoClient.DeleteRef(config.ProjectName, repo.name, @ref.name, @ref.objectId);
            }
          }
        }
      }
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}
