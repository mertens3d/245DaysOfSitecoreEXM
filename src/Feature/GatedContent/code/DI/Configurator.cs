using Feature.GatedContent.Controllers;
using Feature.GatedContent.Models.Settings;
using Forms.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Feature.GatedContent.DI
{
    public class Configurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICaptchaGatedFormSettings, CaptchaGateSettings>();
            serviceCollection.AddTransient<ICaptchaGateRepository, RecaptchaRepository>();
            serviceCollection.AddTransient<GatedFormRenderingController>();
            serviceCollection.AddTransient<GatedFormAPIController>();
        }
    }
}
