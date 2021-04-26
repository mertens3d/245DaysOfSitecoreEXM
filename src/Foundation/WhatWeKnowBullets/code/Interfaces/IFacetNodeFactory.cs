using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Interfaces
{
  public interface IFacetNodeFactory 
  {
    string  AssociatedDefaultFacetKey { get; set; }

    ITreeNode BuildTreeNode(Facet facet);

    ITreeNode SerializeAsRaw(Facet facet);
    void SetClient(XConnectClient xConnectClient);
  }
}