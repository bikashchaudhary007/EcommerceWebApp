using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string role = Session["UserRole"] as string ?? "";
                bool isUser = role.Equals("user", StringComparison.OrdinalIgnoreCase);
                bool hasName = Session["UserName"] != null;

                profileContainer.Visible = isUser && hasName;
                loginSignupContainer.Visible = !(isUser && hasName);

                if (profileContainer.Visible)
                {
                    string name = Session["UserName"].ToString();
                    profileName.InnerText = Server.HtmlEncode(name);

                    string[] parts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string initials = parts.Length == 0 ? "" :
                                      parts.Length == 1 ? parts[0].Substring(0, 1) :
                                      parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1);
                    avatarPlaceholder.InnerText = initials.ToUpperInvariant();
                }
            }

        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var cookie = new System.Web.HttpCookie("ASP.NET_SessionId")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true
                };
                Response.Cookies.Add(cookie);
            }

            Response.Redirect("Default.aspx");
        }

    }
}