using Shared.Models.SitecoreCinema;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
  public class KnownDataXConnect
  {
    public Guid? ContactId { get; set; }
    public List<ContactIdentifier> Identifiers { get; set; }
    public List<InteractionProxy> KnownInteractions { get;  set; }
    public bool IsKnown { get; set; }
    
    public PersonalInformation PersonalInformationDetails { get; set; }
    public string UserId { get; set; }
    public CinemaVisitorInfo VisitorInfoMovie { get; set; }
    public string ContactIdAsString (){
      string toReturn = "{unknown}";
      if(ContactId != null)
      {
        toReturn = ContactId.ToString();
      }

      return toReturn;
    }
  }
}