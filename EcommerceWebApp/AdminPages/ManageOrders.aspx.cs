using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;

namespace EcommerceWebApp.AdminPages
{
    public partial class ManageOrders : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
            }

        }

        private void LoadOrders()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT 
                        o.OrderId,
                        u.Fullname AS CustomerName,
                        o.OrderDate,
                        p.ImageUrl,
                        p.ProductName,
                        od.Quantity,
                        o.TotalAmount,
                        ISNULL(pay.PaymentStatus, 'Pending') AS PaymentStatus,
                        o.OrderStatus
                    FROM Orders o
                    INNER JOIN Users u ON o.UserId = u.Id
                    INNER JOIN OrderDetails od ON o.OrderId = od.OrderId
                    INNER JOIN Products p ON od.ProductId = p.ProductId
                    LEFT JOIN Payments pay ON o.OrderId = pay.OrderId
                    ORDER BY o.OrderDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrders.EditIndex = e.NewEditIndex;
            LoadOrders();
        }

        protected void gvOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrders.EditIndex = -1;
            LoadOrders();
        }

        protected void gvOrders_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int orderId = Convert.ToInt32(gvOrders.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvOrders.Rows[e.RowIndex];
            DropDownList ddlOrderStatus = (DropDownList)row.FindControl("ddlOrderStatus");

            string newStatus = ddlOrderStatus.SelectedValue;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Orders SET OrderStatus = @OrderStatus WHERE OrderId = @OrderId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OrderStatus", newStatus);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            gvOrders.EditIndex = -1;
            LoadOrders();
        }
    }
}