using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using LearnEXMProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXMProject.Controllers.Helpers
{
  public class MovieTicketHelper
  {
    public List<Movie> AvailableMovies()
    {
      var toReturn = new List<Movie>();
      var movieRootItem = Sitecore.Context.Database.GetItem(WebConst.Items.Content.MovieTicketRootItem);
      if (movieRootItem != null)

      {
        var foundChildren = movieRootItem
          .GetChildren()
          .Where(x => x.TemplateID == WebConst.Items.Templates.Feature.SitecoreCinema.MovieTicket);
      }
      else
      {
        Sitecore.Diagnostics.Log.Error("movie root item not found", this);
      }

      return toReturn;
    }
  }
}