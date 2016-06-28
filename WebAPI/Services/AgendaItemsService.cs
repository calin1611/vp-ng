using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitsPlannerModel;
using VisitsPlannerModel.Repository;
using AutoMapper;
using log4net;

namespace WebAPI.Services
{
    public class AgendaItemsService
    {
        private readonly AgendaItemsRepository _repo;
        public AgendaItemsService()
        {
            _repo = new AgendaItemsRepository();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void AddAgendaItem(AgendaItemDto agendaItem)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " traced. Added agendaItem with ID: " + agendaItem.Id);
            _repo.AddAgendaItem(agendaItem);
        }

        public void DeleteAgendaItem(int agendaItemId)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " traced. Removed agendaItem with ID: " + agendaItemId);
            _repo.DeleteAgendaItem(agendaItemId);
        }

        public AgendaItemDto GetAgendaItemById(int id)
        {
            return _repo.GetAgendaItemById(id);
        }

        public IEnumerable<AgendaItemDto> GetAgendaItemsForVisit(int visitId)
        {
            return _repo.GetAgendaItemsForVisit(visitId);
        }

//        public IEnumerable<AgendaItemDto> GetNextAgendaItems()
//        {
//           
//        }

        public IList<AgendaItemDto> GetAllAgendaItems(int requestingEmployeeId)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " traced. Requested by: " + requestingEmployeeId);

            var allAgendaItems = _repo.GetAllAgendaItems();
            var agendaItemsWhereResponsable = _repo.GetAgendaItemsIdsWhereResponsable(requestingEmployeeId);

            var returnList = new List<AgendaItemDto>();
            foreach (var agendaItem in allAgendaItems)
            {
                if (agendaItemsWhereResponsable.Contains(agendaItem.Id))
                {
                    returnList.Add(new AgendaItemDto
                    {
                        Id = agendaItem.Id,
                        Date = agendaItem.Date,
                        VisitId = agendaItem.VisitId,
                        Title = agendaItem.Title,
                        VisitTypeId = agendaItem.VisitTypeId,
                        LocationId = agendaItem.LocationId
                    });
                }
                else
                {
                    returnList.Add(new AgendaItemDto()
                    {
                        Title = agendaItem.Title,
                        Date = agendaItem.Date,
                        VisitId = agendaItem.VisitId
                    });
                }
            }

            return returnList;
        }

        public IEnumerable<AgendaItemDto> GetAgendaItemsByVisitAndEmployee(int visitId, int employeeId)
        {
            return _repo.GetAgendaItemsByVisitAndEmployee(visitId, employeeId);
        }

    }
}