using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Transform;
using PartnerNet.Domain;
using ProjectBase.Data;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Globalization;
using System.Drawing;

namespace PartnerNet.Business
{
    public class AlertProductController : AbstractNHibernateDao<AlertProduct, int>
    {
        public AlertProductController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<AlertProduct> ShowAlert3()
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Type", 1));
            return crit.List<AlertProduct>() as List<AlertProduct>;
        }

        public List<AlertProduct> ShowAlert5()
        {
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Type", 2));
            return crit.List<AlertProduct>() as List<AlertProduct>;
        }

        public List<AlertProduct> ShowAlert(string column, string order, int type)
        {
            bool boolorder = true;
            if (order == "desc")
                boolorder = false;
           
            ICriteria crit = GetCriteria();
            crit.Add(new EqExpression("Type", type));
            crit.AddOrder(new Order(column, boolorder));

            return crit.List<AlertProduct>() as List<AlertProduct>;
        }

        public void CleanAlertProduct()
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_alertproduct_clean");
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
            bf.DataField = "ProductCode";
            bf.HeaderText = "Producto";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            if(type == 1)
            {
                bf = new BoundField();
                bf.ItemStyle.Width = Unit.Pixel(95);
                bf.DataField = "StandardCost";
                bf.HeaderText = "Costo Standard";
                bf.HtmlEncode = false;
                grdProductList.Columns.Add(bf);

                bf = new BoundField();
                bf.ItemStyle.Width = Unit.Pixel(295);
                bf.DataField = "Subtotal";
                bf.HeaderText = "SubTotal";
                bf.HtmlEncode = false;
                grdProductList.Columns.Add(bf);
            }
            else
            {
                bf = new BoundField();
                bf.ItemStyle.Width = Unit.Pixel(95);
                bf.DataField = "NegativeDate";
                bf.HeaderText = "Fecha de Quiebre";
                bf.HtmlEncode = false;
                grdProductList.Columns.Add(bf);
            }

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Quantity";
            bf.HeaderText = "Cantidad";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            #endregion

            if (!string.IsNullOrEmpty(column))
                grdProductList.DataSource = ShowAlert(column, order, type);
            else
            {
                if (type == 1)
                    grdProductList.DataSource = ShowAlert3();
                else
                    grdProductList.DataSource = ShowAlert5();


            }
            grdProductList.DataBind();

            HttpContext.Current.Response.Clear();
            if(type == 1)
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=ProductosConStockNegativo.xls"));
            else
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=StockQueCaeraDelSafety.xls"));

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
                if (type == 1)
                {
                    table.Rows[pos].Cells[1].Text = "$ " + table.Rows[pos].Cells[1].Text;
                    table.Rows[pos].Cells[2].Text = "$ " + table.Rows[pos].Cells[2].Text;
                    table.Rows[pos].Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    table.Rows[pos].Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    table.Rows[pos].Cells[3].HorizontalAlign = HorizontalAlign.Left;
                }
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
