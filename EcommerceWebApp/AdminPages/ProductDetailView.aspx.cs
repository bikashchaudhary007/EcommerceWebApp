using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


namespace EcommerceWebApp.AdminPages
{
    public partial class ProductDetailView : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId;
                if (int.TryParse(Request.QueryString["id"], out productId))
                {
                    LoadProduct(productId);
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid product ID.</div>";
                }
            }

        }

        private void LoadProduct(int productId)
        {
            string sql = @"
SELECT p.ProductName, p.Price, p.StockQuantity, p.IsFeatured, p.Dimensions,
       p.ShortDescription, p.LongDescription, p.ImageUrl,
       c.CategoryName
FROM dbo.Products p
LEFT JOIN dbo.Categories c ON p.CategoryId = c.CategoryId
WHERE p.ProductId = @ProductId";

            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        lblName.Text = rdr["ProductName"].ToString();
                        lblCategory.Text = rdr["CategoryName"].ToString();
                        lblPrice.Text = Convert.ToDecimal(rdr["Price"]).ToString("C");
                        lblStock.Text = rdr["StockQuantity"].ToString();
                        lblFeatured.Text = Convert.ToBoolean(rdr["IsFeatured"]) ? "Yes" : "No";
                        lblDimensions.Text = rdr["Dimensions"]?.ToString() ?? "-";
                        lblShortDesc.Text = rdr["ShortDescription"]?.ToString() ?? "-";
                        lblLongDesc.Text = rdr["LongDescription"]?.ToString() ?? "-";

                        string imageUrl = rdr["ImageUrl"]?.ToString();
                        imgProduct.ImageUrl = !string.IsNullOrWhiteSpace(imageUrl) ? ResolveUrl(imageUrl) : ResolveUrl("~/images/placeholder.png");
                    }
                    else
                    {
                        ltMessage.Text = "<div class='alert alert-warning'>Product not found.</div>";
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductListView.aspx"); // Redirect back to the list page
        }


    }
}