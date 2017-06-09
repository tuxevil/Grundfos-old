using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using PartnerNet.Domain;
using ProjectBase.Data;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Drawing;
using System.Globalization;

namespace PartnerNet.Business
{
    public class AlertRepositionController : AbstractNHibernateDao<AlertReposition, int>
    {
        public AlertRepositionController(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }

        public List<AlertReposition> ShowAlert6()
        {
            
            ICriteria crit = GetCriteria();
            return crit.List<AlertReposition>() as List<AlertReposition>;
        }

        public List<AlertReposition> ShowAlert(string column, string order)
        {
            if (string.IsNullOrEmpty(column))
                return ShowAlert6();

            bool boolorder = true;
            if (order == "desc")
                boolorder = false;

            ICriteria crit = GetCriteria();
            string[] sortfield = new string[2];
            sortfield = column.Split('.');

            if (sortfield.Length < 2)
                crit.AddOrder(new Order(sortfield[0], boolorder));
            else
            {
                ICriteria products = crit.CreateCriteria("Product");
                products.AddOrder(new Order(sortfield[1], boolorder));
            }
            return crit.List<AlertReposition>() as List<AlertReposition>;
        }

        public void CleanAlertReposition()
        {
            IQuery q = NHibernateSession.GetNamedQuery("sp_alertreposition_clean");
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
            bf.DataField = "ProductCode";
            bf.HeaderText = "Producto";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "ProductName";
            bf.HeaderText = "Descripci&oacute;n";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Group";
            bf.HeaderText = "Grupo";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Sales";
            bf.HeaderText = "VTA. 12M";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "CuatrimestralSales";
            bf.HeaderText = "VTA. 12M/3";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "SaleMonths";
            bf.HeaderText = "Vida del Producto";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "RepositionLevel";
            bf.HeaderText = "N. Reposici&oacute;n";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "Result";
            bf.HeaderText = "Diferenc&iacute;a %";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "ProductCountryCode";
            bf.HeaderText = "Código de País";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            bf = new BoundField();
            bf.ItemStyle.Width = Unit.Pixel(95);
            bf.DataField = "OrderInfo";
            bf.HeaderText = "Información de Orden de Venta (Cant. Ordenes/%/N. Orden)";
            bf.HtmlEncode = false;
            grdProductList.Columns.Add(bf);

            #endregion

            grdProductList.DataSource = ShowAlert(column, order);
            grdProductList.DataBind();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename=DiferenciaEnNivelDeReposición.xls"));
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
                    table.Rows[0].Cells[i].BackColor =Color.FromArgb(225, 224, 224);
            }

            //  add each of the data rows to the table
            List<AlertReposition> arLSt = grdProductList.DataSource as List<AlertReposition>;
            foreach (GridViewRow row in grdProductList.Rows)
            {
                PrepareControlForExport(row);
                int pos = table.Rows.Add(row);
                table.Rows[pos].Cells[0].Attributes.Add("class", "cF");
                
                //ver esto
                AlertReposition ar = arLSt[pos-1] ;
                if (ar != null && ar.IsConflicted)
                {
                    for (int i = 0; i < table.Rows[pos].Cells.Count; i++)
                        table.Rows[pos].Cells[i].BackColor = Color.FromArgb(250, 128, 114);
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
