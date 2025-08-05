using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApp
{
    public partial class ContactPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // You can add code here to save data or send email
                // For now, just show a success message

                lblResult.Text = "Thank you for contacting us. We will get back to you soon!";
                lblResult.Visible = true;

                // Clear form fields
                txtName.Text = "";
                txtEmail.Text = "";
                txtSubject.Text = "";
                txtMessage.Text = "";
            }
        }
    }
}