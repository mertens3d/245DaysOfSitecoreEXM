using LearnEXM.Feature.SitecoreCinema.Models.ViewModels;
using System.Collections.Generic;

namespace LearnEXM.Project.SitecoreCinema.Model.ViewModels
{

  public class LobbyOptionsViewModel : _baseViewModel
  {
    public List<ConcessionCategoryViewModel> ConcessionCategories { get; internal set; }
  }
}