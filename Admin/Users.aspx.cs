using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class Users : System.Web.UI.Page
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
                getAllUsers();
                getAllRoles();

                dgUsersListBindData();
            }

            btnAddNewUser.Attributes.Add("OnClick", "return  addNewUser();");

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

        protected void getAllUsers()
        {
            DataSet dsUsers = DBSqlWeekendSchool.getAllUsersList();
            ddbUsersList.DataSource = dsUsers;
            ddbUsersList.DataValueField = "Username";
            ddbUsersList.DataTextField = "Name";
            ddbUsersList.DataBind();
            
        }

        protected void getAllRoles()
        {
            DataSet dsRole = DBSqlWeekendSchool.getAllRolesList();
            ddbRolesList.DataSource = dsRole;
            ddbRolesList.DataValueField = "ROLE_ID";
            ddbRolesList.DataTextField = "ROLE_NAME";
            ddbRolesList.DataBind();

        }

        private void dgUsersListBindData()
        {
            //Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            DataSet dsUsers = DBSqlWeekendSchool.getUsersList(); ;
            dgUsersList.DataSource = dsUsers;
            dgUsersList.DataBind();
        }

        public void dg_Update(Object s, DataGridCommandEventArgs e)
        {
            //Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

            //int levelId = Convert.ToInt32(dgTuitionFee.DataKeys[e.Item.ItemIndex]);

            //TextBox txtTuitionFee = (TextBox)e.Item.Cells[1].FindControl("txtTuitionFee");
            //decimal tuitionFee = Convert.ToDecimal(txtTuitionFee.Text.Trim().Replace("$", ""));

            //TextBox txtTShirtPrice = (TextBox)e.Item.Cells[1].FindControl("txtTShirtPrice");
            //decimal tshirtFee = Convert.ToDecimal(txtTShirtPrice.Text.Trim().Replace("$", ""));

            //TextBox txtBookPrice = (TextBox)e.Item.Cells[1].FindControl("txtBookPrice");
            //decimal bookPrice = Convert.ToDecimal(txtBookPrice.Text.Trim().Replace("$", ""));



            //DBSqlWeekendSchool.updateTuitionByLevel(levelId, tuitionFee, tshirtFee, bookPrice, enrollementYear);

            //Session["TuitionByLevel"] = DBSqlWeekendSchool.getTuitionByLevel(enrollementYear);


            //dgTuitionFee.EditItemIndex = -1;
            //dgTuitionFeeBindData();

        }

        public void dg_Cancel(Object s, DataGridCommandEventArgs e)
        {
            //dgTuitionFee.EditItemIndex = -1;
            //dgTuitionFeeBindData();
        }

        public void dg_Edit(Object s, DataGridCommandEventArgs e)
        {
            //dgTuitionFee.EditItemIndex = e.Item.ItemIndex;
            //dgTuitionFeeBindData();
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            string username = ddbUsersList.SelectedValue;
            string password = RandomString(8);
            int roleId = Convert.ToInt32(ddbRolesList.SelectedValue);
            string roleName = ddbRolesList.SelectedItem.Text;

            DBSqlWeekendSchool.insertNewUser(username, password, roleName, roleId);

            dgUsersListBindData();

            //Email The username and password to the user vis this system

            lblMessage.Text = "" + ddbUsersList.SelectedItem.Text + " is added to the user list";
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}