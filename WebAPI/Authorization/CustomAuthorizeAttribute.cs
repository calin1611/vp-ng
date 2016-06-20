using System.Web.Http;
using System.Web.Http.Controllers;
using WebAPI.Services;
using WebAPI.Helpers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using VisitsPlannerModel;

namespace WebAPI.Authorization
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        //public override void OnAuthorization(HttpActionContext actionContext)
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
            {
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //Code to handle unauthorized request
            var response = actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "uZnauthorized");
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "permission required."));

            actionContext.Response = response;
        }

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization.Parameter;
            var authService = new AuthenticationService();
            string username =  HelperClass.DecodeCredentials(actionContext.Request)[0];
            string password = HelperClass.DecodeCredentials(actionContext.Request)[1];

            EmployeeDto authenticatedEmployee = authService.Authenticate(username, password);
            if (authenticatedEmployee != null)
            {
               return true;
            }
            return false;
        }

    }
}