using System.Collections.Generic;
using System.Web.Http;
using VisitsPlannerModel;
using WebAPI.Services;
using WebAPI.Helpers;
using log4net;

namespace WebAPI.Controllers
{
    public class AgendaItemsController : ApiController
    {
        private readonly AgendaItemsService _agendaItemsService;

        public AgendaItemsController()
        {
            _agendaItemsService = new AgendaItemsService();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public AgendaItemDto AddAgendaItem(AgendaItemDto agendaItem)
        {
            Log.Debug("POST " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
            return _agendaItemsService.AddAgendaItem(agendaItem);
        }

        [HttpGet]
        public IList<AgendaItemDto> GetAllAgendaItems()
        {
            Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
            var employeesService = new EmployeesService();

            string[] credentials = HelperClass.DecodeCredentials(Request);
            int userId = employeesService.GetIdByEmail(credentials[0]);

            return _agendaItemsService.GetAllAgendaItems(userId);
        }

        [HttpGet]
        public IEnumerable<AgendaItemDto> GetAgendaItemsForVisit(int id)
        {
            Log.Debug("GET " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");

            return _agendaItemsService.GetAgendaItemsForVisit(id);
        }

        [HttpGet]
        public IEnumerable<AgendaItemDto> GetAgendaItemsByVisitAndEmployee(int visitId)
        {
            var employeesService = new EmployeesService();

            string[] credentials = HelperClass.DecodeCredentials(Request);
            int userId = employeesService.GetIdByEmail(credentials[0]);
            return _agendaItemsService.GetAgendaItemsByVisitAndEmployee(visitId, userId);
        }

        [HttpGet]
        public AgendaItemsRelatedDataDto GetRelatedData()
        {
            return _agendaItemsService.GetRelatedData();
        }

        [HttpPut]
        public AgendaItemDto UpdateAgendaItem(AgendaItemDto agendaItemDto) 
        {
            return _agendaItemsService.UpdateAgendaItem(agendaItemDto);
        }
    }
}
