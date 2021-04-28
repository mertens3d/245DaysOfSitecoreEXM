using LearnEXM.Foundation.Marketing;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels
{
  public class CinemaBusinessMarketingCollectionModel
  {
    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder(CollectionConst.SitecoreCinema.CollectionModelNames.CinemaBusinessMarketingCollectionModel, new XdbModelVersion(1, 1));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, Facets.CinemaBusinessMarketing>(MarketingConst.FacetKeys.CinemaBusinessMarketing);

      return modelBuilder.BuildModel();
    }
  }
}