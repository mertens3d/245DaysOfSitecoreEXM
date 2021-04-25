using LearnEXM.Foundation.WhatWeKnowBullets.Concretions;
using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Newtonsoft.Json;
using Sitecore.XConnect;

namespace LearnEXM.Foundation.WhatWeKnowBullets.TreeNodeFactories
{
  public abstract class _baseFacetTreeNode
  {
    public ITreeNode SerializeAsRaw(Facet facet)
    {
      var toReturn = new TreeNode("Raw");

      var serializerSettings = new JsonSerializerSettings
      {
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented,
        MaxDepth = 1
      };

      var serialized = string.Empty;

      try
      {
        serialized = JsonConvert.SerializeObject(facet, serializerSettings);
      }
      catch (System.Exception ex)
      {
        serialized = "{couldn't serialize}";
      }
      toReturn.Leaves.Add(new TreeNode(serialized));

      return toReturn;
    }
  }
}