using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Settings;
using Forms.Repositories;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;

namespace Feature.GatedContent.DI
{
    internal class RecaptchaRepository : VariantsRepository, ICaptchaGateRepository
    {
        private ICaptchaGatedFormSettings _captchaGateSettings;

        public RecaptchaRepository(ICaptchaGatedFormSettings captchaGateSettings)
        {
            _captchaGateSettings = captchaGateSettings;
        }

        private GatedFormDataModel DataModel { get; set; }

        public override IRenderingModelBase GetModel()
        {
            DataModel = new GatedFormDataModel(_captchaGateSettings, Rendering.DataSourceItem);
            DataModel.DataSourceItem = Rendering.DataSourceItem;

            FillBaseProperties(DataModel);

            return DataModel;
        }
    }
}
