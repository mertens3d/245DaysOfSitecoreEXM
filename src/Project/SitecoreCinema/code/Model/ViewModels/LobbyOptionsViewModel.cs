using LearnEXM.Feature.SitecoreCinema.Models.Proxies;
using System.Collections.Generic;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class LobbyOptionsViewModel : _baseViewModel
  {
    public List<ConcessionItemProxy> Concessions { get; internal set; }
  }
}