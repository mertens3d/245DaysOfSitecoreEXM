using Feature.GatedContent.Models.Logging;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Feature.GatedContent.Extensions
{
    public static class ItemExtensions
    {
        public static bool SafeCheckboxValue(this Item item, ID fieldId)
        {
            var toReturn = false;
            if (item != null && !fieldId.IsNull)
            {
                CheckboxField checkboxField = item.Fields[fieldId];
                if (checkboxField != null)
                {
                    toReturn = checkboxField.Checked;
                }
            }
            else
            {
                if (item == null)
                {
                    GatedContentLogger.Error("item is null", new object());
                }

                if (fieldId.IsNull)
                {
                    GatedContentLogger.Error("fieldId is null", new object());
                }
            }
            return toReturn;
        }

        public static string SafeLinkFieldFriendlyUrl(this Item item, ID fieldId)
        {
            var toReturn = string.Empty;
            if (item != null && !fieldId.IsNull)
            {
                LinkField linkField = item.Fields[fieldId];
                if (linkField != null)
                {
                    toReturn = linkField.GetFriendlyUrl();
                }
            }
            else
            {
                if (item == null)
                {
                    GatedContentLogger.Error("item is null", new object());
                }
                if (fieldId.IsNull)
                {
                    GatedContentLogger.Error("fieldId is null", new object());
                }
            }

            return toReturn;
        }

        public static string SafeStringGet(this Item item, ID fieldId)
        {
            var toReturn = string.Empty;

            if (item != null && !fieldId.IsNull)
            {
                toReturn = item[fieldId];
            }
            else
            {
                if (item == null)
                {
                    GatedContentLogger.Error("item is null", new object());
                }

                if (fieldId.IsNull)
                {
                    GatedContentLogger.Error("fieldId is null", new object());
                }
            }

            return toReturn;
        }
    }
}
