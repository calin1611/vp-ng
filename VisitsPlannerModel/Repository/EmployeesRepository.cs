using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace VisitsPlannerModel.Repository
{
    public class EmployeesRepository: IEmployesRepository
    {

        public int GetEmployeeIdByEmail(string email)
        {
            using (var context = new VPEntities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee != null)
                {
                    return employee.Id;
                }
                else return 0;
            }
        }

        public EmployeeDto GetEmployeeByEmail(string email)
        {
            using (var context = new VPEntities())
            {
                EmployeeDto returnedEmployee = new EmployeeDto();
                var employee = context.Employees.FirstOrDefault(e => e.Email == email);
                if (employee != null)
                {
                    returnedEmployee.Id = employee.Id;
                    returnedEmployee.Email = employee.Email;
                    returnedEmployee.Role = employee.Role;
                }
                return returnedEmployee;
            }
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            using (var context = new VPEntities())
            {
                EmployeeDto returnedEmployee = new EmployeeDto();
                var employee = context.Employees.FirstOrDefault(e => e.Id == id);

                if (employee != null)
                {
                    returnedEmployee.Id = employee.Id;
                    returnedEmployee.FirstName = employee.FirstName;
                    returnedEmployee.LastName = employee.LastName;
                    returnedEmployee.Email = employee.Email;
                    returnedEmployee.Role = employee.Role;
                }
                return returnedEmployee;
            }
        }

        public void InsertEmployee(EmployeeDto newEmployee)
        {
            using (var context = new VPEntities())
            {
                var insertEmployee = new Employee
                {
                    FirstName = newEmployee.FirstName,
                    LastName = newEmployee.LastName,
                };

                context.Employees.Add(insertEmployee);
                context.SaveChanges();
            }
        }

        public EmployeeDto Authenticate(string email)
        {
            using (var context = new VPEntities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == email);

                if (employee != null)
                {
                    EmployeeDto employeeDto = new EmployeeDto
                    {
                        Id = employee.Id,
                        Email = employee.Email,
                        CreatedOn = employee.CreatedOn,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        ModifiedOn = employee.ModifiedOn,
                        Password = employee.Password,
                        Role = employee.Role
                    };
                    return employeeDto;
                }
                else
                {
                    
                return null;
                }
            }
        }

        public IEnumerable<EmployeeDto> GetEmployeesWithNotificationPreferences()
        {
            using (var context = new VPEntities())
            {
                //var employeesNotifications = context.Employees.Select(e => e.NotificationPreferences).ToList();
//                var employeesNotifications = context.Employees.Include("NotificationPreferences");
//                var employeesNotifications =
//                    context.EmployeesAgendaItems.Select(eai => eai.Employee).Distinct().ToList().Select(e => e.NotificationPreferences).ToList();
                var returnList = new List<EmployeeDto>();
                var employeesNotifications =
                    context.EmployeesAgendaItems.Select(eai => eai.Employee).Distinct();
                foreach (var employeeNotification in employeesNotifications)
                {
                   
                    returnList.Add(new EmployeeDto
                    {
                        Id = employeeNotification.Id,
                        FirstName = employeeNotification.FirstName,
                        LastName = employeeNotification.LastName,
                        Email = employeeNotification.Email,
                        Role = employeeNotification.Role,
                        NotificationPreferences = employeeNotification.NotificationPreferences,
                    });
                }

                return returnList;
            }
        }
        
        //Possibly useless

        public IList<EmployeeDto> GetEmployees()
        {
            using (var context = new VPEntities())
            {
                var employees = context.Employees.ToList();
                var returnList = new List<EmployeeDto>();

                foreach (var employee in employees)
                {
                    returnList.Add(new EmployeeDto 
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName
                    });
                }

                return returnList;
            };
        }

        public EmployeeDto GetEmployee(int employeeId)
        {
            using (var context = new VPEntities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Id == employeeId);

                Mapper.CreateMap<Employee, EmployeeDto>();
                EmployeeDto employeeDto = Mapper.Map<Employee, EmployeeDto>(employee);

                return employeeDto;
            }
        }

        public IList<CustomerDto> GetCustomers()
        {
            using ( var context = new VPEntities())
            {
                var customers = context.Customers.ToList();
                var returnList = new List<CustomerDto>();
                foreach (var customer in customers)
                {
                    returnList.Add(new CustomerDto
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Phone = customer.Phone,
                        Email = customer.Email
                    });
                }
                return returnList;
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var context = new VPEntities())
            {
                Employee deleteEmp = context.Employees.FirstOrDefault(e => e.Id == id);
                
                context.Employees.Remove(deleteEmp);
                context.SaveChanges();
            }
        }

        public void UpdateEmployee(int id, EmployeeDto newEmployee)
        {
            using (var context = new VPEntities())
            {
                var existingEmployee = context.Employees.FirstOrDefault(e => e.Id == id);

                existingEmployee.FirstName = newEmployee.FirstName;
                existingEmployee.LastName = newEmployee.LastName;
                existingEmployee.ModifiedOn = newEmployee.ModifiedOn;

                context.SaveChanges();
            }
        }
    }
}
