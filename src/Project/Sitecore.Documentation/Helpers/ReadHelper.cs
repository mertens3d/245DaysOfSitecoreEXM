using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Threading.Tasks;

namespace Sitecore.Documentation.Helpers
{
  public class ReadHelper
  {
    public async Task<Contact> RetrieveExistingContactAsync(XConnectClient client)
    {
      Contact existingContact = null;

      try
      {
        IdentifiedContactReference reference = new IdentifiedContactReference(Const.XConnect.ContactIdentifiers.Sources.Twitter, Const.XConnect.ContactIdentifiers.ExampleData.MyrtleIdentifier);

        var interactions = new RelatedInteractionsExpandOptions(IpInfo.DefaultFacetKey)
        {
          StartDateTime = DateTime.MinValue,
          EndDateTime = DateTime.MaxValue
        };

        var targetedFacets = new string[]
        {
          PersonalInformation.DefaultFacetKey
        };

        var contactExpandOptions = new ContactExpandOptions(targetedFacets)
        {
          Interactions = interactions
        };

        existingContact = await client.GetAsync<Contact>(reference, contactExpandOptions);
      }
      catch (Exception ex)
      {
        throw;
      }

      return existingContact;
    }

    public PersonalInformation RetrieveExistingContactFacetData(XConnectClient client, Contact existingContact)
    {
      PersonalInformation existingContactFacet = null;

      try
      {
        if (existingContact != null)
        {
          existingContactFacet = existingContact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
        }
        else
        {
          Console.WriteLine($"Identifier {Const.XConnect.ContactIdentifiers.ExampleData.MyrtleIdentifier} not found");
        }
      }
      catch (Exception)
      {
        // Deal with exception
      }

      return existingContactFacet;
    }
  }
}