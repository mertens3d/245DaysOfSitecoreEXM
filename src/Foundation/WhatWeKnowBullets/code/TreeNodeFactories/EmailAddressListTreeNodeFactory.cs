using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class EmailAddressListTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = EmailAddressList.DefaultFacetKey;

    public IWhatWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new WhatWeKnowTreeNode("Email Address List");

      var emailAddressList = facet as EmailAddressList;

      if (emailAddressList != null)
      {
        var preferredEmail = emailAddressList.PreferredEmail;
        if (preferredEmail != null)
        {
          toReturn.AddNode(new WhatWeKnowTreeNode("Preferred Email", preferredEmail.SmtpAddress));
        }

        toReturn.AddNode(LastModified(facet));
        toReturn.AddRawNode(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}