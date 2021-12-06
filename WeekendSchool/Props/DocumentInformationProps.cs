using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class DocumentInformationProps
    {
        public Int32 ParentId { get; set; }

        public Int32 StudentId { get; set; }

        public int DocumentType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public string Level { get; set; }

        public int EnrollementYear { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string AddedBy { get; set; }

        public string AddedDate { get; set; }

        public string IsParent { get; set; }
    }
}