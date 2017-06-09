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
using PartnerNet.Business;

namespace Grundfos.StockForecast
{
    public partial class RepLevelAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvRepositionLevel.DataSource = ControllerManager.ProductRepositionLevelHistory.GetAll();
            if(!IsPostBack)
            {
                gvRepositionLevel.DataBind();
            }
            
            
        }

        protected void gvRepositionLevel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRepositionLevel.PageIndex = e.NewPageIndex;
            gvRepositionLevel.DataBind();
        }
    }
}
