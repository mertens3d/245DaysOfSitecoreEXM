using Feature.GatedContent.Helpers.CaptchaV3;

namespace Feature.GatedContent.Models.Settings
{
    public interface ICaptchaGatedFormSettings
    {
        CaptchaV3EnvironmentSettings CaptchaV3EnvironmentSettings { get; }
        GatedFormGlobalSettings GatedFormGlobalSettings { get; }
        bool IsValidForRendering { get; }
    }
}
