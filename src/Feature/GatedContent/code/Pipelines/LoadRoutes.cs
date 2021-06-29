using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Feature.GatedContent.Pipelines
{
    public class LoadRoutes
    {
        public void Process(PipelineArgs args)
        {
            //WebApiConfig.RegisterRoutes(RouteTable.Routes);
            Sitecore.Diagnostics.Log.Error("Starting WebApi for Feature.GatedContent", new object());

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute(
              name: "nuaireapi",
              url: "api/GatedFormAPI/{action}",
              defaults: new
            {
                controller = "GatedFormAPI",
                action = "testget"
            }
                );

            Sitecore.Diagnostics.Log.Error(RouteTable.Routes.ToString(), new object());
        }
    }
}
