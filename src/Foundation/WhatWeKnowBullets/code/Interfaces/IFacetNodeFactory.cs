using Sitecore.XConnect.Client;
using Facet = Sitecore.XConnect.Facet;

namespace LearnEXM.Foundation.WhatWeKnowTree.Interfaces
{
  public interface IFacetNodeFactory
  {
    string AssociatedDefaultFacetKey { get; set; }

    IWeKnowTreeNode BuildTreeNode(Facet facet);

    string SerializeFacet(Facet facet);

    void SetClient(XConnectClient xConnectClient);
  }
}