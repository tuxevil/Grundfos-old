using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using PartnerNet.Business;
using PartnerNet.Domain;
using ProjectBase.Newsletter;

namespace PartnerNet.NewsLetter
{
    public class Alert3 : IPersonalizedNewsletter
    {
        #region IPersonalizedNewsletter Members

        private readonly string title = HttpUtility.HtmlEncode("Alerta diaria de Productos con Stock Negativo - Grundfos StockForecast");

        public MailMessage Get(System.Web.Security.MembershipUser mu, string templatePath, DateTime? lastRunDate)
        {
//#if DEBUG
//            Debugger.Break();
//#endif

            List<AlertProduct> AP = ControllerManager.AlertProduct.ShowAlert3();

            if (AP.Count == 0)
                return null;

            if (Path.IsPathRooted(templatePath))
            {
                // Determine if we are running in a web context
                if (HttpContext.Current != null)
                    templatePath = HttpContext.Current.Server.MapPath(templatePath);
                else
                    templatePath =
                    Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), templatePath);
            }

            string fullMail = File.ReadAllText(Path.Combine(templatePath, "common.htm"));
            string templateBody = File.ReadAllText(Path.Combine(templatePath, "template3.htm"));
            string lineTemplate = File.ReadAllText(Path.Combine(templatePath, "template3_line.htm"));

            string template3_lines = "";
            string subject = HttpUtility.HtmlDecode(title);
            foreach (AlertProduct n in AP)
            {
                string newsInfo = lineTemplate;
                newsInfo = newsInfo.Replace("[PRODUCTCODE]", n.ProductCode);
                newsInfo = newsInfo.Replace("[STANDARDCOST]", n.StandardCost.ToString());
                newsInfo = newsInfo.Replace("[SUBTOTAL]", n.SubTotal.ToString());
                newsInfo = newsInfo.Replace("[QUANTITY]", n.Quantity.ToString());
                template3_lines += newsInfo;
            }

            fullMail = fullMail.Replace("[BODY]", templateBody.Replace("[LINES]", template3_lines));
            fullMail = fullMail.Replace("[TITLE]", title);
            MailMessage m = new MailMessage();
            m.Body = fullMail;
            m.Subject = subject;
            return m;
        }

        #endregion
    }
}
