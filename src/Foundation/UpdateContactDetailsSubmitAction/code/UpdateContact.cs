using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using XConnectContact = Sitecore.XConnect.Contact;

namespace LearnEXM.Foundation.UpdateContactDetailsSubmitAction
{
  public class UpdateContact : SubmitActionBase<UpdateContactData>
  {
    public UpdateContact(ISubmitActionData submitActionData) : base(submitActionData)
    {
    }

    protected virtual ITracker CurrentTracker => Tracker.Current;

    protected override bool Execute(UpdateContactData data, FormSubmitContext formSubmitContext)
    {
      Assert.ArgumentNotNull(data, nameof(data));
      Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));

      var firstNameField = GetFieldById(data.FirstNameFieldId, formSubmitContext.Fields);
      var lastNameField = GetFieldById(data.LastNameFieldId, formSubmitContext.Fields);
      var emailField = GetFieldById(data.EmailFieldId, formSubmitContext.Fields);

      if (firstNameField == null && lastNameField == null && emailField == null)
      {
        return false;
      }

      using (var client = CreateClient())
      {
        try
        {
          var source = "Subscribe.Form";
          var id = CurrentTracker.Contact.ContactId.ToString("N");

          CurrentTracker.Session.IdentifyAs(source, id);
          var trackerIdentifier = new IdentifiedContactReference(source, id);

          var expandOptions = new ContactExpandOptions(
            PersonalInformation.DefaultFacetKey,
            CollectionModel.FacetKeys.EmailAddressList);

          XConnectContact contact = client.Get(trackerIdentifier, expandOptions);

          SetPersonalInformation(GetValue(firstNameField), GetValue(lastNameField), contact, client);

          SetEmail(GetValue(emailField), contact, client);
          client.Submit();
          return true;
        }
        catch (Exception ex)
        {
          Logger.LogError(ex.Message, ex);
          return false;
        }
      }
    }

    private static void SetEmail(string email, XConnectContact contact, IXdbContext client)
    {
      if (string.IsNullOrEmpty(email))
      {
        return;
      }
      EmailAddressList emailFacet = contact.Emails();
      if (emailFacet == null)
      {
        emailFacet = new EmailAddressList(new EmailAddress(email, false), "Preferred");
      }
      else
      {
        if (emailFacet.PreferredEmail?.SmtpAddress == email)
        {
          return;
        }

        emailFacet.PreferredEmail = new EmailAddress(email, false);
      }

      client.SetEmails(contact, emailFacet);
    }

    public static string GetValue(object field)
    {
      return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
    }

    private static void SetPersonalInformation(string firsName, string lastName, XConnectContact contact, IXdbContext client)
    {
      if (string.IsNullOrEmpty(firsName) && string.IsNullOrEmpty(lastName))
      {
        return;
      }

      PersonalInformation personalInfoFacet = contact.Personal() ?? new PersonalInformation();

      if (personalInfoFacet.FirstName == firsName && personalInfoFacet.LastName == lastName)
      {
        return;
      }

      personalInfoFacet.FirstName = firsName;
      personalInfoFacet.LastName = lastName;

      client.SetPersonal(contact, personalInfoFacet);
    }

    protected virtual IXdbContext CreateClient()
    {
      return SitecoreXConnectClientConfiguration.GetClient();
    }

    private static IViewModel GetFieldById(Guid id, IList<IViewModel> fields)
    {
      return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
    }
  }
}