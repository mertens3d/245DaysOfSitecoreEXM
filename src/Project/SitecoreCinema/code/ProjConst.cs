namespace LearnEXM.Project.SitecoreCinema
{
  public struct ProjectConst
  {
    public struct Logging
    {
      public static string Prefix = "[LearnEXM.Project.SitecoreCinema] : ";
    }

    public struct Pages
    {
      public static string ErrorPage = "/error";
    }

    public struct PlaceHolders
    {
      public static string Main = "main";
      public static string RightAside = "right-aside";
    }

    public struct ControllerViews
    {
      public static string _viewRoot = "/views/project/SitecoreCinema";
      public static string Lobby = _viewRoot + "/Lobby.cshtml";
      public static string SelfServiceMachine = _viewRoot + "/SelfServiceMachine.cshtml";
      public static string StartJourney = _viewRoot + "/StartJourney.cshtml";
      public static string WatchMovie = WatchMovie + "/StartJourney.cshtml";
    }
  }
}