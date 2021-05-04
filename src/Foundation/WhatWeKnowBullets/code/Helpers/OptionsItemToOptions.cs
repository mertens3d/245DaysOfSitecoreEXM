using Sitecore.Data;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace LearnEXM.Foundation.WhatWeKnowTree.Helpers
{
  public class OptionsItemToOptions
  {
    public WeKnowTreeOptions WeKnowTreeOptions { get; set; } = new WeKnowTreeOptions();

    public OptionsItemToOptions(Item optionsItem)
    {
      if (optionsItem != null && optionsItem.TemplateID.Equals(ProjConstants.Items.Templates.WeKnowTreeOptions.Root))
      {
        WeKnowTreeOptions.IncludeFacets = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeFacetsField);
        WeKnowTreeOptions.IncludeIdentifiers = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeIdentifiers);
        WeKnowTreeOptions.Interactions.IncludeInteractionEvents = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.IncludeInteractionEvents);
        WeKnowTreeOptions.Interactions.IncludeInteractions = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.IncludeInteractions);
        WeKnowTreeOptions.Interactions.ChannelFilters = GetMultiListFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.Interactions.ChannelFilter);
        WeKnowTreeOptions.IncludeLastModified = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeLastModified);
        WeKnowTreeOptions.IncludeNullAndEmptyValueLeaves = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeLeavesWithNullOrEmptyValues);
        WeKnowTreeOptions.IncludeRaw = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeRaw);
        WeKnowTreeOptions.IncludeTrackingContact = GetBooleanFieldValue(optionsItem, ProjConstants.Items.Templates.WeKnowTreeOptions.IncludeTrackingContact);
      }
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

    private bool GetBooleanFieldValue(Item item, ID fieldId)
    {
      var toReturn = false;

      if (item != null)
      {
        toReturn = item[fieldId] == "1";
      }

      return toReturn;
    }
  }
}