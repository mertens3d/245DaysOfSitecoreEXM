namespace LearnEXMProject.Models.SitecoreCinema
{
  public class CandidateContactInfo
  {
    public string Email
    {
      get { return FirstName + "." + LastName + "@fakemail.com"; }
    }

    public string FavoriteMovie { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}