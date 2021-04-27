using LearnEXM.Feature.SitecoreCinema.Models.Proxies;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.ViewModels
{
  public class ConcessionCategoryViewModel
  {
    public string CategoryName { get; set; }
    public List<ConcessionItemProxy> Concessions { get; internal set; }

  }
}