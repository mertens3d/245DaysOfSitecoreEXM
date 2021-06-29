using Feature.GatedContent.Extensions;
using Feature.GatedContent.Models;
using Sitecore.Data.Items;

namespace Feature.GatedContent.Proxies
{
    public class TestingSettings
    {
        private Item dataSourceItem;

        public TestingSettings(Item dataSourceItem)
        {
            this.dataSourceItem = dataSourceItem;
        }

        public bool IsAnyTestingEnabled
        {
            get
            {
                return
                    TestingForceCaptchaFail
                    || TestingForceCaptchaSuccess
                    || TestingForceFormSubmitFail
                    || TestingForceInvalidFormMarkup;
            }
        }

        public bool TestingForceCaptchaFail { get { return dataSourceItem.SafeCheckboxValue(FormsConstants.Templates.GatedForm.Fields.Testing.ForceCaptchaFail); } }
        public bool TestingForceCaptchaSuccess { get { return dataSourceItem.SafeCheckboxValue(FormsConstants.Templates.GatedForm.Fields.Testing.ForceCaptchaSuccess); } }
        public bool TestingForceFormSubmitFail { get { return dataSourceItem.SafeCheckboxValue(FormsConstants.Templates.GatedForm.Fields.Testing.ForceFormSubmitError); } }
        public bool TestingForceInvalidFormMarkup { get { return dataSourceItem.SafeCheckboxValue(FormsConstants.Templates.GatedForm.Fields.Testing.ForceInvalidFormMarkup); } }
    }
}
