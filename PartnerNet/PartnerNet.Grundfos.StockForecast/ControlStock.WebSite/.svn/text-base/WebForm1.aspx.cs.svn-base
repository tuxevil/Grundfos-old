using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;

namespace Grundfos.StockForecast
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ControllerManager.AlertReposition.CleanAlertReposition();
            List<AlertReposition> lstrepotmp = ControllerManager.TransactionHistoryWeekly.GetAlert6(Config.CurrentWeek, Config.CurrentDate.Year) as List<AlertReposition>;
            
            int i = 1;
            int i2 = 0;
            foreach (AlertReposition alert in lstrepotmp)
            {
                if (alert.Result != 0)
                {
                    ControllerManager.AlertReposition.Save(alert);
                    i2++;
                }
                i++;
            }
        }
    }
}
