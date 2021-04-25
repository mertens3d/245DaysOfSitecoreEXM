using LearnEXM.Foundation.WhatWeKnowBullets.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace LearnEXM.Foundation.WhatWeKnowBullets.Concretions
{
  public class FancyTreeWriter : IWhatWeKnowTreeWriter
  {
    private ITreeNode Root;

    public FancyTreeWriter(ITreeNode root)
    {
      this.Root = root;
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