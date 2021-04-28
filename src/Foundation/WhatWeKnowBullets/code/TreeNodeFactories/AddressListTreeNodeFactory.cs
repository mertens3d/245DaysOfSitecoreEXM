using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class AddressListTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = AddressList.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Personal Information Details");
      var personalInformationDetails = facet as PersonalInformation;
      if (personalInformationDetails != null)
      {
        toReturn.AddNode(new TreeNode("Name", personalInformationDetails.FirstName + " " + personalInformationDetails.LastName));
        toReturn.AddNode(new TreeNode("Birthdate", personalInformationDetails.Birthdate.ToString()));
        toReturn.AddNode(new TreeNode("Gender", personalInformationDetails.Gender));

        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}