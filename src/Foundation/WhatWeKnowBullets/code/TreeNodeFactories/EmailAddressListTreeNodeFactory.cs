using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace LearnEXM.Foundation.WhatWeKnowBullets.BuiltInBulletFactories
{
  public class EmailAddressListTreeNodeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public string AssociatedDefaultFacetKey { get; set; } = EmailAddressList.DefaultFacetKey;

    public ITreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new TreeNode("Email Address List");

      var emailAddressList = facet as EmailAddressList;

      if (emailAddressList != null)
      {
        var preferredEmail = emailAddressList.PreferredEmail;
        if (preferredEmail != null)
        {
          toReturn.Leaves.Add(new TreeNode("Preferred Email", preferredEmail.SmtpAddress));
        }

        toReturn.Leaves.Add(LastModified(facet));
        toReturn.AddRawLeaf(SerializeFacet(facet));
      }
      return toReturn;
    }
  }
}