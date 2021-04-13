using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.Framework.Rules;
using Sitecore.XConnect;
using Sitecore.XConnect.Segmentation.Predicates;
using System;
using System.Linq.Expressions;

namespace Feature.Marketing.B.Conditions
{
  internal class MarketingFavoriteMovie : ICondition, IContactSearchQueryFactory
  {
    public StringOperationType Comparison { get; set; }
    public string TargetCompanyName { get; set; }
    public Expression<Func<Contact, bool>> CreateContactSearchQuery(IContactSearchQueryContext context)
    {
      return contact => Comparison.Evaluate(contact.GetFacet<CinemaVisitorInfo>(CinemaVisitorInfo.DefaultFacetKey).FavoriteMovie, TargetCompanyName);
    }

    public bool Evaluate(IRuleExecutionContext context)
    {
      var contact = context.Fact<Contact>();

      return Comparison.Evaluate(contact.GetFacet<CinemaVisitorInfo>(CinemaVisitorInfo.DefaultFacetKey)?.FavoriteMovie, TargetCompanyName);
    }
  }
}



