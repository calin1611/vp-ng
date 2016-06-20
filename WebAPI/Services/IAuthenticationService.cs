using VisitsPlannerModel.Repository;
using VisitsPlannerModel;

namespace WebAPI.Services
{
    public interface IAuthenticationService
    {
        EmployeeDto Authenticate(string user, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public EmployeeDto Authenticate(string email, string password)
        {
            var repo = new EmployeesRepository();
            EmployeeDto returnedEmployee = repo.Authenticate(email);

            if (returnedEmployee != null)
            {
                if (returnedEmployee.Password == password)
                {
                    return returnedEmployee;
                }
            }

            return null;
        }
    }
}