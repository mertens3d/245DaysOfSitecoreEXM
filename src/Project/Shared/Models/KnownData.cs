using Shared.Models.SitecoreCinema;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
  public class KnownData
  {
    public CinemaVisitorInfo movie { get; set; }
    public PersonalInformation details { get;  set; }
    public Guid? Id { get;  set; }
    public string IdAsString (){
      string toReturn = "{unknown}";
      if(Id != null)
      {
        toReturn = Id.ToString();
      }

      return toReturn;
    }
    public IReadOnlyCollection<Interaction> Interactions { get; internal set; }
  }
}