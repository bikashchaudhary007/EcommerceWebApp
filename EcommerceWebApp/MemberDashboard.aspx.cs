using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

            if (Session["user"] != null)
            {
                // Clear the session
                Session.Clear();
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                // If no user is logged in, redirect to login page
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}