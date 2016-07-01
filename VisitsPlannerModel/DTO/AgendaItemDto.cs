using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel
{
    public class AgendaItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int VisitId { get; set; }
        public int LocationId { get; set; }
        public DateTime? Date { get; set; }
        public int VisitTypeId { get; set; }
        public string Outcome { get; set; }

        public LocationDto Location { get; set; }
        public VisitTypeDto VisitType { get; set; }

//        public Nullable<int> CreatedBy { get; set; }
//        public Nullable<System.DateTime> CreatedOn { get; set; }
//        public Nullable<int> ModifiedBy { get; set; }
//        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
