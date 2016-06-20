using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using log4net;

namespace WebAPI.Services
{
    public class PerformanceMonitorService : IDisposable
    {
        private MethodBase _methodName;
        private static ILog Log;
        private Stopwatch watch;

        public PerformanceMonitorService(MethodBase methodName)
        {
            _methodName = methodName;
            Log = LogManager.GetLogger(_methodName.DeclaringType);
            watch = Stopwatch.StartNew();            
        }

        public void Dispose()
        {
            watch.Stop();
            Log.Debug("STOPWATCH from " + _methodName.Name + "(). Duration: " + watch.Elapsed);     
        }
    }
}