using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;

namespace LearnEXM.Feature.SitecoreCinema.Models.Proxies
{
  public class ConcessionItemProxy : _baseItemProxy
  {
    public ConcessionItemProxy()
    {
      // for generics instantiation only
    }

    public SingleLineFieldProxy ProductName { get; set; }

    public ImageFieldProxy ProductLogo { get; set; }

    public ConcessionItemProxy(ID itemId) : base(itemId)
    {
    }

    protected override void CommonCTOR()
    {
      ProductName = new SingleLineFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.MovieName);
      ProductLogo = new ImageFieldProxy(Item, ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieData.Poster);
    }
  }
}