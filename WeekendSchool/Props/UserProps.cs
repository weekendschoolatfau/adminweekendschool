using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class UserProps
    {
        public int StaffUserId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserType { get; set; }

        public Int32 RoleId { get; set; }

        public string RoleDesc { get; set; }

        public List<string> moduleList { get; set; }


    }
}