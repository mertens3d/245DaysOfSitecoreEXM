using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Feature.WhatWeKnowAboutYou.Models
{
  public class KnownDataFacets
  {
    public PersonalInformation PersonalInformationDetails { get; set; }
    public CinemaVisitorInfo CinemaVisitorInfo { get; set; }
    public EmailAddressList EmailAddressList { get; internal set; }
    public CinemaInfo CinemaInfo { get; internal set; }
    public CinemaDetails CinemaDetails { get; internal set; }
  }
}