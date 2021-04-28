using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class AddressListNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = AddressList.DefaultFacetKey;

    public IWhatWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new WhatWeKnowTreeNode("Address List");
      AddressList addressList = facet as AddressList;
      if (addressList != null)
      {
        if (addressList.PreferredAddress != null)
        {
          var preferredNode = new WhatWeKnowTreeNode("Preferred Address");
          preferredNode.AddNode(new WhatWeKnowTreeNode("Address Line 1", addressList.PreferredAddress.AddressLine1));
          toReturn.AddNode(preferredNode);
        }

        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}