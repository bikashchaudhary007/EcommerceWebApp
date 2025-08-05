using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;



namespace EcommerceWebApp
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    ltMessage.Text = "<div class='alert alert-warning'>Please login to view your orders.</div>";
                    return;
                }
                BindOrders();
            }

        }

        private void BindOrders()
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            string sql = @"
SELECT OrderId, OrderDate, TotalAmount, PaymentStatus, ShippingAddress
FROM Orders
WHERE UserId = @UserId
ORDER BY OrderDate DESC";

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

            if (dt.Rows.Count == 0)
            {
                ltMessage.Text = "<div class='alert alert-info'>No orders found.</div>";
            }
            else
            {
                ltMessage.Text = "";
            }

            gvOrders.DataSource = dt;
            gvOrders.DataBind();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                // Redirect to an order details page, passing the order id
                Response.Redirect($"OrderDetails.aspx?orderId={orderId}");
            }
        }
    }
}