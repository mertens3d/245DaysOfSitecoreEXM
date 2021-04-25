using LearnEXM.Project.SitecoreCinema.Model;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;

namespace LearnEXM.Project.SitecoreCinema.Controllers.Helpers
{
  public class MovieItemProxy
  {
    public Item MovieItem { get; }
    public MovieItemProxy(Item movieItem)
    {
      this.MovieItem = movieItem;
    }

    public MovieItemProxy(Guid movieId)
    {
       MovieItem = Sitecore.Context.Database.GetItem(new ID(movieId));
    }

    public string MovieName { get { return MovieItem[ProjConst.Items.Templates.Feature.SitecoreCinema.MovieData.MovieName]; } }

    public ImageField PosteImageField { get { return MovieItem.Fields[ProjConst.Items.Templates.Feature.SitecoreCinema.MovieData.Poster]; } }
  }
}