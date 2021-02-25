using LearnEXMProject.Models;
using Shared.Models.SitecoreCinema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnEXMProject.Controllers.Helpers
{
  public class MovieTicketHelper
  {


    public List<Movie> AvailableMovies()
    {

      var toReturn = new List<Movie>();
      var movieRootItem = Sitecore.Context.Database.GetItem(CinemaConst.Items.Content.MovieTicketRootItem);
      if (movieRootItem != null)

      {
        var foundChildren = movieRootItem
          .GetChildren()
          .Where(x => x.TemplateID == CinemaConst.Items.Templates.Feature.SitecoreCinema.MovieTicket);
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("movie root item not found", this);
      }


      return toReturn;
    }

  }
}