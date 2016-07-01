using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel
{
    public class AgendaItemsRelatedDataDto
    {
        public IList<LocationDto> Location { get; set; }
        public IList<VisitTypeDto> VisitType { get; set; }

    }
}
