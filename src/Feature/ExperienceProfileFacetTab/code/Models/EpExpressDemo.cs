using EPExpressTab.Data;
using EPExpressTab.Repositories;
using Foundation.Marketing;
using System;

namespace ExperienceProfileFacetTab.Models
{
  public class EpExpressDemo : EpExpressModel
  {
    public override string TabLabel => "Awesome Tab";
    public override string Heading => "this is the start";

    public override string RenderToString(Guid contactId)
    {
      Sitecore.XConnect.Contact model = EPRepository.GetContact(contactId, MarketingConst.FacetKeys.Marketing);
      return $"<h1>Wow!!</h1><p>{model.Id}</p>";
    }
  }
}