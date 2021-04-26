using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models.ViewModels
{
  public class SelfServiceMachineViewModel
  {
    public SelfServiceMachineViewModel()
    {
    }

    public List<MovieShowTimeProxy> ShowTimes { get; set; }

    public string TicketLink() => ProjectConst.Links.SitecoreCinema.BuyTicket;
  }
}