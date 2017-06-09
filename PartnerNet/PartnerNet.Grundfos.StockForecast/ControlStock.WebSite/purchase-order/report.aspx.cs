using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;

namespace Grundfos.StockForecast.purchase_order
{
    public partial class report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet prueba = (DataSet)Session["dsOrdCompras"];
            ReportDocument rep = new ReportDocument();
            rep.Load(Server.MapPath("/reports/rptOrdCompras.rpt"));
            rep.SetDataSource(prueba);
            CrystalReportViewer1.ReportSource = rep;
            CrystalReportViewer1.DataBind();
        }
    }
}
