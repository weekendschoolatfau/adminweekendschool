using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class PaymentInformationProps
    {
        public Int32 ParentId { get; set; }

        public Int32 StudentId { get; set; }

        public Int32 FinancialId { get; set; }

        public int SchoolPeriod { get; set; }

        public int EnrollementYear { get; set; }

        public int PaymentPlan { get; set; }

        public string PaymentDate { get; set; }

        public decimal Tuition { get; set; }

        public decimal TShirt { get; set; }

        public decimal Books { get; set; }

        public int PaymentType { get; set; }

        public decimal TotalPaymentToPay { get; set; }

        public decimal TotalPaymentPayed { get; set; }

        public decimal TotalPaymentRemaining { get; set; }

        public string CheckNumber { get; set; }


    }
}