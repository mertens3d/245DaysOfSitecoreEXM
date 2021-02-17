//using Shared.Models;
//using Shared.XConnect;
//using Sitecore.XConnect;
//using Sitecore.XConnect.Client;
//using Sitecore.XConnect.Collection.Model;
//using SitecoreCinema.Model.Collection;
//using System;
//using System.Threading.Tasks;

//namespace Shared.Helpers
//{
//  public class GetContactData
//  {
//    public async Task<KnownData> GetKnownData(ContactIdentifier identifier)
//    {
//      var KnownData = new KnownData();

//      var cfgGenerator = new CFGGenerator();

//      var cfg = cfgGenerator.GetCFG(SitecoreCinemaModel.Model);

//      Contact contact = new Contact(new ContactIdentifier[] { identifier });


//      using (var Client = new XConnectClient(cfg))
//      {
//        var contactAgain = await Client.GetAsync(contact, new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey)
//        {
//          Interactions = new RelatedInteractionsExpandOptions(new string[]
//          {
//          CinemaInfo.DefaultFacetKey
//          })
//          {
//            StartDateTime = DateTime.Today
//          }
//        });

//        if (contactAgain != null)
//        {
//          KnownData.Id = contactAgain.Id;
//          KnownData.details = contactAgain.GetFacet<PersonalInformation>();
//          KnownData.movie = contactAgain.GetFacet<CinemaVisitorInfo>();

//          KnownData.Interactions = contactAgain.Interactions;
//        }
//      }

//      return KnownData;
//    }

//    //public async Task<KnownData> GetKnownData(XConnectClient client, Contact contact)
//    //{
//    //  var KnownData = new KnownData();

//    //  var contactAgain = await client.GetAsync<Contact>(contact, new ContactExpandOptions(PersonalInformation.DefaultFacetKey, CinemaVisitorInfo.DefaultFacetKey)
//    //  {
//    //    Interactions = new RelatedInteractionsExpandOptions(new string[]
//    //      {
//    //      CinemaInfo.DefaultFacetKey
//    //      })
//    //    {
//    //      StartDateTime = DateTime.Today
//    //    }
//    //  });

//    //  if (contactAgain != null)
//    //  {
//    //    KnownData.Id = contactAgain.Id;
//    //    KnownData.details = contactAgain.GetFacet<PersonalInformation>();
//    //    KnownData.movie = contactAgain.GetFacet<CinemaVisitorInfo>();

//    //    KnownData.Interactions = contactAgain.Interactions;
//    //  }

//    //  return KnownData;
//    //}
//  }
//}