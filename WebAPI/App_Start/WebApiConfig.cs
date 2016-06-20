using System.Timers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Handlers;
using WebAPI.Services;
using WebAPI.Authorization;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var xmlFormatter = config.Formatters.XmlFormatter;
            var jsonFormatter = config.Formatters.JsonFormatter;
            xmlFormatter.UseXmlSerializer = true;
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);
            config.Formatters.Add(xmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            // Web API message handlers
            config.MessageHandlers.Add(new BasicAuthenticationHandler(new AuthenticationService()));

            // Authorization filters
            //config.Filters.Add(new CustomAuthorizeAttribute());

            //Timer service
            TimerService.SetTimer();

            //log4net
            log4net.Config.XmlConfigurator.Configure();

            config.EnableCors(new EnableCorsAttribute(
            origins: "*", // "http://localhost:64196",
            headers: "*",
            methods: "*"
        ));
        }
    }
}
