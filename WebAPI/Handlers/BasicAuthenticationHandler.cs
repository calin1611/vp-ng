using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;
using WebAPI.Services;


namespace WebAPI.Handlers
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _service;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BasicAuthenticationHandler(IAuthenticationService service)
        {
            _service = service;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
                return base.SendAsync(request, cancellationToken);

            AuthenticationHeaderValue authHeader = request.Headers.Authorization;

            // Test
            //var headers = request.Headers.GetValues("Authorization");
            //var ajaxHeader = headers.FirstOrDefault();

            if (authHeader == null || authHeader.Scheme != "Basic")
            {
                Log.Debug("AUTHENTICATION - Someone tried to access the API without being authenticated or with an authentication scheme different from Basic.");
                return Unauthorized(request);
            }

            string encodedCredentials = authHeader.Parameter;
            byte[] credentialBytes = Convert.FromBase64String(encodedCredentials);
            string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');

            var authenticatedEmployee = _service.Authenticate(credentials[0], credentials[1]);
            if (authenticatedEmployee == null)
            {
                Log.Warn("AUTHENTICATION - Someone tried to access the API without being registered.");
                return Unauthorized(request);
            }

            string[] roles = {authenticatedEmployee.Role.ToString()};

            IIdentity identity = new GenericIdentity(credentials[0], "Basic");
            IPrincipal user = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = user;

            Log.Debug("User logged: " + 
                authenticatedEmployee.FirstName + " " +
                authenticatedEmployee.LastName + " (" +
                authenticatedEmployee.Id + ")");

            return base.SendAsync(request, cancellationToken);
        }

        private Task<HttpResponseMessage> Unauthorized(HttpRequestMessage request)
        {
            var response = request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Headers.Add("WWW-Authenticate", "Basic");

            TaskCompletionSource<HttpResponseMessage> task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult(response);
            
            return task.Task;
        }

    }
}