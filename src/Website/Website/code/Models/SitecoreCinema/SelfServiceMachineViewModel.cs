using Sitecore.Analytics.Tracking;

namespace LearnEXMProject.Models.SitecoreCinema
{
  public class SelfServiceMachineViewModel : _baseViewModel
  {
    private Contact TrackingContact;

    public SelfServiceMachineViewModel(Contact trackingContact)
    {
      this.TrackingContact = trackingContact;
    }

    public string TicketLink() => WebConst.Links.SitecoreCinema.BuyTicket;// + UserIdQueryString();
  }
}