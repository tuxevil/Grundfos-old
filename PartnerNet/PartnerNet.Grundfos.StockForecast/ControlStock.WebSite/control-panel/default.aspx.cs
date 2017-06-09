using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ChartDirector;
using PartnerNet.Business;
using PartnerNet.Domain;

namespace Grundfos.StockForecast.control_panel
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole(UserType.Administradores.ToString()))
                lnbAdministrarMails.Visible = true;
            else
                lnbAdministrarMails.Visible = false;
            if (!IsPostBack)
            {
                AngularMeter m = GenerateChart(1);

                WebChartViewer1.Image = m.makeWebImage(Chart.PNG);

                AngularMeter m2 = GenerateChart(2);

                WebChartViewer2.Image = m2.makeWebImage(Chart.PNG);

                AngularMeter m3 = GenerateChart(3);

                WebChartViewer3.Image = m3.makeWebImage(Chart.PNG);

                AngularMeter m4 = GenerateChart(4);

                WebChartViewer4.Image = m4.makeWebImage(Chart.PNG);

                AngularMeter m5 = GenerateChart(5);

                WebChartViewer5.Image = m5.makeWebImage(Chart.PNG);

                AngularMeter m6 = GenerateChart(6);

                WebChartViewer6.Image = m6.makeWebImage(Chart.PNG);
            }
            
        }

        protected void lnbAdministrarMails_Click(object sender, EventArgs e)
        {
            Response.Redirect("../administration/usersmail.aspx");
        }

        private AngularMeter GenerateChart(int alert)
        {
            double value = 0;

            AngularMeter m = new AngularMeter(180, 180, Chart.silverColor(), 0x000000, -2);
            m.setMeter(155, 165, 130, 270, 360);

            switch (alert)
            {
                case 1:
                    {
                        
                        m.addText(90, 10, "OC Confirmadas y", "Verdana", 12, Chart.TextColor, Chart.Center);
                        m.addText(90, 25, "No Entregadas", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertPurchaseOrder.ShowAlert1().Count;

                        GetRange(m, alert, value);

                        break;
                    }
                case 2:
                    {
                        m.addText(90, 10, "OC No Confirmadas", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertPurchaseOrder.ShowAlert2().Count;

                        GetRange(m, alert, value);
                        
                        break;
                    }
                case 3:
                    {
                        m.addText(90, 10, "Productos con", "Verdana", 12, Chart.TextColor, Chart.Center);
                        m.addText(90, 25, "Stock Negativo", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertProduct.ShowAlert3().Count;

                        GetRange(m, alert, value);
                        
                        break;
                    }
                case 4:
                    {
                        m.addText(90, 10, "OV", "Verdana", 12, Chart.TextColor, Chart.Center);
                        m.addText(90, 25, "no Cumplimentadas", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertSaleOrder.ShowAlert4().Count;

                        GetRange(m, alert, value);
                        
                        break;
                    }
                case 5:
                    {
                        m.addText(90, 10, "Stock que Caerá", "Verdana", 12, Chart.TextColor, Chart.Center);
                        m.addText(90, 25, "del Safety", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertProduct.ShowAlert5().Count;

                        GetRange(m, alert, value);
                        
                        break;
                    }
                case 6:
                    {
                        m.addText(90, 10, "Nivel de Reposición", "Verdana", 12, Chart.TextColor, Chart.Center);
                        value = ControllerManager.AlertReposition.ShowAlert6().Count;

                        GetRange(m, alert, value);

                        break;
                    }
            }

            return m;
        }

        private AngularMeter GetRange(AngularMeter m, int alert, double value)
        {
            m.addText(7, 35, value.ToString("0.##"), "Verdana", 8, 0xffffff, Chart.TopLeft).setBackground(0, 0, -1);

            int total = ControllerManager.AlertTotal.GetTotalForAlert(alert); 
            m.setScale(0, total, total / 3, total / 6);

            m.addZone((total / 3) * 2, total, 0xffcccc, 0x808080);
            m.addZone(total / 3, (total / 3) * 2, 0xffff66, 0x808080);
            m.addZone(0, total / 3, 0x99ff99, 0x808080);

            m.addPointer(value, unchecked((int)0x80000000));

            return m;
        }
    }
}
