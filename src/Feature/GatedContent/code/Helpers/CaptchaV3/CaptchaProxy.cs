using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Feature.GatedContent.Helpers.CaptchaV3
{
    public class CaptchaProxy
    {
        public CaptchaProxy(string gREcpatchaResponse, CaptchaV3EnvironmentSettings captchaV3Environment, CaptchaTestSettings captchaTestSettings)
        {
            GRecaptchaResponse = gREcpatchaResponse;
            CaptchaV3Environment = captchaV3Environment;
            CaptchaTestSettings = captchaTestSettings;
        }

        private CaptchaV3EnvironmentSettings CaptchaV3Environment { get; set; }
        private CaptchaTestSettings CaptchaTestSettings { get; }
        private string GRecaptchaResponse { get; }

        public async Task<bool> IsCaptchaValid()
        {
            var toReturn = false;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                {
                    {"secret",  CaptchaV3Environment.SecretKey},
                    {"response", GRecaptchaResponse },
                    {"remoteip", HttpContext.Current.Request.UserHostAddress }
                };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await httpClient.PostAsync(FormsConstants.RecaptchaV3.SiteVerifyEndPoint, content);

                    var captchaResponseStr = await verify.Content.ReadAsStringAsync();

                    GatedContentLogger.Log.Info("Captcha Response:");
                    if (!string.IsNullOrEmpty(captchaResponseStr))
                    {
                        GatedContentLogger.Log.Info(captchaResponseStr);
                    }

                    var captchaResult = JsonConvert.DeserializeObject<RecapatchaResponseViewModel>(captchaResponseStr);

                    toReturn = captchaResult.Success
                        // todo - put back in?  && captchaResult.Action == "todo contact_us"
                        && captchaResult.Score > CaptchaV3Environment.MinPassScore;
                }
            }
            catch (System.Exception ex)
            {
                GatedContentLogger.Error(ex.ToString(), this);
            }

            if (CaptchaTestSettings.TestingForceCaptchaFail)
            {
                toReturn = false;
                GatedContentLogger.Log.Info("Return value has been forced to false from settings");
            }
            else
            if (CaptchaTestSettings.TestingForceCaptchaSuccess)
            {
                toReturn = true;
                GatedContentLogger.Log.Info("Return value has been forced to true from settings");
            }

            return toReturn;
        }
    }
}
