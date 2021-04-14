﻿using LearnEXM.Feature.Marketing.Email.Extensions;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Services;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Xml.Xsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnEXM.Feature.Marketing.Email.Extensions
{
  public static class ItemExtensions
  {
    public static string Url(this Item item, UrlOptions options = null)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      if (options != null)
        return LinkManager.GetItemUrl(item, options);
      return !item.Paths.IsMediaItem ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
    }

    public static string ImageUrl(this Item item, ID imageFieldId, MediaUrlOptions options = null)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var imageField = (ImageField)item.Fields[imageFieldId];
      return imageField?.MediaItem == null ? string.Empty : imageField.ImageUrl(options);
    }

    public static Item TargetItem(this Item item, ID linkFieldId)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      if (item.Fields[linkFieldId] == null || !item.Fields[linkFieldId].HasValue)
        return null;
      return ((LinkField)item.Fields[linkFieldId]).TargetItem ?? ((ReferenceField)item.Fields[linkFieldId]).TargetItem;
    }

    public static string MediaUrl(this Item item, ID mediaFieldId, MediaUrlOptions options = null)
    {
      var targetItem = item.TargetItem(mediaFieldId);
      return targetItem == null ? string.Empty : (MediaManager.GetMediaUrl(targetItem) ?? string.Empty);
    }

    public static bool IsImage(this Item item)
    {
      return new MediaItem(item).MimeType.StartsWith("image/", StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IsVideo(this Item item)
    {
      return new MediaItem(item).MimeType.StartsWith("video/", StringComparison.InvariantCultureIgnoreCase);
    }

    public static Item GetAncestorOrSelfOfTemplate(this Item item, ID templateID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      return item.IsDerived(templateID) ? item : item.Axes.GetAncestors().LastOrDefault(i => i.IsDerived(templateID));
    }

    public static IList<Item> GetAncestorsAndSelfOfTemplate(this Item item, ID templateID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var returnValue = new List<Item>();
      if (item.IsDerived(templateID))
      {
        returnValue.Add(item);
      }

      returnValue.AddRange(item.Axes.GetAncestors().Where(i => i.IsDerived(templateID)));
      return returnValue;
    }

    public static string LinkFieldUrl(this Item item, ID fieldID)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }
      if (ID.IsNullOrEmpty(fieldID))
      {
        throw new ArgumentNullException(nameof(fieldID));
      }
      var field = item.Fields[fieldID];
      if (field == null)
      {
        return string.Empty;
      }
      var linkUrl = new LinkUrl();
      return linkUrl.GetUrl(item, fieldID.ToString());
    }

    public static string LinkFieldTarget(this Item item, ID fieldID)
    {
      return item.LinkFieldOptions(fieldID, LinkFieldOption.Target);
    }

    public static string LinkFieldClass(this Item item, ID fieldID)
    {
      return item.LinkFieldOptions(fieldID, LinkFieldOption.Class);
    }

    public static string LinkFieldOptions(this Item item, ID fieldID, LinkFieldOption option)
    {
      XmlField field = item.Fields[fieldID];
      switch (option)
      {
        case LinkFieldOption.Text:
          return field?.GetAttribute("text");

        case LinkFieldOption.LinkType:
          return field?.GetAttribute("linktype");

        case LinkFieldOption.Class:
          return field?.GetAttribute("class");

        case LinkFieldOption.Alt:
          return field?.GetAttribute("title");

        case LinkFieldOption.Target:
          return field?.GetAttribute("target");

        case LinkFieldOption.QueryString:
          return field?.GetAttribute("querystring");

        default:
          throw new ArgumentOutOfRangeException(nameof(option), option, null);
      }
    }

    public static bool HasLayout(this Item item)
    {
      return item?.Visualization?.Layout != null;
    }

    public static bool IsDerived(this Item item, ID templateId)
    {
      if (item == null)
      {
        return false;
      }

      return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
    }

    private static bool IsDerived(this Item item, Item templateItem)
    {
      if (item == null)
      {
        return false;
      }

      if (templateItem == null)
      {
        return false;
      }

      var itemTemplate = TemplateManager.GetTemplate(item);
      return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
    }

    public static bool FieldHasValue(this Item item, ID fieldID)
    {
      return item.Fields[fieldID] != null && !string.IsNullOrWhiteSpace(item.Fields[fieldID].Value);
    }

    public static int? GetInteger(this Item item, ID fieldId)
    {
      int result;
      return !int.TryParse(item.Fields[fieldId].Value, out result) ? new int?() : result;
    }

    public static IEnumerable<Item> GetMultiListValueItems(this Item item, ID fieldId)
    {
      return new MultilistField(item.Fields[fieldId]).GetItems();
    }

    public static bool HasContextLanguage(this Item item)
    {
      var latestVersion = item.Versions.GetLatestVersion();
      return latestVersion?.Versions.Count > 0;
    }

    public static HtmlString Field(this Item item, ID fieldId)
    {
      Assert.IsNotNull(item, "Item cannot be null");
      Assert.IsNotNull(fieldId, "FieldId cannot be null");
      return new HtmlString(FieldRendererService.RenderField(item, fieldId));
    }
  }

  public enum LinkFieldOption
  {
    Text,
    LinkType,
    Class,
    Alt,
    Target,
    QueryString
  }
}

namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Extensions;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Pipelines;
  using Sitecore.Pipelines.RenderField;
  using Sitecore.Web.UI.WebControls;
  using System;
  using System.Collections.Generic;
  using System.Web;

  public class FieldRendererService
  {
    [ThreadStatic]
    private static Stack<string> _endFieldStack;

    private static Stack<string> EndFieldStack => _endFieldStack ?? (_endFieldStack = new Stack<string>());

    private static Item CurrentItem
    {
      get
      {
        var currentRendering = CurrentRendering;
        return currentRendering == null ? PageContext.Current.Item : currentRendering.Item;
      }
    }

    private static Mvc.Presentation.Rendering CurrentRendering
    {
      get
      {
        return RenderingContext.CurrentOrNull.ValueOrDefault(context => context.Rendering);
      }
    }

    public static string RenderField(Item item, string fieldName)
    {
      return FieldRenderer.Render(item, fieldName);
    }

    public static string RenderField(Item item, ID fieldId)
    {
      var field = item.Fields[fieldId];
      Assert.IsNotNull(field, "Field with id: " + fieldId + " is null on item " + item.Name);
      return FieldRenderer.Render(item, field.Name);
    }

    public static HtmlString BeginField(ID fieldId, Item item, object parameters)
    {
      Assert.ArgumentNotNull(fieldId, nameof(fieldId));
      var renderFieldArgs = new RenderFieldArgs
      {
        Item = item,
        FieldName = item.Fields[fieldId].Name
      };
      if (parameters != null)
      {
        CopyProperties(parameters, renderFieldArgs);
        CopyProperties(parameters, renderFieldArgs.Parameters);
      }
      renderFieldArgs.Item = renderFieldArgs.Item ?? CurrentItem;

      if (renderFieldArgs.Item == null)
      {
        EndFieldStack.Push(string.Empty);
        return new HtmlString(string.Empty);
      }
      CorePipeline.Run("renderField", renderFieldArgs);
      var result1 = renderFieldArgs.Result;
      var str = result1.ValueOrDefault(result => result.FirstPart).OrEmpty();
      EndFieldStack.Push(result1.ValueOrDefault(result => result.LastPart).OrEmpty());
      return new HtmlString(str);
    }

    private static void CopyProperties(object source, object target)
    {
      var type = target.GetType();
      foreach (var info in source.GetType().GetProperties())
      {
        var property = type.GetProperty(info.Name);
        if ((property != null) && info.PropertyType.IsAssignableTo(property.PropertyType))
        {
          property.SetValue(target, info.GetValue(source, null), null);
        }
      }
    }

    public static HtmlString EndField()
    {
      var endFieldStack = EndFieldStack;
      if (endFieldStack.Count == 0)
      {
        throw new InvalidOperationException("There was a call to EndField with no corresponding call to BeginField");
      }
      return new HtmlString(endFieldStack.Pop());
    }
  }
}