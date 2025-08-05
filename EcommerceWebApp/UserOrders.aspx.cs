using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceWebApp
{
    public partial class UserOrders : System.Web.UI.Page

    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
                pnlCheckout.Visible = false;
            }

        }
        private void BindCart()
        {
            if (Session["UserId"] == null)
            {
                ltMessage.Text = "<div class='alert alert-warning'>Please login to view your cart.</div>";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            string sql = @"
SELECT uci.CartItemId,
       p.ProductId,
       p.ProductName,
       uci.Quantity,
       uci.PriceAtAdd,
       uci.AddedDate
FROM UserCartItems uci
JOIN Products p ON p.ProductId = uci.ProductId
WHERE uci.UserId = @UserId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            gvCart.DataSource = dt;
            gvCart.DataBind();

            // compute grand total
            decimal grand = 0m;
            foreach (DataRow row in dt.Rows)
            {
                int qty = Convert.ToInt32(row["Quantity"]);
                decimal price = Convert.ToDecimal(row["PriceAtAdd"]);
                grand += qty * price;
            }

            ltGrandTotal.Text = grand.ToString("C");
            txtPaymentAmount.Text = grand.ToString("F2");
        }

        // Cart gridview edit, update, delete event handlers here
        // (Same as your existing code for gvCart_RowEditing, gvCart_RowCancelingEdit, gvCart_RowUpdating, gvCart_RowDeleting)

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (gvCart.Rows.Count == 0)
            {
                ltMessage.Text = "<div class='alert alert-warning'>Your cart is empty.</div>";
                return;
            }

            // Show the checkout panel with shipping address and amount
            pnlCheckout.Visible = true;
            btnPlaceOrder.Enabled = false;
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Please login to place an order.</div>";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string shippingAddress = txtShippingAddress.Text.Trim();
            decimal paymentAmount;

            if (string.IsNullOrEmpty(shippingAddress))
            {
                ltMessage.Text = "<div class='alert alert-danger'>Please enter a shipping address.</div>";
                return;
            }

            if (!decimal.TryParse(txtPaymentAmount.Text.Trim(), out paymentAmount) || paymentAmount <= 0)
            {
                ltMessage.Text = "<div class='alert alert-danger'>Invalid payment amount.</div>";
                return;
            }

            string getCartSql = @"
SELECT ProductId, Quantity, PriceAtAdd 
FROM UserCartItems 
WHERE UserId = @UserId";

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Get cart items
                    SqlCommand getCartCmd = new SqlCommand(getCartSql, conn, transaction);
                    getCartCmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = getCartCmd.ExecuteReader();

                    List<(int ProductId, int Quantity, decimal PriceAtAdd)> cartItems = new List<(int, int, decimal)>();
                    while (reader.Read())
                    {
                        cartItems.Add((
                            ProductId: reader.GetInt32(0),
                            Quantity: reader.GetInt32(1),
                            PriceAtAdd: reader.GetDecimal(2)
                        ));
                    }
                    reader.Close();

                    if (cartItems.Count == 0)
                    {
                        ltMessage.Text = "<div class='alert alert-warning'>Your cart is empty.</div>";
                        transaction.Rollback();
                        pnlCheckout.Visible = false;
                        btnPlaceOrder.Enabled = true;
                        return;
                    }

                    decimal totalAmount = 0;
                    foreach (var item in cartItems)
                        totalAmount += item.PriceAtAdd * item.Quantity;

                    if (paymentAmount != totalAmount)
                    {
                        ltMessage.Text = $"<div class='alert alert-danger'>Payment amount ({paymentAmount:C}) does not match cart total ({totalAmount:C}).</div>";
                        transaction.Rollback();
                        return;
                    }

                    // Insert Order
                    string insertOrderSql = @"
INSERT INTO Orders (UserId, OrderDate, PaymentStatus, PaymentMethod, ShippingAddress, TotalAmount)
VALUES (@UserId, GETDATE(), @PaymentStatus, @PaymentMethod, @ShippingAddress, @TotalAmount);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand insertOrderCmd = new SqlCommand(insertOrderSql, conn, transaction);
                    insertOrderCmd.Parameters.AddWithValue("@UserId", userId);
                    insertOrderCmd.Parameters.AddWithValue("@PaymentStatus", "Paid"); // or 'Pending' if you want to process later
                    insertOrderCmd.Parameters.AddWithValue("@PaymentMethod", "Online"); // or from user input
                    insertOrderCmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                    insertOrderCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                    int orderId = (int)insertOrderCmd.ExecuteScalar();

                    // Insert OrderDetails
                    string insertOrderDetailSql = @"
INSERT INTO OrderDetails (OrderId, ProductId, Quantity, PriceAtOrder)
VALUES (@OrderId, @ProductId, @Quantity, @PriceAtOrder);";

                    foreach (var item in cartItems)
                    {
                        SqlCommand insertDetailCmd = new SqlCommand(insertOrderDetailSql, conn, transaction);
                        insertDetailCmd.Parameters.AddWithValue("@OrderId", orderId);
                        insertDetailCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                        insertDetailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        insertDetailCmd.Parameters.AddWithValue("@PriceAtOrder", item.PriceAtAdd);
                        insertDetailCmd.ExecuteNonQuery();
                    }

                    // Insert Payment record
                    string insertPaymentSql = @"
INSERT INTO Payments (OrderId, PaymentDate, Amount, PaymentMethod, PaymentStatus)
VALUES (@OrderId, GETDATE(), @Amount, @PaymentMethod, @PaymentStatus);";

                    using (SqlCommand paymentCmd = new SqlCommand(insertPaymentSql, conn, transaction))
                    {
                        paymentCmd.Parameters.AddWithValue("@OrderId", orderId);
                        paymentCmd.Parameters.AddWithValue("@Amount", totalAmount);
                        paymentCmd.Parameters.AddWithValue("@PaymentMethod", "Online");  // or from user input
                        paymentCmd.Parameters.AddWithValue("@PaymentStatus", "Completed");
                        paymentCmd.ExecuteNonQuery();
                    }



                    // Clear Cart
                    string clearCartSql = "DELETE FROM UserCartItems WHERE UserId = @UserId";
                    SqlCommand clearCartCmd = new SqlCommand(clearCartSql, conn, transaction);
                    clearCartCmd.Parameters.AddWithValue("@UserId", userId);
                    clearCartCmd.ExecuteNonQuery();

                    transaction.Commit();

                    ltMessage.Text = "<div class='alert alert-success'>Order placed and payment recorded successfully!</div>";
                    pnlCheckout.Visible = false;
                    btnPlaceOrder.Enabled = true;
                    BindCart();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ltMessage.Text = "<div class='alert alert-danger'>Error placing order: " + Server.HtmlEncode(ex.Message) + "</div>";
                    pnlCheckout.Visible = false;
                    btnPlaceOrder.Enabled = true;
                }
            }
        }

        protected void gvCart_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCart.EditIndex = e.NewEditIndex;
            BindCart();
        }

        protected void gvCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCart.EditIndex = -1;
            BindCart();
        }

        protected void gvCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Implement update logic, or just cancel edit for now
            gvCart.EditIndex = -1;
            BindCart();
        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int cartItemId = Convert.ToInt32(gvCart.DataKeys[e.RowIndex].Value);
            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM UserCartItems WHERE CartItemId = @CartItemId", conn))
            {
                cmd.Parameters.AddWithValue("@CartItemId", cartItemId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindCart();
        }

    }
}