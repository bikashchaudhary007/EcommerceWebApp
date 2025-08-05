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
    public partial class SignUpPage : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "INSERT INTO Users (Fullname, Address, Email, Password) VALUES (@Fullname, @Address, @Email, @Password)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Fullname", txtFullName.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Registration successful
                    Session["RegisterToastMessage"] = $"Hello {txtFullName.Text}, registration successful!";
                    Response.Redirect("LoginPage.aspx");

                }
                else
                {
                    // Registration failed
                    ToastHelper.RegisterToast(this, "Registration failed!", isError: true);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ToastHelper.RegisterToast(this, $"Error: {ex.Message}", isError: true);
            }
            finally
            {
                con.Close();
            }

        }
    }
}