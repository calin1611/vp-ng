using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsPlannerModel.Repository
{
    public interface IEmployesRepository
    {
        IList<EmployeeDto> GetEmployees();
        IList<CustomerDto> GetCustomers();

        EmployeeDto GetEmployee(int id);
        int GetEmployeeIdByEmail(string email);
        EmployeeDto GetEmployeeByEmail(string email);
        void InsertEmployee(EmployeeDto newEmployee);
        void DeleteEmployee(int id);
        void UpdateEmployee(int id, EmployeeDto newEmployee);

        EmployeeDto Authenticate(string email);
    }
}
