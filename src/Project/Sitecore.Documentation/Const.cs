using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Documentation
{
public struct Const
  {

    public struct Certificate
    {
      public static string CertificateStore = "StoreName=My;StoreLocation=LocalMachine;FindType=FindByThumbprint;FindValue=";
      public static string CertificateThumbprint = "42829cff3fa746441d6faaf8e6af216c0063a685";
    }

    public struct EndPoints
    {
      public static string Odata = "https://LearnEXMxconnect.dev.local/odata";
      public static string Configuration = "https://LearnEXMxconnect.dev.local/configuration";
    }
  }
}
