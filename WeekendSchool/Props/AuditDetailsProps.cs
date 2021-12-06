using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class AuditDetailsProps
    {
        public Int32 AuditId { get; set; }

        public string UserName { get; set; }

        public string TableName { get; set; }

        public string FieldName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string Action { get; set; }

        public string TransactionDate { get; set; }




    }
}