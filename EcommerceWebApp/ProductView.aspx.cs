using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp
{
    public partial class ProductView : System.Web.UI.Page
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
            Response.Redirect("Default.aspx"); // Redirect back to the list page
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || !string.Equals(Session["UserRole"] as string, "user", StringComparison.OrdinalIgnoreCase))
            {
                ltMessage.Text = "<div class='alert alert-warning'>Login as user to add to cart.</div>";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            if (!int.TryParse(Request.QueryString["id"], out int productId))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid product.</div>";
                return;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int qty) || qty <= 0)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Quantity must be positive.</div>";
                return;
            }

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                conn.Open();

                // get current price & stock
                string prodSql = "SELECT Price, StockQuantity FROM Products WHERE ProductId = @ProductId";
                decimal price;
                int stock;
                using (SqlCommand prodCmd = new SqlCommand(prodSql, conn))
                {
                    prodCmd.Parameters.AddWithValue("@ProductId", productId);
                    using (SqlDataReader dr = prodCmd.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            ltMessage.Text = "<div class='alert alert-danger'>Product not found.</div>";
                            return;
                        }
                        price = dr.GetDecimal(dr.GetOrdinal("Price"));
                        stock = dr.GetInt32(dr.GetOrdinal("StockQuantity"));
                    }
                }

                if (qty > stock)
                {
                    ltMessage.Text = $"<div class='alert alert-warning'>Only {stock} available.</div>";
                    return;
                }

                // upsert into cart
                string selectCart = @"
            SELECT CartItemId, Quantity 
            FROM UserCartItems 
            WHERE UserId = @UserId AND ProductId = @ProductId";
                using (SqlCommand sel = new SqlCommand(selectCart, conn))
                {
                    sel.Parameters.AddWithValue("@UserId", userId);
                    sel.Parameters.AddWithValue("@ProductId", productId);
                    using (SqlDataReader dr = sel.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int cartItemId = dr.GetInt32(dr.GetOrdinal("CartItemId"));
                            int existingQty = dr.GetInt32(dr.GetOrdinal("Quantity"));
                            dr.Close();

                            int newQty = existingQty + qty;
                            if (newQty > stock) newQty = stock;

                            string upd = "UPDATE UserCartItems SET Quantity=@Quantity WHERE CartItemId=@CartItemId";
                            using (SqlCommand up = new SqlCommand(upd, conn))
                            {
                                up.Parameters.AddWithValue("@Quantity", newQty);
                                up.Parameters.AddWithValue("@CartItemId", cartItemId);
                                up.ExecuteNonQuery();
                            }

                            ltMessage.Text = "<div class='alert alert-success'>Cart updated.</div>";
                        }
                        else
                        {
                            dr.Close();
                            string insert = @"
                        INSERT INTO UserCartItems (UserId, ProductId, Quantity, PriceAtAdd)
                        VALUES (@UserId,@ProductId,@Quantity,@PriceAtAdd)";
                            using (SqlCommand ins = new SqlCommand(insert, conn))
                            {
                                ins.Parameters.AddWithValue("@UserId", userId);
                                ins.Parameters.AddWithValue("@ProductId", productId);
                                ins.Parameters.AddWithValue("@Quantity", qty);
                                ins.Parameters.AddWithValue("@PriceAtAdd", price);
                                ins.ExecuteNonQuery();
                            }

                            ltMessage.Text = "<div class='alert alert-success'>Added to cart.</div>";
                        }
                    }
                }
            }
        }
    }
}