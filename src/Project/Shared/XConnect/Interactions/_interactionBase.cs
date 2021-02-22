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

    public abstract void InteractionBody();

    public async Task ExecuteInteraction()
    {
      // Initialize a client using the validated configuration

      XConnectConfigHelper configHelper = new XConnectConfigHelper();

      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
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

    //protected IdentifiedContactReference IdentifiedContactReference { get; set; }
    //public KnownData KnownData { get; private set; }
  }
}