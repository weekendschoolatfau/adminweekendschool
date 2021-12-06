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
    public partial class PriceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                dgTuitionFeeBindData();
            }

            isAuthorized();

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

        private void dgTuitionFeeBindData()
        {
            Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            DataSet dsDocuments = DBSqlWeekendSchool.getTuitionFeeList(enrollementYear);
            dgTuitionFee.DataSource = dsDocuments;
            dgTuitionFee.DataBind();
        }

        public void dg_Update(Object s, DataGridCommandEventArgs e)
        {
            Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

            int levelId = Convert.ToInt32(dgTuitionFee.DataKeys[e.Item.ItemIndex]);

            TextBox txtTuitionFee= (TextBox)e.Item.Cells[1].FindControl("txtTuitionFee");
            decimal tuitionFee = Convert.ToDecimal(txtTuitionFee.Text.Trim().Replace("$",""));

            TextBox txtTShirtPrice = (TextBox)e.Item.Cells[1].FindControl("txtTShirtPrice");
            decimal tshirtFee = Convert.ToDecimal(txtTShirtPrice.Text.Trim().Replace("$", ""));

            TextBox txtBookPrice = (TextBox)e.Item.Cells[1].FindControl("txtBookPrice");
            decimal bookPrice = Convert.ToDecimal(txtBookPrice.Text.Trim().Replace("$", ""));



            DBSqlWeekendSchool.updateTuitionByLevel(levelId, tuitionFee, tshirtFee, bookPrice, enrollementYear);

            Session["TuitionByLevel"] = DBSqlWeekendSchool.getTuitionByLevel(enrollementYear);


            dgTuitionFee.EditItemIndex = -1;
            dgTuitionFeeBindData();

        }

        public void dg_Cancel(Object s, DataGridCommandEventArgs e)
        {
            dgTuitionFee.EditItemIndex = -1;
            dgTuitionFeeBindData();
        }

        public void dg_Edit(Object s, DataGridCommandEventArgs e)
        {
            dgTuitionFee.EditItemIndex = e.Item.ItemIndex;
            dgTuitionFeeBindData();
        }


       

    }
}