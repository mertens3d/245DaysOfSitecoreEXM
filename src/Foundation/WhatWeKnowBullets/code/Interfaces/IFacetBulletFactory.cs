using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IFacetBulletFactory 
  {
    string  AssociatedDefaultFacetKey { get; set; }

    IBullet GetBullet(Facet facet);
  }
}