using LearnEXM.Foundation.WhatWeKnowTree;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WeKnowTree.Helpers
{
  public class WeKnowTreeOptionsFactory
  {
    public WeKnowTreeOptions GetWeKnowTreeOptions(Item optionsItem)
    {
      WeKnowTreeOptions WeKnowTreeOptions = new WeKnowTreeOptions();

      if (optionsItem != null && optionsItem.TemplateID.Equals(ProjConstants.Items.Templates.WeKnowTreeOptions.Root))
      {


        WeKnowTreeOptions.Interactions.IncludeInteractionEvents = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.IncludeInteractionEvents);
        WeKnowTreeOptions.Interactions.IncludeInteractions = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.IncludeInteractions);
        WeKnowTreeOptions.Interactions.ChannelFilters = GetMultiListFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.ChannelFilter);

        WeKnowTreeOptions.IncludeFacets = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeFacetsField);
        WeKnowTreeOptions.IncludeIdentifiers = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeIdentifiers);
        WeKnowTreeOptions.IncludeLastModified = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeLastModified);
        WeKnowTreeOptions.IncludeNullAndEmptyValueLeaves = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeLeavesWithNullOrEmptyValues);
        WeKnowTreeOptions.IncludeRaw = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeRaw);
        WeKnowTreeOptions.IncludeTrackingContact = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeTrackingContact);
        WeKnowTreeOptions.IncludeTreeSettings = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeTrackingContact);
      }
      return WeKnowTreeOptions;
    }

    public WeKnowTreeOptions GetWeKnowTreeOptions(ID id)
    {
      WeKnowTreeOptions toReturn = null;
      if (!id.IsNull)
      {
        var item = Sitecore.Context.Database.GetItem(id);

        toReturn = GetWeKnowTreeOptions(item);
      }

      return toReturn;
    }

    private bool GetBooleanFieldValue(Item item, ID fieldId)
    {
      var toReturn = false;

      if (item != null)
      {
        toReturn = item[fieldId] == "1";
      }

      return toReturn;
    }

    private List<Item> GetMultiListFieldValue(Item item, ID fieldId)
    {
      var toReturn = new List<Item>();

      if (item != null)
      {
        Sitecore.Data.Fields.MultilistField multilistfield = item.Fields[fieldId];
        if (multilistfield != null)
        {
          var items = multilistfield.GetItems();
          if (items != null)
          {
            toReturn.AddRange(items);
          }
        }
      }

      return toReturn;
    }
  }
}