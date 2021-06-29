using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;

namespace LearnEXM.Feature.Marketing.Email.Models
{
  public abstract class _baseMarketingEmailViewModel
  {
    public _baseMarketingEmailViewModel()
    {
      try
      {
        var dataSource = RenderingContext.CurrentOrNull.Rendering.DataSource;
        if (!string.IsNullOrEmpty(dataSource))
        {
          DataSource = Sitecore.Context.Database.GetItem(new ID(dataSource));
        }
        else
        {
          Sitecore.Diagnostics.Log.Error(ProjConstants.Diagnostics.Prefix + "null or empty datasource", this);
        }
      }
      catch (Exception ex)
      {
        Sitecore.Diagnostics.Log.Error(ProjConstants.Diagnostics.Prefix + GetType().Name, ex, this);
      }
    }

    public Item DataSource { get; set; }
  }
}