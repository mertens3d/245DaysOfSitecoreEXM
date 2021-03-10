using LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels;
using LearnEXM.Foundation.ConsoleTools.XConnect.Client;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEXM.Foundation.ConsoleTools.Helpers
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