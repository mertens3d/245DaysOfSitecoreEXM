namespace LearnEXMProject.Models.SitecoreCinema
{
  public class SelfServiceMachineViewModel : _baseViewModel
  {
    public string TicketLink() => CinemaConst.Links.SitecoreCinema.BuyTicket + UserIdQueryString();
  }
}