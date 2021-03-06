﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel.Repository.Interfaces
{
    public interface IAgendaItemsRepository
    {
        AgendaItemDto AddAgendaItem(AgendaItemDto agendaItem);
        bool DeleteAgendaItem(int agendaItemId);
        AgendaItemDto GetAgendaItemById(int id);
        IList<AgendaItemDto> GetAllAgendaItems();
        IEnumerable<AgendaItemDto> GetAgendaItemsForVisit(int visitId);
        IList<int> GetAgendaItemsIdsWhereResponsable(int employeeId);
    }
}
