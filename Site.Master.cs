using adminweekendschool.WeekendSchool.Props;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                menu.Text = GenerateMenu();
            }
        }

        private string GenerateMenu()
        {
            string initialMenu = null;

            if (Session["AdminUserInformation"] != null)
            {
                UserProps userObj =  (UserProps)Session["AdminUserInformation"];

                initialMenu = "<li><a class=menu-text runat=server href='AddNewParent'>Add New Parent</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='Verification'>Verification</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='Users'>Users</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='PriceList'>Price List</a></li>";
                //initialMenu += "<li><a class=menu-text runat=server href='SchoolYear'>School Year</a></li>";
                //initialMenu += "<li><a class=menu-text runat=server href='Audit'>Audit</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='StudentReports'>Student Reports</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='Finances'>Finances</a></li>";
                //initialMenu += "<li><a class=menu-text runat=server href='Roles'>Roles</a></li>";
                //initialMenu += "<li><a class=menu-text runat=server href='Help'>Help</a></li>";
                initialMenu += "<li><a class=menu-text runat=server href='Login?Logout=1'>Logout</a></li>";
                initialMenu += "<li> <a> <div class=menu-text >"+ userObj.Name + "  ( " + userObj.UserType + ") </div></a> </li>";
            }
                      

            return initialMenu;
            
        }
    }
}