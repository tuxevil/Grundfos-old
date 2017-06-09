using System;
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
using PartnerNet.Common;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.Templates
{
    public partial class Detalle : System.Web.UI.UserControl
    {
        public int ProductId
        {
            get { return (ViewState["ProductId"] != null) ? (int)ViewState["ProductId"] : 0; }
            set { ViewState["ProductId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void LoadInformation()
        {
            LoadInformation(ProductId);
        }

        private void LoadInformation(int id)
        {
            Product prod = ControllerManager.Product.GetById(id);
            
            TransactionHistoryWeekly tran = ControllerManager.TransactionHistoryWeekly.GetIndividualInfo(prod.Id, Config.CurrentWeek, Config.CurrentDate.Year);
            Grundfos.ScalaConnector.Product prodscala = Grundfos.ScalaConnector.ControllerManager.Product.GetProductInfo(prod.ProductCode);
            IList<TransactionHistoryWeekly> sales = ControllerManager.TransactionHistoryWeekly.GetSalesTotal(prod, Config.CurrentWeek, Config.CurrentDate.Year);
            IList<ProductStatisticWeekly> fullstats = ControllerManager.ProductStatisticWeekly.GetProductFullInfo(prod.Id);

           //seguridad por si no existen registros PERO DEBEN EXISTIR AUNQUE SEA EN 0!!!!!!!!
            if (tran == null)
                tran = new TransactionHistoryWeekly();
            //termina la seguridad
            Label25.Text = id.ToString();
            Label2.Text = prodscala.PurchasePrice.ToString("#,##0.00");
            Label4.Text = "% " + prodscala.OverCost.ToString();
            Label6.Text = "U$S " + prodscala.SalePrice.ToString("#,##0.00");
            Label27.Text = "$ " + prodscala.StandardCost.ToString("#,##0.000");
            Label9.Text = sales[3].Sale.ToString();
            Label11.Text = sales[2].Sale.ToString();
            Label13.Text = sales[1].Sale.ToString();
            Label15.Text = sales[0].Sale.ToString();
            Label18.Text = fullstats[4].Sale.ToString();
            Label20.Text = fullstats[3].Sale.ToString();
            Label22.Text = fullstats[2].Sale.ToString();
            Label24.Text = fullstats[0].Sale.ToString();
            if (prodscala.PurchaseCurrency == "00")
                Label2.Text = "$ " + prodscala.PurchasePrice.ToString("#,##0.00");
            else if(prodscala.PurchaseCurrency == "01")
                Label2.Text = "U$S " + prodscala.PurchasePrice.ToString("#,##0.00");
            else if(prodscala.PurchaseCurrency == "02")
                Label2.Text = "€ " + prodscala.PurchasePrice.ToString("#,##0.00");


            IList<BreakDown> despiece = ControllerManager.BreakDown.GetBreakDown(prod);
            if (despiece.Count > 1)
                ImageButton2.Visible = true;

        }

        protected void Button1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/forecast/default.aspx?Id=" + Label25.Text);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/breakdown/default.aspx?Id=" + Label25.Text);
        }

    }
}