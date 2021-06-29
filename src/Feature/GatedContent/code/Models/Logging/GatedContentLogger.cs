using log4net;

namespace Feature.GatedContent.Models.Logging
{
    public static class GatedContentLogger
    {
        private static ILog _log;

        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = LogManager.GetLogger(FormsConstants.Logger.LoggerName);
                }
                return _log;
            }
        }

        internal static void Error(string message, object owner)
        {
            var errorMsg = "[Feature.GatedContent. Re: FeatureGateContent Log File] " + message;
            Sitecore.Diagnostics.Log.Error(errorMsg, owner);
            Log.Error(errorMsg);
        }
    }
}
