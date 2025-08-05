using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp.AdminPages
{
    public partial class AdminMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string username = Session["Username"]?.ToString() ?? "AdminUser";
                litRole.Text = "Administrator";
                litUsernameTopbar.Text = username;
                litTodayDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");

                string initials = GetInitials(username);
                divAvatarInitials.InnerText = initials;
            }
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "";

            string[] parts = fullName.Trim().Split(' ');
            if (parts.Length == 1)
                return parts[0].Substring(0, 1).ToUpper();

            return (parts[0][0].ToString() + parts[1][0].ToString()).ToUpper();
        }



        public string GetActiveClass(string page)
        {
            string current = Path.GetFileName(Request.Path);
            return string.Equals(current, page, StringComparison.OrdinalIgnoreCase) ? "active-link" : "";
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                // Clear the session
                Session.Clear();
                Response.Redirect("../LoginPage.aspx");
            }
            else
            {
                // If no user is logged in, redirect to login page
                Response.Redirect("../LoginPage.aspx");
            }
        }
    }
}