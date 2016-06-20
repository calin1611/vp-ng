using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using VisitsPlannerModel.Repository;
using VisitsPlannerModel;
using WebAPI.Helpers;

namespace WebAPI.Services
{
    public class EmployeesService
    {
        private readonly EmployeesRepository _repo;
        public EmployeesService()
        {
            _repo = new EmployeesRepository();
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IList<EmployeeDto> GetAll()
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + "() traced.");
            return _repo.GetEmployees();
        }

        public void Add(EmployeeDto employee)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + 
                "() traced. Added employee: " + 
                employee.FirstName + " " + 
                employee.LastName);
            _repo.InsertEmployee(employee);
        }

        public void AddNotificationPreference(NotificationPreferenceDto notificationPreference)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + 
                "() traced. Added Notification Preference for: " + notificationPreference.EmployeeId);

            var notificationsRepository = new NotificationsRepository();
            notificationsRepository.AddNotificationPreference(notificationPreference);
        }

        internal int GetIdByEmail(string email)
        {
            Log.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + 
                "() traced. Got ID for: " + email);

            int id = _repo.GetEmployeeIdByEmail(email);
            if (id != 0)
            {
                return id;
            }
            else return 0; //sau poate o exceptie
        }

        public EmployeeDto GetEmplyoeeByEmail(string email)
        {
            EmployeeDto employee = _repo.GetEmployeeByEmail(email);
            return employee; //sau poate o exceptie
        }

        public EmployeeDto GetEmplyoeeById(int id)
        {
            EmployeeDto employee = _repo.GetEmployeeById(id);
            return employee; //sau poate o exceptie
        }


//        internal void Authenticate(string credentials)
//        {
//            DecoderClass.DecodeCredentials(credentials)
//            _repo.Authenticate()
//        }
    }
}