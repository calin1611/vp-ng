//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisitsPlannerModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificationPreference
    {
        public NotificationPreference()
        {
            this.AgendaItemsNotifications = new HashSet<AgendaItemsNotification>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Time { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual ICollection<AgendaItemsNotification> AgendaItemsNotifications { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
