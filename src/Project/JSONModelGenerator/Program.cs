using System.IO;

namespace JSONModelGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      System.Console.WriteLine("Generating your model...");
      var model = Shared.Models.SitecoreCinema.Collection.SitecoreCinemaModel.Model;

      var serializedModel = Sitecore.XConnect.Serialization.XdbModelWriter.Serialize(model);

      var jsonFileName = "c:\\temp\\" + model.FullName + ".json";

      File.WriteAllText(jsonFileName, serializedModel);

      System.Console.WriteLine("Press any key to continue. Your model is here: " + jsonFileName);

      System.Console.ReadKey();
    }
  }
}