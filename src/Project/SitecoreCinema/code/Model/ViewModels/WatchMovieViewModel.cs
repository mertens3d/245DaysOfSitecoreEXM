using Sitecore.Analytics.Tracking;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class WatchMovieViewModel : _baseViewModel
  {
    private Contact TrackingContact;

    public WatchMovieViewModel(Contact trackingContact)
    {
      TrackingContact = trackingContact;
    }


  }
}