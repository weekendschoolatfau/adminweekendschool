using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class StudentPaymentInformationProps
    {
        public Int32 ParentId { get; set; }

        public Int32 StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Int32 Age { get; set; }

        public decimal Tuition { get; set; }

        public decimal TShirt { get; set; }

        public decimal Books { get; set; }

        public decimal TotalPaymentToPay { get; set; }

        public decimal TotalPaymentPayed { get; set; }

        public decimal TotalPaymentRemaining { get; set; }


    }
}