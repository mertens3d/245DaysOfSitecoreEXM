using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace Shared.XConnect
{
  public class XConnectInitializer
  {
    public List<string> Errors { get; set; } = new List<string>();

    public bool InitSuccess { get; private set; } = false;

    public async System.Threading.Tasks.Task InitCFGAsync(XConnectClientConfiguration cfg)
    {
      try
      {
        await cfg.InitializeAsync();

        InitSuccess = true;
      }
      catch (XdbModelConflictException ce)
      {
        Errors.Add("ERROR:" + ce.Message);
        return;
      }
    }
  }
}