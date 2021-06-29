using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Feature.GatedContent.Helpers.FormRelay
{

    public class FormRelayer
    {
        public FormRelayer(FormCollection formCollection, bool isValid)
        {
            FormCollection = formCollection;
            FormModelIsValid = isValid;

            if (GatedContentLogger.Log.IsInfoEnabled)
            {
                try
                {
                    var receivedFormCollection = JsonConvert.SerializeObject(FormCollection, Formatting.Indented);

                    GatedContentLogger.Log.Info("Received FormCollection: " + receivedFormCollection);
                }
                catch (System.Exception ex)
                {
                    GatedContentLogger.Error(ex.ToString(), this);
                }
            }
        }

        public bool DataIsValid { get { return ValidateFormCollection(); } }

        public string GateItemIdStr { get { return FormCollection.Get(FormsConstants.FormsHtml.Attributes.GateItemIdFieldName); } }

        public bool TestingFormFormSubmitFail { get; set; }
        private FormCollection FormCollection { get; set; }
        private bool FormModelIsValid { get; }
        public string this[string key]
        {
            get
            {
                return FormCollection.Get(key);
            }
        }

        internal void CleanForm()
        {
            RemoveKey(FormsConstants.FormsHtml.Attributes.GateItemIdFieldName);
            RemoveKey(FormsConstants.RecaptchaV3.TokenElemId);
        }

        internal string FieldValue(string formKey)
        {
            return FormCollection.Get(formKey);
        }

        internal void RemoveKey(string gateItemIdFieldName)
        {
            FormCollection.Remove(gateItemIdFieldName);
        }

        internal async Task<HttpResponseMessage> SubmitFormPostAsync(string action)
        {
            HttpResponseMessage toReturn = null;

            using (var httpClient = new HttpClient())
            {
                var encodedContent = new FormUrlEncodedContent(FormCollection.AllKeys.ToDictionary(key => key, value => FormCollection[value]));

                if (!TestingFormFormSubmitFail)
                {
                    toReturn = await httpClient.PostAsync(action, encodedContent);
                }
                else
                {
                    toReturn = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }
            }

            return toReturn;
        }

        private bool ValidateFormCollection()
        {
            bool toReturn = FormModelIsValid
            && !string.IsNullOrEmpty(FormCollection.Get(FormsConstants.RecaptchaV3.TokenElemId))
            && !string.IsNullOrEmpty(FormCollection.Get(FormsConstants.FormsHtml.Attributes.GateItemIdFieldName));
            return toReturn;
        }
    }
}
