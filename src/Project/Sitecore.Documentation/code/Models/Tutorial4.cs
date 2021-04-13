using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitecore.Documentation.Models
{
  public class Tutorial4
  {
    internal async Task ExecuteAsync(XConnect.Client.XConnectClient client)
    {
      try
      {
        var reportingHelper = new ReportingHelpers();

        var result0 = client.Contacts.ToEnumerable().Count();

        Console.WriteLine($"Total contacts: " + result0.ToString());

        // Use InteractionsCache instead of client.Contacts.Where(x => x.Interactions.Any()) as not all search providers support joins

        var results = await client.Contacts.Where(c => c.InteractionsCache().InteractionCaches.Any()).GetBatchEnumerator();

        Console.WriteLine("Contacts with Interactions: " + results.TotalCount);

        var results2 = await client.Contacts.Where(c => c.LastModified > DateTime.UtcNow.AddHours(-10)).GetBatchEnumerator();

        Console.WriteLine("Updated 10hrs ago: " + results2.TotalCount);

        var results3 = await client.Contacts.Where(c => c.GetFacet<PersonalInformation>().JobTitle == Const.XConnect.JobTitles.ProgrammerWriter).GetBatchEnumerator();

        Console.WriteLine(Const.XConnect.JobTitles.ProgrammerWriter + " : " + results3.TotalCount);

        var targetFacets = new string[] { IpInfo.DefaultFacetKey };
        var targetContact = new RelatedContactExpandOptions(PersonalInformation.DefaultFacetKey);
        var expandOptions = new InteractionExpandOptions(targetFacets) { Contact = targetContact };

        var results4 = await client.Interactions
          .Where(i => i.EndDateTime > DateTime.UtcNow.AddHours(-10))
          .WithExpandOptions(expandOptions)
          .GetBatchEnumerator();

        Console.WriteLine("Interactions < 10hrs old: " + results4.TotalCount);

        ReportingHelpers.EnterToProceed();

        var interactionNumber = 0;

        while (await results4.MoveNext())
        {
          // Loop through interactions in current batch
          foreach (var interaction in results4.Current)
          {
            interactionNumber++;

            Console.WriteLine("Interaction #" + interactionNumber);

            var ipInfoFacet = interaction.GetFacet<IpInfo>(IpInfo.DefaultFacetKey);

            reportingHelper.ReportIpInfoFacet(ipInfoFacet);

           
            var contact = interaction.Contact;
            if (contact != null)
            {
              var realContact = contact as Contact;

              reportingHelper.ReportRealContact(realContact);
            }

            Console.WriteLine();
          }
          ReportingHelpers.EnterToProceed();
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}