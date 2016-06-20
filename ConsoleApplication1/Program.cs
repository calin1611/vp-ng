using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitsPlannerModel;
using VisitsPlannerModel.Repository;
using AutoMapper;

namespace ConsoleApplication
{
    class Program
    {
        static IEmployesRepository repository = new EmployeesRepository();
        static IVisitsRepository visitsRepository = new VisitsRepository();

        static void Main(string[] args)
        {
            //GetCustomers();

            //Console.WriteLine(GetEmployee().LastName);
            //InsertEmployee();
            //DeleteEmployee();
            GetEmployees();
            //UpdateEmployee(); //nu e gata
            GetEmployees();

            GetVisits();
            //AddVisit();
            //GetVisits();
        }

        private static EmployeeDto GetEmployee()
        {
            Console.WriteLine("Find employee by this ID:");
            int id = int.Parse(Console.ReadLine());
            EmployeeDto employee = repository.GetEmployee(id);
            return employee;
        }

        private static void AddVisit()
        {
            Console.Write("Visit title: ");
            string title = Console.ReadLine();
            Console.Write("Date: ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            Console.Write("Organiser ID: ");
            int organiserId = int.Parse(Console.ReadLine());
            DateTime createdOn = DateTime.Now;

            VisitDto visit = new VisitDto
            {
                Title = title,
                Date = date,
                OrganiserId = organiserId,
                CreatedOn = createdOn
            };
            Console.WriteLine(visit.Date);
            visitsRepository.AddVisit(visit);



            //Mapper.CreateMap<Client, ClientViewModel>();

            //ClientViewModel cvm = Mapper.Map<Client, ClientViewModel>(client);

            //Console.WriteLine(cvm.ClientCode);
            
        }

        private static void UpdateEmployee()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("First name: ");
            string fName = Console.ReadLine();
            Console.Write("Last name: ");
            string lName = Console.ReadLine();

            EmployeeDto newEmployee = new EmployeeDto()
            {
                Id = id,
                FirstName = fName,
                LastName = lName,
                ModifiedOn = DateTime.Now
            };
            repository.UpdateEmployee(id, newEmployee);
        }

        private static void GetVisits()
        {
            Console.WriteLine("Visits:");
            var visits = visitsRepository.GetVisits().ToList();
            foreach (var visit in visits)
            {
                Console.WriteLine("{0}: {1} ({2})", visit.Id, visit.Title, visit.OrganiserId);
            }
            Console.WriteLine("-------");

        }

        private static void DeleteEmployee()
        {
            Console.WriteLine("Id of employee to delete (press '0' to skip):");
            int idToDelete = int.Parse(Console.ReadLine());
            if (idToDelete !=0)
            {
            repository.DeleteEmployee(idToDelete);
            }
            GetEmployees();
        }

        private static void GetCustomers()
        {
            var customers = repository.GetCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
            }
        }

        private static void InsertEmployee()
        {
            var newEmployee = new EmployeeDto()
            {
                FirstName = "George",
                LastName = "Marin"
            };
            repository.InsertEmployee(newEmployee);
            GetEmployees();
        }

        private static void GetEmployees()
        {
            var employees = repository.GetEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine("{0}: {1} {2}", employee.Id, employee.FirstName, employee.LastName);
            }
        }
    }
}
