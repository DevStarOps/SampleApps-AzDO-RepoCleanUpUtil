using Newtonsoft.Json;
using RepoCleanUpUtil.CommonUtils.AzureDevOps;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace RepoCleanUpUtil.CommonUtils
{
  public partial class AzureDevOpsDirectUtil
  {
    private AzureDevOpsDirectUtil() { }
    public static AzureDevOpsDirectUtil CreateInstance(string patToken, string baseUrl)
    {
      return new AzureDevOpsDirectUtil
      {
        PatToken = patToken,
        BaseUri = baseUrl,
      };
    }

    public string PatToken { get; private set; }
    public string BaseUri { get; private set; }

    private string GetAuthorizationHeader() => $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($":{PatToken}"))}";

    public T Get<T>(string uriRelativeToRoot)
    {
      string uri = $@"{BaseUri}{uriRelativeToRoot.Replace("//", "/")}";
      using (var client = new WebClient())
      {
        client.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader();
        return AzDORestTry(uri, () =>
        {
          var responseString = client.DownloadString(uri);
          return JsonConvert.DeserializeObject<T>(responseString);
        });
      }
    }

    public T GeneralPushData<T>(string uriRelativeToRoot, object data, string method, string contentType)
    {
      string uri = $@"{BaseUri}{uriRelativeToRoot.Replace("//", "/")}";
      using (var client = new WebClient())
      {
        client.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader();
        client.Headers[HttpRequestHeader.ContentType] = contentType;
        var requestString = string.Empty;
        if (data != null)
        {
          requestString = JsonConvert.SerializeObject(data);
        }
        return AzDORestTry(uri, () =>
        {
          var responseString = client.UploadString(uri, method, requestString);
          return JsonConvert.DeserializeObject<T>(responseString);
        });
      }
    }

    public void Post(string uriRelativeToRoot, object data)
    {
      Post<object>(uriRelativeToRoot, data);
    }

    public T Post<T>(string uriRelativeToRoot, object data)
    {
      return GeneralPushData<T>(uriRelativeToRoot, data, "POST", "application/json");
    }

    public void Patch(string uriRelativeToRoot, object data)
    {
      Patch<object>(uriRelativeToRoot, data);
    }

    public T Patch<T>(string uriRelativeToRoot, object data)
    {
      return GeneralPushData<T>(uriRelativeToRoot, data, "PATCH", "application/json");
    }

    public void Delete(string uriRelativeToRoot)
    {
      Delete<object>(uriRelativeToRoot);
    }

    public T Delete<T>(string uriRelativeToRoot)
    {
      return GeneralPushData<T>(uriRelativeToRoot, null, "DELETE", "application/json");
    }

    public void Delete(string uriRelativeToRoot, object data)
    {
      Delete<object>(uriRelativeToRoot, data);
    }

    public T Delete<T>(string uriRelativeToRoot, object data)
    {
      return GeneralPushData<T>(uriRelativeToRoot, data, "DELETE", "application/json");
    }

    public void Put(string uriRelativeToRoot, object data)
    {
      GeneralPushData<object>(uriRelativeToRoot, data, "PUT", "application/json");
    }

    public void Patch2(string uriRelativeToRoot, object data)
    {
      Patch2<object>(uriRelativeToRoot, data);
    }

    public T Patch2<T>(string uriRelativeToRoot, object data)
    {
      return GeneralPushData<T>(uriRelativeToRoot, data, "PATCH", "application/json-patch+json");
    }

    public T AzDORestTry<T>(string uri, Func<T> f)
    {
      try
      {
        return f();
      }
      catch (WebException webEx) when (webEx.Status == WebExceptionStatus.ProtocolError && (((HttpWebResponse)webEx.Response).StatusCode == HttpStatusCode.BadRequest || ((HttpWebResponse)webEx.Response).StatusCode == HttpStatusCode.NotFound))
      {
        using (var sr = new StreamReader(webEx.Response.GetResponseStream()))
        {
          var responseString = sr.ReadToEnd();
          var exception = JsonConvert.DeserializeObject<RestCallException>(responseString);
          //throw new Exception($"{exception.message} | {uri}");
          throw new Exception(exception.message);
        }
      }
    }
  }
}
