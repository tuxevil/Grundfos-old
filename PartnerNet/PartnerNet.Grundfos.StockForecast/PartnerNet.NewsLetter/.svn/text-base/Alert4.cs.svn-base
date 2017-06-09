using System;
using System.Collections.Generic;
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
    public class Alert4 : IPersonalizedNewsletter
    {
        #region IPersonalizedNewsletter Members

        private readonly string title = HttpUtility.HtmlEncode("Alerta diaria de OV no Cumplimentadas - Grundfos StockForecast");

        public System.Net.Mail.MailMessage Get(System.Web.Security.MembershipUser mu, string templatePath, DateTime? lastRunDate)
        {
            List<AlertSaleOrder> ASO = ControllerManager.AlertSaleOrder.ShowAlert4();

            if (ASO.Count == 0)
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
            string templateBody = File.ReadAllText(Path.Combine(templatePath, "template4.htm"));
            string lineTemplate = File.ReadAllText(Path.Combine(templatePath, "template4_line.htm"));

            string template4_lines = "";
            string subject = HttpUtility.HtmlDecode(title);
            foreach (AlertSaleOrder n in ASO)
            {
                string newsInfo = lineTemplate;
                newsInfo = newsInfo.Replace("[SALEORDERCODE]", n.SaleOrderCode);
                newsInfo = newsInfo.Replace("[PURCHASEORDERITEMCODE]", n.PurchaseOrderItemCode);
                newsInfo = newsInfo.Replace("[PURCHASEORDERCODE]", n.PurchaseOrderCode);
                newsInfo = newsInfo.Replace("[QUANTITY]", n.Quantity.ToString());
                newsInfo = newsInfo.Replace("[CUSTOMERCODE]", n.CustomerCode);
                newsInfo = newsInfo.Replace("[GAP]", n.GAP.ToString());
                newsInfo = newsInfo.Replace("[WAYOFDELIVERY]", n.WayOfDelivery.ToString());
                newsInfo = newsInfo.Replace("[SALEORDERDELIVERYDATE]", n.SaleOrderDeliveryDate.ToShortDateString());
                newsInfo = newsInfo.Replace("[PURCHASEORDERARRIVALDATE]", n.PurchaseOrderArrivalDate.ToShortDateString());
                template4_lines += newsInfo;
            }

            fullMail = fullMail.Replace("[BODY]", templateBody.Replace("[LINES]", template4_lines));
            fullMail = fullMail.Replace("[TITLE]", title);
            MailMessage m = new MailMessage();
            m.Body = fullMail;
            m.Subject = subject;
            return m;
        }

        #endregion
    }
}
