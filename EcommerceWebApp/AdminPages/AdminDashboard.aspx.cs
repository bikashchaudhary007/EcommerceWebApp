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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
            }

        }

        private void LoadDashboardData()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                lblPlacedOrders.Text = GetOrderCount(con, "New").ToString();
                lblShippedOrders.Text = GetOrderCount(con, "Shipped").ToString();
                lblProcessingOrders.Text = GetOrderCount(con, "Processing").ToString();
                lblDeliveredOrders.Text = GetOrderCount(con, "Delivered").ToString();
                lblCancelledOrders.Text = GetOrderCount(con, "Cancelled").ToString();

                lblTotalUsers.Text = GetTotalUsers(con).ToString();
            }
        }

        private int GetOrderCount(SqlConnection con, string status)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE OrderStatus = @status", con))
            {
                cmd.Parameters.AddWithValue("@status", status);
                return (int)cmd.ExecuteScalar();
            }
        }

        private int GetTotalUsers(SqlConnection con)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users", con))
            {
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}