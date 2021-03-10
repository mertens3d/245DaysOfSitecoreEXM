using Sitecore.Framework.Rules;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Segmentation.Predicates;
using System;
using System.Linq.Expressions;

namespace LearnEXM.Feature.ConditionAndSegmentationQueries.Conditions
{
  public class FirstNameMatches : ICondition, IContactSearchQueryFactory
  {
    public string TargetName { get; set; }

    public StringOperationType Comparison { get; set; }

    public bool Evaluate(IRuleExecutionContext context)
    {
      var contact = context.Fact<Contact>();

      return Comparison.Evaluate(contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey)?.FirstName, TargetName);
    }

    // Evaluate contact in a search context
    // Use InteractionsCache()
    public Expression<Func<Contact, bool>> CreateContactSearchQuery(IContactSearchQueryContext context)
    {
      return contact => Comparison.Evaluate(contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey).FirstName, TargetName);
    }
  }
}