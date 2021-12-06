using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminweekendschool.PDF;
using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;

namespace adminweekendschool.Admin
{
    public partial class UserInformation : System.Web.UI.Page
    {
        private ArrayList studentList = null;
        protected DataView dvFinace1 = null;
        protected DataSet dsFinance1 = null;
        
        private StudentInformationProps initStudentObj = new StudentInformationProps();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["AdminUserInformation"] == null)
                {
                    Response.Redirect("./Login.aspx?Login=UserInfo");
                }

                string url = HttpContext.Current.Request.Url.AbsoluteUri;


                InitCtrls();
                LoadDataToCtrls();
                ErrorParentMessage(false, "");

                titleMsg.InnerText = "Student Registration Year 2021 / 2022";
            }

            btnUpdateParentInformation.Attributes.Add("OnClick", "return AddNewParent();");

            btnAddNewStudent.Attributes.Add("OnClick", "return AddNewStudent();");
        }

        protected void ErrorParentMessage(bool isVisible, string message)
        {
            lblParentMessage.Text = message;
            lblParentMessage.Visible = isVisible;
        }

        protected void LoadDataToCtrls()
        {
            Int32 parentId = Convert.ToInt32(Request.QueryString["parentId"]);
            hdnParentId.Value = ""+parentId;
            LoginInformationProps loginObj = DBSqlWeekendSchool.getParentStudentsInformationById(parentId);
            Session["InitUserInformation"] = loginObj;
            Session["UserInformation"] = loginObj;

            txtUsername.Text = loginObj.UserName;
            txtParentFirstName.Text = loginObj.FirstName;
            txtParentLastName.Text = loginObj.LastName;
            string phone = (""+ loginObj.Phone).Replace("(","").Replace(")","").Replace("-","");
            if(phone.Length == 10)
            {
                txtHomePhone1.Text = phone.Substring(0, 3);
                txtHomePhone2.Text = phone.Substring(4, 3);
                txtHomePhone3.Text = phone.Substring(6,4);
            }

            txtEmailAddress.Text = loginObj.Email;

            dgStudentBindData();
        }


        protected void InitCtrls()
        {
            getStudentAge();
            getStudentLevel();
            getStudentStatus();


        }

        protected DataSet getStudentStatus()
        {
            DataSet dsStudentStatus = DBUtils.getStudentStatus();
            ddlStudentStatus.DataSource = dsStudentStatus;
            ddlStudentStatus.DataValueField = "STUDENT_STATUS_CODE";
            ddlStudentStatus.DataTextField = "STUDENT_STATUS_DESC";
            ddlStudentStatus.DataBind();
            return dsStudentStatus;
        }


        protected DataSet getStudentAge()
        {
            DataSet dsStudentAge = DBUtils.getStudentAge();
            ddlAddStudentAge.DataSource = dsStudentAge;
            ddlAddStudentAge.DataValueField = "Age_Id";
            ddlAddStudentAge.DataTextField = "Age_Desc";
            ddlAddStudentAge.DataBind();
            return dsStudentAge;
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

        protected DataSet getStudentPaymentStatus()
        {
            DataSet dsStudentPayment = DBUtils.getStudentPaymentStatus();
            ddlStudentPaymentStatus.DataSource = dsStudentPayment;
            ddlStudentPaymentStatus.DataValueField = "PAYMENT_STATUS_ID";
            ddlStudentPaymentStatus.DataTextField = "PAYMENT_STATUS_DESC";
            ddlStudentPaymentStatus.DataBind();
            return dsStudentPayment;
        }

        protected void btnUpdateParentInformation_Click(object sender, EventArgs e)
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
            parentObj.UserName = txtUsername.Text.Trim().ToUpper();
            parentObj.FirstName = txtParentFirstName.Text.Trim().ToUpper();
            parentObj.LastName = txtParentLastName.Text.Trim().ToUpper() ;
            parentObj.Phone = txtHomePhone1.Text.Trim() + txtHomePhone2.Text.Trim() + txtHomePhone3.Text.Trim();
            parentObj.Email = txtEmailAddress.Text.Trim();

            LoginInformationProps loginObj = DBSqlWeekendSchool.IsUsernameOrEmailExists(parentObj.UserName, parentObj.Email, parentObj.ParentId);

            UserProps adminObj = (UserProps)Session["AdminUserInformation"];
            Int32 auditId = DBSqlWeekendSchool.Audit(adminObj.UserName, parentObj.ParentId);

            LoginInformationProps initParentObj = DBSqlWeekendSchool.getParentStudentsInformationById(parentObj.ParentId);
            List<AuditDetailsProps> auditDetailsList = DBSqlWeekendSchool.AuditDetailsParentComparaison(auditId, initParentObj, parentObj);
            DBSqlWeekendSchool.AuditDetails(auditDetailsList);

            if (loginObj != null)
            {
                ErrorParentMessage(true, "Username Or Email is used by another subscriber. Choose another one");
            }
            else
            {

                DBSqlWeekendSchool.updateParentInformation(parentObj);
                ErrorParentMessage(true, "Parent information have been updated");
                Session["UserInformation"] = parentObj;
                dgStudentBindData();
            }


            
        }


        protected void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];

            StudentInformationProps studentObj = new StudentInformationProps();
            studentObj.ParentId = parentObj.ParentId;
            studentObj.FirstName = txtStudentFirstName.Text;
            studentObj.LastName = txtParentLastName.Text;
            studentObj.Age = Convert.ToInt32(ddlAddStudentAge.SelectedValue);
            studentObj.Level = Convert.ToInt32(ddbAddStudentLevel.SelectedValue);
            studentObj.EnrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);


            this.initStudentObj = studentObj;
            List< StudentInformationProps> studentsList = DBSqlWeekendSchool.addNewStudents(studentObj);
            dgStudentBindData();

            StudentInformationProps initStudentObj = DBSqlWeekendSchool.getStudentInformation(studentObj.StudentId, studentObj.EnrollementYear);

            UserProps adminObj = (UserProps)Session["AdminUserInformation"];
            Int32 auditId = DBSqlWeekendSchool.Audit(adminObj.UserName, studentObj.ParentId);
            LoginInformationProps initParentObj = DBSqlWeekendSchool.getParentStudentsInformationById(studentObj.ParentId);
            List<AuditDetailsProps> auditDetailsList = DBSqlWeekendSchool.AuditDetailsStudentComparaison(auditId, initStudentObj, studentObj);
            DBSqlWeekendSchool.AuditDetails(auditDetailsList);



            //Show student grid
            dgStudents.Visible = true;

            //Initialize all fields
            txtStudentFirstName.Text = "";
            txtStudentFirstName.Focus();
            ddlAddStudentAge.SelectedIndex = 0;
            ddbAddStudentLevel.SelectedIndex = 0;

            headerMessage.InnerText = ">>>>  "+ studentObj.FirstName +" " + studentObj.LastName +" is added to the School Database";
            displayHeaderMessage(true);


        }

        private void displayHeaderMessage(bool isDirty)
        {
            if (isDirty)
            {
                headerMessage.Attributes["style"] = "color:Yellow; ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scrollKey", "scrollTo(0,0);", true);
            }
        }

        private void dgStudentBindData()
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
            Int32 parentId = parentObj.ParentId;
            Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
           
            this.dsFinance1 = DBSqlWeekendSchool.getStudentsList( parentId,  enrollementYear);
            dgStudents.DataSource = this.dsFinance1;
            dgStudents.DataBind();

            Session["DvFinace1"] = this.dsFinance1.Tables["StudentsList"].DefaultView;

            DataView dvStudent = (DataView)Session["DvFinace1"];

            if (dvStudent.Count == 0)
                dgStudents.Visible = false;
            else
                dgStudents.Visible = true;

        }

        protected string GetAgeName(string ageId)
        {
            int iLoop;
            string loopAgeId;
            string ageDesc;
            DataTable dt = (DataTable)getStudentAge().Tables["StudentAge"];

            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopAgeId = dt.Rows[iLoop]["Age_id"].ToString();
                ageDesc = dt.Rows[iLoop]["Age_Desc"].ToString();
                if (loopAgeId.Equals(ageId))
                    return ageDesc;
            };

            return "";
        }

       

        protected string GetLevelName(string levelId)
        {
            int iLoop;
            string loopLevelId;
            string levelDesc;
            DataTable dt = (DataTable)getStudentLevel().Tables["StudentLevel"];

            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopLevelId = dt.Rows[iLoop]["Level_id"].ToString();
                levelDesc = dt.Rows[iLoop]["Level_Desc"].ToString();
                if (loopLevelId.Equals(levelId))
                    return levelDesc;
            };

            return "";
        }

        protected string GetStudentStatusName(string status)
        {
            int iLoop;
            string loopStatusId;
            string statusDesc;
            DataTable dt = (DataTable)getStudentStatus().Tables["StudentStatus"];

            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopStatusId = dt.Rows[iLoop]["STUDENT_STATUS_CODE"].ToString();
                statusDesc = dt.Rows[iLoop]["STUDENT_STATUS_DESC"].ToString();
                if (loopStatusId.Equals(status))
                    return statusDesc;
            };

            return "";
        }





        protected int GetSelectedIndexLevel(string level)
        {
            int iLoop;
            string loopLevelId = "";
            string levelDesc = "";
            DataTable dt = (DataTable)getStudentLevel().Tables["StudentLevel"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopLevelId = dt.Rows[iLoop]["Level_id"].ToString();
                levelDesc = dt.Rows[iLoop]["Level_Desc"].ToString();
                if (loopLevelId.Equals(level))
                    return iLoop;
            };

            return 0;
        }

        protected int GetSelectedIndexStudentStatus(string status)
        {
            int iLoop;
            string loopStudentStatusCode = "";
            string statusDesc = "";
            DataTable dt = (DataTable)getStudentStatus().Tables["StudentStatus"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopStudentStatusCode = dt.Rows[iLoop]["STUDENT_STATUS_CODE"].ToString();
                statusDesc = dt.Rows[iLoop]["STUDENT_STATUS_DESC"].ToString();
                if (loopStudentStatusCode.Equals(status))
                    return iLoop;
            };

            return 0;
        }




        protected int GetSelectedIndexAge(string age)
        {
            int iLoop;
            string loopAgeId = "";
            string ageDesc = "";
            DataTable dt = (DataTable)getStudentAge().Tables["StudentAge"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopAgeId = dt.Rows[iLoop]["Age_id"].ToString();
                ageDesc = dt.Rows[iLoop]["Age_Desc"].ToString();
                if (loopAgeId.Equals(age))
                    return iLoop;
            };

            return 0;
        }


        protected int GetSelectedStudentPaymentStatus(string paymentId)
        {
            if (paymentId == null)
                paymentId = "1";

            int iLoop;
            string loopPaymentId = "";
            string levelDesc = "";
            DataTable dt = (DataTable)getStudentPaymentStatus().Tables["StudentPaymentStatus"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopPaymentId = dt.Rows[iLoop]["PAYMENT_STATUS_ID"].ToString();
                levelDesc = dt.Rows[iLoop]["PAYMENT_STATUS_DESC"].ToString();
                if (loopPaymentId.Equals(paymentId))
                    return iLoop;
            };

            return 0;
        }

        protected string GetSelectedStudentPaymentStatusName(string paymentId)
        {
            if (paymentId == null)
                paymentId = "1";

            int iLoop;
            string loopPaymentId = "";
            string paymentDesc = "";
            DataTable dt = (DataTable)getStudentPaymentStatus().Tables["StudentPaymentStatus"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopPaymentId = dt.Rows[iLoop]["PAYMENT_STATUS_ID"].ToString();
                paymentDesc = dt.Rows[iLoop]["PAYMENT_STATUS_DESC"].ToString();
                if (loopPaymentId.Equals(paymentId))
                    return paymentDesc;
            };

            return "";
        }



        protected int Payment(Int32 age)
        {
            int iLoop;
            string loopAgeId = "";
            string ageDesc = "";
            DataTable dt = (DataTable)getStudentAge().Tables["StudentAge"];
            for (iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
            {
                loopAgeId = dt.Rows[iLoop]["Age_id"].ToString();
                ageDesc = dt.Rows[iLoop]["Age_Desc"].ToString();
                if (loopAgeId.Equals(age))
                    return iLoop;
            };

            return 0;
        }




        public void dg_Update(Object s, DataGridCommandEventArgs e)
        {
            StudentInformationProps studentObj = new StudentInformationProps();
            int studentId = Convert.ToInt32(dgStudents.DataKeys[e.Item.ItemIndex]);
            studentObj.ParentId = Convert.ToInt32(Request.QueryString["parentId"]);
            studentObj.StudentId = studentId;
            TextBox txtFirstName = (TextBox)e.Item.Cells[1].FindControl("txtFN");
            studentObj.FirstName = txtFirstName.Text;
            studentObj.LastName = txtParentLastName.Text;
            DropDownList ddlAgeObj = (DropDownList)e.Item.Cells[3].FindControl("ddlStudentAge");
            studentObj.Age = Convert.ToInt32(ddlAgeObj.SelectedItem.Value);
            DropDownList ddlLevelsObj = (DropDownList)e.Item.Cells[4].FindControl("ddlStudentLevel");
            studentObj.Level = Convert.ToInt32(ddlLevelsObj.SelectedItem.Value);
            DropDownList ddlPaymentObj = (DropDownList)e.Item.Cells[5].FindControl("ddlStudentPaymentStatus");
            studentObj.StudentPaymentStatus = Convert.ToInt32(ddlPaymentObj.SelectedItem.Value);
            studentObj.EnrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            DropDownList ddlStudentStatusObj = (DropDownList)e.Item.Cells[6].FindControl("ddlStudentStatus");
            studentObj.StudentStatus = Convert.ToString(ddlStudentStatusObj.SelectedItem.Value);


            StudentInformationProps initStudentObj = DBSqlWeekendSchool.getStudentInformation(studentObj.StudentId, studentObj.EnrollementYear);
            DBSqlWeekendSchool.updateStudentInformation(studentObj);
            DBSqlWeekendSchool.updateStudentStatus(studentId, studentObj.StudentStatus);

            UserProps adminObj = (UserProps)Session["AdminUserInformation"];
            Int32 auditId = DBSqlWeekendSchool.Audit(adminObj.UserName, studentObj.ParentId);
            Int32 parentId = Convert.ToInt32(Request.QueryString["parentId"]);
           
            List<AuditDetailsProps> auditDetailsList = DBSqlWeekendSchool.AuditDetailsStudentComparaison(auditId, initStudentObj, studentObj);
            DBSqlWeekendSchool.AuditDetails(auditDetailsList);

            //audit Here
            this.initStudentObj = studentObj;

            dgStudents.EditItemIndex = -1;
            dgStudentBindData();

            headerMessage.InnerText = ">>>>  " + studentObj.FirstName + " " + studentObj.LastName + " is updated into the School Database";
            displayHeaderMessage(true);

        }

        public void dg_Cancel(Object s, DataGridCommandEventArgs e)
        {
            dgStudents.EditItemIndex = -1;
            dgStudentBindData();
        }

        public void dg_Edit(Object s, DataGridCommandEventArgs e)
        {
            dgStudents.EditItemIndex = e.Item.ItemIndex;
            dgStudentBindData();
        }

        public void dg_Delete(Object s, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string test = "" + e.CommandArgument;

                int studentId = Convert.ToInt32(dgStudents.DataKeys[e.Item.ItemIndex]);
                DBSqlWeekendSchool.deleteStudentInformation(studentId);
                dgStudentBindData();

                Label lblFirstName = (Label)e.Item.Cells[1].FindControl("lblFirstName");
                headerMessage.InnerText = ">>>>  " + lblFirstName.Text + " " + txtParentLastName.Text + " is deleted from the School Database";
                displayHeaderMessage(true);


                //Audit
            }
        }

        protected void dg_Page(Object s, DataGridPageChangedEventArgs e)
        {
            dgStudents.CurrentPageIndex = e.NewPageIndex;
        }

        protected void btnPrintPayments_Click(object sender, EventArgs e)
        {
            RegistrationForm regObj = new RegistrationForm();

            LoginInformationProps parentObj = (LoginInformationProps) Session["UserInformation"];

            this.dvFinace1 = (DataView)Session["DvFinace1"];

            string fileName = regObj.generatePDFfile(parentObj);//, studentObj, financeObj1);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "OpenPdfFile('" + fileName + "');", true);
        }
    }
}