using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PartnerNet.Business;
using PartnerNet.Common;
using PartnerNet.Domain;
using ChartDirector;

namespace Grundfos.StockForecast.forecast
{
    public partial class _default : System.Web.UI.Page
    {
        private int leadtime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    Generate_Forecast(Convert.ToInt32(Request.QueryString["Id"]));
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 10)
            {
                e.Row.Font.Bold = true;
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            if (e.Row.RowIndex == leadtime)
            {
                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
            }
        }

        protected void Generate_Forecast(int productid)
        {
            int totalCount = 0;
            Product info = ControllerManager.Product.GetById(productid);	
            IList<ProductInformation> prodinfolist = ControllerManager.Product.GetProductInformation(info.ProductCode, "N/A", 0, 0, 1, 1, out totalCount, false);
            if(prodinfolist == null || prodinfolist.Count == 0)
                return;
            ProductInformation prodinfo = prodinfolist[0];
            leadtime = prodinfo.LeadTime + 10;
            //IList<TransactionHistoryWeekly> history = ControllerManager.TransactionHistoryWeekly.GetInfo(prodinfo.Id, Config.CurrentWeek, Config.CurrentDate.Year, 1);
            List<TransactionHistoryMonthly> historym = ControllerManager.TransactionHistoryMonthly.GetInfo(prodinfo.Id);
            this.Label12.Text = prodinfo.ProductCode;
            this.Label3.Text = prodinfo.Description;
            this.Label5.Text = prodinfo.Provider;
            this.Label7.Text = prodinfo.Stock.ToString() + "/" + prodinfo.ReservedStock.ToString() + "/" + prodinfo.OrderedStock.ToString();
            this.Label9.Text = prodinfo.RepositionLevel.ToString();
            this.Label11.Text = prodinfo.RepositionPoint.ToString();
            this.Label16.Text = prodinfo.LeadTime.ToString();
            this.Label1.Text = prodinfo.Safety.ToString();

            this.Label01.Text = GetTransactionMonthly(historym, 1).Sale.ToString();
            this.lblY01.Text = GetTransactionMonthly(historym, 1).Year.ToString();
            this.Label02.Text = GetTransactionMonthly(historym, 2).Sale.ToString();
            this.lblY02.Text = GetTransactionMonthly(historym, 2).Year.ToString();
            this.Label03.Text = GetTransactionMonthly(historym, 3).Sale.ToString();
            this.lblY03.Text = GetTransactionMonthly(historym, 3).Year.ToString();
            this.Label04.Text = GetTransactionMonthly(historym, 4).Sale.ToString();
            this.lblY04.Text = GetTransactionMonthly(historym, 4).Year.ToString();
            this.Label05.Text = GetTransactionMonthly(historym, 5).Sale.ToString();
            this.lblY05.Text = GetTransactionMonthly(historym, 5).Year.ToString();
            this.Label06.Text = GetTransactionMonthly(historym, 6).Sale.ToString();
            this.lblY06.Text = GetTransactionMonthly(historym, 6).Year.ToString();
            this.Label07.Text = GetTransactionMonthly(historym, 7).Sale.ToString();
            this.lblY07.Text = GetTransactionMonthly(historym, 7).Year.ToString();
            this.Label08.Text = GetTransactionMonthly(historym, 8).Sale.ToString();
            this.lblY08.Text = GetTransactionMonthly(historym, 8).Year.ToString();
            this.Label09.Text = GetTransactionMonthly(historym, 9).Sale.ToString();
            this.lblY09.Text = GetTransactionMonthly(historym, 9).Year.ToString();
            this.Label010.Text = GetTransactionMonthly(historym, 10).Sale.ToString();
            this.lblY10.Text = GetTransactionMonthly(historym, 10).Year.ToString();
            this.Label011.Text = GetTransactionMonthly(historym, 11).Sale.ToString();
            this.lblY11.Text = GetTransactionMonthly(historym, 11).Year.ToString();
            this.Label012.Text = GetTransactionMonthly(historym, 12).Sale.ToString();
            this.lblY12.Text = GetTransactionMonthly(historym, 12).Year.ToString();

            IList<Forecast> forecast = ControllerManager.Forecast.GetForecast(info, Config.CurrentWeek, Config.CurrentDate.Year);
            this.GridView1.DataSource = forecast;
            this.GridView1.DataBind();

            //------GRAFICO-------------------------------------------
            if (forecast.Count > 0)
            {
                string[] Titulos = new string[forecast.Count];
                double[] Stock = new double[forecast.Count];
                double[] Ventas = new double[forecast.Count];
                double[] Compras = new double[forecast.Count];
                for (int cont = 0; cont < forecast.Count; cont++)
                {
                    Stock.SetValue(forecast[cont].FinalStock, cont);
                    Ventas.SetValue(forecast[cont].Sale, cont);
                    Compras.SetValue(forecast[cont].Purchase, cont);
                    Titulos.SetValue(forecast[cont].Week.ToString(), cont);
                }

                XYChart c = new XYChart(480, 300, 0xeeeeff, 0x000000, 3);
                c.setPlotArea(50, 70, 410, 180, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);
                c.addLegend(50, 50, false, "Arial Bold", 8).setBackground(Chart.Transparent);
                c.addTitle("Gráficos Estadísticos", "Verdana Bold", 12).setBackground(0xccccff, 0x000000, Chart.glassEffect());
                c.xAxis().setTitle("Semanas");
                c.yAxis().setTitle("Cantidad");

                Mark actual = c.xAxis().addMark(10, 0x000000, "SEMANA ACTUAL");
                actual.setLineWidth(1);
                actual.setAlignment(Chart.TopRight);
                actual.setFontAngle(90);
                Mark actual0 = c.xAxis().addMark(10 + prodinfo.LeadTime, 0x000000, "LEADTIME");
                actual0.setLineWidth(1);
                actual0.setAlignment(Chart.TopLeft);
                actual0.setFontAngle(90);
                LineLayer linea = c.addLineLayer();
                linea.setLineWidth(3);
                linea.addDataSet(Stock, 0x000000, "Stock Final");
                linea.addDataSet(Ventas, 0x00ff00, "Ventas");
                linea.addDataSet(Compras, 0x0000FF, "Compras");
                c.addAreaLayer(Stock, c.yZoneColor(forecast[10].Safety, unchecked((int)0x50ff3c3c), unchecked((int)0x500080c0)));
                c.xAxis().addZone(10, 10 + prodinfo.LeadTime, 0xdcdcdc);
                c.xAxis().setLabels(Titulos);
                c.xAxis().setLabelStep(2);
                WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
                WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='Semana {xLabel}: {value}'");
                WebChartViewer1.Visible = true;
            }
            Product pfrom = ControllerManager.Product.GetAlternativeFrom(info);
            
            if(pfrom != null)
            {
                btnAlternativeFrom.Text = pfrom.ProductCode;
                btnAlternativeFrom.CommandArgument = pfrom.Id.ToString();
                lblAlternativeFrom.Visible = true;
            }
            Product pto = ControllerManager.Product.GetAlternativeTo(info);
            if (pto != null)
            {
                btnAlternativeTo.Text = pto.ProductCode;
                btnAlternativeTo.CommandArgument = pto.Id.ToString();
                lblAlternativeTo.Visible = true;
            }

        }
        private TransactionHistoryMonthly GetTransactionMonthly(List<TransactionHistoryMonthly> historym, int month)
        {
            TransactionHistoryMonthly trans = historym.Find(delegate(TransactionHistoryMonthly record)
                                                        {
                                                            if (record.Month == month)
                                                            {
                                                                return true;
                                                            }
                                                            return false;
                                                        });
            if (trans != null)
                return trans;
        
            trans = new TransactionHistoryMonthly();
            trans.Sale = 0;
            trans.Year = 0;
            return trans;
        }

        protected void btnAlternativeTo_Click(object sender, EventArgs e)
        {
            Response.Redirect("?Id=" + btnAlternativeTo.CommandArgument);
        }

        protected void btnAlternativeFrom_Click(object sender, EventArgs e)
        {
            Response.Redirect("?Id=" + btnAlternativeFrom.CommandArgument);
        }
    }
}
