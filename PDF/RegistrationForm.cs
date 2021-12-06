using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

using adminweekendschool.WeekendSchool.DS;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;
using System.Collections.Generic;

namespace adminweekendschool.PDF
{
    public class RegistrationForm
    {

        public RegistrationForm()
        {

        }

        public string generatePDFfile(LoginInformationProps parentObj)
        {
            int enrollementYear = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);
            List<StudentPaymentInformationProps> studentsFianceList = DBSqlWeekendSchool.getStudentsPaymentInformation(parentObj.ParentId, enrollementYear);

            if (parentObj == null)
                return "";

            string fileName = "Payment_Form_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".pdf";

            Document document = new Document();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"C:\GitHub\adminweekendschool\adminweekendschool\Files\" + fileName, FileMode.Create));

                PdfAcroForm acroForm = writer.AcroForm;

                document.Open();

                PdfContentByte cb = writer.DirectContent;

                cb.BeginText();
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 14);

                // we show some text starting on some absolute position with a given alignment
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Weekend School", 295, 740, 0);
                cb.SetFontAndSize(bf, 10);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(954) 234-4773", 295, 727, 0);
                cb.SetFontAndSize(bf, 14);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Payment Receipt", 295, 714, 0);
                cb.SetFontAndSize(bf, 10);

