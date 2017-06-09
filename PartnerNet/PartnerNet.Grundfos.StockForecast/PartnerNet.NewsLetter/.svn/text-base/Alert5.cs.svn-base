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
    public class Alert5 : IPersonalizedNewsletter
    {
        #region IPersonalizedNewsletter Members
        private readonly string title = HttpUtility.HtmlEncode("Alerta diaria de Productos con quiebre de Stock Pronosticado - Grundfos StockForecast");

        public MailMessage Get(System.Web.Security.MembershipUser mu, string templatePath, DateTime? lastRunDate)
        {
            IList<AlertProduct> AP = ControllerManager.AlertProduct.ShowAlert5();
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
            string templateBody = File.ReadAllText(Path.Combine(templatePath, "template5.htm"));
            string lineTemplate = File.ReadAllText(Path.Combine(templatePath, "template5_line.htm"));

            string template3_lines = "";
            string subject = HttpUtility.HtmlDecode(title);
            foreach (AlertProduct n in AP)
            {
                string newsInfo = lineTemplate;
                newsInfo = newsInfo.Replace("[PRODUCTCODE]", n.ProductCode);
                newsInfo = newsInfo.Replace("[QUANTITY]", n.Quantity.ToString());
                newsInfo = newsInfo.Replace("[NEGATIVEDATE]", n.NegativeDate.ToShortDateString());
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
