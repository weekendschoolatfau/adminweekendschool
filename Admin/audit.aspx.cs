using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class audit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string studentId = Request.Params["studentId"];

            UserProps adminObj = null;

            if (Session["AdminUserInformation"] != null)
            {
                 adminObj = (UserProps)Session["AdminUserInformation"];
            }

            if (!Page.IsPostBack)
            {
                Int32 parentId = Convert.ToInt32(Request.QueryString["parentId"]);
                Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
                DataSet dsDocuments = DBSqlWeekendSchool.getAuditList(parentId, enrollementYear);
                dgAudit.DataSource = dsDocuments;
                dgAudit.DataBind();
            }
        }
    }
}