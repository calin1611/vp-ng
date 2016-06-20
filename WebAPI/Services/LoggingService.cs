using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using log4net;

namespace WebAPI.Services
{
    public static class LoggingService
    {
//        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static MethodBase _methodName;
        private static ILog Log;

        static LoggingService()
        {
//            _methodName = methodName;
            Log = LogManager.GetLogger(_methodName.DeclaringType);
        }
        public static void NewLog(string details, MethodBase methodName)
        {
            Log.Debug(_methodName.Name + "() traced." + details);
        }

        public static void NewLog(string beginningDetails, string endingDetails, MethodBase methodName)
        {
            Log.Debug(beginningDetails + methodName.Name + "() traced." + endingDetails);
        }
    }
}