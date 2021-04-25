using Sitecore.Analytics.Tracking;
using System.Collections.Generic;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class SelfServiceMachineViewModel : _baseViewModel
  {
    private Contact TrackingContact;

    public SelfServiceMachineViewModel(Contact trackingContact)
    {
      TrackingContact = trackingContact;
    }

    public List<MovieShowTime> ShowTimes { get; set; }

    public string TicketLink() => WebConst.Links.SitecoreCinema.BuyTicket;// + UserIdQueryString();
  }
}