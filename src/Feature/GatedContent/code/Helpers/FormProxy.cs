using Feature.GatedContent.Models;
using Feature.GatedContent.Models.Logging;
using HtmlAgilityPack;
using System.Linq;

namespace Feature.GatedContent.Helpers
{
    public  class FormProxy
    {
        public FormProxy(string htmlCodeRaw, bool testingForceInvalidFormMarkup)
        {
            OriginalHtml = htmlCodeRaw;
            TestingForceInvalidFormMarkup = testingForceInvalidFormMarkup;
            ConvertMarkupToAgilityNode();
        }

        public bool HasFormNode { get { return FormNode != null; } }
        public bool HasOriginalAction { get { return !string.IsNullOrEmpty(OriginalAction); } }
        public bool HasPopulatedMarkup { get { return !string.IsNullOrEmpty(OriginalHtml); } }

        internal string GetInputNodeValue(string nodeName)
        {
            var toReturn = string.Empty;
            var found = GetInputNode(nodeName);
            if (found != null)
            {
                toReturn = found.GetAttributeValue(FormsConstants.FormsHtml.Attributes.Value, string.Empty);
            }

            return toReturn;
        }

        private HtmlNode GetInputNode(string nodeName)
        {
            HtmlNode toReturn = null;
            if (Doc != null)
            {
                toReturn = Doc
                     .DocumentNode
                     .Descendants(FormsConstants.FormsHtml.Tags.Input)
                     .FirstOrDefault(x => x.GetAttributeValue(FormsConstants.FormsHtml.Attributes.Name, string.Empty)
                                        .Equals(nodeName));
            }

            return toReturn;
        }

        internal bool HasInputNode(string nodeName)
        {
            return GetInputNode(nodeName) != null;
        }

        public bool HasValidMarkup { get { return Doc != null; } }

        public bool IsValidForRendering
        {
            get
            {
                return
                    HasPopulatedMarkup
                    && HasValidMarkup
                    && HasFormNode
                    && HasOriginalAction
                    && !TestingForceInvalidFormMarkup;
            }
        }

        public string ModifiedHtmlCode
        {
            get
            {
                var toReturn = string.Empty;

                if (IsValidForRendering)
                {
                    toReturn = Doc.DocumentNode.OuterHtml;
                }

                return toReturn;
            }
        }

        public string OriginalAction { get; private set; }
        public string OriginalHtml { get; }
        public string OriginalMethod { get; private set; }
        public bool TestingForceInvalidFormMarkup { get; set; } = false;
        private HtmlDocument Doc { get; set; }

        private HtmlNode FormNode { get; set; }

        public void AddInputNode(string nameId, string type, string value)
        {
            var inputNode = Doc.CreateElement("input");

            if (!string.IsNullOrEmpty(nameId))
            {
                var nameAttribute = Doc.CreateAttribute("name", nameId);
                inputNode.Attributes.Add(nameAttribute);

                var idAttribute = Doc.CreateAttribute("id", nameId);
                inputNode.Attributes.Add(idAttribute);
            }

            var hiddenAttribute = Doc.CreateAttribute("type", type);
            inputNode.Attributes.Add(hiddenAttribute);

            var valueAttribute = Doc.CreateAttribute("value", value);
            inputNode.Attributes.Add(valueAttribute);

            FormNode.AppendChild(inputNode);
        }

        internal void RemoveNodesOfTypeComment()
        {
            HtmlNodeType htmlNodeType = HtmlNodeType.Comment;

            Doc.DocumentNode.Descendants()
            .Where(x => x.NodeType.Equals(htmlNodeType))
            .ToList()
            .ForEach(x => x.Remove());
        }

        internal void RemoveMeta()
        {
            Doc.DocumentNode.Descendants()
            .Where(x => x.Name.Equals(FormsConstants.FormsHtml.Tags.Meta, System.StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(x => x.Remove());
        }

        private void ConvertMarkupToAgilityNode()
        {
            Doc = new HtmlDocument();
            if (HasPopulatedMarkup)
            {
                Doc.LoadHtml(OriginalHtml);
                FormNode = Doc.DocumentNode.SelectSingleNode("//form");
                ProcessFormNode();
            }
        }

        private void ProcessAttributeAction()
        {
            HtmlAttribute actionAttribute = FormNode.Attributes["action"];

            if (actionAttribute != null)
            {
                OriginalAction = actionAttribute.Value;

                GatedContentLogger.Log.Debug("OriginalAction: " + OriginalAction);

                //AddToEEDiagnostics("Original Action: " + model.OriginalAction);

                actionAttribute.Value = FormsConstants.Endpoints.Nuaire.ApiGateEndpoint;
            }
            else
            {
                GatedContentLogger.Error("Missing Action attribute", this);
            }
        }

        private void ProcessAttributeMethod()
        {
            HtmlAttribute methodAttribute = FormNode.Attributes[FormsConstants.FormsHtml.Attributes.Method];

            if (methodAttribute == null)
            {
                FormNode.Attributes.Add(FormsConstants.FormsHtml.Attributes.Method, string.Empty);
                OriginalMethod = string.Empty;
                methodAttribute = FormNode.Attributes[FormsConstants.FormsHtml.Attributes.Method];
            }
            else
            {
                OriginalMethod = methodAttribute.Value;
            }

            GatedContentLogger.Log.Debug("OriginalMethod: " + OriginalMethod);
            methodAttribute.Value = FormsConstants.FormsHtml.Attributes.Post;
        }

        private void ProcessFormNode()
        {
            if (HasFormNode)
            {
                ProcessAttributeAction();
                ProcessAttributeMethod();
            }
        }
    }
}
