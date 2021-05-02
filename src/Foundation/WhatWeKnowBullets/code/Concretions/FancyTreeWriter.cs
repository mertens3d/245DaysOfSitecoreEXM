using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using Newtonsoft.Json;
using Sitecore.Data.Query;
using System.Text;

namespace LearnEXM.Foundation.WhatWeKnowTree.Concretions
{
  public class ULWriterForWeKnowTree {

    public ULWriterForWeKnowTree(IWeKnowTreeNode root)
    {
      Root = root;
    }

    public IWeKnowTreeNode Root { get; }
  }
  public class FancyTreeWriter : IWhatWeKnowTreeWriter
  {
    private IWeKnowTreeNode Root;

  
    public FancyTreeWriter(IWeKnowTreeNode root)
    {
      Root = root;
    }

    public string DrawDataForTree()
    {
      var toReturn = new StringBuilder();

      toReturn.Append("<script>");
      toReturn.Append("window.fancyTreeData = [");

      var serializerSettings = new JsonSerializerSettings
      {
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented
      };

      var serialized = JsonConvert.SerializeObject(new FancyTreeNode(Root), serializerSettings);
      toReturn.Append(serialized);

      toReturn.Append("];");
      toReturn.Append("</script>");

      return toReturn.ToString();
    }
  }
}