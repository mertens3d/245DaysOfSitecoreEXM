using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class PersonalInformationTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = PersonalInformation.DefaultFacetKey;

    public IWhatWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new WhatWeKnowTreeNode("Personal Information Details");
      
      PersonalInformation personalInformationDetails = facet as PersonalInformation;
      if (personalInformationDetails != null)
      {
        toReturn.AddNode(new WhatWeKnowTreeNode("Name", personalInformationDetails.FirstName + " " + personalInformationDetails.LastName));
        toReturn.AddNode(new WhatWeKnowTreeNode("Birthdate", personalInformationDetails.Birthdate.ToString()));
        toReturn.AddNode(new WhatWeKnowTreeNode("Gender", personalInformationDetails.Gender));

        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}