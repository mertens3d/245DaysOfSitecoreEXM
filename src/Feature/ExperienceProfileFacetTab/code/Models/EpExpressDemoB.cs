using EPExpressTab.Data;
using EPExpressTab.Repositories;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using System;

namespace LearnEXM.Feature.ExperienceProfileFacetTab.Models
{
  public class EpExpressDemoB : EpExpressViewModel
  {
    public override string Heading => "Look Ma! MVC!";
    public override string TabLabel => "Special MVC Tab";

    public override object GetModel(Guid contactId)
    {
      Sitecore.XConnect.Contact contact = EPRepository.GetContact(contactId, CinemaInfo.DefaultFacetKey);

      return new EpExpressDemoModel
      {
        ContactId = contact.Id.ToString(),
        VisitCount = (int)((dynamic)contact).VisitCount
      };
    }

    public override string GetFullViewPath(object model)
    {
      return "/views/feature/ExperienceProfileFacetTab/EpExpressDemo.cshtml";
    }
  }
}