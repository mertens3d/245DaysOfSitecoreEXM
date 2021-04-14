using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.Framework.Rules;
using Sitecore.XConnect;
using Sitecore.XConnect.Segmentation.Predicates;
using System;
using System.Linq.Expressions;

namespace LearnEXM.Feature.Marketing.Conditions
{
  internal class MarketingCompanyName : ICondition, IContactSearchQueryFactory
  {
    public StringOperationType Comparison { get; set; }
    public string TargetCompanyName { get; set; }

    public Expression<Func<Contact, bool>> CreateContactSearchQuery(IContactSearchQueryContext context)
    {
      return contact => Comparison.Evaluate(contact.GetFacet<Foundation.CollectionModel.Builder.Models.Facets.Marketing>(Foundation.CollectionModel.Builder.Models.Facets.Marketing.DefaultFacetKey).CompanyName, TargetCompanyName);
    }

    public bool Evaluate(IRuleExecutionContext context)
    {
      var contact = context.Fact<Contact>();

      return Comparison.Evaluate(contact.GetFacet<Foundation.CollectionModel.Builder.Models.Facets.Marketing>(Foundation.CollectionModel.Builder.Models.Facets.Marketing.DefaultFacetKey)?.CompanyName, TargetCompanyName);
    }
  }

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