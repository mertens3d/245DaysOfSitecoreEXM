using Feature.GatedContent.Extensions;
using Feature.GatedContent.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Feature.GatedContent.Proxies
{
    public class DataSourceItemProxy
    {
        private readonly Item DataSourceItem;

        public DataSourceItemProxy(Item dataSourceItem)
        {
            DataSourceItem = dataSourceItem;
            CommonProcessItem();

            TestSettings = new TestingSettings(DataSourceItem);
        }

        public string FormMarkup { get; private set; }
        public bool HasValidRedirects { get { return HasCaptchaFailUrl && HasOnFormSubmitFailUrl && HasOnFormSubmitSuccessUrl; } }
        public bool HasCaptchaFailUrl { get { return !string.IsNullOrEmpty(OnCaptchaFailUrl.GetFriendlyUrl()); } }
        public bool HasOnFormSubmitFailUrl { get { return !string.IsNullOrEmpty(OnFormSubmitFailUrlStr); } }
        public bool HasOnFormSubmitSuccessUrl { get { return !string.IsNullOrEmpty(OnFormSubmitSuccess); } }

        public string ID { get { return DataSourceItem?.ID.ToString(); } }

        public bool IsValidForRendering
        {
            get
            {
                return DataSourceItem != null
                    && HasCaptchaFailUrl
                    && HasOnFormSubmitFailUrl
                    && HasOnFormSubmitSuccessUrl;
            }
        }

        public string OnCaptchFailUrlFriendly { get { return HasCaptchaFailUrl ? OnCaptchaFailUrl.GetFriendlyUrl() : string.Empty; } }
        public string OnFormSubmitFailUrlStr { get { return DataSourceItem.SafeLinkFieldFriendlyUrl(FormsConstants.Templates.GatedForm.Fields.Redirects.OnFormSubmitFailRedirect); } }
        public string OnFormSubmitSuccess { get { return DataSourceItem.SafeLinkFieldFriendlyUrl(FormsConstants.Templates.GatedForm.Fields.Redirects.OnFormSubmitSuccessRedirect); } }

        public TestingSettings TestSettings { get; }
        private LinkField OnCaptchaFailUrl { get; set; }

        private void CommonProcessItem()
        {
            if (DataSourceItem != null)
            {
                ProcessFormMarkupField();

                ProcessCaptchaFailLinkField();
            }
        }

        private void ProcessCaptchaFailLinkField()
        {
            OnCaptchaFailUrl = DataSourceItem.Fields[FormsConstants.Templates.GatedForm.Fields.Redirects.OnCaptchaFailRedirect];
        }

        private void ProcessFormMarkupField()
        {
            FormMarkup = DataSourceItem.Fields[FormsConstants.Templates.GatedForm.Fields.Form.FormHtmlCode]?.Value;
        }
    }
}
