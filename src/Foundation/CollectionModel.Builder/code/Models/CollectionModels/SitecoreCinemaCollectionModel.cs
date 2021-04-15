using LearnEXM.Foundation.CollectionModel.Builder.Models.Events;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Facets;
using LearnEXM.Foundation.CollectionModel.Builder.Models.Outcomes;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;

namespace LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels
{
  public class SitecoreCinemaCollectionModel
  {
    public static XdbModel Model { get; set; } = BuildModel();

    private static XdbModel BuildModel()
    {
      XdbModelBuilder modelBuilder = new XdbModelBuilder(CollectionConst.SitecoreCinema.CollectionModelNames.SitecoreCinemaCollectionModel, new XdbModelVersion(1, 1));

      modelBuilder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
      modelBuilder.DefineFacet<Contact, CinemaVisitorInfo>(CollectionConst.FacetKeys.CinemaVisitorInfo);
      //modelBuilder.DefineFacet<Interaction, CinemaInfo>(CollectionConst.FacetKeys.CinemaInfo);
      modelBuilder.DefineEventType<WatchMovieOutcome>(false);
      modelBuilder.DefineEventType<BuyConcessionOutcome>(false);
      modelBuilder.DefineEventType<UseSelfServiceEvent>(false);

      return modelBuilder.BuildModel();
    }
  }
}