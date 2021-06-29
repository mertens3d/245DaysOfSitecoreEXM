using Feature.GatedContent.Extensions;
using Newtonsoft.Json;
using Sitecore.Data.Items;

namespace Feature.GatedContent.Models.Settings
{
    public class GatedFormGlobalSettings
    {
        public GatedFormGlobalSettings()
        {
            // DI
        }

        public GatedFormGlobalSettings(Item item)
        {
            this.SettingItem = item;
        }

        public string EEComponentHelp { get { return SettingItem.SafeStringGet(FormsConstants.Templates.GlobalGatedFormSettings.Fields.EEMessages.ComponentHelp); } }
        public bool IsValidForRendering { get { return SettingItem != null; } }
        public bool RemoveComments { get { return SettingItem.SafeCheckboxValue(FormsConstants.Templates.CaptchaV3Environment.Fields.Options.RemoveComments); } }
        public bool RemoveMeta { get { return SettingItem.SafeCheckboxValue(FormsConstants.Templates.CaptchaV3Environment.Fields.Options.RemoveMeta); } }

        [JsonIgnore]
        public Item SettingItem { get; }

        public bool TestingForceNuaireAPITimeout { get { return SettingItem.SafeCheckboxValue(FormsConstants.Templates.GatedForm.Fields.Testing.ForceNuaireAPITimeout); } }
    }
}
