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

namespace Grundfos.StockForecast.product_list
{
    public partial class report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            {
                DataSet prueba = (DataSet) Session["dsListProductos"];
                ReportDocument rep = new ReportDocument();
                rep.Load(Server.MapPath("/reports/rptListProductos.rpt"));
                rep.SetDataSource(prueba);
                CrystalReportViewer1.ReportSource = rep;
                CrystalReportViewer1.DataBind();
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet prueba = (DataSet)Session["dsListProductos"];
            ReportDocument rep = new ReportDocument();
            if (RadioButtonList1.SelectedValue == "0")
            {
                rep.Load(Server.MapPath("/reports/rptListProductos.rpt"));
            }
            else if (RadioButtonList1.SelectedValue == "1")
            {
                rep.Load(Server.MapPath("/reports/rptListProductos-det.rpt"));
            }
            rep.SetDataSource(prueba);
            CrystalReportViewer1.ReportSource = rep;
            CrystalReportViewer1.DataBind();
        }
    }
}
