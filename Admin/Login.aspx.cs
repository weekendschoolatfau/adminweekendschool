using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;


namespace adminweekendschool.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                string logout = Request.Params["Logout"];

                if ((logout != null)&&(logout.Trim().Equals("1")))
                {
                    Session["AdminUserInformation"] = null;
                    Session["UserInformation"] = null;
                    Session["TuitionByLevel"] = null;
                    Response.Redirect("./login.aspx");
                }

                InitCtrls();

            }

            btnLogin.Attributes.Add("OnClick", "return ValidateLogin();");
        }

        protected void activateMenu()
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim().ToUpper();
            string password = txtPassword.Text.Trim();
            UserProps loginObj = DBSqlWeekendSchool.getUserLoginInformation( email,  password);

            if (loginObj != null)
            {
                List<string> modulesList = DBSqlWeekendSchool.getUserModules(loginObj.UserName);
                loginObj.moduleList = modulesList;

                Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
                Session["TuitionByLevel"] = DBSqlWeekendSchool.getTuitionByLevel(enrollementYear);
                Session["AdminUserInformation"] = loginObj;
                Response.Redirect("./welcome.aspx");
            }

            dvMessage.Visible = true;
            lblMessage.Text = "Username or password is incorrect";
            Session["AdminUserInformation"] = null;
            Session["TuitionByLevel"] = null;
           
           


        }

        protected void NewUser_Click(object sender, EventArgs e)
        {

            Response.Redirect("./NewUser.aspx");
        }

        protected void ForgetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ForgetPassword.aspx");
        }

        protected void InitCtrls()
        {
            dvMessage.Visible = false;
            lblMessage.Text = "";
        }
    }
}