using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitCtrls();
            }
        }

        protected void InitCtrls()
        {
            dvMessage.Visible = false;
            lblMessage.Text = "";
            Session["UserInformation"] = null;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            dvMessage.Visible = true;
            lblMessage.Text = "An email was sent to your email account to reset your password";
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("./login.aspx");
        }

    }
}