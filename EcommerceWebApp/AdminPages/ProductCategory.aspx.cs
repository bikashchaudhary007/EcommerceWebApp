using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EcommerceWebApp.Helpers;


namespace EcommerceWebApp.AdminPages
{
    public partial class ProductCategory : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                BindCategories();

        }

        private void BindCategories()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("SELECT CategoryId, CategoryName, Description FROM Categories ORDER BY CreatedAt DESC", conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "INSERT INTO Categories (CategoryName, Description) VALUES (@CategoryName,@Description)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Registration successful
                    ClearFields();
                    BindCategories();
                    ToastHelper.RegisterToast(this, "Product category created successfully!", isError: false);

                }
                else
                {
                    // Registration failed
                    ToastHelper.RegisterToast(this, "Product category creation failed.", isError: true);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                //ShowToast($"Error: {ex.Message}", isError: true);
                ToastHelper.RegisterToast(this, $"Error: {ex.Message}", isError: true);
            }
            finally
            {
                con.Close();
            }


        }

        private void ClearFields()
        {
            txtCategoryName.Text = "";
            txtDescription.Text = "";
        }

        protected void gvCategories_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            BindCategories();
        }

        protected void gvCategories_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            BindCategories();
        }


        protected void gvCategories_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int categoryId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvCategories.Rows[e.RowIndex];

            var txtName = (TextBox)row.FindControl("txtEditName");
            var txtDescription = (TextBox)row.FindControl("txtEditDescription");

            if (txtName == null || txtDescription == null)
            {
                litListMessage.Text = "<div class='alert alert-danger mt-2'>Failed to locate edit fields.</div>";
                return;
            }

            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();

            using (SqlConnection conn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand(@"
        UPDATE Categories
        SET CategoryName=@name, Description=@desc
        WHERE CategoryId=@id", conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                cmd.Parameters.AddWithValue("@id", categoryId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    litListMessage.Text = "<div class='alert alert-success mt-2'>Updated successfully.</div>";
                }
                catch (SqlException ex)
                {
                    litListMessage.Text = $"<div class='alert alert-danger mt-2'>Error: {ex.Message}</div>";
                }
            }

            gvCategories.EditIndex = -1;
            BindCategories();
        }

        protected void gvCategories_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int categoryId = Convert.ToInt32(gvCategories.DataKeys[e.RowIndex].Value);
            using (SqlConnection conn = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryId=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", categoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            litListMessage.Text = "<div class='alert alert-success mt-2'>Deleted.</div>";
            BindCategories();
        }


    }
}