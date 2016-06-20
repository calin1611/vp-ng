using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisitsPlannerModel.Repository;

namespace WebAPI.Services
{
    public class ReportingService
    {
        private EmployeesRepository _employeesRepository;
        private VisitsRepository _visitsRepository;


        public void GetVisitsFromCurrentMonth()
        {
            _visitsRepository = new VisitsRepository();
            var visitsFromCurrentMonth = _visitsRepository.GetVisitsFromCurrentMonth();
        }
    }
}