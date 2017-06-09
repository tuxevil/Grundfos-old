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
using PartnerNet.Domain;
using PartnerNet.Providers.Stock;
using Product=PartnerNet.Domain.Product;

namespace Grundfos.StockForecast
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/control-panel/");
        }
    }
}
