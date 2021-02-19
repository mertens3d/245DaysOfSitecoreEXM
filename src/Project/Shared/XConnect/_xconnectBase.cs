using Shared.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.XConnect
{
  public abstract class _xconnectBase
  {
    public _xconnectBase(string identifier)
    {
      Identifier = identifier;
    }

    public abstract void InteractionBody();

    public async Task ExecuteInteraction()
    {
      var cfgGenerator = new CFGGenerator();

      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

      var initializer = new XConnectInitializer();

      if (!initializer.Errors.Any())
      {
        await initializer.InitCFGAsync(cfg);

        if (!initializer.Errors.Any())
        {
          // Initialize a client using the validated configuration

          using (Client = new XConnectClient(cfg))
          {
            try
            {
              //await PopulateContactDataAsync();
              InteractionBody();
              await PopulateContactDataAsync();
            }
            catch (XdbExecutionException ex)
            {
              Errors.Add(ex.Message);
            }
          }
        }
        else
        {
          Errors.AddRange(initializer.Errors);
        }
      }
      else
      {
        Errors.AddRange(initializer.Errors);
      }
    }

    public List<string> Errors { get; set; } = new List<string>();
    protected XConnectClient Client { get; set; }
    public Contact Contact { get; set; } = null;
    public string Identifier { get; set; }
    protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    public KnownData KnownData { get; private set; }

    private async Task PopulateContactDataAsync()
    {
      KnownData = new KnownData();

      if (!string.IsNullOrEmpty(Identifier))
      {
        IdentifiedContactReference = new IdentifiedContactReference(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);

        if (Client != null)
        {
          try
          {
            var expandOptions = new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey);
            Contact = await Client.GetAsync(IdentifiedContactReference, expandOptions);
            if (Contact != null)
            {
              KnownData.Id = Contact.Id;
              KnownData.details = Contact.GetFacet<PersonalInformation>();
              KnownData.movie = Contact.GetFacet<CinemaVisitorInfo>();

              KnownData.Interactions = Contact.Interactions;
            }
            else
            {
              Errors.Add("Contact was null");
            }
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
    }
  }
}