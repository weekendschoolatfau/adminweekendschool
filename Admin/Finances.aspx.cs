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
    public partial class Finances : System.Web.UI.Page
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
                getStudentPaymentStatus();
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
            string studentStatus = ddlStudentPaymentStatus.SelectedValue;
            Int32 level = Convert.ToInt32(ddbAddStudentLevel.SelectedValue);

            if ((studentStatus != null) && (studentStatus.Trim().Equals("A")))
                studentStatus = null;

            DataSet ds = DBSqlWeekendSchool.getFinanceReport( schoolYear, studentStatus, level);
            dgVerificationList.DataSource = ds;
            dgVerificationList.DataBind();

            DataView dvStudent = ds.Tables["FinanceList"].DefaultView;

            decimal amountDue = 0;
            decimal amountPayed = 0;
            decimal amountRemaining = 0;
            for (int i = 0; i < dvStudent.Count; i++)
            {
                amountDue = amountDue + Convert.ToDecimal(dvStudent[i]["TOTAL_AMOUNT_DUE"].ToString());
                amountPayed = amountPayed + Convert.ToDecimal(dvStudent[i]["TOTAL_AMOUNT_PAYED"].ToString());
                amountRemaining = amountRemaining + Convert.ToDecimal(dvStudent[i]["REMAINING"].ToString());
            }

            lblAmountDue.Text = "" + amountDue;
            lblAmountPayed.Text = "" + amountPayed;
            lblAmountRemaining.Text = "" + amountRemaining;

            if (amountDue == 0)
                pnlTotal.Visible = false;
            else
                pnlTotal.Visible = true;

        }

        public void dg_Delete(Object s, DataGridCommandEventArgs e)
        {

        }


        protected void btnProcess_Click(object sender, EventArgs e)
        {
            getVerificationList();
        }

         protected DataSet getStudentPaymentStatus()
        {
            DataSet dsStudentPayment = DBUtils.getStudentPaymentStatus();
            ddlStudentPaymentStatus.DataSource = dsStudentPayment;
            ddlStudentPaymentStatus.DataValueField = "PAYMENT_STATUS_ID";
            ddlStudentPaymentStatus.DataTextField = "PAYMENT_STATUS_DESC";
            ddlStudentPaymentStatus.DataBind();
            ddlStudentPaymentStatus.Items.Insert(0, new ListItem("All", "0"));
            return dsStudentPayment;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
           // txtHomePhone1.Text = "";
           // txtHomePhone2.Text = "";
           // txtHomePhone3.Text = "";
           // txtParentLastName.Text = "";
           // txtEmail.Text = "";
            ddbSchoolYear.SelectedIndex = 0;
            ddlStudentPaymentStatus.SelectedIndex = 0;
        }
    }
}