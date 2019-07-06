using MyWindowsService.Logs;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyWindowsService
{
    public partial class TimingService : ServiceBase
    {
        private static System.Timers.Timer timer;
        Logger timingLogger;

        public TimingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var logger = NLogHelper.GetFileLogger(LogNames.ServiceLog);
            logger.Info("TimingService Start");

            timingLogger = NLogHelper.GetFileLogger(LogNames.TimingLog);
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            timingLogger.Info("Timing");
        }

        protected override void OnStop()
        {
            var logger = NLogHelper.GetFileLogger(LogNames.ServiceLog);
            logger.Info("TimingService Stop");
        }
    }
}
