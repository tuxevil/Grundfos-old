using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Domain;
using ProjectBase.Data;
using System.Threading;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Globalization;

namespace PartnerNet.Business
{
    public class AlertPurchaseOrderController : AbstractNHibernateDao<AlertPurchaseOrder, int>
    {
        public AlertPurchaseOrderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<AlertPurchaseOrder> ShowAlert1()
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Type", AlertPurchaseOrderType.Alert1));
            crit.AddOrder(new Order("Destination", false));
            crit.AddOrder(new Order("ArrivalDate", true));
            return crit.List<AlertPurchaseOrder>() as List<AlertPurchaseOrder>;
        }

        public List<AlertPurchaseOrder> ShowAlert2()
        {
            ICriteria crit = GetCriteria();

            crit.Add(new EqExpression("Type", AlertPurchaseOrderType.Alert2));
            crit.AddOrder(new Order("GAP", false));
            crit.AddOrder(new Order("WayOfDelivery", true));

            return crit.List<AlertPurchaseOrder>() as List<AlertPurchaseOrder>;
        }

        public List<AlertPurchaseOrder> ShowAlert(string column, string order, int type)
        {
            bool boolorder = true;
            if(order == "desc")
                boolorder = false;
            AlertPurchaseOrderType apoType = AlertPurchaseOrderType.Alert1;
            if (type == 2)
                apoType = AlertPurchaseOrderType.Alert2;

            ICriteria crit = GetCriteria();

            crit.Add(new EqExpression("Type", apoType));
            crit.AddOrder(new Order(column, boolorder));

            return crit.List<AlertPurchaseOrder>() as List<AlertPurchaseOrder>;
        }

        public void CleanAlertPurchaseOrder()
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_alertpurchaseorder_clean");
            q.UniqueResult();
        }

        public void ExportToExcel(string column, string order, int type)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-us");// Thread.CurrentThread.CurrentUICulture;

            GridView grdProductList = new GridView();
            grdProductList.AutoGenerateColumns = false;

            #region Columns
            BoundField bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "PurchaseOrderCode";
            bf.HeaderText = "OC";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "PurchaseOrderItemCode";
            bf.HeaderText = "Producto";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "PurchaseOrderProviderCode";
            bf.HeaderText = "Codigo de Proveedor";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(295);
            bf.DataField = "PurchaseOrderProviderName";
            bf.HeaderText = "Proveedor";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Quantity";
            bf.HeaderText = "Cantidad";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "GAP";
            bf.HeaderText = "GAP";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "WayOfDelivery";
            bf.HeaderText = "Modo Envio";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Destination";
            bf.HeaderText = "Tipo";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "ArrivalDate";
            bf.HeaderText = "Llegada";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "CalculatedArrivalDate";
            bf.HeaderText = "Llegada (Calculada)";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);
            #endregion
            if (!string.IsNullOrEmpty(column))
                grdProductList.DataSource = ShowAlert(column, order, type);
            else
            {
                if (type == 1)
                    grdProductList.DataSource = ShowAlert1();
                else
                    grdProductList.DataSource = ShowAlert2();


            }
            grdProductList.DataBind();

            HttpContext.Current.Response.Clear();
            if(type == 1)
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=OCConfirmadasYNoEntregadas.xls"));
            else
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=OCNoConfirmadas.xls"));

            HttpContext.Current.Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
            table.GridLines = grdProductList.GridLines;

            //Set the Cell format 
            string codeFormat = @"<style>.cF  { mso-number-format:'\@'; }</style>";

            //  add the header row to the table
            if (grdProductList.HeaderRow != null)
            {
                PrepareControlForExport(grdProductList.HeaderRow);
                table.Rows.Add(grdProductList.HeaderRow);
                table.Rows[0].ForeColor = Color.FromArgb(102, 102, 102);

                for (int i = 0; i < table.Rows[0].Cells.Count; i++)
                    table.Rows[0].Cells[i].BackColor = Color.FromArgb(225, 224, 224);
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in grdProductList.Rows)
            {
                PrepareControlForExport(row);
                int pos = table.Rows.Add(row);
                table.Rows[pos].Cells[1].Attributes.Add("class", "cF");
                table.Rows[pos].Cells[2].Attributes.Add("class", "cF");
                table.Rows[pos].Cells[3].Width = 300;
            }

            //  render the table into the htmlwriter
            table.RenderControl(htw);

            //  render the htmlwriter into the response adding de style
            HttpContext.Current.Response.Write(codeFormat + sw.ToString());
            HttpContext.Current.Response.End();
        }

        private void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];


                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }
    }
}
