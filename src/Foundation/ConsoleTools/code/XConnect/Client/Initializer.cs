using Sitecore.XConnect.Client;
using System.Collections.Generic;

namespace LearnEXM.Foundation.ConsoleTools.XConnect.Client
{
  public class XConnectInitializer
  {
    public List<string> Errors { get; set; } = new List<string>();

    public async System.Threading.Tasks.Task InitCFGAsync(XConnectClientConfiguration cfg)
    {
      try
      {
        await cfg.InitializeAsync();
      }
      catch (XdbModelConflictException ce)
      {
        Errors.Add("ERROR:" + ce.Message);
        return;
      }
    }
  }
}