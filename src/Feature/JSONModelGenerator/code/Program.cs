﻿using LearnEXM.Foundation.CollectionModel.Builder;
using LearnEXM.Foundation.CollectionModel.Builder.Models.CollectionModels;
using Sitecore.XConnect.Schema;
using System;
using System.IO;
using System.Linq;

namespace LearnEXM.Feature.JSONModelGenerator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      GenericGenerator(SitecoreCinemaCollectionModel.Model, CollectionConst.SitecoreCinema.CollectionModelNames.SitecoreCinemaCollectionModel);
      GenericGenerator(CinemaDetailsCollectionModel.Model, CollectionConst.SitecoreCinema.CollectionModelNames.CinemaDetailsCollectionModel);
      GenericGenerator(MarketingCollectionModel.Model, CollectionConst.SitecoreCinema.CollectionModelNames.MarketingCollectionModel);
      GenericGenerator(CinemaInfoCollectionModel.Model, CollectionConst.SitecoreCinema.CollectionModelNames.CinemaInfoCollectionModel);
    }

    private static DirectoryInfo GetAutoBuildFolder()
    {
      DirectoryInfo toReturn = null;

      try
      {
        var binFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        var projFolder = binFolder.Parent.Parent;
        toReturn = projFolder.GetDirectories("AutoGenerated").FirstOrDefault();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

      return toReturn;
    }

    private static void GenericGenerator(XdbModel model, string collectionModelName)
    {
      System.Console.WriteLine("Generating your model: " + collectionModelName);

      var targetDirectory = GetAutoBuildFolder();
      if (targetDirectory != null)
      {
        var serializedModel = Sitecore.XConnect.Serialization.XdbModelWriter.Serialize(model);

        var jsonFilePrefix = "";// do not change the file name per https://doc.sitecore.com/developers/93/sitecore-experience-platform/en/deploy-a-custom-model.html "LearnEXM.";

        var jsonFileName = targetDirectory.FullName + "\\" + jsonFilePrefix +  model.FullName + ".json";

        File.WriteAllText(jsonFileName, serializedModel);

        YourModelIsHere(jsonFileName);
      }
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