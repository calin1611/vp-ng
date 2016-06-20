using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel
{
    public interface IHistory
    {
        Nullable<int> CreatedBy { get; set; }
        Nullable<System.DateTime> CreatedOn { get; set; }
        Nullable<int> ModifiedBy { get; set; }
        Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
