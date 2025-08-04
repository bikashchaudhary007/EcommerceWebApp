using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }

        }

        private void BindProducts()
        {
            string sql = @"
        SELECT ProductId, ProductName, Price, ShortDescription, ImageUrl
        FROM dbo.Products
        ORDER BY ProductName";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ECDB"].ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
        }

        protected string ResolveImageUrl(object imageUrlObj)
        {
            string imageUrl = imageUrlObj as string;
            if (string.IsNullOrEmpty(imageUrl))
                return ResolveUrl("~/images/placeholder.png");

            return ResolveUrl(imageUrl);
        }
    }
}