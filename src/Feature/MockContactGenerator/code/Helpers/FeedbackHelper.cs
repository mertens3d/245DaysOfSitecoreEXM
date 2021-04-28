using Newtonsoft.Json;
using Sitecore.XConnect;
using Sitecore.XConnect.Client.Serialization;
using System;
using System.IO;
using System.Linq;

namespace LearnEXM.Feature.MockContactGenerator.Helpers
{
  internal class FeedbackHelper
  {
    private Contact RetrievedContact;

    public FeedbackHelper(Contact retrievedContact)
    {
      RetrievedContact = retrievedContact;
    }

    private static DirectoryInfo GetMockDataFolder()
    {
      DirectoryInfo toReturn = null;

      try
      {
        var binFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        var ParentFolder = binFolder.Parent.Parent;
        toReturn = ParentFolder.GetDirectories("MockData").FirstOrDefault();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      return toReturn;
    }

    public void ReportContactData(Sitecore.XConnect.Client.XConnectClient client)
    {
      try
      {
        var serializerSettings = new JsonSerializerSettings
        {
          ContractResolver = new XdbJsonContractResolver(client.Model,
            serializeFacets: true,
            serializeContactInteractions: true),
          DateTimeZoneHandling = DateTimeZoneHandling.Utc,
          DefaultValueHandling = DefaultValueHandling.Ignore,
          Formatting = Formatting.Indented
        };

        var json = JsonConvert.SerializeObject(RetrievedContact, serializerSettings);

        var jsonFullFileName = GetMockDataFolder().FullName + "\\" + "contact." + RetrievedContact.Id.ToString() + ".json";
        File.WriteAllText(jsonFullFileName, json);

        Console.WriteLine("------------------------");
        Console.WriteLine("Retrieved Data");
        Console.WriteLine("");
        Console.WriteLine(json);
        Console.WriteLine("------------------------");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}