using Feature.GatedContent.Helpers.CaptchaV3;
using Feature.GatedContent.Models.Logging;
using Newtonsoft.Json;

namespace Feature.GatedContent.Models.Settings
{
    public class CaptchaGateSettings : ICaptchaGatedFormSettings
    {
        public CaptchaGateSettings()
        {
            var gatedFormGlobalSettingsItem = Sitecore.Context.Database.GetItem(FormsConstants.Content.CaptchaGateSettings.GatedFormGlobalSettings);
            GatedFormGlobalSettings = new GatedFormGlobalSettings(gatedFormGlobalSettingsItem);
            var EnvironmentSettingsHelper = new EnvironmentSettingsHelper();
            CaptchaV3EnvironmentSettings = EnvironmentSettingsHelper.GetEnvironmentSettings();

            if (GatedContentLogger.Log.IsDebugEnabled)
            {
                LogSettings();
            }
        }

        public CaptchaV3EnvironmentSettings CaptchaV3EnvironmentSettings { get; }
        public GatedFormGlobalSettings GatedFormGlobalSettings { get; }

        public bool IsValidForRendering { get { return CaptchaV3EnvironmentSettings != null  && CaptchaV3EnvironmentSettings.IsValidForRendering
                                                        && GatedFormGlobalSettings != null && GatedFormGlobalSettings.IsValidForRendering; } }

        private void LogSettings()
        {
            try
            {
                var recaptchaSettingsSerialized = JsonConvert.SerializeObject(CaptchaV3EnvironmentSettings, Formatting.Indented);
                GatedContentLogger.Log.Debug("Environment: " + recaptchaSettingsSerialized);

                var globalSerialized = JsonConvert.SerializeObject(GatedFormGlobalSettings, Formatting.Indented);
                GatedContentLogger.Log.Debug("Global: " + globalSerialized);
            }
            catch (System.Exception ex)
            {
                GatedContentLogger.Error(ex.ToString(), this);
            }
        }
    }
}
