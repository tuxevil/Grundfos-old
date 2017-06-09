using System;
using System.Reflection;
using System.Web.UI;
using PartnerNet.Business;

namespace Grundfos.StockForecast
{
    public partial class Base : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            AssemblyName name = a.GetName();
            Label2.Text = "v" + name.Version.Major + "." + name.Version.Minor + "." + name.Version.Build;
            lblUltimoForecast.Text = "Ultimo Forecast: " + ControllerManager.Log.GetLastForecast().CreationDate.ToShortDateString();
        }

        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/users/password.aspx");
        }
    }
}
