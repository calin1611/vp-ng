using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VisitsPlannerModel.Repository.Interfaces;
using AutoMapper;
using VisitsPlannerModel.DTO;

namespace VisitsPlannerModel.Repository
{
    public class AgendaItemsRepository : IAgendaItemsRepository
    {

        public void AddAgendaItem(AgendaItemDto agendaItem)
        {
            using (var context = new VPEntities())
            {
                Mapper.CreateMap<AgendaItemDto, AgendaItem>();
                AgendaItem newAgendaItem = Mapper.Map<AgendaItemDto, AgendaItem>(agendaItem);

                context.AgendaItems.Add(newAgendaItem);
                context.SaveChanges();
            }
        }

        public void DeleteAgendaItem(int agendaItemId)
        {
            using (var context = new VPEntities())
            {
                AgendaItem agendaItemToDelete = context.AgendaItems.FirstOrDefault(ai => ai.Id == agendaItemId);

                context.AgendaItems.Remove(agendaItemToDelete);
                context.SaveChanges();
            }
        }

        public AgendaItemDto GetAgendaItemById(int agendaItemId)
        {
            using (var context = new VPEntities())
            {
                var agendaItem = context.AgendaItems.FirstOrDefault(ai => ai.Id == agendaItemId);

                AgendaItemDto agendaItemDto = new AgendaItemDto
                {
                    Id = agendaItem.Id,
                    Date = agendaItem.Date,
                    VisitId = agendaItem.VisitId,
                    Title = agendaItem.Title,
                    VisitTypeId = agendaItem.VisitTypeId,
                    LocationId = agendaItem.LocationId
                };

                return agendaItemDto;
            }
        }

        public IList<AgendaItemDto> GetAllAgendaItems()
        {
            using (var context = new VPEntities())
            {
                var agendaItems = context.AgendaItems.ToList();
                IList<AgendaItemDto> returnList = new List<AgendaItemDto>();
                
                foreach (var agendaItem in agendaItems)
                {
                    returnList.Add(new AgendaItemDto
                    {
                        Id = agendaItem.VisitId,
                        Title = agendaItem.Title,
                        Date = agendaItem.Date,
                        VisitId = agendaItem.VisitId,
                        VisitTypeId = agendaItem.VisitTypeId,
                        LocationId = agendaItem.LocationId
                    });
                }
                return returnList;
            }
        }


        public IEnumerable<AgendaItemDto> GetAgendaItemsForVisit(int visitId)
        {
            using (var context = new VPEntities())
            {
                var agendaItems = context.AgendaItems.Where(ai => ai.VisitId == visitId);
                IList<AgendaItemDto> returnList = new List<AgendaItemDto>();

                foreach (var agendaItem in agendaItems)
                {
                    returnList.Add(new AgendaItemDto
                    {
                        Id = agendaItem.VisitId,
                        Title = agendaItem.Title,
                        Date = agendaItem.Date,
                        VisitId = agendaItem.VisitId,
                        VisitTypeId = agendaItem.VisitTypeId,
                        LocationId = agendaItem.LocationId
                    });
                }
                return returnList;
            }
        }

        public IList<int> GetAgendaItemsIdsWhereResponsable(int employeeId)
        {
            using (var context = new VPEntities())
            {
                var returnList = new List<int>();
                var agendaItems = context.EmployeesAgendaItems.Where(eai => eai.EmployeeId == employeeId && eai.IsResponsable == true).ToList();

                foreach (var employeesAgendaItem in agendaItems)
                {
                    returnList.Add(employeesAgendaItem.AgendaItemId);
                }

                return returnList;
            }
        }

        public IEnumerable<AgendaItemDto> GetNextAgendaItems()
        {
            using (var context = new VPEntities())
            {
                var agendaItems = context.EmployeesAgendaItems.Select(eai => eai.AgendaItem).Where(ai => ai.Date.Value > DateTime.Now).Distinct().ToList();
                var returnList = new List<AgendaItemDto>();
                foreach (var agendaItem in agendaItems)
                {
                    returnList.Add(new AgendaItemDto
                    {
                        Id = agendaItem.Id,
                        VisitId = agendaItem.VisitId,
                        Date = agendaItem.Date,
                        Title = agendaItem.Title,
                        VisitTypeId = agendaItem.VisitTypeId,
                        LocationId = agendaItem.LocationId,
                        Outcome = agendaItem.Outcome
                    }); 
                }
                return returnList;
            }
        }

        public IEnumerable<AgendaItemDto> GetAgendaItemsByVisitAndEmployee(int visitId, int employeeId)
        {
            using (var context = new VPEntities())
            {
                var returnList = new List<AgendaItemDto>();
//                var agendaItems = context.AgendaItems.Where(ai => ai.VisitId == visitId).Include(ai => ai.EmployeesAgendaItems);
                var agendaItems = context.AgendaItems.Where(ai => ai.VisitId == visitId).Include(ai => ai.EmployeesAgendaItems).ToList();
//                                        .Where(eai => eai.EmployeeId == employeeId));

                foreach (var agendaItem in agendaItems)
                {
                    if (agendaItem.EmployeesAgendaItems.Any(eai => eai.EmployeeId == employeeId))
                    {
                        returnList.Add(new AgendaItemDto
                        {
                            Id = agendaItem.Id,
                            VisitId = agendaItem.VisitId,
                            Date = agendaItem.Date,
                            Title = agendaItem.Title,
                            VisitTypeId = agendaItem.VisitTypeId,
                            LocationId = agendaItem.LocationId,
                            Outcome = agendaItem.Outcome
                        });
                    }
                }

                return returnList;
            }
        }
    }
}
