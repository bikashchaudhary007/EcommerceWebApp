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
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration successful!');", true);
                    //ClearFields(); // Clear the input fields after successful registration
                    Response.Redirect("LoginPage.aspx");

                }
                else
                {
                    // Registration failed
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registration failed!');", true);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
            finally
            {
                con.Close();
            }

        }
    }
}