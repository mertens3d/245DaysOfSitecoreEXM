using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels
{
  public class SitecoreInfoModelBuilder
  {
    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder(CollectionConst.SitecoreCinema.CollectionModelNames.CinemaInfo, new XdbModelVersion(1, 1));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, CinemaInfo>(CollectionConst.FacetKeys.CinemaInfo);

      return modelBuilder.BuildModel();
    }

  }
}