using LearnEXM.Feature.Marketing.Email.Extensions;
using LearnEXM.Feature.Marketing.Email.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Globalization;

namespace LearnEXM.Feature.Marketing.Email.Controllers
{
  public class EmailMarketingControllerHelper
  {
    public string GetBackgroundColor(string backgroundColorParamValue, string fallback)
    {
      string toReturn = fallback;

      if (!string.IsNullOrEmpty(backgroundColorParamValue))
      {
        Guid itemGuid;
        bool success = Guid.TryParse(backgroundColorParamValue, out itemGuid);
        if (success && itemGuid != Guid.Empty)
        {
          var colorItem = Sitecore.Context.Database.GetItem(new ID(itemGuid));
          if (colorItem != null)
          {
            toReturn = colorItem[Const.Fields.BackgroundColor.ColorHex];
          }
        }
      }

      return toReturn;
    }

    public string GetRenderingParamValue(Item context, string propertyName)
    {
      string toReturn = null;

      if (context != null && !string.IsNullOrEmpty(propertyName))
      {
        toReturn = RenderingContext.Current.Rendering.Parameters[propertyName];
      }

      return toReturn;
    }

    internal string FormatDate(string paramDate, DateTime fallback)
    {
      string toReturn = string.Empty;
      DateTime date;
      if (!string.IsNullOrEmpty(paramDate))
      {
        try
        {
          date = DateTime.ParseExact(paramDate, "yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
          Sitecore.Diagnostics.Log.Error(Const.Diagnostics.Prefix, ex, this);
          date = fallback;
        }
      }
      else
      {
        date = fallback;
      }

      toReturn = date.ToString(Const.Styling.TextFormat.Date);

      return toReturn;
    }

    internal LinkData GetLinkData(Item dataSource, string fieldName, string fallbackValue)
    {
      LinkData toReturn = new LinkData();

      if (dataSource != null && !string.IsNullOrEmpty(fieldName))
      {
        Sitecore.Data.Fields.LinkField linkField = dataSource.Fields[fieldName];

        if (linkField != null)
        {
          try
          {
            toReturn.LinkUrlIsValid = !string.IsNullOrEmpty(linkField.Url);

            toReturn.LinkText = dataSource.LinkFieldOptions(linkField.InnerField.ID, LinkFieldOption.Text);
            if (string.IsNullOrEmpty(toReturn.LinkText))
            {
              toReturn.LinkText = fallbackValue;
            }
          }
          catch (Exception ex)
          {
            Sitecore.Diagnostics.Log.Error(Const.Diagnostics.Prefix, ex, this);
            toReturn.LinkText = fallbackValue;
          }
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Error(Const.Diagnostics.Prefix + " GetLinkText. dataSource is null: " + (dataSource == null), this);
        Sitecore.Diagnostics.Log.Error(Const.Diagnostics.Prefix + " GetLinkText. fieldName is null: " + string.IsNullOrEmpty(fieldName), this);
      }

      return toReturn;
    }
  }
}