using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["AdminUserInformation"] == null)
                {
                    Response.Redirect("./Login.aspx?Login=UserInfo");
                }



            }
        }
    }
}