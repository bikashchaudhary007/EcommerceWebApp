using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace EcommerceWebApp.AdminPages
{

    public partial class EditProduct : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        private readonly string[] _allowedExt = new[] { ".jpg", ".jpeg", ".png" };
        private const int MaxFileBytes = 2 * 1024 * 1024;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();

                int prodId;
                if (int.TryParse(Request.QueryString["id"], out prodId))
                {
                    hfProductId.Value = prodId.ToString();
                    LoadProduct(prodId);
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid product ID.</div>";
                }
            }

        }
        private void LoadCategories()
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem("-- select --", ""));

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CategoryId, CategoryName FROM dbo.Categories ORDER BY CategoryName", conn))
                {
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(rdr.GetString(1), rdr.GetInt32(0).ToString()));
                        }
                    }
                }
            }
        }

        private void LoadProduct(int productId)
        {
            string sql = @"
SELECT ProductName, CategoryId, ShortDescription, LongDescription,
       Price, StockQuantity, IsFeatured, Dimensions, ImageUrl
FROM dbo.Products
WHERE ProductId = @ProductId";

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            txtProductName.Text = rdr["ProductName"].ToString();
                            ddlCategory.SelectedValue = rdr["CategoryId"].ToString();
                            txtShortDescription.Text = rdr["ShortDescription"] != DBNull.Value ? rdr["ShortDescription"].ToString() : "";
                            txtLongDescription.Text = rdr["LongDescription"] != DBNull.Value ? rdr["LongDescription"].ToString() : "";
                            txtPrice.Text = rdr["Price"] != DBNull.Value ? rdr["Price"].ToString() : "0.00";
                            txtStock.Text = rdr["StockQuantity"] != DBNull.Value ? rdr["StockQuantity"].ToString() : "0";
                            chkIsFeatured.Checked = rdr["IsFeatured"] != DBNull.Value && Convert.ToBoolean(rdr["IsFeatured"]);
                            txtDimensions.Text = rdr["Dimensions"] != DBNull.Value ? rdr["Dimensions"].ToString() : "";
                            txtImageUrl.Text = rdr["ImageUrl"] != DBNull.Value ? rdr["ImageUrl"].ToString() : "";
                        }
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(hfProductId.Value, out productId))
                return;

            string imageUrl = null;

            if (fuImage.HasFile)
            {
                var file = fuImage.PostedFile;
                string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (Array.IndexOf(_allowedExt, ext) < 0)
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid image type. Allowed: .jpg, .jpeg, .png</div>";
                    return;
                }
                if (file.ContentLength > MaxFileBytes)
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Image too large. Max 2MB.</div>";
                    return;
                }

                string imagesFolder = Server.MapPath("~/images/products/");
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string uniqueName = $"{Guid.NewGuid()}{ext}";
                string savePath = Path.Combine(imagesFolder, uniqueName);
                file.SaveAs(savePath);
                imageUrl = $"~/images/products/{uniqueName}";
            }
            else if (!string.IsNullOrWhiteSpace(txtImageUrl.Text))
            {
                imageUrl = txtImageUrl.Text.Trim();
            }

            string updateSql = @"
UPDATE dbo.Products
SET ProductName = @ProductName,
    CategoryId = @CategoryId,
    ShortDescription = @ShortDescription,
    LongDescription = @LongDescription,
    Price = @Price,
    StockQuantity = @StockQuantity,
    IsFeatured = @IsFeatured,
    Dimensions = @Dimensions,
    ImageUrl = @ImageUrl,
    UpdatedAt = SYSUTCDATETIME()
WHERE ProductId = @ProductId";

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
                    cmd.Parameters.AddWithValue("@ShortDescription", string.IsNullOrWhiteSpace(txtShortDescription.Text) ? (object)DBNull.Value : txtShortDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@LongDescription", string.IsNullOrWhiteSpace(txtLongDescription.Text) ? (object)DBNull.Value : txtLongDescription.Text.Trim());

                    decimal price;
                    if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                        price = 0m;
                    cmd.Parameters.AddWithValue("@Price", price);

                    int stock;
                    if (!int.TryParse(txtStock.Text.Trim(), out stock))
                        stock = 0;
                    cmd.Parameters.AddWithValue("@StockQuantity", stock);

                    cmd.Parameters.AddWithValue("@IsFeatured", chkIsFeatured.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Dimensions", string.IsNullOrWhiteSpace(txtDimensions.Text) ? (object)DBNull.Value : txtDimensions.Text.Trim());
                    cmd.Parameters.AddWithValue("@ImageUrl", string.IsNullOrWhiteSpace(imageUrl) ? (object)DBNull.Value : imageUrl);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            ltMessage.Text = "<div class='alert alert-success'>Product updated successfully.</div>";
        }
    }
}