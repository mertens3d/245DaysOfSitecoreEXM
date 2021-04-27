using LearnEXM.Feature.SitecoreCinema.Models.Proxies;
using LearnEXM.Feature.SitecoreCinema.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Feature.SitecoreCinema.Helpers
{
  public class ConcessionHelper
  {
    public List<ConcessionCategoryViewModel> GetConcessions()
    {
      var toReturn = new List<ConcessionCategoryViewModel>();

      var concessionsItemProxy = new ConcessionItemsProxy(ProjectConst.Items.Content.ConcesssionsFolderItemID);
      if (concessionsItemProxy != null && concessionsItemProxy.ConcessionGroups.Any())
      {

        foreach (var conessionItem in concessionsItemProxy.ConcessionGroups)
        {
        //  var concessionCategory = conessionItem.
        //= concessionsItemProxy.ConcessionGroups;
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("Concession root folder was null", this);
      }

      return toReturn;
    }
  }
}