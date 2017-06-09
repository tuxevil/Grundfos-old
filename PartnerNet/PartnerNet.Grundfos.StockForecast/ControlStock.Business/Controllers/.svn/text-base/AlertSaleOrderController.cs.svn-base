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
using System.Web;
using System.IO;
using System.Web.UI;
using System.Drawing;
using System.Globalization;

namespace PartnerNet.Business
{
    public class AlertSaleOrderController : AbstractNHibernateDao<AlertSaleOrder, int>
    {
        public AlertSaleOrderController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<AlertSaleOrder> ShowAlert4()
        {
            ICriteria crit = GetCriteria();
            crit.AddOrder(new Order("WayOfDelivery", true));
            return crit.List<AlertSaleOrder>() as List<AlertSaleOrder>;
        }

        public List<AlertSaleOrder> ShowAlert(string column, string order)
        {
            bool boolorder = true;
            if (order == "desc")
                boolorder = false;

            ICriteria crit = GetCriteria();

            crit.AddOrder(new Order(column, boolorder));

            return crit.List<AlertSaleOrder>() as List<AlertSaleOrder>;
        }

        public void CleanAlertSaleOrder()
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_alertsaleorder_clean");
            q.UniqueResult();
        }

        public void ExportToExcel(string column, string order)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-us");// Thread.CurrentThread.CurrentUICulture;

            GridView grdProductList = new GridView();
            grdProductList.AutoGenerateColumns = false;

            #region Columns
            BoundField bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "SaleOrderCode";
            bf.HeaderText = "OV";
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
            bf.DataField = "PurchaseOrderCode";
            bf.HeaderText = "OC";
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
            bf.DataField = "CustomerCode";
            bf.HeaderText = "Cliente";
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
            bf.DataField = "SaleOrderDeliveryDate";
            bf.HeaderText = "Entrega OV";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "PurchaseOrderArrivalDate";
            bf.HeaderText = "Llegada OC";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "OrderDate";
            bf.HeaderText = "Creaci&oacute;n";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);
            #endregion

            if (!string.IsNullOrEmpty(column))
                grdProductList.DataSource = ShowAlert(column, order);
            else
                grdProductList.DataSource = ShowAlert4();

            grdProductList.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=OVNoCumplimentadas.xls"));
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
                table.Rows[pos].Cells[0].Attributes.Add("class", "cF");
                table.Rows[pos].Cells[1].Attributes.Add("class", "cF");
                table.Rows[pos].Cells[2].Attributes.Add("class", "cF");
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
