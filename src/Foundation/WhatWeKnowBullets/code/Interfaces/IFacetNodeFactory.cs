using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IFacetNodeFactory 
  {
    string  AssociatedDefaultFacetKey { get; set; }

    ITreeNode BuildTreeNode(Facet facet);

    string SerializeFacet(Facet facet);
    void SetClient(XConnectClient xConnectClient);
  }
}