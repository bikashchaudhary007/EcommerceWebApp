using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using EcommerceWebApp.Helpers;

namespace EcommerceWebApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterToastMessage"] != null)
            {
                string msg = Session["RegisterToastMessage"].ToString();
                ToastHelper.RegisterToast(this, msg, isError: false);
                Session.Remove("RegisterToastMessage");
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text); 
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string role = dr["Role"].ToString();
                                string fullName = dr["Fullname"].ToString();
                                string userId = dr["Id"].ToString();

                                // normalize session keys to what master page expects
                                Session["user"] = fullName;
                                Session["UserName"] = fullName;
                                Session["UserRole"] = role; // e.g., "user" or "admin"
                                Session["UserId"] = userId;

                                if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    
                                    Session["LoginToastMessage"] = $"Hello {fullName}, login successful!";
                                    Response.Redirect("AdminPages/AdminDashboard.aspx");
                                }
                                else // assume user
                                {

                                    Session["LoginToastMessage"] = $"Hello {fullName}, login successful!";
                                    Response.Redirect("MemberDashboard.aspx");
                                }
                            }
                        }
                        else
                        {
                            ToastHelper.RegisterToast(this, "Login failed!!", isError: true);
                        }
                    }
                }
            }

        }
    }
}