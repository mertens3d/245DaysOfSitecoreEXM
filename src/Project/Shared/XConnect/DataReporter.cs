using Shared.Models;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.XConnect
{
  public class DataReporter
  {
    public DataReporter()
    {
      //var helper = new XConnectConfigHelper();
    }

    public List<string> Errors { get; set; } = new List<string>();

    public async Task<KnownData> GetKnownDataByContactId(Guid contactGuid)
    {
      KnownData toReturn = null;

      XConnectConfigHelper configHelper = new XConnectConfigHelper();
      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
      using (var Client = new XConnectClient(cfg))
      {
        try
        {
          var xConnectClientHelper = new XConnectClientHelper(Client);
          if (contactGuid != Guid.Empty)
          {
            var contact = xConnectClientHelper.GetContactById(contactGuid);
            toReturn = xConnectClientHelper.GetKnownDataFromContact(contact);
          }
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }

      return toReturn;
    }

    public async Task<KnownData> GetKnownDataByIdentifier(string Identifier)
    {
      KnownData toReturn = null;

      XConnectConfigHelper configHelper = new XConnectConfigHelper();
      XConnectClientConfiguration cfg = await configHelper.ConfigureClient();
      using (var Client = new XConnectClient(cfg))
      {
        try
        {
          var xConnectClientHelper = new XConnectClientHelper(Client);
          var contact = await xConnectClientHelper.GetContactByIdentifierAsync(Identifier);
          if (contact != null)
          {
            toReturn = xConnectClientHelper.GetKnownDataFromContact(contact);
          }
        }
        catch (XdbExecutionException ex)
        {
          Errors.Add(ex.Message);
        }
      }

      return toReturn;
    }
  }
}

//public override void InteractionBody()

//{
//  XConnectClientConfiguration cfg = await configureClient();
//  using (Client = new XConnectClient(cfg))
//  {
//    try
//    {
//    }
//    catch (XdbExecutionException ex)
//    {
//      Errors.Add(ex.Message);
//    }
//  }
//}