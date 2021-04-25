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
    private string GetPosterSrc(MovieItemProxy movieDataItem)
    {
      var toReturn = string.Empty;
      ImageField imageField = movieDataItem.PosteImageField;
      if (imageField != null && imageField.MediaItem != null)
      {
        MediaItem image = new MediaItem(imageField.MediaItem);
        toReturn = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
      }

      return toReturn;
    }

    private DateTime GetMovieTime(Item movieShowTime)
    {
      DateTime toReturn = Sitecore.DateUtil.IsoDateToDateTime(movieShowTime.Fields[ProjConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.MovieTimeField].Value);

      return toReturn;
    }

    private MovieItemProxy GetMovieDataItem(Item movieShowTime)
    {
      MovieItemProxy toReturn = null;
      if (movieShowTime != null)
      {
        var movieDataRef = (ReferenceField)movieShowTime.Fields[ProjConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.MovieNameField];

        if (movieDataRef != null && movieDataRef.TargetItem != null)
        {
          Item movieItem = movieDataRef.TargetItem;
          toReturn = new MovieItemProxy(movieItem);
        }
      }
      return toReturn;
    }

    public MovieShowTime HarvestShowTime(Item movieShowTime)
    {
      // Sitecore.Data.Fields.MultilistField multiselectField =
      MovieShowTime toReturn = null;
      if (movieShowTime != null)
      {
        var movieDataItem = GetMovieDataItem(movieShowTime);
        if (movieDataItem != null)
        {
          toReturn = new MovieShowTime()
          {
            MovieName = movieDataItem.MovieName,
            MoviePoster = GetPosterSrc(movieDataItem),
            Id = movieShowTime.ID.Guid,
            MovieTime = GetMovieTime(movieShowTime),
          };
        }
      }
      return toReturn;
    }

    public List<MovieShowTime> AvailableMovies()
    {
      var toReturn = new List<MovieShowTime>();
      var showTimesItem = Sitecore.Context.Database.GetItem(ProjConst.Items.Content.MovieShowTimesFolderItem);
      if (showTimesItem != null)

      {
        var foundChildren = showTimesItem
          .GetChildren()
          .Where(x => x.TemplateID == ProjConst.Items.Templates.Feature.SitecoreCinema.MovieTicket.Root);

        if (foundChildren != null && foundChildren.Any())
        {
          foreach (Item movieShowTime in foundChildren)
          {
            var candidate = HarvestShowTime(movieShowTime);
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