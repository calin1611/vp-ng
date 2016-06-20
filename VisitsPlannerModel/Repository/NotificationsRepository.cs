using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using AutoMapper;
using VisitsPlannerModel.DTO;
using VisitsPlannerModel.Repository.Interfaces;

namespace VisitsPlannerModel.Repository
{
    public class NotificationsRepository : INotificationsRepository
    {
        public void AddNotificationPreference(NotificationPreferenceDto notificationPreference)
        {
            using (var context = new VPEntities())
            {
                var insertNotificationPreference = new NotificationPreference
                {
                    Type = notificationPreference.Type,
                    Time = notificationPreference.Time,
                    EmployeeId = notificationPreference.EmployeeId,
                };

                context.NotificationPreferences.Add(insertNotificationPreference);
                context.SaveChanges();
            }
        }

        public List<NotificationPreferenceDto> GetNotificationTimesForAgendaItem(int agendaItemId)
        {
            using (var context = new VPEntities())
            {
                var agendaItem = context.AgendaItems.FirstOrDefault(ai => ai.Id == agendaItemId);
                var employees = context.EmployeesAgendaItems.Where(eai => eai.AgendaItemId == agendaItem.Id).ToList();
                var notificationPreferencesList = new List<NotificationPreference>();
                
                foreach (var employee in employees)
                {
                    var employeeNotificationPreferences = context.NotificationPreferences.Where(np => np.EmployeeId == employee.EmployeeId).ToList();

                    foreach (var employeeNotificationPreference in employeeNotificationPreferences)
                    {
                        notificationPreferencesList.Add(employeeNotificationPreference);
                    }
                }

                List<NotificationPreferenceDto> notificationPreferencesListDto = new List<NotificationPreferenceDto>();

                foreach (var notificationPreference in notificationPreferencesList)
                {
                    notificationPreferencesListDto.Add(new NotificationPreferenceDto()
                    {
                        Id = notificationPreference.Id,
                        Type = notificationPreference.Type,
                        Time = notificationPreference.Time,
                        EmployeeId = notificationPreference.EmployeeId
                    });
                }

                return notificationPreferencesListDto;
            }
        }



        public List<NotificationTimeDto> GetNotificationTimes()
        {
            using (var context = new VPEntities())
            {
                var agendaItemsEmployeesNotPref = //rename
                    context.AgendaItems
                        .Where(ai => ai.Date.Value >= DateTime.Now)
                        .Include(ai => ai.EmployeesAgendaItems
//                            .Where(eai => eai.AgendaItemId == ai.Id)
                            .Select(eai => eai.Employee.NotificationPreferences))
                        .ToList();

                IList<NotificationTimeDto> listNotificationTimeDto = new List<NotificationTimeDto>();
                foreach (var agendaItem in agendaItemsEmployeesNotPref)
                {
                    foreach (var employeesAgendaItem in agendaItem.EmployeesAgendaItems)
                    {
                        var notificationPreferences = employeesAgendaItem.Employee.NotificationPreferences;

                        foreach (var notificationPreference in notificationPreferences)
                        {
                            var notificationTime = agendaItem.Date.Value.AddMinutes(-(double)notificationPreference.Time.Value);
                            if(notificationTime >= DateTime.Now)
                            {
                                listNotificationTimeDto.Add(new NotificationTimeDto
                                {
                                    EmployeeId = employeesAgendaItem.EmployeeId,
                                    Time = agendaItem.Date.Value.AddMinutes(-(double)notificationPreference.Time.Value),
                                    AgendaItemId = agendaItem.Id,
                                    Type = notificationPreference.Type
                                });
                                
                            }
                            
                        }
                    }
                    

                }

                return listNotificationTimeDto.OrderBy(lntd => lntd.Time).ToList();
            }
        }

    }
}
