using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class LoginInformationProps
    {
        public Int32 ParentId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PreferrredContact { get; set; }

        public string SecurityQuestion { get; set; }

        public string Status { get; set; }

        public string AccountCreated { get; set; }


        public LoginInformationProps()
        {

        }
    }
}