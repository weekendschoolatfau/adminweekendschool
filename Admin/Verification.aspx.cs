using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class Verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["AdminUserInformation"] == null)
                {
                    Response.Redirect("./Login.aspx?Login=UserInfo");
                }

                isAuthorized();

                getSchoolYear();
                getStudentStatus();
                getVerificationList();
            }
        }

        private void isAuthorized()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;

            List<string> moduleList = ((UserProps)Session["AdminUserInformation"]).moduleList;

            bool isFound = false;

            if ((moduleList != null) && (moduleList.Count > 0))
            {
                string moduleName = "";

                for (int i = 0; ((i < moduleList.Count) && (!isFound)); i++)
                {
                    moduleName = moduleList[i];

                    if (url.Contains(moduleName))
                    {
                        isFound = true;
                    }
                }

                if (!isFound)
                {
                    Response.Redirect("./ErrorPage.aspx");
                }

            }
            else
            {
                Response.Redirect("./ErrorPage.aspx");
            }

        }


        protected void getSchoolYear()
        {
            DataSet dsStudentLevel = DBUtils.getSchoolYear();
            ddbSchoolYear.DataSource = dsStudentLevel;
            ddbSchoolYear.DataValueField = "SCHOOL_YEAR_ID";
            ddbSchoolYear.DataTextField = "SCHOOL_YEAR_DESC";
            ddbSchoolYear.DataBind();
        }

        protected void getStudentStatus()
        {
            DataSet dsStudentStatus = DBUtils.getStudentStatus();
            ddbStudentStatus.DataSource = dsStudentStatus;
            ddbStudentStatus.DataValueField = "STUDENT_STATUS_CODE";
            ddbStudentStatus.DataTextField = "STUDENT_STATUS_DESC";
            ddbStudentStatus.DataBind();
        }

        protected void getVerificationList()
        {
            string lastName = txtParentLastName.Text.Trim().ToUpper();
            string phoneNumber = txtHomePhone1.Text.Trim() + txtHomePhone2.Text.Trim() + txtHomePhone3.Text.Trim();
            string email = txtEmail.Text.Trim().ToUpper();
            string schoolYear = ddbSchoolYear.SelectedValue;
            string studentStatus = ddbStudentStatus.SelectedValue;

            if ((studentStatus != null) && (studentStatus.Trim().Equals("A")))
                studentStatus = null;

             DataSet ds = DBSqlWeekendSchool.getVerificationList(lastName, phoneNumber, email, schoolYear, studentStatus);
            dgVerificationList.DataSource = ds;
            dgVerificationList.DataBind();
           
            DataView dvStudent = ds.Tables["VerificationList"].DefaultView;

        }

        public void dg_Delete(Object s, DataGridCommandEventArgs e)
        {

        }


        protected void btnProcess_Click(object sender, EventArgs e)
        {
            getVerificationList();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtHomePhone1.Text = "";
            txtHomePhone2.Text = "";
            txtHomePhone3.Text = "";
            txtParentLastName.Text = "";
            txtEmail.Text = "";
            ddbSchoolYear.SelectedIndex = 0;
            ddbStudentStatus.SelectedIndex = 0;
        }
    }
}