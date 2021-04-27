using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Helpers
{
  public class PersonalInformationTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = PersonalInformation.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Personal Information Details");
      var personalInformationDetails = facet as PersonalInformation;
      if (personalInformationDetails != null)
      {
        toReturn.Leaves.Add(new TreeNode("Name", personalInformationDetails.FirstName + " " + personalInformationDetails.LastName));
        toReturn.Leaves.Add(new TreeNode("Birthdate", personalInformationDetails.Birthdate.ToString()));
        toReturn.Leaves.Add(new TreeNode("Gender", personalInformationDetails.Gender));

        toReturn.Leaves.Add(LastModified(facet));
        toReturn.AddRawLeaf(SerializeFacet(facet));

      }
      return toReturn;
    }
  }
}