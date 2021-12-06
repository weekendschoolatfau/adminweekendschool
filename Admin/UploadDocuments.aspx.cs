using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;
using adminweekendschool.WeekendSchool.DS;
using System.Data;

namespace adminweekendschool.Admin
{
    public partial class UploadDocuments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["AdminUserInformation"] == null)
                {
                    Response.Redirect("./Login.aspx?Login=UserInfo");
                }

                lblErrorMessage.Visible = false;

                InitCtrls();
                uploadStudentInformation();
                uploadDocumentsList();
            }

            btnUploadDocument.Attributes.Add("OnClick", "return UploadDocument_Click();");
        }

        protected void getStudentStatus()
        {
            DataSet dsStudentStatus = DBUtils.getStudentStatus();
            ddbStudentStatus.DataSource = dsStudentStatus;
            ddbStudentStatus.DataValueField = "STUDENT_STATUS_CODE";
            ddbStudentStatus.DataTextField = "STUDENT_STATUS_DESC";
            ddbStudentStatus.DataBind();
        }


        protected void uploadDocumentsList()
        {
            Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);
            Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            DataSet dsDocuments = DBSqlWeekendSchool.getDocumentsList( studentId, enrollementYear);
            dgDocuments.DataSource = dsDocuments;
            dgDocuments.DataBind();

            DataView dvDocument = dsDocuments.Tables["StudentDocumentsList"].DefaultView;

            if (dvDocument.Count == 0)
                dgDocuments.Visible = false;
            else
                dgDocuments.Visible = true;
        }

        public void dg_Delete(Object s, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string test = "" + e.CommandArgument;


                int documentId = Convert.ToInt32(dgDocuments.DataKeys[e.Item.ItemIndex]);
                DBSqlWeekendSchool.deleteDocument(documentId);
                uploadDocumentsList();


            }
        }


        protected void uploadStudentInformation()
        {
            Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);
            Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

            //Student Information
            StudentInformationProps studentObj = DBSqlWeekendSchool.getStudentInformation(studentId, enrollementYear);

            txtStudentFirstName.Text = studentObj.FirstName;
            txtStudentLastName.Text = studentObj.LastName;
            txtStudentAge.Text = "" + studentObj.Age;
            txtStudentLevel.Text = ""+ studentObj.Level;

            if ((studentObj.StudentStatus != null) && (!studentObj.StudentStatus.Trim().Equals("")))
            {
                ddbStudentStatus.SelectedValue = studentObj.StudentStatus;

                if (studentObj.StudentStatus.Trim().Equals("P"))
                {
                    ddbStudentStatus.Enabled = false;
                    btnApplicationStatus.Visible = false;
                }
                else
                {
                    ddbStudentStatus.Enabled = true;
                    btnApplicationStatus.Visible = true;
                }

            }

        }


        protected void InitCtrls()
        {
            getDocumentsType();
            getStudentStatus();

        }

        private DataSet getDocumentsType()
        {
            DataSet dsDocumentsType = DBUtils.getDocumentsType();
            ddlDocumentsType.DataSource = dsDocumentsType;
            ddlDocumentsType.DataValueField = "Document_Type_Id";
            ddlDocumentsType.DataTextField = "Document_Description";
            ddlDocumentsType.DataBind();
            return dsDocumentsType;
        }


        protected void btnUploadDocument_Click(object sender, EventArgs e)
        {

            string sourceFullPathFileName = "";
            string destinationFullPathFileName = "";
            string serverPath = Utility.getFileFullPath();

            if (assetsFileUpload.HasFile)
            {
                string extension = Path.GetExtension(assetsFileUpload.PostedFile.FileName);

                if (extension.ToLower() == ".pdf")
                {
                    LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
                    Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);
                    sourceFullPathFileName = serverPath + assetsFileUpload.FileName;
                    destinationFullPathFileName = @"\\WeekendSchool\" + ConfigurationManager.AppSettings["EnrollementYear"] + @"\" + parentObj.ParentId + @"\" + studentId + @"\"+ assetsFileUpload.FileName;
                    uploadDocuments(assetsFileUpload.FileName, destinationFullPathFileName);
                    headerMessage.InnerText = ">>>>   Your PDF file is uploaded. ";
                }
                else
                {
                    headerMessage.InnerText = ">>>>   The file extension not permitted: " + extension;
                }
            }
            else
            {
                headerMessage.InnerText = ">>>>   You must select a PDF file to upload by clicking on 'Choose file' button" ;
            }

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


        private void uploadDocuments(string fileName, string filePath )
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
            UserProps adminObj = (UserProps)Session["AdminUserInformation"];
            Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);

            DocumentInformationProps documentObj = new DocumentInformationProps();
            documentObj.StudentId = studentId;
            documentObj.ParentId = parentObj.ParentId;
            documentObj.EnrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            documentObj.DocumentType = Convert.ToInt32(ddlDocumentsType.SelectedValue);
            documentObj.DocumentName = fileName;
            documentObj.DocumentPath= filePath;
            documentObj.AddedBy = adminObj.Name;
            documentObj.IsParent = "N";

            DBSqlWeekendSchool.addDocumentformation(documentObj);

            uploadDocumentsList();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
            Response.Redirect("./UserInformation.aspx?parentId=" + parentObj.ParentId);
        }

        protected void btnApplicationStatus_Click(object sender, EventArgs e)
        {
            string applicationStatus = ddbStudentStatus.SelectedValue;
            Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);
            DBSqlWeekendSchool.updateStudentStatus(studentId, applicationStatus);
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Application Status Updated";
        }
    }
}