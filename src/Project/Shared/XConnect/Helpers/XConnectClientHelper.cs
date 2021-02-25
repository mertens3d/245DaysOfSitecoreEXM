using Shared.Models.SitecoreCinema;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.XConnect
{
  public class XConnectClientHelper
  {
    public XConnectClientHelper(XConnectClient client)
    {
      this.Client = client;
    }

    public List<string> Errors { get; set; } = new List<string>();
    private XConnectClient Client { get; }

    public Contact GetContactById(Guid contactId)
    {
      Contact toReturn = null;

      if (contactId != Guid.Empty)
      {
        try
        {
          var contactReference = new Sitecore.XConnect.ContactReference(contactId);
          if (contactReference != null)
          {
            toReturn = Client.Get<Contact>(contactReference, new Sitecore.XConnect.ContactExpandOptions() { });
          }
        }
        catch (Exception ex)
        {
          Errors.Add(ex.Message);
        }
      }

      return toReturn;
    }

    public async Task<Contact> GetXConnectContactByIdentifierAsync(string identifier)
    {
      Contact toReturn = null;

      if (!string.IsNullOrEmpty(identifier))
      {
        var IdentifiedContactReference = new IdentifiedContactReference(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, identifier);
        if (Client != null)
        {
          try
          {
            var interactions = new RelatedInteractionsExpandOptions(IpInfo.DefaultFacetKey)
            {
              StartDateTime = DateTime.MinValue,
              EndDateTime = DateTime.MaxValue
            };

            var expandOptions = new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey)
            {
              Interactions = interactions
            };
            toReturn = await Client.GetAsync(IdentifiedContactReference, expandOptions);
          }
          catch (System.Exception ex)
          {
            Errors.Add(ex.Message);
          }
        }
        else
        {
          Errors.Add("client was null");
        }
      }
      else
      {
        Errors.Add("Identifier was null");
      }

      if (Errors.Any())
      {
        toReturn = null;
      }
      return toReturn;
    }
  }
}