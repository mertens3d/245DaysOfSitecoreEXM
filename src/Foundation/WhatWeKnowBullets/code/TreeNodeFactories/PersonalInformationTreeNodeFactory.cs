using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class PersonalInformationTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = PersonalInformation.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Address List");
      var addressList = facet as AddressList;
      if (addressList != null)
      {
        if (addressList.PreferredAddress != null)
        {
          var preferredNode = new TreeNode("Preferred Address");
          preferredNode.AddNode(new TreeNode("Address Line 1", addressList.PreferredAddress.AddressLine1));
          toReturn.AddNode(preferredNode);
        }
        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}