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

        public AgendaItemDto AddAgendaItem(AgendaItemDto agendaItemToAdd)
        {
            using (var context = new VPEntities())
            {
                AgendaItem newAgendaItem = new AgendaItem
                {
                    Title = agendaItemToAdd.Title,
                    Date = agendaItemToAdd.Date,
                    VisitId = agendaItemToAdd.VisitId,
                    LocationId = agendaItemToAdd.LocationId,
                    VisitTypeId = agendaItemToAdd.VisitTypeId,
                    Outcome = agendaItemToAdd.Outcome,
                    Location= context.Locations.Where(l=>l.Id==agendaItemToAdd.LocationId).FirstOrDefault(),
                    VisitType = context.VisitTypes.Where(vt => vt.Id == agendaItemToAdd.VisitTypeId).FirstOrDefault()
                };

                context.AgendaItems.Add(newAgendaItem);
                context.SaveChanges();

                AgendaItemDto justAdded = new AgendaItemDto();

                AgendaItemDto recentlyAddedAgendaItem = new AgendaItemDto
                {
                    Id = newAgendaItem.Id,
                    Title = newAgendaItem.Title,
                    Date = newAgendaItem.Date,
                    VisitId = newAgendaItem.VisitId,
                    VisitTypeId = newAgendaItem.VisitTypeId,
                    LocationId = newAgendaItem.LocationId,
                    Outcome = newAgendaItem.Outcome,
                    Location = new LocationDto()
                    {
                        Id = newAgendaItem.Location.Id,
                        Name = newAgendaItem.Location.Name,
                        Address = newAgendaItem.Location.Address
                    },
                    VisitType = new VisitTypeDto
                    {
                        Id = newAgendaItem.VisitType.Id,
                        Type = newAgendaItem.VisitType.Type
                    }
                };

                return recentlyAddedAgendaItem;
            }
        }

        public bool DeleteAgendaItem(int agendaItemId)
        {
            using (var context = new VPEntities())
            {
                AgendaItem agendaItemToDelete = context.AgendaItems.FirstOrDefault(ai => ai.Id == agendaItemId);

                context.AgendaItems.Remove(agendaItemToDelete);
                context.SaveChanges();

                return true;
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
                        Id = agendaItem.Id,
                        Title = agendaItem.Title,
                        Date = agendaItem.Date,
                        VisitId = agendaItem.VisitId,
                        Outcome = agendaItem.Outcome,
                        Location = new LocationDto
                        {
                            Id = agendaItem.Location.Id,
                            Name = agendaItem.Location.Name,
                            Address = agendaItem.Location.Address
                        },
                        VisitType = new VisitTypeDto
                        {
                            Id = agendaItem.VisitType.Id,
                            Type = agendaItem.VisitType.Type
                        }
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

        public AgendaItemsRelatedDataDto GetRelatedData()
        {
            using (var context = new VPEntities())
            {
                var locations = context.Locations.ToList();
                var visitTypes = context.VisitTypes.ToList();

                var relatedData = new AgendaItemsRelatedDataDto();
                relatedData.Location = new List<LocationDto>();
                relatedData.VisitType = new List<VisitTypeDto>();

                foreach (var location in locations)
                {
                    relatedData.Location.Add(new LocationDto
                    {
                        Id = location.Id,
                        Address = location.Address,
                        Name = location.Name
                    });
                }

                foreach (var visitType in visitTypes)
                {
                    relatedData.VisitType.Add(new VisitTypeDto
                    {
                        Id = visitType.Id,
                        Type = visitType.Type
                    });
                }

                return relatedData;
            }
        }

        public AgendaItemDto UpdateAgendaItem(AgendaItemDto agendaItemDto)
        {
            using (var context = new VPEntities())
            {
                var agendaItem = context.AgendaItems.FirstOrDefault(ai => ai.Id == agendaItemDto.Id);

                agendaItem.Title = agendaItemDto.Title;
                agendaItem.Date = agendaItemDto.Date;
                agendaItem.Outcome = agendaItemDto.Outcome;
                agendaItem.LocationId = agendaItemDto.Location.Id != 0 ? agendaItemDto.Location.Id : agendaItem.LocationId;
                agendaItem.VisitTypeId = agendaItemDto.VisitType.Id != 0 ? agendaItemDto.VisitType.Id : agendaItem.VisitTypeId;

                context.SaveChanges();

                AgendaItemDto returnedAgendaItem = new AgendaItemDto
                {
                    Id = agendaItem.Id,
                    Title = agendaItem.Title,
                    Date = agendaItem.Date,
                    Outcome = agendaItem.Outcome,
                    Location = new LocationDto 
                    {
                        Id = agendaItem.Location.Id,
                        Name = agendaItem.Location.Name,
                        Address = agendaItem.Location.Address
                    },
                    VisitType = new VisitTypeDto 
                    {
                        Id = agendaItem.VisitType.Id,
                        Type = agendaItem.VisitType.Type
                    },
                    LocationId = agendaItem.LocationId,
                    VisitTypeId = agendaItem.VisitTypeId
                };

                return returnedAgendaItem;
            }
        }
    }
}
