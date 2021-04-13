using Marketing.MockContactGenerator.Models;
using Newtonsoft.Json;
using Sitecore.XConnect;
using Sitecore.XConnect.Client.Serialization;
using System;
using System.IO;
using System.Linq;

namespace Marketing.MockContactGenerator.Helpers
{
  internal class FeedbackHelper
  {
    private Contact RetrievedContact;

    public FeedbackHelper(Contact retrievedContact)
    {
      this.RetrievedContact = retrievedContact;
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
    internal class MockContactGeneratorHelper
    {
      public MockContactGeneratorHelper()
      {
        RandomHelper = new RandomHelper();

        SourceLists = new SourceLists();
      }

      public RandomHelper RandomHelper { get; }

      public SourceLists SourceLists { get; private set; }
      public CandidateContactInfo GenerateContact()
      {
        CandidateContactInfo toReturn = new CandidateContactInfo();

        toReturn.AddressStateOrProvince = RandomHelper.RandomListItem(SourceLists.AddressState);
        toReturn.AddressStreet = RandomHelper.RandomInt(9999) + " " + RandomHelper.RandomListItem(SourceLists.AddressStreet);
        toReturn.PostalCode = RandomHelper.RandomInt(11111, 99999).ToString();

        toReturn.CompanyName = RandomHelper.RandomListItem(SourceLists.CompanyName);
        toReturn.ContactType = RandomHelper.RandomListItem(SourceLists.ContactType);

        toReturn.ContactDivision = RandomHelper.RandomListItem(SourceLists.Divisions);
        toReturn.FirstName = RandomHelper.RandomListItem(SourceLists.FirstNames);
        toReturn.LastName = RandomHelper.RandomListItem(SourceLists.LastNames);
        toReturn.ContactLineOfBusiness = RandomHelper.RandomListItem(SourceLists.LineOfBusiness);

        toReturn.ContactPhone = RandomHelper.RandomPhoneNumber();

        toReturn.SimpleId = RandomHelper.RandomInt(10000);
        toReturn.MarketingIdentifierId = "MockContact." + Guid.NewGuid();
        toReturn.Gender = RandomHelper.RandomInt(2) == 0 ? MarketingConst.XConnect.Genders.Male : MarketingConst.XConnect.Genders.Female;

        toReturn.Email = toReturn.FirstName + "." + toReturn.LastName + "." + toReturn.SimpleId + "@mock." + toReturn.CompanyName.Replace(" ", string.Empty) + ".com";

        return toReturn;
      }
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