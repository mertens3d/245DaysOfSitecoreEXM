using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System.Diagnostics;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
//  public class GenericInteractionsBranchFactory : _baseFacetTreeNode
//{
//    public GenericInteractionsBranchFactory( WeKnowTreeOptions treeOptions) : base(treeOptions)
//    {

//    }
   
//  }


  public class GenericFacetBranchFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public GenericFacetBranchFactory(string targetFacetKey, WeKnowTreeOptions treeOptions, XConnectClient xConnectClient) : base(treeOptions)
    {
      this.AssociatedDefaultFacetKey = targetFacetKey;
      XConnectClient = xConnectClient;
      
    }

    public string AssociatedDefaultFacetKey { get; set; }
    private XConnectClient XConnectClient { get; }

    public IWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      IWeKnowTreeNode toReturn = null;// new WeKnowTreeNode(AssociatedDefaultFacetKey, TreeOptions);

      if (facet != null)
      {
        var objectToTreeNode = new ObjectToTreeNode(TreeOptions, XConnectClient);
        
        toReturn = objectToTreeNode.MakeTreeNodeFromObject(facet, AssociatedDefaultFacetKey);
        if (TreeOptions.IncludeRaw)
        {
          toReturn.AddRawNode(objectToTreeNode.SerializeObject(facet));

        }
      }

      return toReturn;
    }
  }
}