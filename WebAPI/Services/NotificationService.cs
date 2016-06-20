using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using VisitsPlannerModel;
using VisitsPlannerModel.DTO;
using VisitsPlannerModel.Repository;

namespace WebAPI.Services
{
    public class NotificationService
    {
        public IList<NotificationPreferenceDto> GetNotificationTimesForAgendaItem(int agendaItemId)
        {
            var repo = new NotificationsRepository();
            var notificationTimesList = repo.GetNotificationTimesForAgendaItem(agendaItemId);
            return notificationTimesList.OrderBy(ntl => ntl.Time).ToList();
        }

        public IList<NotificationTimeDto> GetNotificationTimes()
        {
            var repo = new NotificationsRepository();
            var notificationTimesList = repo.GetNotificationTimes();
            return notificationTimesList.OrderBy(ntl => ntl.Time).ToList();
        }

        public IList<NotificationTimeDto> ComputeNotificationTimeForAgendaItem(int agendaItemId)
        {
            var agendaItemRepo = new AgendaItemsRepository();
            var agendaItem = agendaItemRepo.GetAgendaItemById(agendaItemId);
            var agendaItemTime = agendaItem.Date.Value;

            var notificationTimes = GetNotificationTimesForAgendaItem(agendaItemId);

            IList<NotificationTimeDto> notificationsTimesList = new List<NotificationTimeDto>();

            foreach (var pendingNotificationsDto in notificationTimes)
            {
                var time = agendaItemTime.AddMinutes(-(double)pendingNotificationsDto.Time.Value);
                notificationsTimesList.Add(new NotificationTimeDto()
                {
                    AgendaItemId = pendingNotificationsDto.Id,
                    Time = time,
                    EmployeeId = pendingNotificationsDto.EmployeeId,
                    Type = pendingNotificationsDto.Type
                });
            }
            return notificationsTimesList;
        }

        public void FindNotificationTimes()
        {
            var agendaItemsRepo = new AgendaItemsRepository();
            var employeesRepo = new EmployeesRepository();

            var nextAgendaItems = agendaItemsRepo.GetNextAgendaItems();
            var employeesWithNotifications = employeesRepo.GetEmployeesWithNotificationPreferences();
            var nextNotifications = new List<NotificationTimeDto>();
        }

//public static void SendEmail(MailMessage myMessage)
        public static void SendEmail(MailMessage myMessage)

        {
            //Mutat din controller:
            //MailMessage myMessage = new MailMessage();
            ////subject line
            //myMessage.Subject = "Subiectul meu 2.5";
            ////whats going to be in the body
            //myMessage.Body = "Your Body Info";
            ////who the message is from
            //myMessage.From = (new MailAddress("iddproba@gmail.com"));
            ////who the message is to
            //myMessage.To.Add(new MailAddress("calin1611@gmail.com"));

            //NotificationService.SendEmail(myMessage);
            //---------^

            //myMessage.From = EmailData.From;
            

            //sends the message
            SmtpClient mySmtpClient = new SmtpClient {EnableSsl = true};

            mySmtpClient.Send(myMessage);
        }
    }
}