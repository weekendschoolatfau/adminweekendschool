using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminweekendschool.Admin
{
    public partial class Payments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["AdminUserInformation"] == null)
                {
                    Response.Redirect("./Login.aspx?Login=UserInfo");
                }


               
                lblPaymentMessage.Visible = false;

                LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];

                if ((parentObj.Phone != null)&& (parentObj.Phone.Trim().Length == 10))
                {
                    txtPhoneNumber.Text = "( " + parentObj.Phone.Substring(0, 3) + " )" + " " + parentObj.Phone.Substring(4, 3) + "-" + parentObj.Phone.Substring(6, 4);
                }
                else
                    txtPhoneNumber.Text = "" + parentObj.Phone;

                txtCardHolderName.Text = parentObj.FirstName + " " + parentObj.LastName;

               

                Int32 studentId = Convert.ToInt32(Request.QueryString["studentId"]);
                Int32 enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

                //Student Information
                StudentInformationProps studentObj = DBSqlWeekendSchool.getStudentInformation(studentId, enrollementYear);
                Session["StudentObj"] = studentObj;
                txtStudentName.Text = studentObj.FirstName + " " + studentObj.LastName;
                txtPaymentDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtTotalAmountPayed.Text = "$0.00";

                //PaymentInformation
                studentPaymentInformation(studentId, enrollementYear);

               

            }

            DateTime dt = DateTime.Now;
            int month = dt.Month;

            //if (month>= 0 && month <=6)
            //    lblSemester.Text = "2nd Semester Payment";
            //else
            //    lblSemester.Text = "1st Semester Payment";

            btnProcessPayment.Attributes.Add("OnClick", "return StudentPayment();");

        }

        private void studentPaymentInformation(Int32 studentId, Int32 enrollementYear)
        {
            List<PaymentInformationProps>   paymentsList = DBSqlWeekendSchool.getPaymentsInformation(studentId, enrollementYear);
            pnlPaymentformation.Visible = true;
            pnlCash.Visible = true;

            if ((paymentsList != null) && (paymentsList.Count == 1))
            {
                PaymentInformationProps paymentObj = paymentsList[0];

                if (paymentObj != null)
                {
                    if ((paymentObj.TotalPaymentToPay == paymentObj.TotalPaymentPayed)&& (paymentObj.PaymentPlan == 1))
                    {
                        pnlPaymentformation.Visible = false;
                        pnlCash.Visible = false;
                        pnlCheck.Visible = false;
                        btnProcessPayment.Visible = false;

                        if (paymentObj.PaymentDate != null)
                        {
                            txtPaymentDate.Text = paymentObj.PaymentDate;
                        }

                        if (paymentObj.PaymentPlan == 2)
                            chbPaymentPlan.Checked = true;
                        else if (paymentObj.PaymentPlan ==1)
                            chbPaymentFull.Checked = true;

                        if (paymentObj.PaymentType == 4)
                            chbPaymentTypeDebit.Checked = true;
                        else if (paymentObj.PaymentType == 3)
                            chbPaymentTypeCredit.Checked = true;
                        else if (paymentObj.PaymentType == 1)
                        {
                            chbPaymentTypeCheck.Checked = true;
                            txtCheckNumber.Text = paymentObj.CheckNumber;
                            txtCheckNumber.Enabled = false;
                            txtCheckAmount.Text = "$" + paymentObj.TotalPaymentPayed;
                            txtCheckAmount.Enabled = false;
                            pnlCheck.Visible = true;
                        }
                        else if (paymentObj.PaymentType == 2)
                            chbPaymentTypeCash.Checked = true;

                        txtTuitionFee.Text = "$" + paymentObj.Tuition;
                        txtUniformPrice.Text = "$" + paymentObj.TShirt;
                        txtBookPrice.Text = "$" + paymentObj.Books;
                        txtTotalAmountPayed.Text = "$" + paymentObj.TotalPaymentPayed;
                        txtTotalAmountToPay.Text = "$0.00"  ;
                        txtCheckNumber.Text = paymentObj.CheckNumber;
                        txtCardHolderName.Enabled = false;

                        lblPaymentMessage.Visible = true;
                        lblPaymentMessage.Text = "Total Payment Was Processed";


                    }
                    else if ((paymentObj.TotalPaymentToPay != paymentObj.TotalPaymentPayed) && (paymentObj.PaymentType == 2))
                    {
                        pnlPaymentformation.Visible = true;
                        pnlCash.Visible = false;
                        pnlCheck.Visible = false;
                        btnProcessPayment.Visible = true;

                        if (paymentObj.PaymentDate != null)
                        {
                            txtPaymentDate.Text = paymentObj.PaymentDate;
                        }

                        if (paymentObj.PaymentPlan == 2)
                            chbPaymentPlan.Checked = true;
                        else if (paymentObj.PaymentPlan == 1)
                            chbPaymentFull.Checked = true;

                        if (paymentObj.PaymentType == 4)
                            chbPaymentTypeDebit.Checked = true;
                        else if (paymentObj.PaymentType == 3)
                            chbPaymentTypeCredit.Checked = true;
                        else if (paymentObj.PaymentType == 1)
                            chbPaymentTypeCheck.Checked = true;
                        else if (paymentObj.PaymentType == 2)
                            chbPaymentTypeCash.Checked = true;

                        txtTuitionFee.Text = "$" + paymentObj.Tuition;
                        txtUniformPrice.Text = "$" + paymentObj.TShirt;
                        txtBookPrice.Text = "$" + paymentObj.Books;
                        txtTotalAmountPayed.Text = "$" + paymentObj.TotalPaymentPayed;
                        txtTotalAmountToPay.Text = "$" + ((paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books) / 4) + " of " + ((paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books));


                        lblPaymentMessage.Visible = true;

                    }
                }
            }
            else if ((paymentsList != null) && (paymentsList.Count > 1))
            {
                PaymentInformationProps paymentObj = paymentsList[0];

                if (paymentObj != null)
                {
                     if ((paymentObj.TotalPaymentToPay != paymentObj.TotalPaymentPayed) && (paymentObj.PaymentPlan == 2))
                    {
                        pnlPaymentformation.Visible = true;
                        pnlCash.Visible = false;
                        pnlCheck.Visible = false;
                        btnProcessPayment.Visible = true;

                        if (paymentObj.PaymentDate != null)
                        {
                            txtPaymentDate.Text = paymentObj.PaymentDate;
                        }

                        if (paymentObj.PaymentPlan == 2)
                            chbPaymentPlan.Checked = true;
                        else if (paymentObj.PaymentPlan == 1)
                            chbPaymentFull.Checked = true;

                        if (paymentObj.PaymentType == 4)
                            chbPaymentTypeDebit.Checked = true;
                        else if (paymentObj.PaymentType == 3)
                            chbPaymentTypeCredit.Checked = true;

                        txtTuitionFee.Text = "$" + paymentObj.Tuition;
                        txtUniformPrice.Text = "$" + paymentObj.TShirt;
                        txtBookPrice.Text = "$" + paymentObj.Books;

                        decimal totalPayed = 0;

                        for (int i = 0; i < paymentsList.Count; i++)
                        {
                            totalPayed = totalPayed + paymentsList[i].TotalPaymentPayed;
                        }



                        txtTotalAmountPayed.Text = "$" + totalPayed;

                        if (totalPayed != (paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books))
                            txtTotalAmountToPay.Text = "$" + ((paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books) / 4) + " of " + ((paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books));
                        else
                        {
                            txtTotalAmountToPay.Text = "$0.00";
                            txtCardHolderName.Enabled = false;
                            pnlPaymentformation.Visible = false;
                            pnlCash.Visible = false;
                            pnlCheck.Visible = false;
                            btnProcessPayment.Visible = false;
                            lblPaymentMessage.Visible = true;
                            lblPaymentMessage.Text = "Total Payment Was Processed";
                        }



                        lblPaymentMessage.Visible = true;

                    }
                }
            }
            else
            {
                chbPaymentFull.Checked = true;
               // chbPaymentTypeDebit.Checked = true;
                initDebit();
                totalPayment();
            }

        }

        private void totalPayment()
        {
            StudentInformationProps studentObj = (StudentInformationProps)Session["StudentObj"];

            List<TuitionByLevelProps> tuitionByLevelList = (List<TuitionByLevelProps>)Session["TuitionByLevel"];

            var tuitionByLevelObj = (from c in tuitionByLevelList where c.LevelId == studentObj.Level select (c.LevelId, c.TuitionFee, c.TShirtPrice, c.BookPrice)).ToList();
            txtTuitionFee.Text = "$" + tuitionByLevelObj[0].TuitionFee;
            txtUniformPrice.Text = "$" + tuitionByLevelObj[0].TShirtPrice;
            txtBookPrice.Text = "$" + tuitionByLevelObj[0].BookPrice;

            decimal totalPayment = (tuitionByLevelObj[0].TuitionFee + tuitionByLevelObj[0].TShirtPrice + tuitionByLevelObj[0].BookPrice);

            if ((studentObj.PaymentPlan == 0)|| (studentObj.PaymentPlan == 1))
                txtTotalAmountToPay.Text = "$" + totalPayment;
            else if (studentObj.PaymentPlan == 2)
                txtTotalAmountToPay.Text =   "$" + (totalPayment/4) + " Of " + "$" + totalPayment;
     

            
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            LoginInformationProps parentObj = (LoginInformationProps)Session["UserInformation"];
            Response.Redirect("./UserInformation.aspx?parentId="+ parentObj.ParentId);
        }

        protected void chbPaymentTypeDebit_CheckedChanged(object sender, EventArgs e)
        {

            initDebit();
        }


        private void initDebit()
        {
            chbPaymentTypeDebit.Checked = true;
            pnlCardHolder.Visible = true;
            txtCardHolderName.Enabled = true;
            txtCardNumber.Enabled = true;
            txtThreeDigits.Enabled = true;
            txtExpirationDate.Enabled = true;
            txtCheckAmount.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtCashAmount.Enabled = false;

            txtCashAmount.Text = "";
            txtCheckAmount.Text = "";

            pnlPaymentformation.Visible = true;
            pnlCash.Visible = false;
            pnlCheck.Visible = false;

            chbPaymentFull.Visible = true;
            chbPaymentPlan.Visible = true;
        }

        protected void chbPaymentTypeCredit_CheckedChanged(object sender, EventArgs e)
        {
            txtCardHolderName.Enabled = true;
            txtCardNumber.Enabled = true;
            txtThreeDigits.Enabled = true;
            txtExpirationDate.Enabled = true;
            txtCheckAmount.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtCashAmount.Enabled = false;

            txtCashAmount.Text = "";
            txtCheckAmount.Text = "";

            pnlPaymentformation.Visible = true;
            pnlCash.Visible = false;
            pnlCheck.Visible = false;
            chbPaymentFull.Visible = true;
            chbPaymentPlan.Visible = true;
            pnlCardHolder.Visible = true;
        }

        protected void chkPayPal_CheckedChanged(object sender, EventArgs e)
        {
            txtCardHolderName.Enabled = false;
            txtCardNumber.Enabled = false;
            txtThreeDigits.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCashAmount.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtCheckAmount.Enabled = false;

            txtCardHolderName.Text = "";
            txtCardNumber.Text = "";
            txtThreeDigits.Text = "";
            txtExpirationDate.Text = "";
            txtCashAmount.Text = "";
            txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            pnlPaymentformation.Visible = false;
            pnlCash.Visible = false;
        }

        protected void btnProcessPayment_Click(object sender, EventArgs e)
        {
            StudentInformationProps studentObj = (StudentInformationProps)Session["StudentObj"];

            PaymentInformationProps paymentObj = new PaymentInformationProps();
            paymentObj.ParentId = studentObj.ParentId;
            paymentObj.StudentId = studentObj.StudentId;
            paymentObj.EnrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

            DateTime dt = DateTime.Now;

            if (dt.Month >= 0 && dt.Month <= 6)
                paymentObj.SchoolPeriod = 2;
            else if (dt.Month >= 8 && dt.Month <= 12)
                paymentObj.SchoolPeriod = 1;

            if (chbPaymentFull.Checked)
                paymentObj.PaymentPlan = 1;
            else if (chbPaymentPlan.Checked)
                paymentObj.PaymentPlan = 2;

            if (chbPaymentTypeDebit.Checked)
                paymentObj.PaymentType = 4;
            else if (chbPaymentTypeCredit.Checked)
                paymentObj.PaymentType = 3;
            else if (chbPaymentTypeCash.Checked)
            {
                paymentObj.PaymentType = 2;
                txtTotalAmountToPay.Text = txtCashAmount.Text;
            }
            else if (chbPaymentTypeCheck.Checked)
            {
                paymentObj.PaymentType = 1;
                txtTotalAmountToPay.Text = txtCheckAmount.Text;
            }

            paymentObj.Tuition = Convert.ToDecimal(txtTuitionFee.Text.Trim().Replace("$", ""));
            paymentObj.TShirt = Convert.ToDecimal(txtUniformPrice.Text.Trim().Replace("$", ""));
            paymentObj.Books = Convert.ToDecimal(txtBookPrice.Text.Trim().Replace("$", ""));


            if (chbPaymentFull.Checked)
            {
                txtTotalAmountPayed.Text = txtTotalAmountToPay.Text;
            }
            else if (chbPaymentPlan.Checked)
            {
                txtTotalAmountPayed.Text = "$" + ((paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books) / 4);
            }

            paymentObj.TotalPaymentToPay = Convert.ToDecimal(txtTotalAmountPayed.Text.Trim().Replace("$", ""));
            paymentObj.TotalPaymentPayed = Convert.ToDecimal(txtTotalAmountPayed.Text.Trim().Replace("$", ""));
            paymentObj.CheckNumber = txtCheckNumber.Text;

            DBSqlWeekendSchool.addPaymentInformation(paymentObj);
           studentPaymentInformation(paymentObj.StudentId, paymentObj.EnrollementYear); 
        }

        protected void chbPaymentPlan_CheckedChanged(object sender, EventArgs e)
        {
            StudentInformationProps studentObj = (StudentInformationProps)Session["StudentObj"];
            studentObj.PaymentPlan = 2;
            Session["StudentObj"] = studentObj;
            totalPayment();
        }

        protected void chbPaymentFull_CheckedChanged(object sender, EventArgs e)
        {
            StudentInformationProps studentObj = (StudentInformationProps)Session["StudentObj"];
            studentObj.PaymentPlan = 1;
            Session["StudentObj"] = studentObj;
            totalPayment();
        }

        protected void chbPaymentTypeCash_CheckedChanged(object sender, EventArgs e)
        {
            pnlCardHolder.Visible = false;
            txtCardHolderName.Enabled = true;
            txtCardHolderName.Enabled = false;
            txtCardNumber.Enabled = false;
            txtThreeDigits.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCheckAmount.Enabled = false;
            txtCheckNumber.Enabled = false;
            txtCashAmount.Enabled = false;
            txtCashAmount.Text = txtTotalAmountToPay.Text;
            txtCheckAmount.Text = "";
            pnlPaymentformation.Visible = false;
            pnlCash.Visible = true;
            pnlCheck.Visible = false;
            chbPaymentFull.Visible = true;
            chbPaymentFull.Checked = true;
            chbPaymentPlan.Visible = false;

        }

        protected void chbPaymentTypeCheck_CheckedChanged(object sender, EventArgs e)
        {
            pnlCardHolder.Visible = false;
            txtCardHolderName.Enabled = false;
            txtCardNumber.Enabled = false;
            txtThreeDigits.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCheckAmount.Enabled = false;

            txtCheckAmount.Text = txtTotalAmountToPay.Text;
            txtCheckNumber.Enabled = true;
            txtCashAmount.Enabled = false;
            txtCashAmount.Text = "";

            pnlPaymentformation.Visible = false;
            pnlCash.Visible = false;
            pnlCheck.Visible = true;
            chbPaymentFull.Visible = true;
            chbPaymentFull.Checked = true;
            chbPaymentPlan.Visible = false;
        }
    }
}