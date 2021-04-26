using LearnEXM.Feature.SitecoreCinema.Models.Proxies;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace LearnEXM.Foundation.LearnEXMRoot.Models
{
  public class MovieDataRefFieldProxy :ReferenceFieldProxy
  {
    public MovieItemProxy MovieItemProxy { get; set; }
    

    public MovieDataRefFieldProxy(Item item, ID fieldId): base(item, fieldId)
    {
    }
  }
}