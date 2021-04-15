using EPExpressTab.Data;
using EPExpressTab.Repositories;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using System;

namespace LearnEXM.Feature.ExperienceProfileFacetTab.Models
{
  public class EpExpressCinemaVisitorInfoViewModel : EpExpressViewModel
  {
    public override string Heading => "Cinema Visitor Info";
    public override string TabLabel => "Cinema Visitor Info";

    public override object GetModel(Guid contactId)
    {
      Sitecore.XConnect.Contact contact = EPRepository.GetContact(contactId, CinemaVisitorInfo.DefaultFacetKey);

      string favoriteMovie = "{unknown}";

      if (contact?.Facets != null)
      {
        if (contact.Facets.ContainsKey(CinemaVisitorInfo.DefaultFacetKey))
        {
          var cinemaVisitorInfo = contact.Facets[CinemaVisitorInfo.DefaultFacetKey] as CinemaVisitorInfo;
          favoriteMovie = cinemaVisitorInfo.FavoriteMovie;
        }
        else
        {
        }
      }

      return new EpExpressCinemaVisitorInfoModel
      {
        ContactId = contact.Id.ToString(),
        FavoriteMovie = favoriteMovie
        //VisitCount = (int)((dynamic)contact).VisitCount
      };
    }

    public override string GetFullViewPath(object model)
    {
      return "/views/feature/ExperienceProfileFacetTab/CinemaVisitorInfo.cshtml";
    }
  }
}