using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitsPlannerModel;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            GetEmployees();
        }

        private static void GetEmployees()
        {
            using(var context = new VPEntities())
            {
                var customers = context.Employees.ToList();
                //Console.WriteLine(customers);
            };
            

        }
    }
}
