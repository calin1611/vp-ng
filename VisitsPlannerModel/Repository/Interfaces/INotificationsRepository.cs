using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel.Repository.Interfaces
{
    public interface INotificationsRepository
    {
        void AddNotificationPreference(NotificationPreferenceDto notificationPreference);
        List<NotificationPreferenceDto> GetNotificationTimesForAgendaItem(int agendaItemId);
    }
}
