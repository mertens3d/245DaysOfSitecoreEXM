using System;
using System.IO;
using System.Web.Mvc;

namespace Feature.GatedContent.Extensions
{
    public class EEAccordianPanel : IDisposable {
        private TextWriter _writer;

        public EEAccordianPanel(ViewContext viewContext)
        {
            _writer = viewContext.Writer;
        }

        public void Dispose()
        {
            this._writer.Write("</div>");
        }
    }
}
