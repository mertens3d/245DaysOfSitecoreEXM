using LearnEXM.Foundation.LearnEXMRoot;
using Sitecore.Data;
using System.Collections.Generic;

namespace LearnEXM.Feature.SitecoreCinema.Models
{
  public class MovieShowTimesProxy : _baseItemProxy
  {
    public MovieShowTimesProxy(ID itemId) : base(itemId)
    {
    }

    protected override ID AssociatedTemplateID { get; }

    public List<MovieShowTimeProxy> ShowTimes { get; set; }

    protected override void CommonCTOR()
    {
      ShowTimes = ChildrenOfTemplateType<MovieShowTimeProxy>();
    }
  }
}