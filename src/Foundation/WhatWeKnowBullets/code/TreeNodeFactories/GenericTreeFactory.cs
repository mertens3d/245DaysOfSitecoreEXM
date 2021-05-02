using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;
using System.Diagnostics;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class GenericTreeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public GenericTreeFactory(string targetFacetKey, WeKnowTreeOptions treeOptions):base(treeOptions)
    {
      this.AssociatedDefaultFacetKey = targetFacetKey;
    }

    public string AssociatedDefaultFacetKey { get; set; }

    public IWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new WeKnowTreeNode(AssociatedDefaultFacetKey, TreeOptions);

      if (facet != null)
{
        var objectToTreeNode = new ObjectToTreeNode(TreeOptions);
        toReturn.AddNode(objectToTreeNode.MakeTreeNodeFromFacet(facet, AssociatedDefaultFacetKey));
        toReturn.AddRawNode(SerializeFacet(facet));
      }

      return toReturn;
    }
  }
}