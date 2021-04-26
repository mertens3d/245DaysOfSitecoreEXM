using LearnEXM.Feature.SitecoreCinema.Models;
using LearnEXM.Foundation.LearnEXMRoot;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Feature.SitecoreCinema.Helpers
{
  public class MovieTicketHelper
  {
    public List<MovieShowTimeProxy> AvailableMovies()
    {
      var toReturn = new List<MovieShowTimeProxy>();
      var showTimesItemProxy = new GenericItemProxy(ProjectConst.Items.Content.MovieShowTimesFolderItemID);

      if (showTimesItemProxy != null)

      {
        List<GenericItemProxy> foundChildrenItemProxies =
          showTimesItemProxy.ChildrenOfTemplateType(ProjectConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.Root);

        if (foundChildrenItemProxies != null && foundChildrenItemProxies.Any())
        {
          foreach (var childItemProxy in foundChildrenItemProxies)
          {
            var candidate = new MovieShowTimeProxy(childItemProxy);
            if (candidate != null)
            {
              toReturn.Add(candidate);
            }
          };
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("movie root item not found", this);
      }

      return toReturn;
    }
  }
}