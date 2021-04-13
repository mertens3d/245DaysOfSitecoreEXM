using LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Marketing.MockContactGenerator.Helpers;
using Marketing.MockContactGenerator.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Threading.Tasks;

namespace Marketing.MockContactGenerator
{
  internal class XConnectBroker
  {
    private XConnectClientConfiguration cfg { get; set; }
    private bool IsInit { get; set; }

    public async Task<bool> InitBrokerAsync()
    {
      try
      {
        var cfgHelper = new CFGHelper();
        cfg = CFGHelper.GenerateCFG(SitecoreCinemaModel.Model);
        await cfg.InitializeAsync();
        IsInit = true;
      }
      catch (Exception ex)
      {
        IsInit = false;
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.ToString());
      }

      return IsInit;
    }

    internal async Task AddKnownContactAsync(CandidateContactInfo candidate)
    {
      if (IsInit)
      {
        using (var client = new XConnectClient(cfg))
        {
          await CreateOneContactAsync(client, candidate);
        }
      }
      else
      {
        Console.WriteLine("Broker not init");
      }
    }

    internal async Task ReportOnKnownContactAsync(CandidateContactInfo candidate)
    {
      if (IsInit)
      {
        using (var client = new XConnectClient(cfg))
        {
          var retrievedContact = await RetrieveContactAsync(client, candidate.MarketingIdentifierId);
          if (retrievedContact != null)
          {
            var feedbackHelper = new FeedbackHelper(retrievedContact);
            feedbackHelper.ReportContactData(client);
          }
        }
      }
      else
      {
        Console.WriteLine("Broker not init");
      }
    }

    private async Task CreateOneContactAsync(XConnectClient client, CandidateContactInfo candidate)
    {
      try
      {
        var operationsHelper = new OperationsDataHelper(candidate);

        var contact = operationsHelper.CreateContact();

        client.AddContact(contact);
        client.SetFacet(contact, PersonalInformation.DefaultFacetKey, operationsHelper.MakeFacetPersonalInformation());
        client.SetFacet(contact, EmailAddressList.DefaultFacetKey, operationsHelper.MakeFacetEmailAddressList());
        client.SetFacet(contact, AddressList.DefaultFacetKey, operationsHelper.MakeFacetAddress());
        client.SetFacet(contact, CinemaVisitorInfo.DefaultFacetKey, operationsHelper.MakeFacetCinemaVisitorInfo());
        client.AddInteraction(operationsHelper.SetRegistrationGoalInteraction(contact));

        DrawCandidateDataToConsole(candidate);

        await client.SubmitAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    private void DrawCandidateDataToConsole(CandidateContactInfo candidate)
    {
      Console.WriteLine("Attempting to create known contact: " + candidate.FirstName + " " + candidate.LastName);
      Console.WriteLine("Marketing Identifier: " + candidate.MarketingIdentifierId);
    }

    private async Task<Sitecore.XConnect.Contact> RetrieveContactAsync(XConnectClient client, string marketingIdentifier)
    {
      Contact toReturn = null;

      try
      {
        var identifierContactReference = new IdentifiedContactReference(MarketingConst.XConnect.ContactIdentifiers.Sources.Marketing, marketingIdentifier);
        var expandOptions = new ContactExpandOptions(
                //AddressList.DefaultFacetKey,
                //EmailAddressList.DefaultFacetKey,
                //Marketing.DefaultFacetKey,
                //PersonalInformation.DefaultFacetKey
                );

        toReturn = await client.GetAsync(identifierContactReference, expandOptions);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      return toReturn;
    }
  }
}