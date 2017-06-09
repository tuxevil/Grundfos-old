using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using ParnerNet.Integrators.Scala;

namespace PartnerNet.Services.Forecast
{
	public class ForecastService : ServiceBase
	{
		private Timer timer;

        public ForecastService()
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
			IProcessor processor = new ForecastProcessor();
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
            this.ServiceName = "Grundfos StockForecast Processor";

        }
	}
}