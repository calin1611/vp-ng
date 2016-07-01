using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;

namespace VisitsPlannerModel.Repository
{
    public class VisitsRepository : IVisitsRepository
    {
        public void AddVisit(VisitDto visitDto)
        {
            using (var context = new HistoryContext(3))
            {
                Visit newVisit = new Visit();
                newVisit.Date = visitDto.Date;
                newVisit.Title = visitDto.Title;
                newVisit.OrganiserId = visitDto.OrganiserId;

                context.Visits.Add(newVisit);
                context.SaveChanges();
            }
        }

        public IList<VisitDto> GetVisitsFromCurrentMonth(int userId)
        {
            using (var context = new VPEntities())
            {
                var visits = context.Visits.Where(v => v.Date.Value.Month == DateTime.Now.Month && v.Date.Value.Year == DateTime.Now.Year && v.OrganiserId == userId).ToList();

                var returnList = new List<VisitDto>();
                
                foreach (var visit in visits)
                {
                    returnList.Add(new VisitDto 
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        Date = visit.Date,
                        OrganiserId = visit.OrganiserId,
                        Outcome = visit.Outcome,
                        CreatedOn = visit.CreatedOn,
                        CreatedBy = visit.CreatedBy,
                        EmployeeData = new EmployeeShareableDto
                        {
                            Id = visit.Employee.Id,
                            FirstName = visit.Employee.FirstName,
                            LastName = visit.Employee.LastName,
                            Email = visit.Employee.Email,
                            Role = visit.Employee.Role,
                        }
                    });
                }
                return returnList;
            }
        }

        public IList<VisitDto> GetVisitsFromCurrentMonth()
        {
            using (var context = new VPEntities())
            {
                var visits = context.Visits
                    .Where(v => v.Date.Value.Month == DateTime.Now.Month && v.Date.Value.Year == DateTime.Now.Year)
                    .Include(v => v.AgendaItems
                                    .Select(ai => ai.EmployeesAgendaItems
                                                        .Select(eai => eai.Employee)))
                    .ToList();

                var returnList = new List<VisitDto>();

                foreach (var visit in visits)
                {
                    returnList.Add(new VisitDto
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        Date = visit.Date,
                        OrganiserId = visit.OrganiserId,
                        Outcome = visit.Outcome,
                        EmployeeData = new EmployeeShareableDto
                        {
                            Id = visit.Employee.Id,
                            FirstName = visit.Employee.FirstName,
                            LastName = visit.Employee.LastName,
                            Email = visit.Employee.Email,
                            Role = visit.Employee.Role,
                        }
                    });
                }
                return returnList;
            }
        }


        public List<VisitDto> GetVisitsFromCurrentWeek(int id)
        {
            using (var context = new VPEntities())
            {
                var returnList = new List<VisitDto>();

                var visitsThatHappenInCurrentWeek = context.Visits.Where(v => v.OrganiserId == id).ToList().Where(VisitHappensInCurrentWeek);
                foreach (var visit in visitsThatHappenInCurrentWeek)
                {
                    returnList.Add(new VisitDto
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        Date = visit.Date,
                        OrganiserId = visit.OrganiserId,
                        CreatedOn = visit.CreatedOn,
                        EmployeeData = new EmployeeShareableDto
                        {
                            Id = visit.Employee.Id,
                            FirstName = visit.Employee.FirstName,
                            LastName = visit.Employee.LastName,
                            Email = visit.Employee.Email,
                            Role = visit.Employee.Role,
                        }
                    });
                }
                return returnList;
            }
        }

        private bool VisitHappensInCurrentWeek(Visit visit)
        {
            int currentWeek = GetWeekNo(DateTime.Now);
            return visit.Date.Value.Year == DateTime.Now.Year && GetWeekNo(visit.Date.Value) == currentWeek;
        }

        private static int GetWeekNo(DateTime date)
        {
            DateTime test_date = date;
            double week_of_year = Convert.ToDouble(test_date.DayOfYear) / 7;

            return Convert.ToInt32(week_of_year);
        }

        public IList<VisitDto> MyVisits(int id) //???
        {
            using (var context = new VPEntities())
            {
                var visits = context.Visits.Where(v => v.OrganiserId == id);
                
                var innerJoinQuery =
                    from visits2 in visits
                    join emp in context.Employees on visits2.OrganiserId equals emp.Id
                    select visits2;

                var returnList = new List<VisitDto>();
                
                foreach (var visit in innerJoinQuery.ToList())
                {
                    returnList.Add(new VisitDto 
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        Date = visit.Date,
                        OrganiserId = visit.OrganiserId,
                        CreatedOn = visit.CreatedOn
                    });
                }
                return returnList;
            }
        }

        public List<VisitDto> VisitsWithAgendaItemsAssigned(int userId)
        {
            using (var context = new VPEntities())
            {
                var visits = context.Visits.Where(v => v.AgendaItems
                                                .Where(a => a.EmployeesAgendaItems
                                                    .Where(jt => jt.EmployeeId == userId)
                                                    .FirstOrDefault().AgendaItemId == a.Id
                                                ).FirstOrDefault().VisitId == v.Id
                                            ).ToList();

                var returnList = new List<VisitDto>();

                foreach (var visit in visits)
                {
                    returnList.Add(new VisitDto
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        OrganiserId = visit.OrganiserId,
                        Date = visit.Date,
                        CreatedOn = visit.CreatedOn
                    });
                }
                return returnList;
            }
        }

        public IList<VisitDto> GetVisits()
        {
            using (var context = new VPEntities())
            {
                var visits = context.Visits.ToList();
                var returnList = new List<VisitDto>();

                foreach (var visit in visits)
                {
                    returnList.Add(new VisitDto 
                    {
                        Id = visit.Id,
                        Title = visit.Title,
                        OrganiserId = visit.OrganiserId,
                        Date = visit.Date,
                        CreatedOn = visit.CreatedOn,
                        Outcome = visit.Outcome
                    });
                }
                return returnList;
            }
        }

        //public IList<VisitDto> MyAgendaItems(int id)
        //{
        //    using (var context = new VPEntities())
        //    {
        //        //var agendaItemsJT = context.JTEmployees_AgendaItems.Where(ai => ai.EmployeeId == id).Include(agendaItemsJT;
        //        var agendaItemsJT = context.JTEmployees_AgendaItems
        //                            .Where(ai => ai.EmployeeId == id).Include(c => c.AgendaItem).ToList();
                
        //        //var agendaItems = context.AgendaItems.
        //        List<VisitDto> returnList = new List<VisitDto>();
        //        foreach (var agendaItem in agendaItemsJT)
        //        {
        //            //returnList.Add(new VisitDto 
        //            //{
        //            //   Title =  agendaItemsJT.
        //            //});
        //        }
        //        return returnList;
        //    }
        //}

        public Boolean DeleteVisit(int visitId, int organiserId)
        {
            using (var context = new VPEntities())
            {
                Visit deleteVisit = context.Visits.FirstOrDefault(v => v.Id == visitId);

                if (deleteVisit.OrganiserId == organiserId)
                {
                    context.Visits.Remove(deleteVisit);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
