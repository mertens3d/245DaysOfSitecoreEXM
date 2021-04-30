using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Sitecore.XConnect;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class GenericTreeFactory : _baseFacetTreeNode, IFacetNodeFactory
  {
    public GenericTreeFactory(string targetFacetKey)
    {
      this.AssociatedDefaultFacetKey = targetFacetKey;
    }

    public string AssociatedDefaultFacetKey { get; set; }

    public IWeKnowTreeNode BuildTreeNode(Facet facet)
    {
      var toReturn = new WeKnowTreeNode(AssociatedDefaultFacetKey);

      if (facet != null)
      {
        var objectToTreeNode = new ObjectToTreeNode();
        toReturn.AddNode(objectToTreeNode.MakeTreeNodeFromFacet(facet, AssociatedDefaultFacetKey));
        toReturn.AddRawNode(SerializeFacet(facet));
      }

      return toReturn;
    }
  }
}