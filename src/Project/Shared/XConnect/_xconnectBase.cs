using Shared.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System.Collections.Generic;
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
      await initializer.InitCFGAsync(cfg);

      // Initialize a client using the validated configuration

      using (Client = new XConnectClient(cfg))
      {
        try
        {
          InteractionBody();
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }
    }


    public List<string> Errors { get; set; } = new List<string>();
    protected XConnectClient Client { get; set; }
    public Contact Contact { get; set; } = null;
    public string Identifier { get; set; }
    protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    public KnownData KnownData { get; private set; }

    protected async Task PopulateContactDataAsync()
    {
       KnownData = new KnownData();

      if (Identifier != null)
      {
        IdentifiedContactReference = new IdentifiedContactReference(Const.XConnect.ContactIdentifiers.Sources.SitecoreCinema, Identifier);

        if (Client != null)
        {
          Contact = await Client.GetAsync(IdentifiedContactReference, new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey));
          if (Contact != null)
          {
            KnownData.Id = Contact.Id;
            KnownData.details = Contact.GetFacet<PersonalInformation>();
            KnownData.movie = Contact.GetFacet<CinemaVisitorInfo>();

            KnownData.Interactions = Contact.Interactions;
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