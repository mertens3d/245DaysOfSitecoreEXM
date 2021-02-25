using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.XConnect
{
  public abstract class _interactionBase
  {
    public _interactionBase(string identifier)
    {
      Identifier = identifier;
    }

    protected _interactionBase(Contact xConnectcontact)
    {
      XConnectContact = xConnectcontact;
    }

    public List<string> Errors { get; set; } = new List<string>();

    public string Identifier { get; set; }

    public Contact XConnectContact { get; set; } = null;

    protected XConnectClient Client { get; set; }

    public async Task ExecuteInteraction()
    {
      // Initialize a client using the validated configuration

      XConnectConfigHelper configHelper = new XConnectConfigHelper();

      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
      using (Client = new XConnectClient(cfg))
      {
        try
        {
          var clientHelper = new XConnectClientHelper(Client);

          if (!string.IsNullOrEmpty(Identifier))
          {
            XConnectContact = await clientHelper.GetXConnectContactByIdentifierAsync(Identifier);
          }

          InteractionBody();
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }
    }

    public abstract void InteractionBody();
    //protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    //public KnownData KnownData { get; private set; }
  }
}