using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;

namespace Sitecore.Documentation
{
  public class InteractionsReporter
  {
    internal void ReportInteractionsForExistingContact(Contact existingContact, IReadOnlyCollection<IInteractionReference> interactions, XConnect.Client.XConnectClient client, PersonalInformation existingContactFacetData)
    {
      var i = 0;
      Console.WriteLine("Interaction count: " + interactions.Count);
      Console.WriteLine("");

      // Cycle through all interactions
      foreach (Interaction interaction in interactions)
      {
        i++;
        var ipinfo = interaction.GetFacet<IpInfo>(IpInfo.DefaultFacetKey);
        Console.WriteLine("Interaction #" + i);

        if (ipinfo != null)
        {
          // For each interaction, print out the business name
          // associated with that interactions' IpInfo
          Console.WriteLine("Interaction business name: " + ipinfo.BusinessName);
          Console.WriteLine("Interaction Id: " + interaction.Id);

          if (existingContact.Id != null && interaction.Id != null)
          {
            InteractionReference interactionReference = new InteractionReference((Guid)existingContact.Id, (Guid)interaction.Id);

            var contactOptions = new RelatedContactExpandOptions(new string[]
                  {
                  PersonalInformation.DefaultFacetKey
                  });

            var facets = new string[] {
              IpInfo.DefaultFacetKey
            };

            var expandOptions = new InteractionExpandOptions(facets)

            {
              Contact = contactOptions
            };

            var interactionB = client.Get<Interaction>(interactionReference, expandOptions);
            if (interactionB != null)
            {
              Console.WriteLine("interactionB");
              foreach (var prop in interactionB.GetType().GetProperties())
              {
                Console.WriteLine($"{prop.Name} : {prop.GetValue(interactionB,null )}");
              }
            }
          }
        }
      }

      Console.WriteLine("<Enter> to proceed");
      Console.ReadLine();
    }
  }
}