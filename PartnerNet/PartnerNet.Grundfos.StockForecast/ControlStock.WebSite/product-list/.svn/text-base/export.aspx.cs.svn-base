using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.product_list
{
    public partial class export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = (List<ProductForExport>)Session["ProductForExport"];
            GridView1.DataBind();

            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            string codeFormat = @"<style>.cF  { mso-number-format:'\@'; }</style>";
            HttpContext.Current.Response.Write(codeFormat);

            Table table = new Table();

            if (GridView1.HeaderRow != null)
            {
                table.Rows.Add(GridView1.HeaderRow);
                table.Rows[0].ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);

                for (int i = 0; i < table.Rows[0].Cells.Count; i++)
                    table.Rows[0].Cells[i].BackColor = System.Drawing.Color.FromArgb(225, 224, 224);
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                int pos = table.Rows.Add(row);

                table.Rows[pos].Cells[0].Attributes.Add("class", "cF");
                table.Rows[pos].Cells[0].Width = 100;
                table.Rows[pos].Cells[1].Width = 500;
                table.Rows[pos].Cells[2].Width = 100;
                table.Rows[pos].Cells[3].Width = 100;
            }

            table.RenderControl(htw);

            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();

        }
    }
}
