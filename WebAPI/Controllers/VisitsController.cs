using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using log4net;
using VisitsPlannerModel;
using WebAPI.Services;
using WebAPI.Helpers;
using WebAPI.Authorization;
using System.Net.Http;
using System.Net;

namespace WebAPI.Controllers
{
    [CustomAuthorize]
    public class VisitsController : ApiController
    {
        //private readonly VisitsService _visitsService;
        //private readonly EmployeesService _employeesService;
        private VisitsService _visitsService;
        private EmployeesService _employeesService;
        private int UserId
        {
            get
            {
                var employeesService = new EmployeesService();
                var headerContent = Request;

                string[] credentials = HelperClass.DecodeCredentials(headerContent);
                int userId = employeesService.GetIdByEmail(credentials[0]);
                return userId;
            }
        }
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public VisitsController()
        {
            _visitsService = new VisitsService();
            _employeesService = new EmployeesService();
        }

        [HttpDelete]
        public HttpResponseMessage DeleteVisit(int id)
        {

            if (_visitsService.Delete(id, UserId))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }


        [HttpPost]
        public void Add(VisitDto visit)
        {
            Log.Debug("POST " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
            _visitsService.Add(visit);
        }
        
        //[CustomAuthorize]
        [HttpGet]
        public IList<VisitDto> CurrentMonth()
        {
            using (var performanceMonitor = new PerformanceMonitorService(System.Reflection.MethodBase.GetCurrentMethod()))
            {
                Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
                var visitsFromCurrentMonth = _visitsService.CurrentMonth(UserId);
                return visitsFromCurrentMonth;
            }
        }

        //[CustomAuthorize]
        [HttpGet]
        public IList<VisitDto> CurrentWeek()
        {
            using (var performanceMonitor = new PerformanceMonitorService(System.Reflection.MethodBase.GetCurrentMethod()))
            {
                Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
                var visitsFromCurrentWeek = _visitsService.CurrentWeek(UserId);
                return visitsFromCurrentWeek;
            }
        }


        [HttpGet]
        public IList<VisitDto> MyVisits()
        {
            Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
            return _visitsService.MyVisits(UserId);
        }

        [HttpGet]
        public IList<VisitDto> VisitsWAIAssigned()
        {
            Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + "() called.");
            return _visitsService.VisitsWAIAssigned(UserId);
        }

        [HttpPut]
        public VisitDto UpdateVisit(VisitDto visit)
        {
            return _visitsService.UpdateVisit(visit);
        }

        //[HttpGet]
        //public IList<VisitDto> MyAgendaItems(int id)
        //{
        //    var service = new VisitsService();
        //    return service.MyAgendaItems(id);
        //}

    }
}
