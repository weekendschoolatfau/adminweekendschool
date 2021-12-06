using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminweekendschool.PDF;
using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;

namespace adminweekendschool.Admin
{
    public partial class AddNewParent : System.Web.UI.Page
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

                btnUpdateParentInformation.Attributes.Add("OnClick", "return AddNewParent();");

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

        protected void ErrorParentMessage(bool isVisible, string message)
        {
            lblParentMessage.Text = message;
            lblParentMessage.Visible = isVisible;
        }


        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        protected void btnUpdateParentInformation_Click(object sender, EventArgs e)
        {
            LoginInformationProps parentObj = new LoginInformationProps(); //   (LoginInformationProps)Session["UserInformation"];
           
            parentObj.FirstName = txtParentFirstName.Text.Trim().ToUpper();
            parentObj.LastName = txtParentLastName.Text.Trim().ToUpper();
            parentObj.Phone = txtHomePhone1.Text.Trim() + txtHomePhone2.Text.Trim() + txtHomePhone3.Text.Trim();
            parentObj.Email = txtEmailAddress.Text.Trim();
            string[] username = parentObj.Email.Trim().Split('@');

            parentObj.UserName = username[0];
            parentObj.Password = CreatePassword(8);
            parentObj.PreferrredContact = "Email";
            parentObj.SecurityQuestion = "1";



            LoginInformationProps loginObj = DBSqlWeekendSchool.IsUsernameOrEmailExists(parentObj.UserName, parentObj.Email, parentObj.ParentId);

            if (loginObj != null)
            {
                ErrorParentMessage(true, "Username Or Email is used by another subscriber. Choose another one");
            }
            else
            {
                UserProps adminObj = (UserProps)Session["AdminUserInformation"];

                LoginInformationProps newParentObj = DBSqlWeekendSchool.addNewParent(parentObj);
                Int32 auditId = DBSqlWeekendSchool.Audit(adminObj.UserName, newParentObj.ParentId);
                List<AuditDetailsProps> auditDetailsList = DBSqlWeekendSchool.AuditDetailsParentComparaison(auditId, null, newParentObj);
                DBSqlWeekendSchool.AuditDetails(auditDetailsList);

                Response.Redirect("./UserInformation.aspx?parentId="+ newParentObj.ParentId);

            }
        }

        private void displayHeaderMessage(bool isDirty)
        {
            if (isDirty)
            {
                headerMessage.Attributes["style"] = "color:Yellow; ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scrollKey", "scrollTo(0,0);", true);
            }
        }

    }
}