                int schoolYearTemp = Convert.ToInt32(ConfigurationManager.AppSettings["EnrollementYear"]);

                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "School year " + schoolYearTemp + " / " + (schoolYearTemp + 1), 295, 700, 0);
                cb.SetFontAndSize(bf, 14);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Student Information", 20, 685, 0);
                cb.EndText();

                cb.MoveTo(20, 680f);
                cb.LineTo(575, 680f);
               
                cb.MoveTo(20, 665f);
                cb.LineTo(575, 665f);
               
                cb.MoveTo(20, 680f);
                cb.LineTo(20, 645f);
                cb.MoveTo(125, 680f);
                cb.LineTo(125, 645f);
                cb.MoveTo(225, 680f);
                cb.LineTo(225, 645f);
                cb.MoveTo(475, 680f);
                cb.LineTo(475, 645f);
                cb.MoveTo(575, 680f);
                cb.LineTo(575, 645f);

                cb.Stroke();
                cb.BeginText();
                cb.SetFontAndSize(bf, 11);
                if (parentObj.FirstName == null)
                    parentObj.FirstName = "";

                if (parentObj.LastName == null)
                    parentObj.LastName = "";

                decimal totalReaiming = 0;
                decimal totalAmtToPay = 0;
                Int32 step = 668 - 15;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Student's First Name", 21, 668, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Student's Age", 130, 668, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Parent First Name", 255, 668, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Family's Name", 480, 668, 0);
                cb.EndText();

                for (int d = 0; d < studentsFianceList.Count; d++)
                {
                   
                    cb.BeginText();

                    if (d == 0)
                    {
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, parentObj.FirstName, 260, step, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, parentObj.LastName, 480, step, 0);
                    }

                    StudentPaymentInformationProps studentPaymentObj = studentsFianceList[d];

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Convert.ToString(studentPaymentObj.FirstName ), 30, step, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Convert.ToString(studentPaymentObj.Age), 150, step, 0);
                    cb.EndText();
                    cb.MoveTo(20, step - 2);
                    cb.LineTo(225, step - 2);
                    cb.MoveTo(20, step);
                    cb.LineTo(20, step - 13);
                    cb.MoveTo(125, step);
                    cb.LineTo(125, step - 13);
                    cb.MoveTo(225, step);
                    cb.LineTo(225, step - 13);
                    cb.MoveTo(475, step);
                    cb.LineTo(475, step - 13);
                    cb.MoveTo(575, step);
                    cb.LineTo(575, step - 13);
                    cb.Stroke();
                    step = step - 13;

                }

                cb.MoveTo(20, step - 2);
                cb.LineTo(575, step - 2);
                cb.Stroke();

                step = step - 30;

                cb.BeginText();
                cb.SetFontAndSize(bf, 14);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Contact Information", 25, step, 0);
                cb.EndText();

                if (parentObj.Phone == null)
                    parentObj.Phone = "";

                if (parentObj.Email == null)
                    parentObj.Email = "";

                step = step - 5;
                cb.MoveTo(20, step);
                cb.LineTo(575, step);

                cb.MoveTo(20, step - 50);
                cb.LineTo(575, step - 50);

                cb.MoveTo(20, step);
                cb.LineTo(20, step - 50);
                cb.MoveTo(280, step);
                cb.LineTo(280, step - 50);
                cb.MoveTo(575, step);
                cb.LineTo(575, step - 50);
                cb.Stroke();
                step = step - 15;

                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Telephone Num.", 80, step, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, parentObj.Phone, 80, step - 25, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "E-mail", 380, step, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, parentObj.Email, 310, step - 25, 0);
                cb.SetFontAndSize(bf, 11);
                step = step - 20;

                cb.SetFontAndSize(bf, 14);
                step = step - 40;
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Financial Information", 20, step - 8, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "First Name", 20, step - 35, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tuition", 110, step - 35, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Books", 180, step - 35, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "T-shirt", 300, step - 35, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Total Amount Due ($)", 420, step - 35, 0);

                cb.EndText();

                step = step - 15;
                int step1 = step;

                //Big Rectangle
                cb.MoveTo(20, step);
                cb.LineTo(575, step);

                cb.MoveTo(20, step);
                cb.LineTo(20, step - 25);

                cb.MoveTo(575f, step);
                cb.LineTo(575f, step - 25);

              

                cb.MoveTo(20, step - 25);
                cb.LineTo(575, step - 25);


                step = step - 45;

                for (int d = 0; d < studentsFianceList.Count; d++)
                {
                    StudentPaymentInformationProps studentPaymentObj = studentsFianceList[d];

                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                   cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, String.Format("{0:C}", Convert.ToString(studentPaymentObj.FirstName)), 30, step, 0);

                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", Convert.ToDecimal(studentPaymentObj.Tuition)), 150, step, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", Convert.ToDecimal(studentPaymentObj.Books)), 210, step, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", Convert.ToDecimal(studentPaymentObj.TShirt)), 330, step, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", Convert.ToDecimal("0" + studentPaymentObj.TotalPaymentToPay)), 540, step, 0);
                    cb.EndText();

                    step = step - 15;

                    totalAmtToPay = totalAmtToPay + studentPaymentObj.TotalPaymentToPay;
                    totalReaiming = totalReaiming + studentPaymentObj.TotalPaymentRemaining;

                }


                cb.MoveTo(20, step1);
                cb.LineTo(20, step - 53);

                cb.MoveTo(410, step1);
                cb.LineTo(410, step - 53);

                cb.MoveTo(575, step1);
                cb.LineTo(575, step - 53);

                cb.MoveTo(20, step);
                cb.LineTo(575, step);

                cb.MoveTo(20, step - 23);
                cb.LineTo(575, step - 23);

                cb.MoveTo(20, step - 53);
                cb.LineTo(575, step - 53);

                cb.Stroke();

                cb.Stroke();

                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Total Amount Payed", 292, step - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Remaining", 350, step - 45, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", totalAmtToPay), 540, step - 15, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, String.Format("{0:C}", totalReaiming), 540, step - 45, 0);
                //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, String.Format("{0:C}", financeObj.RemainingPayment), 470, 135, 0);
                cb.SetFontAndSize(bf, 8);
                

                cb.EndText();
            }
            catch (DocumentException de)
            {
                //throw de.Message;
            }
            catch (IOException ioe)
            {
                //throw ioe.Message;
            }

            // step 5: we close the document
            document.Close();
            return fileName;
        }



        }
    }