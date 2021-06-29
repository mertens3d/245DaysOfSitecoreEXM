using Feature.GatedContent.Helpers;
using Feature.GatedContent.Models.Settings;
using Feature.GatedContent.Proxies;
using Sitecore.XA.Foundation.Variants.Abstractions.Models;

namespace Feature.GatedContent.Models
{
    public class GatedFormViewModel
    {
        public GatedFormViewModel(ICaptchaGatedFormSettings captchaGatedFormSettings, DataSourceItemProxy dataSourceItemProxy)
        {
            DataSourceItemProxy = dataSourceItemProxy;
            CaptchaGatedFormSettings = captchaGatedFormSettings;
            FormProxy = new FormProxy(DataSourceItemProxy.FormMarkup, dataSourceItemProxy.TestSettings.TestingForceInvalidFormMarkup);

            AddCaptchaGateNodesToFormProxy();
        }

        public bool ModelIsValidForRendering
        {
            get
            {
                return CaptchaGatedFormSettings != null && CaptchaGatedFormSettings.IsValidForRendering
                        && DataModel != null
                        && DataSourceItemProxy != null && DataSourceItemProxy.IsValidForRendering
                        && FormProxy != null && FormProxy.IsValidForRendering;
            }
        }

        public ICaptchaGatedFormSettings CaptchaGatedFormSettings { get; set; }
        public VariantsRenderingModel DataModel { get; set; }
        public DataSourceItemProxy DataSourceItemProxy { get; }
        public FormProxy FormProxy { get; }

        private void AddCaptchaGateNodesToFormProxy()
        {
            FormProxy.AddInputNode(
                FormsConstants.FormsHtml.Attributes.GateItemIdFieldName,
                FormsConstants.FormsHtml.Attributes.Hidden,
                DataSourceItemProxy.ID);

            FormProxy.AddInputNode(
               FormsConstants.RecaptchaV3.TokenElemId,
              FormsConstants.FormsHtml.Attributes.Hidden,
               string.Empty);

            if (CaptchaGatedFormSettings.GatedFormGlobalSettings.RemoveComments)
            {
                FormProxy.RemoveNodesOfTypeComment();
            }

            if (CaptchaGatedFormSettings.GatedFormGlobalSettings.RemoveMeta)
            {
                FormProxy.RemoveMeta();
            }
        }
    }
}
