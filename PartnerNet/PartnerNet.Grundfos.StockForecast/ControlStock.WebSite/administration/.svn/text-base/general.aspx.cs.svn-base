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

namespace Grundfos.StockForecast.administration
{
    public partial class general : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Grab first username and load roles below
            if (!IsPostBack)
            {
                FindFirstUserName();
            }
        }

        /// <summary>
        /// Used to retrieve the first user that would normally be processed
        /// by the Membership List
        /// </summary>
        private void FindFirstUserName()
        {
            MembershipUserCollection muc = Membership.GetAllUsers();
            foreach (MembershipUser mu in muc)
            {
                // Just grab the first name then break out of loop
                string userName = mu.UserName;
                ObjectDataSourceRoleObject.SelectParameters["UserName"].DefaultValue = userName;
                break;
            }
        }


        protected void GridViewMembershipUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            // cover case where there is no current user
            if (Membership.GetUser() != null)
            {
                ObjectDataSourceRoleObject.SelectParameters["UserName"].DefaultValue = Membership.GetUser().UserName;
                ObjectDataSourceRoleObject.SelectParameters["ShowOnlyAssignedRolls"].DefaultValue = "true";
            }

            GridViewRole.DataBind();
        }
        protected void ButtonCreateNewRole_Click(object sender, EventArgs e)
        {
            if (TextBoxCreateNewRole.Text.Length > 0)
            {
                ObjectDataSourceRoleObject.InsertParameters["RoleName"].DefaultValue = TextBoxCreateNewRole.Text; ;
                ObjectDataSourceRoleObject.Insert();
                GridViewRole.DataBind();
                TextBoxCreateNewRole.Text = "";
            }
        }

        protected void ToggleInRole_Click(object sender, EventArgs e)
        {
            // Grab text from button and parse, not so elegant, but gets the job done
            Button bt = (Button)sender;
            string buttonText = bt.Text;

            char[] seps = new char[1];
            seps[0] = ' ';
            string[] buttonTextArray = buttonText.Split(seps);
            string roleName = buttonTextArray[5];
            string userName = buttonTextArray[2];
            string whatToDo = buttonTextArray[0];
            string[] userNameArray = new string[1];
            userNameArray[0] = userName;  // Need to do this because RemoveUserFromRole requires string array.

            if (whatToDo.StartsWith("Qu"))
            {
                // need to remove assignment of this role to this user
                Roles.RemoveUsersFromRole(userNameArray, roleName);
            }
            else
            {
                Roles.AddUserToRole(userName, roleName);
            }
            GridViewRole.DataBind();
        }

        protected void GridViewMembership_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            FindFirstUserName();  // Current user is deleted so need to select a new user as current
            GridViewRole.DataBind(); // update roll lists to reflect new counts
        }


        protected string ShowNumberUsersInRole(int numUsersInRole)
        {
            string result;
            result = "Numero de Usuarios en el Grupo: " + numUsersInRole.ToString();
            return result;
        }

        protected string ShowInRoleStatus(string userName, string roleName)
        {
            string result;

            if (userName == null | roleName == null)
            {
                return "No se selecciono ningun Usuario";
            }

            if (Roles.IsUserInRole(userName, roleName) == true)
            {
                result = "Quitar a " + userName + " del Grupo " + roleName;
            }
            else
            {
                result = "Agregar a " + userName + " al Grupo " + roleName;
            }

            return result;
        }


        protected void DetailsView1_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            GridViewMemberUser.DataBind();
        }
        protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }
        protected void ObjectDataSourceMembershipUser_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
            }
            
        }
    }
}
