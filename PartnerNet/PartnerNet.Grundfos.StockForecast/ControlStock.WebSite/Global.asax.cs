using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Grundfos.StockForecast
{

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
#if RELEASE        
    	string error = Server.GetLastError().ToString();

    	string eventLogName = "FundProPerformance";
        if (!EventLog.SourceExists(eventLogName))
        	EventLog.CreateEventSource(eventLogName, eventLogName);

		EventLog log = new EventLog();
    	log.Source = eventLogName;
    	log.WriteEntry(error, EventLogEntryType.Error);


		Mailing.AlertError( error );
#endif
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Check if user is logged in
            //if (Membership.GetUser())
            // Saves data in Session

        }

        void Session_End(object sender, EventArgs e)
        {
        }
    }
}