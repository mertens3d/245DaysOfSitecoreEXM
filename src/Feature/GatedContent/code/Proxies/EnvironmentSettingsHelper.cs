using Feature.GatedContent.Helpers.CaptchaV3;
using Feature.GatedContent.Models.Logging;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feature.GatedContent.Models
{
    public class EnvironmentSettingsHelper
    {
        internal CaptchaV3EnvironmentSettings GetEnvironmentSettings()
        {
            var detectedHost = Sitecore.Context.Site != null ? Sitecore.Context.Site.HostName : "{error - no host detected}";
            var settingItem = GetMatchingSetting(detectedHost);

            CaptchaV3EnvironmentSettings toReturn = null;

            if (settingItem != null)
            {
                toReturn = new CaptchaV3EnvironmentSettings(settingItem, detectedHost);
            }
            else
            {

                GatedContentLogger.Error("Environment setting item is null. Expected a match for '" + detectedHost + "'", this);
                toReturn = new CaptchaV3EnvironmentSettings(null, detectedHost);
            }

            return toReturn;
        }

        private List<Item> GetCandidateSettings()
        {
            var toReturn = new List<Item>();

            var settingsFolder = Sitecore.Context.Database.GetItem(FormsConstants.Content.CaptchaGateSettings.SettingsFolderItemId);
            if (settingsFolder != null)
            {
                toReturn = settingsFolder
                    .Children
                    .Where(x => x.TemplateID.Equals(FormsConstants.Templates.CaptchaV3Environment.SettingsTemplateID))
                    .ToList();
            }
            else
            {
                GatedContentLogger.Error("Settings folder is null. " + FormsConstants.Content.CaptchaGateSettings.SettingsFolderItemId.ToString(), this);
            }

            return toReturn;
        }

        private Item GetMatchingSetting(string hostName)
        {
            Item toReturn = null;
            var foundSettingsItems = GetCandidateSettings();
            if (foundSettingsItems != null && foundSettingsItems.Any())
            {
                foreach (var settingsItem in foundSettingsItems)
                {
                    var validHosts = settingsItem[FormsConstants.Templates.CaptchaV3Environment.Fields.Keys.HostNames]
                        .Split('|')
                        .ToList()
                        .Select(x => x.Trim().ToLower());

                    if (validHosts.Contains(hostName.ToLower()))
                    {
                        toReturn = settingsItem;
                        break;
                    }
                }
            }
            else
            {
                GatedContentLogger.Error("Candidate Settings Not Found", this);
            }

            return toReturn;
        }
    }
}
