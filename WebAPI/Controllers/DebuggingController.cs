using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VisitsPlannerModel;
using VisitsPlannerModel.DTO;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class DebuggingController : ApiController
    {
        [HttpGet]
        //[Authorize(Roles = "1")]
        public IList<NotificationPreferenceDto> NotificationTimesMinutes(int id)
        {
            var notificationService = new NotificationService();
            return notificationService.GetNotificationTimesForAgendaItem(id);
        }

        [HttpGet]
        public IList<NotificationTimeDto> NotificationTimesDateTime(int id)
        {
            var notificationService = new NotificationService();
            return notificationService.ComputeNotificationTimeForAgendaItem(id);
        }

        [HttpGet]
        //[Authorize(Roles = "1")]
        public IList<NotificationTimeDto> NotificationTimes2()
        {
            var notificationService = new NotificationService();
            return notificationService.GetNotificationTimes();
        }

        [HttpGet]
        public void getAllVisitsFromCurrentMonth()
        {
            var rs = new ReportingService();
            rs.GetVisitsFromCurrentMonth();
        }

        [HttpGet]
        public void FindNotificationTimes()
        {
            var rs = new NotificationService();
            rs.FindNotificationTimes();
        }


        [AllowAnonymous]
        [HttpPost]
        public Boolean Authenticate([FromBody]string credentials)// Am facut si un DTO (CredentialsDto) in care sa pun credentialele(user si parola) pentru a le verifica, dar cred ca e degeaba pentru ca ele oricum se verifica in BasicAuthenticationHandler.
        {

            return true;
        }
    }
}
