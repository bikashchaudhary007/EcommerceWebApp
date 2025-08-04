using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace EcommerceWebApp
{
    public partial class LoginPage : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
    try
            {
                con.Open();
                string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Access Role and redirect accordingly
                        string role = dr["Role"].ToString();
                        Session["user"] = dr["Fullname"].ToString();
                        if (role == "admin") {
                            Response.Redirect("AdminPages/AdminDashboard.aspx");
                        }
                        else
                        {
                            Response.Redirect("MemberDashboard.aspx");
                        }


                    }
                    // User exists, redirect to welcome page
                    //Session["user"] = txtUsername.Text;
                  
                }
                else
                {
                    // User does not exist, show error message
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Login failed!');", true);
                }
            }
            finally
            {
                con.Close();
            }

        }
    }
}