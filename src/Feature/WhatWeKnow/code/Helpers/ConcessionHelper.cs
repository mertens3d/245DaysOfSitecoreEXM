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

      var concessionsItemProxy = new ConcessionRootProxy(ProjectConst.Items.Content.ConcesssionsFolderItemID);
      if (concessionsItemProxy != null && concessionsItemProxy.ConcessionCategories.Any())
      {

        foreach (ConcessionCategoryProxy concessionGroup in concessionsItemProxy.ConcessionCategories)
        {
          var candidateGroup = new ConcessionCategoryViewModel()
          {
            CategoryName = concessionGroup.GroupNameFieldProxy.Value,
            ConcessionItems = concessionGroup.ChildConcessionItems,
            SubCategoryItems = concessionGroup.ChildSubCategoryItems,
          };

          toReturn.Add(candidateGroup);
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