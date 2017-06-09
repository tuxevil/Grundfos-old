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
    public class Alert2 : IPersonalizedNewsletter
    {
        #region IPersonalizedNewsletter Members

        private readonly string title = HttpUtility.HtmlEncode("Alerta diaria de OC No Confirmadas - Grundfos StockForecast");

        public System.Net.Mail.MailMessage Get(System.Web.Security.MembershipUser mu, string templatePath, DateTime? lastRunDate)
        {
//#if DEBUG
//            Debugger.Break();
//#endif
            List<AlertPurchaseOrder> APO = ControllerManager.AlertPurchaseOrder.ShowAlert2();

            if (APO.Count == 0)
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
            string templateBody = File.ReadAllText(Path.Combine(templatePath, "template2.htm"));
            string lineTemplate = File.ReadAllText(Path.Combine(templatePath, "template2_line.htm"));

            string template2_lines = "";
            string subject = HttpUtility.HtmlDecode(title);
            foreach (AlertPurchaseOrder n in APO)
            {
                string newsInfo = lineTemplate;
                newsInfo = newsInfo.Replace("[PURCHASEORDERCODE]", n.PurchaseOrderCode);
                newsInfo = newsInfo.Replace("[PURCHASEORDERITEMCODE]", n.PurchaseOrderItemCode);
                newsInfo = newsInfo.Replace("[QUANTITY]", n.Quantity.ToString());
                newsInfo = newsInfo.Replace("[GAP]", n.GAP.ToString());
                newsInfo = newsInfo.Replace("[WAYOFDELIVERY]", n.WayOfDelivery.ToString());
                newsInfo = newsInfo.Replace("[TYPE]", n.Destination.ToString());
                newsInfo = newsInfo.Replace("[ARRIVAL]", n.ArrivalDate.ToShortDateString());
                template2_lines += newsInfo;
            }

            fullMail = fullMail.Replace("[BODY]", templateBody.Replace("[LINES]", template2_lines));
            fullMail = fullMail.Replace("[TITLE]", title);
            MailMessage m = new MailMessage();
            m.Body = fullMail;
            m.Subject = subject;
            return m;
        }

        #endregion
    }
}
