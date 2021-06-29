using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Logging;
using Forms.Repositories;
using Sitecore.XA.Foundation.Mvc.Controllers;
using Sitecore.XA.Foundation.RenderingVariants.Controllers;
using System.Web.Mvc;

namespace Feature.GatedContent.Controllers
{
    public class GatedFormRenderingController : VariantsController, IController
    {
        private readonly ICaptchaGateRepository _captchaGateRepository;

        public GatedFormRenderingController(ICaptchaGateRepository repository)
        {
            _captchaGateRepository = repository;
        }

        public ActionResult PlainHtmlFormGated()
        {
            ActionResult toReturn = null;
            var dataModel = _captchaGateRepository.GetModel() as GatedFormDataModel;
            if (dataModel != null)
            {
                var viewModel = dataModel.GetViewModel();
                toReturn = View(viewModel);
            }
            else
            {
                GatedContentLogger.Error("Error converting view model", this);
            }
            return toReturn;
        }

       
    }
}
