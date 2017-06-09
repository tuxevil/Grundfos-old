using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Business;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.breakdown
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    GenerateBreakdown(Convert.ToInt32(Request.QueryString["Id"]));
                }
            }
        }

        protected void GenerateBreakdown (int id)
        {
            Product prod = ControllerManager.Product.GetById(id);
            IList<BreakDown> despiece = ControllerManager.BreakDown.GetBreakDown(prod);
            IList<BreakDown2> final = new List<BreakDown2>();

            foreach (BreakDown breakDown in despiece)
            {
                BreakDown2 temp = new BreakDown2();
                temp.Part = ControllerManager.Product.GetById(breakDown.Part);
                temp.Product = ControllerManager.Product.GetById(breakDown.Product);
                temp.Quantity = breakDown.Quantity;

                final.Add(temp);
            }

            Label2.Text = prod.ProductCode;
            Label4.Text = prod.Description;

            repItems.DataSource = final;
            repItems.DataBind();
        }
    }
}
