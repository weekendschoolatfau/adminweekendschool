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
    public partial class StudentReports : System.Web.UI.Page
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
               
                getStudentLevel();
                getSchoolYear();

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

        protected DataSet getStudentLevel()
        {
            DataSet dsStudentLevel = DBUtils.getStudentLevel();
            ddbAddStudentLevel.DataSource = dsStudentLevel;
            ddbAddStudentLevel.DataValueField = "Level_Id";
            ddbAddStudentLevel.DataTextField = "Level_Desc";
            ddbAddStudentLevel.DataBind();
            return dsStudentLevel;
        }

        protected void getVerificationList()
        {

            string schoolYear = ddbSchoolYear.SelectedValue;
           
            Int32 level = Convert.ToInt32(ddbAddStudentLevel.SelectedValue);

         

            DataSet ds = DBSqlWeekendSchool.getStudentByLevelReport(schoolYear,  level);
            dgVerificationList.DataSource = ds;
            dgVerificationList.DataBind();

            DataView dvStudent = ds.Tables["StudentByLevelList"].DefaultView;

           

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
          
            ddbSchoolYear.SelectedIndex = 0;
           
        }
    }
}