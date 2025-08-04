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
    public partial class Product : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        private readonly string[] _allowedExt = new[] { ".jpg", ".jpeg", ".png" };
        private const int MaxFileBytes = 2 * 1024 * 1024; // 2MB
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                // set sensible defaults
                txtPrice.Text = "0.00";
                txtStock.Text = "0";
            }
        }


        private void LoadCategories()
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem("-- select --", ""));

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT CategoryId, CategoryName FROM dbo.Categories ORDER BY CategoryName", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string name = rdr.GetString(1);
                        ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(name, id.ToString()));
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ltMessage.Text = "";

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(ddlCategory.SelectedValue))
            {
                ShowMessage("Product name and category are required.", "alert-danger");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
                price = 0m;

            if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                stock = 0;

            bool isFeatured = chkIsFeatured.Checked;

            string imageUrl = null;

            // Handle uploaded file (takes precedence)
            if (fuImage.HasFile)
            {
                var file = fuImage.PostedFile;
                string ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (Array.IndexOf(_allowedExt, ext) < 0)
                {
                    ShowMessage("Invalid image type. Only .jpg, .jpeg, .png allowed.", "alert-danger");
                    return;
                }

                if (file.ContentLength > MaxFileBytes)
                {
                    ShowMessage("Image too large. Max 2MB.", "alert-danger");
                    return;
                }

                try
                {
                    string imagesFolder = Server.MapPath("~/images/products/");
                    if (!Directory.Exists(imagesFolder))
                        Directory.CreateDirectory(imagesFolder);

                    string uniqueName = $"{Guid.NewGuid()}{ext}";
                    string savePath = Path.Combine(imagesFolder, uniqueName);
                    file.SaveAs(savePath);

                    // store relative (resolved later when rendering)
                    imageUrl = $"~/images/products/{uniqueName}";
                }
                catch (Exception ex)
                {
                    ShowMessage("Failed to save image: " + ex.Message, "alert-danger");
                    return;
                }
            }
           

            try
            {
                using (var conn = new SqlConnection(_cs))
                using (var cmd = new SqlCommand(@"
INSERT INTO dbo.Products
(
    ProductName, CategoryId, ShortDescription, LongDescription,
    Price, StockQuantity, IsFeatured, Dimensions, ImageUrl
)
VALUES
(
    @ProductName, @CategoryId, @ShortDescription, @LongDescription,
    @Price, @StockQuantity, @IsFeatured, @Dimensions, @ImageUrl
);", conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
                    cmd.Parameters.AddWithValue("@ShortDescription",
                        string.IsNullOrWhiteSpace(txtShortDescription.Text) ? (object)DBNull.Value : txtShortDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@LongDescription",
                        string.IsNullOrWhiteSpace(txtLongDescription.Text) ? (object)DBNull.Value : txtLongDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@StockQuantity", stock);
                    cmd.Parameters.AddWithValue("@IsFeatured", isFeatured ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Dimensions",
                        string.IsNullOrWhiteSpace(txtDimensions.Text) ? (object)DBNull.Value : txtDimensions.Text.Trim());
                    cmd.Parameters.AddWithValue("@ImageUrl",
                        string.IsNullOrWhiteSpace(imageUrl) ? (object)DBNull.Value : imageUrl);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                ShowMessage("Product created successfully.", "alert-success");
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Insert failed: " + ex.Message, "alert-danger");
            }

        }


        private void ShowMessage(string text, string bootstrapClass)
        {
            ltMessage.Text = $"<div class='alert {bootstrapClass}'>{text}</div>";
        }

        private void ClearForm()
        {
            txtProductName.Text = "";
            ddlCategory.SelectedIndex = 0;
            txtShortDescription.Text = "";
            txtLongDescription.Text = "";
            txtPrice.Text = "0.00";
            txtStock.Text = "0";
            chkIsFeatured.Checked = false;
            txtDimensions.Text = "";
           
        }
    }
}