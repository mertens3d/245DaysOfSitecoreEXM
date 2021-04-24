using System;

namespace LearnEXM.Project.SitecoreCinema.Model
{
  public class MovieShowTime
  {
    public Guid Id { get; set; }
    public string MovieName { get; set; }
    public DateTime MovieTime { get; set; }
    public string MoviePoster { get; set; }
  }
}