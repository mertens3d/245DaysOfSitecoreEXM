using LearnEXM.Feature.SitecoreCinema.Models.Proxies.ConcessionStand;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.ViewModels
{
  public class ConcessionCategoryViewModel
  {
    public string CategoryName { get; set; }
    public List<ConcessionItemProxy> ConcessionItems { get; internal set; }
    public List<ConcessionSubCategoryProxy> SubCategoryItems { get; internal set; }

  }
}