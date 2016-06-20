using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using log4net;
using VisitsPlannerModel;
using WebAPI.Services;
using VisitsPlannerModel.DTO;

namespace WebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly EmployeesService _employeesService;

        public EmployeesController()
        {
            _employeesService = new EmployeesService();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        [Authorize(Roles = "1")]
        [HttpGet]
        public IList<EmployeeDto> GetAll()
        {
            Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
            return _employeesService.GetAll();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public void Add(EmployeeDto employee)
        {
            Log.Debug("POST " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");

            _employeesService.Add(employee);
        }

        [HttpPost]
        public void AddNotificationPreference(NotificationPreferenceDto notificationPreference)
        {
            Log.Debug("POST " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");

            _employeesService.AddNotificationPreference(notificationPreference);
        }

        [HttpGet]
        public Boolean Authenticate()// Am facut si un DTO (CredentialsDto) in care sa pun credentialele(user si parola) pentru a le verifica, dar cred ca e degeaba pentru ca ele oricum se verifica in BasicAuthenticationHandler.
        {
            return true;
        }

    }
}
