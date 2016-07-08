using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using log4net;
using VisitsPlannerModel.Repository;
using VisitsPlannerModel;
using WebAPI.Helpers;
using Microsoft.Practices.Unity;
using AutoMapper;


namespace WebAPI.Services
{
    public class VisitsService
    {
        private readonly VisitsRepository _repo;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VisitsService()
        {
            _repo = new VisitsRepository();
        }

        public VisitDto Add(VisitDto visit)
        {
//            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + 
//                "() traced. Added visit: " + visit.Title + 
//                " from " + visit.Date);
            return _repo.AddVisit(visit);
        }

        public Boolean Delete(int visitId, int organiserId) 
        {
            return _repo.DeleteVisit(visitId, organiserId);
        }

        public IList<VisitDto> CurrentMonth(int id)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "() traced. Got visits for employee(ID): " + id);

            var visits = _repo.GetVisitsFromCurrentMonth().OrderBy(v => v.Date).ToList();
            var agendaItemsService = new AgendaItemsService();
            var returnList = new List<VisitDto>();
            foreach (var visit in visits)
            {
                var agendaItemsForVisit = agendaItemsService.GetAgendaItemsByVisitAndEmployee(visit.Id, id);
                if (visit.OrganiserId == id)
                {
                    returnList.Add(new VisitDto
                    {
                        Id = visit.Id,
                        Date = visit.Date,
                        Title = visit.Title,
                        Outcome = visit.Outcome,
                        EmployeeData = visit.EmployeeData,
                        //OrganiserId = visit.OrganiserId,
                        AgendaItems = agendaItemsForVisit
                    });
                }
                else
                {
                    returnList.Add(new VisitDto
                    {
                        Id = visit.Id,
                        Date = visit.Date,
                        Title = visit.Title,
                        EmployeeData = visit.EmployeeData,
                        //OrganiserId = visit.OrganiserId
                    });
                }
            }
            return returnList;
        }

        public IList<VisitDto> CurrentWeek(int id)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name +
                "() traced. Got visits for employee(ID): " + id);
            return _repo.GetVisitsFromCurrentWeek(id);
        }

        public IList<VisitDto> MyVisits(int id)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name +
                "() traced. Got visits for employee(ID): " + id);
            return _repo.MyVisits(id);
        }

        internal IList<VisitDto> VisitsWAIAssigned(int userId)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name +
                "() traced. Got visits for employee(ID): " + userId);
            return _repo.VisitsWithAgendaItemsAssigned(userId);
        }

        public VisitDto UpdateVisit(VisitDto visit)
        {
            return _repo.UpdateVisit(visit);
        }

        //internal IList<VisitDto> MyAgendaItems(int id)
        //{
        //    return _repo.MyAgendaItems(id);
        //}
    }
}