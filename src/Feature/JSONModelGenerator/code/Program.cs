using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels;
using Sitecore.XConnect.Schema;
using System.IO;

namespace LearnEXM.Feature.JSONModelGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      GenericGenerator(SitecoreCinemaModel.Model, CollectionConst.SitecoreCinema.CollectionModelNames.SitecoreCinemaModel);
      GenericGenerator(CinemaDetailsCollection.Model, CollectionConst.SitecoreCinema.CollectionModelNames.CinemaDetailsCollection);
    }

    private static void GenericGenerator(XdbModel model, string collectionModelName)
    {
      System.Console.WriteLine("Generating your model: " + collectionModelName);

      var serializedModel = Sitecore.XConnect.Serialization.XdbModelWriter.Serialize(model);

      var jsonFileName = "c:\\temp\\" + model.FullName + ".json";

      File.WriteAllText(jsonFileName, serializedModel);

      YourModelIsHere(jsonFileName);
    }

    private static void YourModelIsHere(string jsonFullFileName)
    {
      System.Console.WriteLine("Your model is here: " + jsonFullFileName);
      System.Console.WriteLine("Copy the model to:");
      System.Console.WriteLine("\t1) {XConnect Webroot}\\App_Data\\App_Data\\Models");
      System.Console.WriteLine("\t2) {XConnect Webroot}\\App_Data\\jobs\\continuous\\IndexWorker\\App_Data\\Models\\");

      System.Console.WriteLine("\t3) if Sitecore 9.0 Update 2 or later");
      System.Console.WriteLine("\t\t{marketing automation operations Webroot}\\App_Data\\Models\\");
      System.Console.WriteLine("Press any key to continue.");
      System.Console.ReadKey();
    }
  }
}