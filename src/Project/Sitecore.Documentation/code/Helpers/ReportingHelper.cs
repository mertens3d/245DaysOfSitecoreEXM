using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;

namespace Sitecore.Documentation
{
  public class ReportingHelpers
  {
    public static void EnterToProceed()
    {
      var existingForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      Console.WriteLine("<Enter> to proceed");
      Console.ForegroundColor = existingForegroundColor;
      Console.ReadKey();
    }

    public void DisplayResults(XConnectClient client)
    {
      var operations = client.LastBatch;

      Console.WriteLine("RESULTS...");
      // Loop through operations and check status
      foreach (var operation in operations)
      {
        Console.WriteLine(operation.OperationType + operation.Target.GetType().ToString() + " Operation: " + operation.Status);
      }
    }

    public void DisplayExistingContactData(Contact existingContact)
    {
      Console.WriteLine("Contact ID: " + existingContact.Id.ToString());
    }

    public void ReportInteractionsForExistingContact(Contact existingContact, XConnect.Client.XConnectClient client)
    {
      IReadOnlyCollection<IInteractionReference> interactions = existingContact.Interactions;

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
          Console.WriteLine("\tInteraction business name: " + ipinfo.BusinessName);
          Console.WriteLine("\tInteraction Id: " + interaction.Id);

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
              Console.WriteLine("");
              Console.WriteLine("All Interaction Properties");
              foreach (var prop in interactionB.GetType().GetProperties())
              {
                Console.WriteLine($"\t{prop.Name} : {prop.GetValue(interactionB, null)}");
              }
            }
          }
        }
      }

      ReportingHelpers.EnterToProceed();
    }

    internal void ReportIpInfoFacet(IpInfo ipInfoFacet)
    {
      if (ipInfoFacet != null)
      {
        Console.WriteLine("\tInteraction business name: " + ipInfoFacet.BusinessName);
      }
      else
      {
        Console.WriteLine("\tNo business name available.");
      }

    }

    public void ReportRealContact(Contact realContact)
    {
      if (realContact != null)
      {
        var personal = realContact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);

        if (personal != null)
        {
          Console.WriteLine("\tInteraction contact name: " + personal.FirstName);
        }
        else
        {
          Console.WriteLine("\tNo contact name available");
        }
      }
      else
      {
        Console.WriteLine("\tNo real contact...don't know why");
      }
    }

    public void DisplayExistingContactFacetData(PersonalInformation existingContact)
    {
      if (existingContact != null)
      {
        Console.WriteLine("Contact Name: " + existingContact.FirstName);
      }
      else
      {
        Console.WriteLine("ExistingContactFaceData is null");
      }
    }
  }
}