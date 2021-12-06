using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Utils
{
    public class DBSqlConnect
    {
        public DBSqlConnect()
        {

        }

        public SqlConnection getSqlConnection()
        {
            string weekendSchoolConStr = ConfigurationManager.AppSettings["ConWeekendSchool"]; 
            SqlConnection dbConn = new SqlConnection(weekendSchoolConStr);
            return dbConn;
        }

    }
}