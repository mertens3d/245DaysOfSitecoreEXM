using LearnEXM.Foundation.Marketing;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels
{
  public class MarketingCollectionModel
  {
    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder(CollectionConst.SitecoreCinema.CollectionModelNames.MarketingCollectionModel, new XdbModelVersion(1, 1));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, LearnEXM.Foundation.CollectionModel.Builder.Models.Facets.Marketing>(MarketingConst.FacetKeys.Marketing);

      return modelBuilder.BuildModel();
    }
  }
}