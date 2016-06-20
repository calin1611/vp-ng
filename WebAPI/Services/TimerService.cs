using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Timers;
using VisitsPlannerModel.DTO;

namespace WebAPI.Services
{
    public static class TimerService
    {
        private static System.Timers.Timer aTimer;

        public static void SetTimer()
        {
            // Create a timer with a 60 second interval.
            aTimer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += SendEmails;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static IList<NotificationTimeDto> GetNextNotifications()
        {
            var service = new NotificationService();
            var notificationTimes = service.GetNotificationTimes();

            TimeSpan difference = notificationTimes[0].Time - DateTime.Now;
            if (difference.TotalMinutes < 1 && difference.TotalMinutes > -1)
            {
                IList<NotificationTimeDto> notificationsToBeSent = new List<NotificationTimeDto>();
                foreach (NotificationTimeDto t in notificationTimes)
                {
                    if (t.Time == notificationTimes[0].Time)
                    {
                        notificationsToBeSent.Add(t);
                    }
                    else
                    {
                        break;
                    }
                }
                return notificationsToBeSent;
            }
            return null;
        }

        private static void SendEmails(Object source, ElapsedEventArgs e)
        {
            var notificationTimes = GetNextNotifications();

            if (notificationTimes != null)
            {
                foreach (var notification in notificationTimes)
                {
                    
                    MailMessage myMessage = new MailMessage();
                    //subject line
                    myMessage.Subject = "O noua notificare!";
                    //whats going to be in the body
                    myMessage.Body =
                        "Notification Time should be: " + notification.Time +
                        " /n For the agenda item with the ID " + notification.AgendaItemId +
                        " /n For the employee with the ID: " + notification.EmployeeId +
                        " /n The type of notification is: " + notification.Type;
                    //who the message is from
                    //myMessage.From = (new MailAddress("iddproba@gmail.com"));
                    //who the message is to
                    myMessage.To.Add(new MailAddress("calin1611@gmail.com"));

                    NotificationService.SendEmail(myMessage);                
                }

            }

        }
    }
}