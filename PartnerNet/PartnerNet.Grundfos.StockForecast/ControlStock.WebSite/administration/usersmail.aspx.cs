using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ProjectBase.Newsletter;
using System.Collections.Generic;

namespace Grundfos.StockForecast.administration
{
    public partial class usersmail : System.Web.UI.Page
    {
        NewsletterManager n = new NewsletterManager("~/WebNHibernate.config");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cargar_Campa�as();
                Cargar_Members();
            }
            
        }

        protected void Cargar_Campa�as()
        {
            IList<Campaign> lst = n.GetCampaigns();
            if (lst != null)
            {
                foreach (Campaign c in lst)
                {
                    ListItem LI = new ListItem(c.Name, c.Code);
                    ddlCampa�a.Items.Add(LI);
                }
            }
        }

        protected void ddlCampa�a_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar_Members();
            if (ddlCampa�a.SelectedValue.ToString() != "N/A")
            {
                btnAgregarAlertasMail.Enabled = true;
                IList<UserCampaign> uC = n.GetSubscriptors(ddlCampa�a.SelectedValue.ToString());

                foreach (GridViewRow grd in grdMembers.Rows)
                {
                    CheckBox chk = (CheckBox)grd.FindControl("chkAlertar");

                    chk.Enabled = true;
                    foreach (UserCampaign u in uC)
                    {
                        if (Membership.GetUser(grd.Cells[0].Text).ProviderUserKey.ToString() == u.UserID.ToString())
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
            else
                btnAgregarAlertasMail.Enabled = false;
        }

        private void Cargar_Members()
        {
            MembershipUserCollection muc = Membership.GetAllUsers();
            
            grdMembers.DataSource = muc;
            grdMembers.DataBind();
            grdMembers.Enabled = true;
        }

        protected void grdMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMembers.PageIndex = e.NewPageIndex;
            grdMembers.DataBind();
        }

        protected void btnAgregarAlertasMail_Click(object sender, EventArgs e)
        {
           
            foreach (GridViewRow grd in grdMembers.Rows)
            {
                string gui = Membership.GetUser(grd.Cells[0].Text).ProviderUserKey.ToString();
                Guid g = new Guid(gui);
                CheckBox selected = (CheckBox) grd.FindControl("chkAlertar");
                if (selected.Checked == true)
                {   
                    if (!n.IsSubscribed(n.GetCampaign(ddlCampa�a.SelectedValue.ToString()), g))
                       n.Subscribe(n.GetCampaign(ddlCampa�a.SelectedValue.ToString()), g);
                }
                else
                {
                 if (n.IsSubscribed(n.GetCampaign(ddlCampa�a.SelectedValue.ToString()), g))
                     n.Unsubscribe(n.GetCampaign(ddlCampa�a.SelectedValue.ToString()), g);
                }
            }
        }

    }

}