using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data.Items;
using System;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class MovieItemProxy : _baseItemProxy
  {
    public MovieItemProxy(Item movieItem) : base(movieItem)
    {
    }

    public MovieItemProxy(Guid movieId) : base(movieId)
    {
    }

    protected override void CommonCTOR()
    {
      MovieNameProxy = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.MovieName);
      PosterImageFieldProxy = new ImageFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.Poster);
    }

    public SingleLineFieldProxy MovieNameProxy { get; set; }

    public ImageFieldProxy PosterImageFieldProxy { get; set; }
  }
}
