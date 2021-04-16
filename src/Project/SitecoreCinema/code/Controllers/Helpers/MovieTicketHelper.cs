using LearnEXM.Feature.WhatWeKnowAboutYou.Models;
using LearnEXM.Project.SitecoreCinema.Model;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Project.SitecoreCinema.Controllers.Helpers
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