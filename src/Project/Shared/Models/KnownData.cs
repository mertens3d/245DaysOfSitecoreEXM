using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using SitecoreCinema.Model.Collection;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
  public class KnownData
  {
    public CinemaVisitorInfo movie { get; internal set; }
    public PersonalInformation details { get; internal set; }
    public Guid? Id { get; internal set; }
    public IReadOnlyCollection<Interaction> Interactions { get; internal set; }
  }
}