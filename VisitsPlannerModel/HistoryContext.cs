using System;
using System.Data.Entity;
using System.Linq;

namespace VisitsPlannerModel
{
    class HistoryContext : VPEntities
    {
        public int UserId { get; set; }
        public HistoryContext(int id) : base()
        {
            UserId = id;
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries()
                       .Where(t => t.State == EntityState.Added)
                       .Select(t => t.Entity)
                       .ToArray();
            foreach (var entity in added)
                if (entity is IHistory)
                {
                    var track = entity as IHistory;
                    track.CreatedOn = DateTime.Now;
                    track.CreatedBy = UserId;
                    track.ModifiedOn = DateTime.Now;
                    track.ModifiedBy = UserId;
                }
            var modified = this.ChangeTracker.Entries()
                       .Where(t => t.State == EntityState.Modified)
                       .Select(t => t.Entity)
                       .ToArray();
            foreach (var entity in modified)
                if (entity is IHistory)
                {
                    var track = entity as IHistory;
                    track.ModifiedOn = DateTime.Now;
                    track.ModifiedBy = UserId;
                }

            return base.SaveChanges();
        }
    }
}
