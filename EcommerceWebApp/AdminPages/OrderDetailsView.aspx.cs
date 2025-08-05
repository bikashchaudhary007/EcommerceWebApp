using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EcommerceWebApp.AdminPages
{
    public partial class OrderDetailsView : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["orderId"], out int orderId))
                {
                    LoadOrderInfo(orderId);
                    BindOrderDetails(orderId);
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid order ID.</div>";
                }
            }


        }

        private void LoadOrderInfo(int orderId)
        {
            string sql = "SELECT OrderStatus, PaymentStatus, OrderDate FROM Orders WHERE OrderId = @OrderId";

            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    string orderStatus = rdr["OrderStatus"]?.ToString() ?? "Unknown";
                    string paymentStatus = rdr["PaymentStatus"]?.ToString() ?? "Unknown";
                    DateTime orderDate = rdr.GetDateTime(rdr.GetOrdinal("OrderDate"));

                    lblOrderStatus.Text = $"Order Status: <strong>{orderStatus}</strong>";
                    lblPaymentStatus.Text = $"Payment Status: <strong>{paymentStatus}</strong>";
                    lblOrderDate.Text = $"Order Date: <strong>{orderDate:yyyy-MM-dd}</strong>";
                }
                else
                {
                    lblOrderStatus.Text = "Order not found";
                    lblPaymentStatus.Text = "";
                    lblOrderDate.Text = "";
                }
            }
        }


        private void BindOrderDetails(int orderId)
        {
            string sql = @"
SELECT od.Quantity, od.PriceAtOrder, p.ProductName, p.ImageUrl
FROM OrderDetails od
JOIN Products p ON od.ProductId = p.ProductId
WHERE od.OrderId = @OrderId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count == 0)
            {
                ltMessage.Text = "<div class='alert alert-info'>No details found for this order.</div>";
            }
            else
            {
                ltMessage.Text = "";
            }

            gvOrderDetails.DataSource = dt;
            gvOrderDetails.DataBind();
        }


    }
}