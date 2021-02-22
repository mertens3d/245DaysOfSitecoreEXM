using Sitecore.XConnect;
using Sitecore.XConnect.Schema;

namespace Shared.Models.SitecoreCinema.Collection
{
  public class SitecoreCinemaModel
  {
    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder("SitecoreCinemaModel", new XdbModelVersion(1, 0));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, CinemaVisitorInfo>(Const.FacetKeys.CinemaVisitorInfo);
      modelBuilder.DefineFacet<Interaction, CinemaInfo>(Const.FacetKeys.CinemaInfo);
      modelBuilder.DefineEventType<WatchMovie>(false);
      modelBuilder.DefineEventType<BuyConcessions>(false);
      modelBuilder.DefineEventType<UseSelfService>(false);

      return modelBuilder.BuildModel();
    }
  }
}