using Feature.GatedContent.Models.Logging;
using Feature.GatedContent.Models.Settings;
using Feature.GatedContent.Proxies;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.Mvc.Models;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.Variants.Abstractions.Models;
using System;

namespace Feature.GatedContent.Models
{
    public class GatedFormDataModel : VariantsRenderingModel, IRenderingModelBase
    {
        public GatedFormDataModel(ICaptchaGatedFormSettings captchaGateSettings, Item dataSourceItem)
        {
            DataSourceItem = dataSourceItem;
            CommonCtor(captchaGateSettings);
        }

        public GatedFormDataModel(ICaptchaGatedFormSettings captchaGateSettings, string guidAsString)
        {
            if (!string.IsNullOrEmpty(guidAsString))
            {
                GatedContentLogger.Log.Debug("DataSourceItemProxy: " + guidAsString);

                Guid guidOut;

                var success = Guid.TryParse(guidAsString, out guidOut);
                if (success)
                {
                    DataSourceItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID(guidOut));
                }
                else
                {
                    Sitecore.Diagnostics.Log.Error("Passed in string cannot be parsed into a GUID: " + guidAsString, this);
                }
            }
            else
            {
                Sitecore.Diagnostics.Log.Error("Empty string passed as GUID", this);
            }

            CommonCtor(captchaGateSettings);
        }

        private void CommonCtor(ICaptchaGatedFormSettings captchaGateSettings)
        {
            CaptchGateSettings = captchaGateSettings;
            DataSourceProxy = new DataSourceItemProxy(DataSourceItem);
        }

        public ICaptchaGatedFormSettings CaptchGateSettings { get; set; }

        internal GatedFormViewModel GetViewModel()
        {
            var toReturn = new GatedFormViewModel(CaptchGateSettings, DataSourceProxy);

            toReturn.DataModel = this;
            return toReturn;
        }

        public DataSourceItemProxy DataSourceProxy { get; set; }
    }
}
