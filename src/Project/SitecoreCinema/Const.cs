using System;

namespace SitecoreCinema
{
  public struct Const
  {
    public struct EventId
    {
      public static Guid BuyConcension = new Guid("5A33A52E-E6EB-49F0-9F62-6F0381F1FA32");
      public static Guid UseSelfService = new Guid("1BCD1800-2376-4725-A5FB-7182550F4FCB");

    }
    public struct FacetKeys
    {
      public const string CinemaInfo = "CinemaInfo";
      public const string CinemaVisitorInfo = "CinemaVisitorInfo";
      //public const string DefaultFacetKey = "DefaultFacetKey";
    }
  }
}