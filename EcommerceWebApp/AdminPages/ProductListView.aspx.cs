using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp.AdminPages
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGrid();

            }
        }
        private void BindGrid()
        {
            string sql = @"
SELECT p.ProductId, p.ProductName, p.Price, p.StockQuantity, p.IsFeatured, p.Dimensions,
       p.ShortDescription, p.LongDescription, p.ImageUrl,
       c.CategoryName
FROM dbo.Products p
LEFT JOIN dbo.Categories c ON p.CategoryId = c.CategoryId
ORDER BY p.ProductName";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "View")
            {
                Response.Redirect($"ProductDetailView.aspx?id={productId}");
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect($"EditProduct.aspx?id={productId}");
            }
            else if (e.CommandName == "DoDelete") // updated here
            {
                DeleteProduct(productId);
                BindGrid();
            }
        }


        private void DeleteProduct(int productId)
        {
            try
            {
                using (var conn = new SqlConnection(_cs))
                using (var cmd = new SqlCommand("DELETE FROM dbo.Products WHERE ProductId = @ProductId", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                ltMessage.Text = "<div class='alert alert-success'>Product deleted.</div>";
            }
            catch (Exception ex)
            {
                ltMessage.Text = $"<div class='alert alert-danger'>Delete failed: {ex.Message}</div>";
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx"); // change if needed
        }





    }
}