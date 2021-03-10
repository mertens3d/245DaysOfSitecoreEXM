using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels;
using LearnEXM.Foundation.ConsoleTools.XConnect.Client;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;

namespace LearnEXM.Feature.MockContactGenerator
{
  public class ContactCreator
  {
    private async System.Threading.Tasks.Task CreateOneContactAsync(XConnectClient client, CandidateContactInfo candidateContactInfo)
    {
      try
      {
        var identifierId = "MockContact." + Guid.NewGuid();
        ContactIdentifier identifier = new ContactIdentifier(CollectionConst.XConnect.ContactIdentifiers.Sources.SitecoreCinema, identifierId, ContactIdentifierType.Known);

        Contact contact = new Contact(new ContactIdentifier[] { identifier });

        PersonalInformation personalInfo = new PersonalInformation()
        {
          FirstName = candidateContactInfo.FirstName,
          LastName = candidateContactInfo.LastName,
          Birthdate = DateTime.Now,
          Gender = candidateContactInfo.Gender,
        };

        client.AddContact(contact);
        client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfo);


        var emailAddress = new EmailAddress(candidateContactInfo.Email, true);
        var address = new EmailAddressList(emailAddress, CollectionConst.XConnect.EmailPreferredKey);
        client.SetFacet(contact, EmailAddressList.DefaultFacetKey, address);

        Interaction interaction = new Interaction(contact, InteractionInitiator.Brand, CollectionConst.XConnect.Channels.RegisterInteractionCode, string.Empty);

        interaction.Events.Add(new Goal(CollectionConst.XConnect.Goals.RegistrationGoal, DateTime.UtcNow));

        client.AddInteraction(interaction);
        await client.SubmitAsync();
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }
    }

    public async System.Threading.Tasks.Task CreateKnownContact(CandidateContactInfo candidateContactInfo)
    {
      var cfgGenerator = new CFGGenerator();

      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      try
      {
        await cfg.InitializeAsync();

        using (var client = new XConnectClient(cfg))
        {
          await CreateOneContactAsync(client, candidateContactInfo);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}