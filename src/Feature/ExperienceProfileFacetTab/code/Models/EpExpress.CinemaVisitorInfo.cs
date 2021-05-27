using EPExpressTab.Data;
using EPExpressTab.Repositories;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.WeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using System;

namespace LearnEXM.Feature.ExperienceProfileFacetTab.Models
{
  public class EpExpressCinemaVisitorInfoViewModel : EpExpressViewModel
  {
    public override string Heading => "Cinema Visitor Info";
    public override string TabLabel => "Cinema Visitor Info";

    public override string GetFullViewPath(object model)
    {
      return ProjConstants.Views.CinemaVisitorInfo;
    }

    public override object GetModel(Guid contactId)
    {

      EpExpressCinemaVisitorInfoModel toReturn = new EpExpressCinemaVisitorInfoModel();


      Sitecore.XConnect.Contact xConnectContact = EPRepository.GetContact(contactId, CinemaVisitorInfo.DefaultFacetKey);

      string favoriteMovie = "{unknown}";

      if (xConnectContact?.Facets != null)
      {
        if (xConnectContact.Facets.ContainsKey(CinemaVisitorInfo.DefaultFacetKey))
        {
          var cinemaVisitorInfo = xConnectContact.Facets[CinemaVisitorInfo.DefaultFacetKey] as CinemaVisitorInfo;
          favoriteMovie = cinemaVisitorInfo.FavoriteMovie;
        }
        else
        {
        }
      }
      var weKnowTreeOptions = new WeKnowTreeOptionsFactory().GetWeKnowTreeOptions(ProjConstants.Items.WeKnowTreeOptionsEPTab);
      
      var weKnowTreeBuilder = new WeKnowTreeBuilder(weKnowTreeOptions);

      toReturn.ContactId = xConnectContact.Id.ToString();
      toReturn.FavoriteMovie = favoriteMovie;
      toReturn.WeKnowTree = weKnowTreeBuilder.GetWhatWeKnowTreeFromXConnectContact(xConnectContact);
        //VisitCount = (int)((dynamic)contact).VisitCount

      return toReturn;
    }
  }
}