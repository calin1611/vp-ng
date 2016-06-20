using System;
using System.Collections.Generic;

namespace VisitsPlannerModel
{
    public class VisitDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> OrganiserId { get; set; }
        public string Outcome { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }

//        public bool? UserHasAgendaItemsAssigned { get; set; }
        public IEnumerable<AgendaItemDto> AgendaItems;
        public EmployeeShareableDto EmployeeData { get; set; }
//        public string OrganizerFirstName { get; set; }
//        public string OrganizerLastName { get; set; }
    }
}
