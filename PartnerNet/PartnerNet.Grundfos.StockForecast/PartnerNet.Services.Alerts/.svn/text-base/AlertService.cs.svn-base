using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using ParnerNet.Integrators.Scala;

namespace PartnerNet.Services.Alerts
{
    public class AlertService : ServiceBase
    {
        private Timer timer;

        public AlertService()
        {
            InitializeComponent();
        }

		protected override void OnStart(string[] args)
		{
#if DEBUG
			Debugger.Launch();
			Debugger.Break();
#endif

			timer = new Timer(
				OnElapsedTime,
				this,
				0,
				Convert.ToInt32(ConfigurationManager.AppSettings["TimeSlice"]));

            EventLog.WriteEntry(ServiceName, ServiceName + " Started.");
		}

		private void OnElapsedTime(object sender)
		{
			IProcessor processor = new AlertsProcessor();
			string errors;
			if (!processor.Execute(out errors))
				EventLog.WriteEntry(processor.Name, errors, EventLogEntryType.Error);
		}

		protected override void OnStop()
		{
			timer.Dispose();
		}

        private void InitializeComponent()
        {
            // 
            // ForecastService
            // 
            this.ServiceName = "Grundfos Alerts Processor";

        }
    }
}
