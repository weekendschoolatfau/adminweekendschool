using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class TuitionByLevelProps
    {
        public Int32 LevelId { get; set; }

        public decimal TuitionFee { get; set; }

        public decimal TShirtPrice { get; set; }

        public decimal BookPrice { get; set; }
    }
}