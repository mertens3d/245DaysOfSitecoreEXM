using Sitecore.Data;

namespace Feature.GatedContent.Models
{
    public struct FormsConstants
    {
        public struct Content
        {
            public struct CaptchaGateSettings
            {
                public static ID GatedFormGlobalSettings = new ID("{7BEA9596-57D5-45AA-B97E-326DD7CC36E9}");
                public static ID SettingsFolderItemId = new ID("{5BBB95DF-83D5-427A-BD96-CCBD06848BA2}");
            }
        }

        public struct Core
        {
            public struct EditFrameButtons
            {
                public static ID GatedFormLinks = new ID("{6633401B-8BE9-4C29-9501-F96CFC6AEFBB}");
                public static ID GatedFormTestSettings = new ID("{7085BBAE-98CB-4A80-A6B7-4F6CD83311F6}");
            }
        }
        public struct EE
        {
            public static string AccordianButtonClass = "ee-accordian-button";
            public static string AccordianPanelClass = "ee-accordian-panel";
            public static string ApiTestResultElemId = "post-test-result";
            public static string ColorFalse = "ee-color-false";
            public static string ColorTrue = "ee-color-true";
            public static string HelpWrapperClass = "ee-help-wrapper";
            public static string ReferToLogFileForAdditionalInformation = "<span style='background-color:yellow'>Refer to the log file for additional detail</span>";
            public static string ScrollableDivFormHtml = "overflow:scroll;height:200px;font-family: 'Courier New', monospace;";
            public static string UlStylingClass = "ee-ul-styling";
        }

        public struct Endpoints
        {
            public struct Nuaire
            {
                public static string ApiController = "GatedFormAPI";
                public static string ApiGateEndpoint = "/api/" + ApiController + "/GatedFormMarkup";
                public static string ApiPostTestRelativeEndpoint = "/api/" + ApiController + "/TestPost";
            }
        }

        public struct FormsHtml
        {
            public struct Attributes
            {
                public static readonly string GateItemIdFieldName = "gate-item-id";
                public static readonly string Hidden = "hidden";
                public static readonly string Method = "method";
                public static readonly string Name = "name";
                public static readonly string Post = "post";
                public static readonly string Value = "value";
            }

            public struct Tags
            {
                public static readonly string Input = "input";
                public static readonly string Meta = "meta";
            }
        }

        public struct Logger
        {
            public static string LoggerName = "Sitecore.Feature.GatedContent";
        }

        public struct RecaptchaV3
        {
            public static double DefaultPassingScore = 0.5;
            public static string ScriptEndPoint = "https://www.google.com/recaptcha/api.js";
            public static string SiteVerifyEndPoint = "https://www.google.com/recaptcha/api/siteverify";
            public static string TokenElemId = "gate-token-id";
        }

        public struct Templates
        {
            public struct CaptchaV3Environment
            {
                public static ID SettingsTemplateID = new ID("{1E8DA0B4-34AE-46D2-ABB0-CBF6910412D3}");

                public struct Fields
                {
                    public struct Keys
                    {
                        public static readonly ID HostNames = new ID("{20F40980-32E6-40CF-BBDE-D047EDE5C8C5}");
                        public static readonly ID PublicSiteKey = new ID("{EA620E23-4CDC-4321-9655-5B765932CC06}");
                        public static readonly ID SecretKey = new ID("{40A5857D-460F-4A88-BC1D-39E2C5C404CB}");
                    }

                    public struct Options
                    {
                        public static readonly ID MinPassScore = new ID("{2C3A52C8-7B23-43C8-807D-215121C59438}");
                        public static readonly ID RemoveComments = new ID("{A84D054F-E22E-4949-B5C8-722CB668591F}");
                        public static readonly ID RemoveMeta = new ID("{B4D94036-1A27-4F3D-BDCF-70738214ECB7}");
                    }
                }
            }

            public struct GatedForm
            {
                public static readonly ID Id = new ID("{29AF8BE5-3E25-4CCE-9D14-C5E9530EA81F}");

                public struct Fields
                {
                    public struct Form
                    {
                        public static readonly ID FormHtmlCode = new ID("{2E9496D7-C4FB-4801-8006-87829C2E2162}");
                    }

                    public struct Redirects
                    {
                        public static readonly ID OnCaptchaFailRedirect = new ID("{239001F3-B987-40B0-A154-A9C52EA393A2}");
                        public static readonly ID OnFormSubmitFailRedirect = new ID("{85AB8D9E-9A7F-4747-9F6D-51DBD0C4BB06}");
                        public static readonly ID OnFormSubmitSuccessRedirect = new ID("{0D0A9F5D-C420-4A76-B630-61C0461D7B28}");
                    }

                    public struct Testing
                    {
                        public static readonly ID ForceCaptchaFail = new ID("{D6FA18F7-76BB-414A-9865-03BB23C06B42}");
                        public static readonly ID ForceCaptchaSuccess = new ID("{1C8AEAED-D89A-4FB3-AA5E-ECD71872952D}");
                        public static readonly ID ForceFormSubmitError = new ID("{75965B0D-8E9B-4469-8CDF-9AF8E4C9A91A}");
                        public static readonly ID ForceInvalidFormMarkup = new ID("{5E4F6F13-FA04-4629-A6E0-747AFEE9331B}");
                        public static readonly ID ForceNuaireAPITimeout = new ID("{3A9566DC-024D-48E7-8018-634C87D89EB8}");
                        public static readonly ID ShowEEDebugData = new ID("{0B98D1DC-D104-496F-9B3E-117EF9D7355C}");
                    }
                }
            }

            public struct GlobalGatedFormSettings
            {
                public static readonly ID Id = new ID("{EA4485CA-01A3-4D18-AAAC-95F5D42527A9}");

                public struct Fields
                {
                    public struct EEMessages
                    {
                        public static readonly ID ComponentHelp = new ID("{E04DAC34-F684-4026-A485-5DE22FF2B1E8}");
                        public static readonly ID FormWillNotEnterWithError = new ID("{377AA915-DE2E-497E-9C66-F0B57B814AAF}");
                        public static readonly ID WarningGlobalItem = new ID("{2B0DE363-0F16-45B4-B327-3F8251C2831F}");
                    }

                    public struct ProdMessages
                    {
                        public static readonly ID UnableToRenderForm = new ID("{BED52DF1-3965-473C-9E07-66D1EC1BF998}");
                    }
                }
            }
        }

        public struct Views
        {
            public struct Css
            {
                public static readonly string DefaultRenderingCssClass = "plain-html-gated";
            }

            public struct Partials
            {
                public static readonly string _base = "/views/GatedFormRendering/_partials";
                public static readonly string EeAccordianScript = _base + "/_eeAccordianScript.cshtml";
                public static readonly string EeComponentHelp = _base + "/_eeComponentHelp.cshtml";
                public static readonly string EeEnvironmentSettings = _base + "/_eeEnvironmentSettings.cshtml";
                public static readonly string EeFormMarkupValidation = _base + "/_eeFormMarkupValidation.cshtml";
                public static readonly string EeRedirectsValidation = _base + "/_eeRedirectsValidation.cshtml";
                public static readonly string EeTestingSwitches = _base + "/_eeTestingSwitches.cshtml";
                public static readonly string EeTestNuaireAPIEndPoint = _base + "/_eeTestNuaireAPIEndpoint.cshtml";
                public static readonly string RecaptchaScript = _base + "/_recpatchaV3Script.cshtml";
            }
        }
    }
}
