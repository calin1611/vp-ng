using System.Collections.Generic;

namespace VisitsPlannerModel.Repository
{
    public interface IVisitsRepository
    {
        IList<VisitDto> GetVisits();
        void AddVisit(VisitDto visitDto);
        IList<VisitDto> GetVisitsFromCurrentMonth(int id);
        List<VisitDto> GetVisitsFromCurrentWeek(int id);
        List<VisitDto> VisitsWithAgendaItemsAssigned(int userId);
        IList<VisitDto> MyVisits(int id);
    }
}
