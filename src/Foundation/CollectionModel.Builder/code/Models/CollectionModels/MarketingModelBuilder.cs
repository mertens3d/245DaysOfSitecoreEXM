using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;

using Foundation.Marketing;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels
{
  public class MarketingModelBuilder
  {

    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder(MarketingConst.Models.MarketingFacetModel, new XdbModelVersion(1, 1));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, LearnEXM.Foundation.CollectionModel.Builder.Models.Facets.Marketing>(MarketingConst.FacetKeys.Marketing);

      return modelBuilder.BuildModel();
    }
  }
}