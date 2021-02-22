using Shared.Models.SitecoreCinema.Collection;
using Shared.XConnect.Client;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.XConnect
{
  public class XConnectConfigHelper
  {
    public List<string> Errors { get; set; } = new List<string>();
    public async Task<XConnectClientConfiguration> ConfigureClient()
    {
      XConnectClientConfiguration toReturn;

      var cfgGenerator = new CFGGenerator();
      toReturn = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);
      var initializer = new XConnectInitializer();
      if (!initializer.Errors.Any())
      {
        await initializer.InitCFGAsync(toReturn);
      }
      else
      {
        Errors.AddRange(initializer.Errors);
      }

      if (initializer.Errors.Any())
      {
        Errors.AddRange(initializer.Errors);
        toReturn = null;
      }

      return toReturn;
    }

  }
}
