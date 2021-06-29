namespace Feature.GatedContent.Helpers.CaptchaV3
{
    public class CaptchaTestSettings
    {
        public CaptchaTestSettings(bool testingForceCaptchaFail, bool testingForceCaptchaSuccess)
        {
            TestingForceCaptchaFail = testingForceCaptchaFail;
            TestingForceCaptchaSuccess = testingForceCaptchaSuccess;
        }

        public bool TestingForceCaptchaFail { get; }
        public bool TestingForceCaptchaSuccess { get; }
    }
}
