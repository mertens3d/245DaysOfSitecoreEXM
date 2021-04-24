using LearnEXM.Project.SitecoreCinema.Model;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnEXM.Project.SitecoreCinema.Controllers.Helpers
{
  public class MovieTicketHelper
  {
    public List<MovieShowTime> AvailableMovies()
    {
      var toReturn = new List<MovieShowTime>();
      var showTimesItem = Sitecore.Context.Database.GetItem(WebConst.Items.Content.MovieShowTimesFolderItem);
      if (showTimesItem != null)

      {
        var foundChildren = showTimesItem
          .GetChildren()
          .Where(x => x.TemplateID == WebConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.Root);

        if (foundChildren != null && foundChildren.Any())
        {
          foreach (Item movieShowTime in foundChildren)
          {
            // Sitecore.Data.Fields.MultilistField multiselectField =

            var movieDataRef = (ReferenceField)movieShowTime.Fields[WebConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.MovieNameField];

            if (movieDataRef != null && movieDataRef.TargetItem != null)
            {
              var movieDataItem = movieDataRef.TargetItem;

              DateTime movieTime = Sitecore.DateUtil.IsoDateToDateTime(movieShowTime.Fields[WebConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.MovieTimeField].Value);

              ImageField imageField = movieDataItem.Fields[WebConst.Items.Templates.Feature.SitecoreCinema.MovieData.Poster];
              var posterSrc = string.Empty;
              if (imageField != null && imageField.MediaItem != null)
              {
                MediaItem image = new MediaItem(imageField.MediaItem);
                posterSrc = Sitecore.StringUtil.EnsurePrefix('/',
               Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
              }

              var showTime = new MovieShowTime()
              {
                MovieName = movieDataItem[WebConst.Items.Templates.Feature.SitecoreCinema.MovieData.MovieName],
                MoviePoster = posterSrc,

                MovieTime = movieTime,
              };
              toReturn.Add(showTime);
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