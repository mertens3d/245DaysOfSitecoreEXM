using Sitecore.Analytics.Tracking;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class SelfServiceMachineViewModel : _baseViewModel
  {
    private Contact TrackingContact;

    public SelfServiceMachineViewModel(Contact trackingContact)
    {
      TrackingContact = trackingContact;
    }

    public string TicketLink() => WebConst.Links.SitecoreCinema.BuyTicket;// + UserIdQueryString();
  }
}