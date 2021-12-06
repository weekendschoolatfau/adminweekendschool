using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class StudentInformationProps
    {
        public Int32 ParentId { get; set; }

        public Int32 StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Int32 Age { get; set; }

        public Int32 Level { get; set; }

        public Int32 EnrollementYear { get; set; }

        public Int32 StudentPaymentStatus { get; set; }

        public string StudentStatus { get; set; }

        public Int32 PaymentPlan { get; set; }
    }
}