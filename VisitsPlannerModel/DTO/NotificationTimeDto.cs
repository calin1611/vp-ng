using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel.DTO
{
    public class NotificationTimeDto
    {
        public int AgendaItemId { get; set; }
        public DateTime Time { get; set; }
        public int? EmployeeId { get; set; }
        public int? Type { get; set; }
    }
}
