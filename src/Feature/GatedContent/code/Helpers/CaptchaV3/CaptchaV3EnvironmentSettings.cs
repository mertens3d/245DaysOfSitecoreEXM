using Feature.GatedContent.Models;
using Newtonsoft.Json;
using Sitecore.Data.Items;

namespace Feature.GatedContent.Helpers.CaptchaV3
{
    public class CaptchaV3EnvironmentSettings
    {
        public CaptchaV3EnvironmentSettings(Item environmentItem, string detectedHost)
        {
            EnvironmentItem = environmentItem;
            DetectedHost = detectedHost;
            ProcessScoreField();
        }

        public bool HasItem { get { return EnvironmentItem != null; } }
        public bool HasHostName { get { return !string.IsNullOrEmpty(DetectedHost); } }
        public bool HasPublicSiteKey { get { return !string.IsNullOrEmpty(PublicSiteKey); } }
        public bool HasSecretKey { get { return !string.IsNullOrEmpty(SecretKey); } }
        public string DetectedHost { get; set; } = string.Empty;

        public bool IsValidForRendering
        {
            get
            {
                return EnvironmentItem != null
                                    && !string.IsNullOrEmpty(SecretKey)
                                    && !string.IsNullOrEmpty(PublicSiteKey);
            }
        }

        public double MinPassScore { get; private set; }

        public string PublicSiteKey
        {
            get
            {
                return EnvironmentItem != null ? EnvironmentItem[FormsConstants.Templates.CaptchaV3Environment.Fields.Keys.PublicSiteKey] : string.Empty;
            }
        }

        [JsonIgnore]
        public string SecretKey
        {
            get
            {
                return EnvironmentItem != null ? EnvironmentItem[FormsConstants.Templates.CaptchaV3Environment.Fields.Keys.SecretKey] : string.Empty;
            }
        }

        public string SecretKeyNote { get; } = "Note: Secret key serialization is disabled";

        //public bool SettingFoundForEnvironement { get { return EnvironmentItem != null; } }
        public string SettingItemId { get { return EnvironmentItem != null ? EnvironmentItem.ID.ToString() : string.Empty; } }

        [JsonIgnore]
        private Item EnvironmentItem { get; set; }

        private void ProcessScoreField()
        {
            if (EnvironmentItem != null)
            {
                var scoreStr = EnvironmentItem.Fields[FormsConstants.Templates.CaptchaV3Environment.Fields.Options.MinPassScore]?.Value;

                if (!string.IsNullOrEmpty(scoreStr))
                {
                    var success = double.TryParse(scoreStr, out double outVal);
                    if (success)
                    {
                        MinPassScore = outVal;
                    }
                    else
                    {
                        Sitecore.Diagnostics.Log.Error(FormsConstants.Templates.CaptchaV3Environment.Fields.Options.MinPassScore.ToString() + "Field does not have a valid number value: " + EnvironmentItem.ID.ToString(), this);
                        Sitecore.Diagnostics.Log.Error("Falling back on default value: " + FormsConstants.RecaptchaV3.DefaultPassingScore, this);
                    };
                }
            }
        }
    }
}
