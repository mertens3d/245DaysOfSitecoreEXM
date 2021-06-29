using Feature.GatedContent.Helpers;
using Feature.GatedContent.Helpers.CaptchaV3;
using Feature.GatedContent.Helpers.FormRelay;
using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Logging;
using Feature.GatedContent.Models.Settings;
using Sitecore.XA.Foundation.Mvc.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FormCollection = System.Web.Mvc.FormCollection;

namespace Feature.GatedContent.Controllers
{
    public class GatedFormAPIController : StandardController, IController
    {
        public ICaptchaGatedFormSettings CaptchaGateSettings { get; }

        public GatedFormAPIController()
        {
            // for DI
        }

        public GatedFormAPIController(ICaptchaGatedFormSettings captchaGateSettings)
        {
            CaptchaGateSettings = captchaGateSettings;
        }

        [HttpGet]
        public ActionResult TestGet(string testInput)
        {
            return Json("TestGet Received: " + testInput);
        }

        [HttpPost]
        public ActionResult TestPost(FormCollection testInput)
        {
            var dict = testInput.AllKeys
                .ToDictionary(key => key, value => testInput[value]);

            var response = string.Empty;

            foreach (var key in dict.Keys)
            {
                response += key + "=" + dict[key] + ",";
            };

            return Json("TestPost Received: " + response);
        }

        [HttpPost]
        public async Task<ActionResult> GatedFormMarkup(FormCollection formCollection)
        {
            GatedContentLogger.Log.Info("[HttpPost] GatedFormMarkup Hit");

            ActionResult toReturn = null;

            var formRelayer = new FormRelayer(formCollection, ModelState.IsValid);

            var captchaDataModel = new GatedFormDataModel(CaptchaGateSettings, formRelayer.GateItemIdStr);
            var gatedFormViewModel = captchaDataModel.GetViewModel();

            if (formRelayer.DataIsValid && gatedFormViewModel.DataSourceItemProxy.IsValidForRendering)
            {
                formRelayer.TestingFormFormSubmitFail = gatedFormViewModel.DataSourceItemProxy.TestSettings.TestingForceFormSubmitFail;

                var CaptchaTestSettings = new CaptchaTestSettings(
                    gatedFormViewModel.DataSourceItemProxy.TestSettings.TestingForceCaptchaFail,
                    gatedFormViewModel.DataSourceItemProxy.TestSettings.TestingForceCaptchaSuccess
                    );

                var captchaProxy = new CaptchaProxy(
                        formRelayer[FormsConstants.RecaptchaV3.TokenElemId],
                        CaptchaGateSettings.CaptchaV3EnvironmentSettings,
                        CaptchaTestSettings
                        );

                if (await captchaProxy.IsCaptchaValid())
                {
                    toReturn = await GetActionForCaptchaPass(formRelayer, gatedFormViewModel, gatedFormViewModel.FormProxy);
                }
                else
                {
                    toReturn = new RedirectResult(gatedFormViewModel.DataSourceItemProxy.OnCaptchFailUrlFriendly); ;
                }
            }
            else
            {
                if (!formRelayer.DataIsValid)
                {
                    GatedContentLogger.Error("formCollection data is invalid", this);
                }

                if (!gatedFormViewModel.DataSourceItemProxy.IsValidForRendering)
                {
                    GatedContentLogger.Error("DataSourceProxy is invalid", this);
                }
            }
            return toReturn;
        }

        private async Task<ActionResult> GetActionForCaptchaPass(FormRelayer formCollectionHelper, GatedFormViewModel viewModel, FormProxy formProxy)
        {
            ActionResult toReturn = null;
            if (viewModel != null)
            {
                if (viewModel.FormProxy != null)
                {
                    formCollectionHelper.CleanForm();

                    if (formProxy.OriginalMethod.Equals(FormsConstants.FormsHtml.Attributes.Post, StringComparison.OrdinalIgnoreCase))
                    {
                        var response = await formCollectionHelper.SubmitFormPostAsync(formProxy.OriginalAction);
                        if (response.IsSuccessStatusCode)
                        {
                            GatedContentLogger.Log.Debug("Setting redirect to: " + viewModel.DataSourceItemProxy.OnFormSubmitSuccess);
                            toReturn = new RedirectResult(viewModel.DataSourceItemProxy.OnFormSubmitSuccess);
                        }
                        else
                        {
                            GatedContentLogger.Error(response.ToString(), this);
                            toReturn = new RedirectResult(viewModel.DataSourceItemProxy.OnFormSubmitFailUrlStr);
                        }
                    }
                    else
                    {
                        GatedContentLogger.Error("Unhandled submit method", this);
                    }
                }
                else
                {
                    Sitecore.Diagnostics.Log.Error("Null formProxy", this);
                }
            }
            return toReturn;
        }
    }
}
