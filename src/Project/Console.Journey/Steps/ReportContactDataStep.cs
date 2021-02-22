using Shared.Interfaces;
using Shared.Models;
using Shared.Models.SitecoreCinema.Collection;
using Shared.XConnect;
using System;
using System.Threading.Tasks;

namespace Console.Journey.Steps
{
  public class ReportContactDataStep : _journeyStepBase
  {
    public ReportContactDataStep(IFeedback feedback, string identifier) : base(feedback, identifier)
    {
    }

    public async Task ReportAsync()
    {
      try
      {
        Feedback.WriteLine("Before you go - do you want to see the data we collected about you today? :)");

        var dataReporter = new DataReporter();

        KnownData knownData = await dataReporter.GetKnownDataByIdentifier(Identifier);

        if (dataReporter != null)
        {
          if (knownData.Id != null)
          {
            Feedback.WriteLine(string.Format("Contact id: " + knownData.Id));
          }

          if (knownData?.details != null && knownData.movie != null)
          {
            Feedback.WriteLine(string.Format("Your name is {0} {1} and your favorite movie is {2}",
              knownData.details.FirstName,
              knownData.details.LastName,
              knownData.movie.FavoriteMovie));
          }
          else
          {
            Feedback.WriteLine("details or movie was null");
          }

          if (knownData?.Interactions != null)
          {
            Feedback.WriteLine("Today you have had " + knownData.Interactions.Count + " interactions with us");

            var i = 0;

            foreach (var interactionsToday in knownData.Interactions)
            {
              i++;
              var cinemaId = interactionsToday.GetFacet<CinemaInfo>(CinemaInfo.DefaultFacetKey);
              Feedback.WriteLine("");

              Feedback.WriteLine("Interaction #" + i + (cinemaId != null ? " at Cinema #" + cinemaId.CinimaId : string.Empty));

              Feedback.WriteLine("Events: ");

              foreach (var evv in interactionsToday.Events)
              {
                Feedback.WriteLine(evv.GetType().ToString() + "");
              };
              Feedback.WriteLine("");
            }
          }
        }
      }
      catch (Exception ex)
      {
        Feedback.WriteLine(ex.Message);
        throw;
      }
      Feedback.ReadKey();
    }
  }
}