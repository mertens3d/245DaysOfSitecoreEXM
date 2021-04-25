using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IFacetTreeNodeFactory 
  {
    string  AssociatedDefaultFacetKey { get; set; }

    ITreeNode BuildTreeNode(Facet facet);
  }
}