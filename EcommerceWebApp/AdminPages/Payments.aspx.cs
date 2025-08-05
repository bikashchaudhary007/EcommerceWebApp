using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EcommerceWebApp.AdminPages
{
    public partial class Payments : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPaymentsGrid(0);
            }

        }

        private void BindPaymentsGrid(int pageIndex)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT PaymentId, OrderId, PaymentDate, Amount, PaymentMethod, TransactionId, PaymentStatus
                         FROM Payments
                         ORDER BY PaymentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Calculate total amount
                decimal totalAmount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["Amount"]);
                }

                lblTotalAmount.Text = totalAmount.ToString("C");

                gvPayments.DataSource = dt;
                gvPayments.PageIndex = pageIndex;
                gvPayments.DataBind();
            }
        }


        protected void gvPayments_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            BindPaymentsGrid(e.NewPageIndex);
        }
    }
}