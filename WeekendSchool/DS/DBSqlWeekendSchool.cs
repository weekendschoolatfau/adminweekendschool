using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;

using System.Data.SqlClient;
using adminweekendschool.WeekendSchool.Props;
using adminweekendschool.WeekendSchool.Utils;


namespace adminweekendschool.WeekendSchool.DS
{
    public class DBSqlWeekendSchool
    {

        public DBSqlWeekendSchool()
        {

        }


        public static UserProps getUserLoginInformation(string email, string password)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            UserProps loginObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_User_Login", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((email != null) && (!email.Trim().Equals("")))
                    prmByEmail.Value = email.ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

                SqlParameter prmByPassword = cmdOwners.Parameters.Add("p_password", SqlDbType.NVarChar, 50);
                prmByPassword.Direction = ParameterDirection.Input;
                if ((password != null) && (!password.Trim().Equals("")))
                    prmByPassword.Value = password;
                else
                    prmByPassword.Value = DBNull.Value;

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    loginObj = new UserProps();
                   
                    loginObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    loginObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    loginObj.Name = Convert.ToString(parentRdr["NAME"]);
                    loginObj.UserType = Convert.ToString(parentRdr["USER_TYPE"]);
                    loginObj.StaffUserId = Convert.ToInt32(parentRdr["STAFF_USERS_ID"]);
                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return loginObj;
        }


        public static LoginInformationProps isParentExist(string email, string username)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            LoginInformationProps loginObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Is_Parent_Exist", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((email != null) && (!email.Trim().Equals("")))
                    prmByEmail.Value = email.ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

                SqlParameter prmByPassword = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmByPassword.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmByPassword.Value = username;
                else
                    prmByPassword.Value = DBNull.Value;


                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    loginObj = new LoginInformationProps();
                    loginObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    loginObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    loginObj.FirstName = Convert.ToString(parentRdr["FIRST_NAME"]);
                    loginObj.LastName = Convert.ToString(parentRdr["LAST_NAME"]);
                    loginObj.Phone = Convert.ToString(parentRdr["PHONE_NUMBER"]);

                    bool usernameExists = false;

                    if ((loginObj.UserName != null)&& (loginObj.UserName.Trim().ToUpper().Equals(username.Trim().ToUpper())))
                    {
                        usernameExists = true;
                        loginObj.Status = "Username already exists";
                    }

                    if ((loginObj.UserName != null) && (loginObj.UserName.Trim().ToUpper().Equals(username.Trim().ToUpper())))
                    {
                        if (usernameExists)
                            loginObj.Status = loginObj.Status + " and ";

                        loginObj.Status = loginObj.Status + " Email Address already exists ";
                    }
                }


            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return loginObj;

        }

        public static LoginInformationProps addNewParent(LoginInformationProps parentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            LoginInformationProps subscriberObj = null;



            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Add_New_Parent", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmUserName = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                if ((parentObj.UserName != null) && (!parentObj.UserName.Trim().Equals("")))
                    prmUserName.Value = parentObj.UserName.Trim().ToUpper();
                else
                    prmUserName.Value = DBNull.Value;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((parentObj.Email != null) && (!parentObj.Email.Trim().Equals("")))
                    prmByEmail.Value = parentObj.Email.Trim().ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

                SqlParameter prmPassword = cmdOwners.Parameters.Add("p_password", SqlDbType.NVarChar, 50);
                prmPassword.Direction = ParameterDirection.Input;
                if ((parentObj.Password != null) && (!parentObj.Password.Trim().Equals("")))
                    prmPassword.Value = parentObj.Password.Trim().ToUpper();
                else
                    prmPassword.Value = DBNull.Value;

                SqlParameter prmFirstName = cmdOwners.Parameters.Add("p_firstname", SqlDbType.NVarChar, 50);
                prmFirstName.Direction = ParameterDirection.Input;
                if ((parentObj.FirstName != null) && (!parentObj.FirstName.Trim().Equals("")))
                    prmFirstName.Value = parentObj.FirstName.Trim().ToUpper();
                else
                    prmFirstName.Value = DBNull.Value;

                SqlParameter prmLastName = cmdOwners.Parameters.Add("p_lastname", SqlDbType.NVarChar, 50);
                prmLastName.Direction = ParameterDirection.Input;
                if ((parentObj.LastName != null) && (!parentObj.LastName.Trim().Equals("")))
                    prmLastName.Value = parentObj.LastName.Trim().ToUpper();
                else
                    prmLastName.Value = DBNull.Value;

                SqlParameter prmPhone = cmdOwners.Parameters.Add("p_phone", SqlDbType.NVarChar, 20);
                prmPhone.Direction = ParameterDirection.Input;
                if ((parentObj.Phone != null) && (!parentObj.Phone.Trim().Equals("")))
                    prmPhone.Value = parentObj.Phone.Trim();
                else
                    prmPhone.Value = DBNull.Value;

                SqlParameter prmPreferredContact = cmdOwners.Parameters.Add("p_preferredcontact", SqlDbType.NVarChar, 1);
                prmPreferredContact.Direction = ParameterDirection.Input;
                if ((parentObj.PreferrredContact != null) && (!parentObj.PreferrredContact.Equals("")))
                    prmPreferredContact.Value = parentObj.PreferrredContact;
                else
                    prmPreferredContact.Value = DBNull.Value;

                SqlParameter prmSecurityQuestion = cmdOwners.Parameters.Add("p_securityquestion", SqlDbType.Int, 1);
                prmSecurityQuestion.Direction = ParameterDirection.Input;
                if ((parentObj.SecurityQuestion != null) && (!parentObj.SecurityQuestion.Equals("")))
                    prmSecurityQuestion.Value = parentObj.SecurityQuestion;
                else
                    prmSecurityQuestion.Value = DBNull.Value;

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    subscriberObj = new LoginInformationProps();
                    subscriberObj.ParentId = Convert.ToInt32(parentRdr["Parent_Id"]);
                    subscriberObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    subscriberObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    subscriberObj.FirstName = Convert.ToString(parentRdr["FIRST_NAME"]);
                    subscriberObj.LastName = Convert.ToString(parentRdr["LAST_NAME"]);
                    subscriberObj.Phone = Convert.ToString(parentRdr["PHONE_NUMBER"]);
                    subscriberObj.AccountCreated = Convert.ToString(parentRdr["ACCOUNT_CREATED_DATE"]);

                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

           
            return subscriberObj;
        }

        public static void updateParentInformation(LoginInformationProps parentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Update_Parent_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentObj.ParentId;
              

                SqlParameter prmUserName = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                if ((parentObj.UserName != null) && (!parentObj.UserName.Trim().Equals("")))
                    prmUserName.Value = parentObj.UserName.Trim().ToUpper();
                else
                    prmUserName.Value = DBNull.Value;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((parentObj.Email != null) && (!parentObj.Email.Trim().Equals("")))
                    prmByEmail.Value = parentObj.Email.Trim().ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;


                SqlParameter prmFirstName = cmdOwners.Parameters.Add("p_firstname", SqlDbType.NVarChar, 50);
                prmFirstName.Direction = ParameterDirection.Input;
                if ((parentObj.FirstName != null) && (!parentObj.FirstName.Trim().Equals("")))
                    prmFirstName.Value = parentObj.FirstName.Trim().ToUpper();
                else
                    prmFirstName.Value = DBNull.Value;

                SqlParameter prmLastName = cmdOwners.Parameters.Add("p_lastname", SqlDbType.NVarChar, 50);
                prmLastName.Direction = ParameterDirection.Input;
                if ((parentObj.LastName != null) && (!parentObj.LastName.Trim().Equals("")))
                    prmLastName.Value = parentObj.LastName.Trim().ToUpper();
                else
                    prmLastName.Value = DBNull.Value;

                SqlParameter prmPhone = cmdOwners.Parameters.Add("p_phone", SqlDbType.NVarChar, 20);
                prmPhone.Direction = ParameterDirection.Input;
                if ((parentObj.Phone != null) && (!parentObj.Phone.Trim().Equals("")))
                    prmPhone.Value = parentObj.Phone.Trim();
                else
                    prmPhone.Value = DBNull.Value;


                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


          
        }

        public static LoginInformationProps IsUsernameOrEmailExists(string username, string email,  int parentId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            LoginInformationProps loginObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Parent_Information_ById", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByUsername = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmByUsername.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmByUsername.Value = username.ToUpper();
                else
                    prmByUsername.Value = DBNull.Value;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((email != null) && (!email.Trim().Equals("")))
                    prmByEmail.Value = email.ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentId;
 
                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    loginObj = new LoginInformationProps();
                    loginObj.ParentId = Convert.ToInt32(parentRdr["PARENT_ID"]);
                    loginObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    loginObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    loginObj.FirstName = Convert.ToString(parentRdr["FIRST_NAME"]);
                    loginObj.LastName = Convert.ToString(parentRdr["LAST_NAME"]);
                    loginObj.Phone = Convert.ToString(parentRdr["PHONE_NUMBER"]);
                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return loginObj;
        }



        public static List<StudentInformationProps> addNewStudents(StudentInformationProps studentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<StudentInformationProps> studentsList = new List<StudentInformationProps>();
            StudentInformationProps newStudentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Add_New_Student", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = studentObj.ParentId;



                SqlParameter prmFirstName = cmdOwners.Parameters.Add("p_firstname", SqlDbType.NVarChar, 50);
                prmFirstName.Direction = ParameterDirection.Input;
                if ((studentObj.FirstName != null) && (!studentObj.FirstName.Trim().Equals("")))
                    prmFirstName.Value = studentObj.FirstName.Trim().ToUpper();
                else
                    prmFirstName.Value = DBNull.Value;

                SqlParameter prmLastName = cmdOwners.Parameters.Add("p_lastname", SqlDbType.NVarChar, 50);
                prmLastName.Direction = ParameterDirection.Input;
                if ((studentObj.LastName != null) && (!studentObj.LastName.Trim().Equals("")))
                    prmLastName.Value = studentObj.LastName.Trim().ToUpper();
                else
                    prmLastName.Value = DBNull.Value;

                SqlParameter prmAge = cmdOwners.Parameters.Add("p_age", SqlDbType.Int, 2);
                prmAge.Direction = ParameterDirection.Input;
                prmAge.Value = studentObj.Age;

                SqlParameter prmLevel = cmdOwners.Parameters.Add("p_level", SqlDbType.Int, 2);
                prmLevel.Direction = ParameterDirection.Input;
                prmLevel.Value = studentObj.Level;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = studentObj.EnrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                if (studentRdr.Read())
                {
                    newStudentObj = new StudentInformationProps();
                    newStudentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    newStudentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    newStudentObj.FirstName = Convert.ToString(studentRdr["FIRST_NAME"]);
                    newStudentObj.LastName = Convert.ToString(studentRdr["LAST_NAME"]);
                    newStudentObj.Age = Convert.ToInt32(studentRdr["AGE"]);
                    newStudentObj.Level = Convert.ToInt32(studentRdr["LEVEL"]);
                    newStudentObj.EnrollementYear = Convert.ToInt32(studentRdr["ENROLLMENT_YEAR"]);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return studentsList;
        }


        public static List<StudentInformationProps> getStudentsInformation(Int32 parentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<StudentInformationProps> studentsList = new List<StudentInformationProps>();
            StudentInformationProps newStudentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Add_New_Student", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                if (studentRdr.Read())
                {
                    newStudentObj = new StudentInformationProps();
                    newStudentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    newStudentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    newStudentObj.FirstName = Convert.ToString(studentRdr["FIRST_NAME"]);
                    newStudentObj.LastName = Convert.ToString(studentRdr["LAST_NAME"]);
                    newStudentObj.Age = Convert.ToInt32(studentRdr["AGE"]);
                    newStudentObj.Level = Convert.ToInt32(studentRdr["LEVEL"]);
                    newStudentObj.EnrollementYear = Convert.ToInt32(studentRdr["ENROLLMENT_YEAR"]);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return studentsList;
        }

        public static DataSet getStudentsList(Int32 parentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Students_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;


                //Retrieve Rows
                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "StudentsList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsStudentsList;
        }

        public static void deleteStudentInformation(Int32 studentId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Delete_Students_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;

                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

        }


        public static void updateStudentInformation(StudentInformationProps studentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<StudentInformationProps> studentsList = new List<StudentInformationProps>();
          

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Update_Student_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentObj.StudentId;

                SqlParameter prmFirstName = cmdOwners.Parameters.Add("p_firstname", SqlDbType.NVarChar, 50);
                prmFirstName.Direction = ParameterDirection.Input;
                if ((studentObj.FirstName != null) && (!studentObj.FirstName.Trim().Equals("")))
                    prmFirstName.Value = studentObj.FirstName.Trim().ToUpper();
                else
                    prmFirstName.Value = DBNull.Value;

                SqlParameter prmLastName = cmdOwners.Parameters.Add("p_lastname", SqlDbType.NVarChar, 50);
                prmLastName.Direction = ParameterDirection.Input;
                if ((studentObj.LastName != null) && (!studentObj.LastName.Trim().Equals("")))
                    prmLastName.Value = studentObj.LastName.Trim().ToUpper();
                else
                    prmLastName.Value = DBNull.Value;

                SqlParameter prmAge = cmdOwners.Parameters.Add("p_age", SqlDbType.Int, 2);
                prmAge.Direction = ParameterDirection.Input;
                prmAge.Value = studentObj.Age;

                SqlParameter prmLevel = cmdOwners.Parameters.Add("p_level", SqlDbType.Int, 2);
                prmLevel.Direction = ParameterDirection.Input;
                prmLevel.Value = studentObj.Level;

                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }
        }

        public static StudentInformationProps getStudentInformation(Int32 studentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
           
            StudentInformationProps newStudentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Student_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = studentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                if (studentRdr.Read())
                {
                    newStudentObj = new StudentInformationProps();
                    newStudentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    newStudentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    newStudentObj.FirstName = Convert.ToString(studentRdr["FIRST_NAME"]);
                    newStudentObj.LastName = Convert.ToString(studentRdr["LAST_NAME"]);
                    newStudentObj.Age = Convert.ToInt32(studentRdr["AGE"]);
                    newStudentObj.Level = Convert.ToInt32(studentRdr["LEVEL"]);
                    newStudentObj.EnrollementYear = Convert.ToInt32(studentRdr["ENROLLMENT_YEAR"]);
                    newStudentObj.StudentStatus = Convert.ToString(studentRdr["Student_Status"]);

                    if (studentRdr["PAYMENT_PLAN"] != DBNull.Value)
                        newStudentObj.PaymentPlan = Convert.ToInt32(studentRdr["PAYMENT_PLAN"]);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return newStudentObj;
        }


        public static List<TuitionByLevelProps> getTuitionByLevel( Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<TuitionByLevelProps> tuitionList = new List<TuitionByLevelProps>();
            TuitionByLevelProps tuitionObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Tuition_By_Level", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                while (studentRdr.Read())
                {
                    tuitionObj = new TuitionByLevelProps();
                    tuitionObj.LevelId = Convert.ToInt32(studentRdr["Level_id"]);
                    tuitionObj.TuitionFee= Convert.ToDecimal(studentRdr["Tuition_Fee"]);
                    tuitionObj.TShirtPrice = Convert.ToDecimal(studentRdr["T-Shirt_Price"]);
                    tuitionObj.BookPrice = Convert.ToDecimal(studentRdr["Book_Price"]);

                    tuitionList.Add(tuitionObj);

                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return tuitionList;
        }


        public static List<PaymentInformationProps> getPaymentsInformation(Int32 studentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<PaymentInformationProps> paymentsList = new List<PaymentInformationProps>();
            PaymentInformationProps paymentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Payments_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                while (studentRdr.Read())
                {
                    paymentObj = new PaymentInformationProps();
                    paymentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    paymentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    paymentObj.EnrollementYear = Convert.ToInt32(studentRdr["ENROLLMENT_YEAR"]);
                    paymentObj.PaymentPlan = Convert.ToInt32(studentRdr["PAYMENT_PLAN"]);
                    paymentObj.PaymentDate = Convert.ToString(studentRdr["PAYMENT_DATE"]);
                    paymentObj.SchoolPeriod = Convert.ToInt32(studentRdr["SCHOOL_PERIOD"]);
                    paymentObj.Tuition = Convert.ToDecimal("0"+studentRdr["TUITION"]);
                    paymentObj.TShirt = Convert.ToDecimal("0" + studentRdr["T_SHIRT"]);
                    paymentObj.Books = Convert.ToDecimal("0" + studentRdr["BOOKS"]);
                    paymentObj.PaymentType = Convert.ToInt32("0" + studentRdr["PAYMENT_TYPE"]);
                    paymentObj.TotalPaymentToPay = Convert.ToDecimal("0" + studentRdr["TOTAL_AMOUNT_DUE"]);
                    paymentObj.TotalPaymentPayed = Convert.ToDecimal("0" + studentRdr["TOTAL_AMOUNT_PAYED"]);
                    paymentObj.TotalPaymentRemaining = Convert.ToDecimal("0" + studentRdr["REMAINING"]);
                    paymentObj.CheckNumber = "" + studentRdr["CHECK_NUMBER"];

                    paymentsList.Add(paymentObj);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return paymentsList;
        }


        public static void addPaymentInformation(PaymentInformationProps paymentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<PaymentInformationProps> studentsList = new List<PaymentInformationProps>();


            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Add_Payment_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = paymentObj.StudentId;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = paymentObj.ParentId;

                SqlParameter prmSchoolPeriod = cmdOwners.Parameters.Add("p_schoolperiodId", SqlDbType.Int,1);
                prmSchoolPeriod.Direction = ParameterDirection.Input;
                prmSchoolPeriod.Value = paymentObj.SchoolPeriod;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int,4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = paymentObj.EnrollementYear;

                SqlParameter prmPaymentPlan = cmdOwners.Parameters.Add("p_paymentplain", SqlDbType.Int, 1);
                prmPaymentPlan.Direction = ParameterDirection.Input;
                prmPaymentPlan.Value = paymentObj.PaymentPlan;

                SqlParameter prmTuition = cmdOwners.Parameters.Add("p_tuition", SqlDbType.Decimal);
                prmTuition.Direction = ParameterDirection.Input;
                prmTuition.Value = paymentObj.Tuition;

                SqlParameter prmTShirt = cmdOwners.Parameters.Add("p_tshirt", SqlDbType.Decimal);
                prmTShirt.Direction = ParameterDirection.Input;
                prmTShirt.Value = paymentObj.TShirt;

                SqlParameter prmBooks = cmdOwners.Parameters.Add("p_books", SqlDbType.Decimal);
                prmBooks.Direction = ParameterDirection.Input;
                prmBooks.Value = paymentObj.Books;

                SqlParameter prmPaymentType = cmdOwners.Parameters.Add("p_paymenttype", SqlDbType.Int, 1);
                prmPaymentType.Direction = ParameterDirection.Input;
                prmPaymentType.Value = paymentObj.PaymentType;

                SqlParameter prmTotalAmountToPay = cmdOwners.Parameters.Add("p_totalamounttopay", SqlDbType.Decimal);
                prmTotalAmountToPay.Precision = 7;
                prmTotalAmountToPay.Scale = 3;
                prmTotalAmountToPay.Direction = ParameterDirection.Input;
                prmTotalAmountToPay.Value = (paymentObj.Tuition + paymentObj.TShirt + paymentObj.Books);

                SqlParameter prmTotalAmountPayed = cmdOwners.Parameters.Add("p_totalamountpayed", SqlDbType.Decimal);
                prmTotalAmountPayed.Precision = 7;
                prmTotalAmountPayed.Scale = 3;
                prmTotalAmountPayed.Direction = ParameterDirection.Input;
                prmTotalAmountPayed.Value = paymentObj.TotalPaymentPayed;


                SqlParameter prmCheckNumber = cmdOwners.Parameters.Add("p_checknumber", SqlDbType.VarChar, 50);
                prmCheckNumber.Direction = ParameterDirection.Input;
                prmCheckNumber.Value = paymentObj.CheckNumber;




                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }
        }


        public static List<StudentPaymentInformationProps> getStudentsPaymentInformation(Int32 parentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<StudentPaymentInformationProps> paymentsList = new List<StudentPaymentInformationProps>();
            StudentPaymentInformationProps paymentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Students_Financial_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                while (studentRdr.Read())
                {
                    paymentObj = new StudentPaymentInformationProps();
                    paymentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    paymentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    paymentObj.FirstName =""+ studentRdr["FIRST_NAME"];
                    paymentObj.Age = Convert.ToInt32(studentRdr["AGE"]);
                    paymentObj.Tuition = Convert.ToDecimal("0" + studentRdr["TUITION"]);
                    paymentObj.TShirt = Convert.ToDecimal("0" + studentRdr["T_SHIRT"]);
                    paymentObj.Books = Convert.ToDecimal("0" + studentRdr["BOOKS"]);
                    paymentObj.TotalPaymentToPay = Convert.ToDecimal("0" + studentRdr["TOTAL_AMOUNT_DUE"]);
                    paymentObj.TotalPaymentPayed = Convert.ToDecimal("0" + studentRdr["TOTAL_AMOUNT_PAYED"]);
                    paymentObj.TotalPaymentRemaining = Convert.ToDecimal("0" + studentRdr["REMAINING"]);

                    paymentsList.Add(paymentObj);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return paymentsList;
        }


        public static List<DocumentInformationProps> getDocumentsInformation(Int32 studentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<DocumentInformationProps> documentsList = new List<DocumentInformationProps>();
            DocumentInformationProps documentObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Student_Documents_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;

                SqlDataReader studentRdr = cmdOwners.ExecuteReader();

                while (studentRdr.Read())
                {
                    documentObj = new DocumentInformationProps();
                    documentObj.ParentId = Convert.ToInt32(studentRdr["PARENT_ID"]);
                    documentObj.StudentId = Convert.ToInt32(studentRdr["STUDENT_ID"]);
                    documentObj.FirstName = "" + studentRdr["FIRST_NAME"];
                    documentObj.Age = Convert.ToString(studentRdr["AGE"]);
                   

                    documentsList.Add(documentObj);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return documentsList;
        }

        public static void addDocumentformation(DocumentInformationProps documentObj)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<DocumentInformationProps> studentsList = new List<DocumentInformationProps>();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Add_Document_Information", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = documentObj.StudentId;

                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = documentObj.ParentId;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = documentObj.EnrollementYear;

                SqlParameter prmDocumentType = cmdOwners.Parameters.Add("p_documenttype", SqlDbType.Int, 1);
                prmDocumentType.Direction = ParameterDirection.Input;
                prmDocumentType.Value = documentObj.DocumentType;

                SqlParameter prmDocumentName = cmdOwners.Parameters.Add("p_documentname", SqlDbType.VarChar);
                prmDocumentName.Direction = ParameterDirection.Input;
                prmDocumentName.Value = documentObj.DocumentName ;

                SqlParameter prmDocumentPath = cmdOwners.Parameters.Add("p_documentpath", SqlDbType.VarChar);
                prmDocumentPath.Direction = ParameterDirection.Input;
                prmDocumentPath.Value = documentObj.DocumentPath;

                SqlParameter prmAddedBy = cmdOwners.Parameters.Add("p_addedby", SqlDbType.VarChar);
                prmAddedBy.Direction = ParameterDirection.Input;
                prmAddedBy.Value = documentObj.AddedBy;

                SqlParameter prmIsParent = cmdOwners.Parameters.Add("p_isparent", SqlDbType.VarChar);
                prmIsParent.Direction = ParameterDirection.Input;
                prmIsParent.Value = documentObj.IsParent;

                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }
        }


        public static DataSet getDocumentsList(Int32 studentId, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Documents_By_Student", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;


                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;


                //Retrieve Rows
                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "StudentDocumentsList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsStudentsList;
        }


        public static void deleteDocument(Int32 studentId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Delete_document", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_documentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;

                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

        }


        public static DataSet getTuitionFeeList(Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsTuitionListList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Tuition_Fee", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;


                //Retrieve Rows
                dsTuitionListList = new DataSet();
                SqlDataAdapter daTuitionFeeList = new SqlDataAdapter(cmdOwners);
                daTuitionFeeList.Fill(dsTuitionListList, "TuitionFeeList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsTuitionListList;
        }


        public static DataSet getVerificationList(string lastName, string phoneNumber, string email, string schoolYear, string studentStatus)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Verification_List", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmLastName = cmdOwners.Parameters.Add("p_lastname", SqlDbType.VarChar, 50);
                prmLastName.Direction = ParameterDirection.Input;
                if ((lastName != null) && (!lastName.Trim().Equals("")))
                    prmLastName.Value = lastName;
                else
                    prmLastName.Value = DBNull.Value;

                SqlParameter prmPhoneNumber = cmdOwners.Parameters.Add("p_phonenumber", SqlDbType.VarChar, 20);
                prmPhoneNumber.Direction = ParameterDirection.Input;
                if ((phoneNumber != null) && (!phoneNumber.Trim().Equals("")))
                    prmPhoneNumber.Value = phoneNumber;
                else
                    prmPhoneNumber.Value = DBNull.Value;


                SqlParameter prmEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.VarChar, 100);
                prmEmail.Direction = ParameterDirection.Input;
                if ((email != null) && (!email.Trim().Equals("")))
                    prmEmail.Value = email;
                else
                    prmEmail.Value = DBNull.Value;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.VarChar, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                if ((schoolYear != null) && (!schoolYear.Trim().Equals("")))
                    prmEnrollementYear.Value = schoolYear;
                else
                    prmEnrollementYear.Value = DBNull.Value;


                SqlParameter prmStudentStatus = cmdOwners.Parameters.Add("p_studentstatus", SqlDbType.VarChar, 1);
                prmStudentStatus.Direction = ParameterDirection.Input;
                if ((studentStatus != null) && (!studentStatus.Trim().Equals("")))
                    prmStudentStatus.Value = studentStatus;
                else
                    prmStudentStatus.Value = DBNull.Value; 


                //Retrieve Rows
                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "VerificationList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsStudentsList;
        }

        public static LoginInformationProps getParentStudentsInformationById(int parentId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            LoginInformationProps loginObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Parent_ById", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 7);
                prmByEmail.Direction = ParameterDirection.Input;
                prmByEmail.Value = parentId;
              

               

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    loginObj = new LoginInformationProps();
                    loginObj.ParentId = Convert.ToInt32(parentRdr["PARENT_ID"]);
                    loginObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    loginObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    loginObj.FirstName = Convert.ToString(parentRdr["FIRST_NAME"]);
                    loginObj.LastName = Convert.ToString(parentRdr["LAST_NAME"]);
                    loginObj.Phone = Convert.ToString(parentRdr["PHONE_NUMBER"]);
                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return loginObj;
        }


        public static LoginInformationProps IsUsernameOrEmailExists(string username, string email)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            LoginInformationProps loginObj = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Parent_Information_ById", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByUsername = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmByUsername.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmByUsername.Value = username.ToUpper();
                else
                    prmByUsername.Value = DBNull.Value;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_email", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((email != null) && (!email.Trim().Equals("")))
                    prmByEmail.Value = email.ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

                //SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                //prmParentId.Direction = ParameterDirection.Input;
                //prmParentId.Value = parentId;

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    loginObj = new LoginInformationProps();
                    loginObj.ParentId = Convert.ToInt32(parentRdr["PARENT_ID"]);
                    loginObj.UserName = Convert.ToString(parentRdr["USERNAME"]);
                    loginObj.Email = Convert.ToString(parentRdr["EMAIL"]);
                    loginObj.FirstName = Convert.ToString(parentRdr["FIRST_NAME"]);
                    loginObj.LastName = Convert.ToString(parentRdr["LAST_NAME"]);
                    loginObj.Phone = Convert.ToString(parentRdr["PHONE_NUMBER"]);
                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return loginObj;
        }

        public static void updateStudentStatus(Int32  studentId, string status)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<StudentInformationProps> studentsList = new List<StudentInformationProps>();


            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Update_Student_Status", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmStudentId = cmdOwners.Parameters.Add("p_studentId", SqlDbType.Int, 10);
                prmStudentId.Direction = ParameterDirection.Input;
                prmStudentId.Value = studentId;

                SqlParameter prmStudentStatus = cmdOwners.Parameters.Add("@p_status", SqlDbType.NVarChar, 1);
                prmStudentStatus.Direction = ParameterDirection.Input;
                if ((status != null) && (!status.Trim().Equals("")))
                    prmStudentStatus.Value = status.Trim().ToUpper();
                else
                    prmStudentStatus.Value = DBNull.Value;

              

                cmdOwners.ExecuteReader();

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }
        }



        public static Int32 Audit(string username, int parentId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            Int32 auditId = 0;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_audit", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmUserName = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmUserName.Value = username.Trim().ToUpper();
                else
                    prmUserName.Value = DBNull.Value;


                SqlParameter prmParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmParentId.Direction = ParameterDirection.Input;
                prmParentId.Value = parentId;



                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                if (parentRdr.Read())
                {
                    auditId = Convert.ToInt32(parentRdr["Audit_Id"]);
                }

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


            return auditId;
        }


        public static List<AuditDetailsProps> AuditDetailsParentComparaison(Int32 auditId,  LoginInformationProps initParentObj, LoginInformationProps currentParentObj)
        {
           
            string tableName = "Parent_Information";
           
            List<AuditDetailsProps> auditDetailList= new List<AuditDetailsProps>();

            if ((initParentObj == null)&& (currentParentObj != null))
            {
                string action = "Insert";

                AuditDetailsProps auditDetailObj1 = new AuditDetailsProps();
                auditDetailObj1.AuditId = auditId;
                auditDetailObj1.TableName = tableName;
                auditDetailObj1.Action = action;
                auditDetailObj1.FieldName = "Parent First Name";
                auditDetailObj1.OldValue = "";
                auditDetailObj1.NewValue = currentParentObj.FirstName;
                auditDetailList.Add(auditDetailObj1);

                AuditDetailsProps auditDetailObj2 = new AuditDetailsProps();
                auditDetailObj2.AuditId = auditId;
                auditDetailObj2.TableName = tableName;
                auditDetailObj2.FieldName = "Parent Last Name";
                auditDetailObj2.Action = action;
                auditDetailObj2.OldValue = "";
                auditDetailObj2.NewValue = currentParentObj.LastName;
                auditDetailList.Add(auditDetailObj2);

                AuditDetailsProps auditDetailObj3 = new AuditDetailsProps();
                auditDetailObj3.AuditId = auditId;
                auditDetailObj3.TableName = tableName;
                auditDetailObj3.FieldName = "Parent Email";
                auditDetailObj3.Action = action;
                auditDetailObj3.OldValue = "";
                auditDetailObj3.NewValue = currentParentObj.Email;
                auditDetailList.Add(auditDetailObj3);

                AuditDetailsProps auditDetailObj4 = new AuditDetailsProps();
                auditDetailObj4.AuditId = auditId;
                auditDetailObj4.TableName = tableName;
                auditDetailObj4.FieldName = "Parent Phone Number";
                auditDetailObj4.Action = action;
                auditDetailObj4.OldValue = "";
                auditDetailObj4.NewValue = currentParentObj.Phone;
                auditDetailList.Add(auditDetailObj4);

                AuditDetailsProps auditDetailObj5 = new AuditDetailsProps();
                auditDetailObj5.AuditId = auditId;
                auditDetailObj5.TableName = tableName;
                auditDetailObj5.FieldName = "Parent Username";
                auditDetailObj5.Action = action;
                auditDetailObj5.OldValue = "";
                auditDetailObj5.NewValue = currentParentObj.UserName;
                auditDetailList.Add(auditDetailObj5);

            }


            if ((initParentObj != null) && (currentParentObj != null))
            {
                string action = "Update";

                if (((initParentObj.FirstName == null) && (!currentParentObj.FirstName.Trim().Equals("")))||
                    ((currentParentObj.FirstName == null) && (!initParentObj.FirstName.Trim().Equals(""))) ||
                    (!currentParentObj.FirstName.Trim().Equals(initParentObj.FirstName.Trim())))
                {
                    AuditDetailsProps auditDetailObj1 = new AuditDetailsProps();
                    auditDetailObj1.AuditId = auditId;
                    auditDetailObj1.TableName = tableName;
                    auditDetailObj1.Action = action;
                    auditDetailObj1.FieldName = "Parent First Name";
                    auditDetailObj1.OldValue = initParentObj.FirstName;
                    auditDetailObj1.NewValue = currentParentObj.FirstName;
                    auditDetailList.Add(auditDetailObj1);
                }


                if (((initParentObj.LastName == null) && (!currentParentObj.LastName.Trim().Equals(""))) ||
                   ((currentParentObj.LastName == null) && (!initParentObj.LastName.Trim().Equals(""))) ||
                   (!currentParentObj.LastName.Trim().Equals(initParentObj.LastName.Trim())))
                {
                    AuditDetailsProps auditDetailObj2 = new AuditDetailsProps();
                    auditDetailObj2.AuditId = auditId;
                    auditDetailObj2.TableName = tableName;
                    auditDetailObj2.FieldName = "Parent Last Name";
                    auditDetailObj2.Action = action;
                    auditDetailObj2.OldValue = initParentObj.LastName;
                    auditDetailObj2.NewValue = currentParentObj.LastName;
                    auditDetailList.Add(auditDetailObj2);
                }

                if (((initParentObj.Email == null) && (!currentParentObj.Email.Trim().Equals(""))) ||
                   ((currentParentObj.Email == null) && (!initParentObj.Email.Trim().Equals(""))) ||
                   (!currentParentObj.Email.Trim().Equals(initParentObj.Email.Trim())))
                {

                    AuditDetailsProps auditDetailObj3 = new AuditDetailsProps();
                    auditDetailObj3.AuditId = auditId;
                    auditDetailObj3.TableName = tableName;
                    auditDetailObj3.FieldName = "Parent Email";
                    auditDetailObj3.Action = action;
                    auditDetailObj3.OldValue = initParentObj.Email;
                    auditDetailObj3.NewValue = currentParentObj.Email;
                    auditDetailList.Add(auditDetailObj3);
                }


                if (((initParentObj.Phone == null) && (!currentParentObj.Phone.Trim().Equals(""))) ||
                   ((currentParentObj.Phone == null) && (!initParentObj.Phone.Trim().Equals(""))) ||
                   (!currentParentObj.Phone.Trim().Equals(initParentObj.Phone.Trim())))
                {
                    AuditDetailsProps auditDetailObj4 = new AuditDetailsProps();
                    auditDetailObj4.AuditId = auditId;
                    auditDetailObj4.TableName = tableName;
                    auditDetailObj4.FieldName = "Parent Phone Number";
                    auditDetailObj4.Action = action;
                    auditDetailObj4.OldValue = initParentObj.Phone;
                    auditDetailObj4.NewValue = currentParentObj.Phone;
                    auditDetailList.Add(auditDetailObj4);
                }

                if (((initParentObj.UserName == null) && (!currentParentObj.UserName.Trim().Equals(""))) ||
                  ((currentParentObj.UserName == null) && (!initParentObj.UserName.Trim().Equals(""))) ||
                  (!currentParentObj.UserName.Trim().Equals(initParentObj.UserName.Trim())))
                {

                    AuditDetailsProps auditDetailObj5 = new AuditDetailsProps();
                    auditDetailObj5.AuditId = auditId;
                    auditDetailObj5.TableName = tableName;
                    auditDetailObj5.FieldName = "Parent Username";
                    auditDetailObj5.Action = action;
                    auditDetailObj5.OldValue = initParentObj.UserName;
                    auditDetailObj5.NewValue = currentParentObj.UserName;
                    auditDetailList.Add(auditDetailObj5);
                }

            }


            return auditDetailList;

        }


        public static List<AuditDetailsProps> AuditDetailsStudentComparaison(Int32 auditId,  StudentInformationProps initStudentObj, StudentInformationProps currentStudentObj)
        {

            string tableName = "Student_Information";

            List<AuditDetailsProps> auditDetailList = new List<AuditDetailsProps>();

            if ((initStudentObj == null) && (currentStudentObj != null))
            {
                string action = "Insert";

                AuditDetailsProps auditDetailObj1 = new AuditDetailsProps();
                auditDetailObj1.AuditId = auditId;
                auditDetailObj1.TableName = tableName;
                auditDetailObj1.Action = action;
                auditDetailObj1.FieldName = "Student First_Name";
                auditDetailObj1.OldValue = "";
                auditDetailObj1.NewValue = currentStudentObj.FirstName;
                auditDetailList.Add(auditDetailObj1);

                AuditDetailsProps auditDetailObj2 = new AuditDetailsProps();
                auditDetailObj2.AuditId = auditId;
                auditDetailObj2.TableName = tableName;
                auditDetailObj2.FieldName = "Student Last_Name";
                auditDetailObj2.Action = action;
                auditDetailObj2.OldValue = "";
                auditDetailObj2.NewValue = currentStudentObj.LastName;
                auditDetailList.Add(auditDetailObj2);

                AuditDetailsProps auditDetailObj3 = new AuditDetailsProps();
                auditDetailObj3.AuditId = auditId;
                auditDetailObj3.TableName = tableName;
                auditDetailObj3.FieldName = "Student Age";
                auditDetailObj3.Action = action;
                auditDetailObj3.OldValue = "";
                auditDetailObj3.NewValue = currentStudentObj.FirstName +" " +currentStudentObj.Age;
                auditDetailList.Add(auditDetailObj3);

                AuditDetailsProps auditDetailObj4 = new AuditDetailsProps();
                auditDetailObj4.AuditId = auditId;
                auditDetailObj4.TableName = tableName;
                auditDetailObj4.FieldName = "Student Level";
                auditDetailObj4.Action = action;
                auditDetailObj4.OldValue = "";
                auditDetailObj4.NewValue = ""+ currentStudentObj.Level;
                auditDetailList.Add(auditDetailObj4);

            }


            if ((initStudentObj != null) && (currentStudentObj != null))
            {
                string action = "Update";

                if (((initStudentObj.FirstName == null) && (!currentStudentObj.FirstName.Trim().Equals(""))) ||
                 ((currentStudentObj.FirstName == null) && (!initStudentObj.FirstName.Trim().Equals(""))) ||
                 (!currentStudentObj.FirstName.Trim().Equals(initStudentObj.FirstName.Trim())))
                {

                    AuditDetailsProps auditDetailObj1 = new AuditDetailsProps();
                    auditDetailObj1.AuditId = auditId;
                    auditDetailObj1.TableName = tableName;
                    auditDetailObj1.Action = action;
                    auditDetailObj1.FieldName = "Student First Name";
                    auditDetailObj1.OldValue = initStudentObj.FirstName;
                    auditDetailObj1.NewValue = currentStudentObj.FirstName;
                    auditDetailList.Add(auditDetailObj1);
                }


                if (((initStudentObj.LastName == null) && (!currentStudentObj.LastName.Trim().Equals(""))) ||
                 ((currentStudentObj.LastName == null) && (!initStudentObj.LastName.Trim().Equals(""))) ||
                 (!currentStudentObj.LastName.Trim().Equals(initStudentObj.LastName.Trim())))
                {
                    AuditDetailsProps auditDetailObj2 = new AuditDetailsProps();
                    auditDetailObj2.AuditId = auditId;
                    auditDetailObj2.TableName = tableName;
                    auditDetailObj2.FieldName = "Student Last Name";
                    auditDetailObj2.Action = action;
                    auditDetailObj2.OldValue = initStudentObj.LastName;
                    auditDetailObj2.NewValue = currentStudentObj.LastName;
                    auditDetailList.Add(auditDetailObj2);
                }


                if (currentStudentObj.Age != initStudentObj.Age)
                {
                    AuditDetailsProps auditDetailObj3 = new AuditDetailsProps();
                    auditDetailObj3.AuditId = auditId;
                    auditDetailObj3.TableName = tableName;
                    auditDetailObj3.FieldName = "Student Age";
                    auditDetailObj3.Action = action;
                    auditDetailObj3.OldValue = currentStudentObj.FirstName + " Age : " + initStudentObj.Age;
                    auditDetailObj3.NewValue = "" + currentStudentObj.Age;
                    auditDetailList.Add(auditDetailObj3);
                }

                if (currentStudentObj.Level != initStudentObj.Level)
                {
                    AuditDetailsProps auditDetailObj4 = new AuditDetailsProps();
                    auditDetailObj4.AuditId = auditId;
                    auditDetailObj4.TableName = tableName;
                    auditDetailObj4.FieldName = "Student Level";
                    auditDetailObj4.Action = action;
                    auditDetailObj4.OldValue = currentStudentObj.FirstName + " Level : " + initStudentObj.Level;
                    auditDetailObj4.NewValue = "" + currentStudentObj.Level;
                    auditDetailList.Add(auditDetailObj4);
                }

            }


            return auditDetailList;

        }


        public static void AuditDetails(  List<AuditDetailsProps> auditDetailsList)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();


            for (int j = 0; ((auditDetailsList != null) && (auditDetailsList.Count > 0) && (j < auditDetailsList.Count)); j++)
            {
                AuditDetailsProps auditDetailObj = auditDetailsList[j];

                try
                {
                    dbWeekendSchoolConn.Open();

                    //Instantiate and inialize command
                    cmdOwners = new SqlCommand("Proc_audit_Details", dbWeekendSchoolConn);
                    cmdOwners.CommandType = CommandType.StoredProcedure;

                    SqlParameter prmAuditId = cmdOwners.Parameters.Add("p_auditId", SqlDbType.Int, 10);
                    prmAuditId.Direction = ParameterDirection.Input;
                    if (auditDetailObj.AuditId > 0)
                        prmAuditId.Value = auditDetailObj.AuditId;
                    else
                        prmAuditId.Value = DBNull.Value;

                    SqlParameter prmAction = cmdOwners.Parameters.Add("p_action", SqlDbType.NVarChar, 100);
                    prmAction.Direction = ParameterDirection.Input;
                    if ((auditDetailObj.Action != null) && (!auditDetailObj.Action.Trim().Equals("")))
                        prmAction.Value = auditDetailObj.Action.Trim().ToUpper();
                    else
                        prmAction.Value = DBNull.Value;

                    SqlParameter prmTableName = cmdOwners.Parameters.Add("p_tableName", SqlDbType.NVarChar, 100);
                    prmTableName.Direction = ParameterDirection.Input;
                    if ((auditDetailObj.TableName != null) && (!auditDetailObj.TableName.Trim().Equals("")))
                        prmTableName.Value = auditDetailObj.TableName.Trim().ToUpper();
                    else
                        prmTableName.Value = DBNull.Value;

                    SqlParameter prmFieldName = cmdOwners.Parameters.Add("p_fieldName", SqlDbType.NVarChar, 100);
                    prmFieldName.Direction = ParameterDirection.Input;
                    if ((auditDetailObj.FieldName != null) && (!auditDetailObj.FieldName.Trim().Equals("")))
                        prmFieldName.Value = auditDetailObj.FieldName.Trim().ToUpper();
                    else
                        prmFieldName.Value = DBNull.Value;

                    SqlParameter prmOldValue = cmdOwners.Parameters.Add("p_oldValue", SqlDbType.NVarChar, 150);
                    prmOldValue.Direction = ParameterDirection.Input;
                    if ((auditDetailObj.OldValue != null) && (!auditDetailObj.OldValue.Trim().Equals("")))
                        prmOldValue.Value = auditDetailObj.OldValue.Trim().ToUpper();
                    else
                        prmOldValue.Value = DBNull.Value;

                    SqlParameter prmNewValue = cmdOwners.Parameters.Add("p_newValue", SqlDbType.NVarChar, 150);
                    prmNewValue.Direction = ParameterDirection.Input;
                    if ((auditDetailObj.NewValue != null) && (!auditDetailObj.NewValue.Trim().Equals("")))
                        prmNewValue.Value = auditDetailObj.NewValue.Trim().ToUpper();
                    else
                        prmNewValue.Value = DBNull.Value;

                    SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                 

                }
                catch (Exception weekendschoolException)
                {
                    throw weekendschoolException;
                }
                finally
                {
                    if (dbWeekendSchoolConn != null)
                        dbWeekendSchoolConn.Close();
                }

            }
           
        }


        public static DataSet getAuditList(int parentId, int enrollementyear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            

            DataSet dsAuditList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Select_Audit_Details", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByParentId = cmdOwners.Parameters.Add("p_parentId", SqlDbType.Int, 10);
                prmByParentId.Direction = ParameterDirection.Input;
                if (parentId > 0)
                    prmByParentId.Value = parentId;
                else
                    prmByParentId.Value = DBNull.Value;

                SqlParameter prmByEnrollementyear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmByEnrollementyear.Direction = ParameterDirection.Input;
                if (enrollementyear > 0 ) 
                    prmByEnrollementyear.Value = enrollementyear;
                else
                    prmByEnrollementyear.Value = DBNull.Value;

                dsAuditList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsAuditList, "StudentsList");
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return dsAuditList;
        }


        public static void updateTuitionByLevel(Int32 levelId, decimal tuitionFee, decimal tshirtFee, decimal bookPrice, Int32 enrollementYear)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            Int32 auditId = 0;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Update_Tuition_Fee_List", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmLevelId = cmdOwners.Parameters.Add("p_levelid", SqlDbType.Int, 10);
                prmLevelId.Direction = ParameterDirection.Input;
                if (levelId > 0)
                    prmLevelId.Value = levelId;
                else
                    prmLevelId.Value = DBNull.Value;


                SqlParameter prmTuitionFee = cmdOwners.Parameters.Add("p_tuitionfee", SqlDbType.Decimal, 10);
                prmTuitionFee.Direction = ParameterDirection.Input;
                prmTuitionFee.Value = tuitionFee;

                SqlParameter prmTShirtFee = cmdOwners.Parameters.Add("p_tshirtfee", SqlDbType.Decimal, 10);
                prmTShirtFee.Direction = ParameterDirection.Input;
                prmTShirtFee.Value = tshirtFee;

                SqlParameter prmBookPrice = cmdOwners.Parameters.Add("p_bookprice", SqlDbType.Decimal, 10);
                prmBookPrice.Direction = ParameterDirection.Input;
                prmBookPrice.Value = bookPrice;

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.Int, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                prmEnrollementYear.Value = enrollementYear;


                cmdOwners.ExecuteReader();

               

            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }


          
        }


        public static List<string> getUserModules(string username)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<string> moduleaList = new List<string>();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_User_Modules", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 100);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmByEmail.Value = username.ToUpper();
                else
                    prmByEmail.Value = DBNull.Value;

               

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                while (parentRdr.Read())
                {


                    moduleaList.Add( Convert.ToString(parentRdr["URL"]));
                  
                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return moduleaList;
        }

        public static DataSet getUsersList()
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Users", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "StudentsList");
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return dsStudentsList;
        }


        public static DataSet getAllUsersList()
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            DataSet dsUsersList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_AllUsers", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                dsUsersList = new DataSet();
                SqlDataAdapter daUsersList = new SqlDataAdapter(cmdOwners);
                daUsersList.Fill(dsUsersList, "AllUsers");
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return dsUsersList;
        }

        public static DataSet getAllRolesList()
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            DataSet dsRolesList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_AllRoles", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                dsRolesList = new DataSet();
                SqlDataAdapter daRoleList = new SqlDataAdapter(cmdOwners);
                daRoleList.Fill(dsRolesList, "AllRoles");
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return dsRolesList;
        }

        public static List<string> insertNewUser(string username, string password, string role, int roleId)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();
            List<string> moduleaList = new List<string>();

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Insert_New_User", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

                SqlParameter prmByEmail = cmdOwners.Parameters.Add("p_username", SqlDbType.NVarChar, 50);
                prmByEmail.Direction = ParameterDirection.Input;
                if ((username != null) && (!username.Trim().Equals("")))
                    prmByEmail.Value = username;
                else
                    prmByEmail.Value = DBNull.Value;

                SqlParameter prmPasswordl = cmdOwners.Parameters.Add("p_password", SqlDbType.NVarChar, 50);
                prmPasswordl.Direction = ParameterDirection.Input;
                if ((password != null) && (!password.Trim().Equals("")))
                    prmPasswordl.Value = password;
                else
                    prmPasswordl.Value = DBNull.Value;

                SqlParameter prmRole = cmdOwners.Parameters.Add("p_role", SqlDbType.NVarChar, 50);
                prmRole.Direction = ParameterDirection.Input;
                if ((role != null) && (!role.Trim().Equals("")))
                    prmRole.Value = role;
                else
                    prmRole.Value = DBNull.Value;

                SqlParameter prmRoleId = cmdOwners.Parameters.Add("p_roleId", SqlDbType.Int, 50);
                prmRoleId.Direction = ParameterDirection.Input;
                prmRoleId.Value = roleId;

                SqlDataReader parentRdr = cmdOwners.ExecuteReader();

                while (parentRdr.Read())
                {


                    moduleaList.Add(Convert.ToString(parentRdr["URL"]));

                }
            }
            catch (Exception weekendschoolException)
            {
                throw weekendschoolException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                    dbWeekendSchoolConn.Close();
            }

            return moduleaList;
        }



        public static DataSet getFinanceReport( string schoolYear, string paymentStatus, int level)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Payments_List", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;

               

                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.VarChar, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                if ((schoolYear != null) && (!schoolYear.Trim().Equals("")))
                    prmEnrollementYear.Value = schoolYear;
                else
                    prmEnrollementYear.Value = DBNull.Value;


                SqlParameter prmPaymentStatus = cmdOwners.Parameters.Add("p_paymentstatus", SqlDbType.VarChar, 1);
                prmPaymentStatus.Direction = ParameterDirection.Input;
                if ((paymentStatus != null) && (!paymentStatus.Trim().Equals("")))
                    prmPaymentStatus.Value = paymentStatus;
                else
                    prmPaymentStatus.Value = DBNull.Value;

                SqlParameter prmLevel = cmdOwners.Parameters.Add("p_level", SqlDbType.VarChar, 2);
                prmLevel.Direction = ParameterDirection.Input;
                prmLevel.Value = level;
                


                //Retrieve Rows
                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "FinanceList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsStudentsList;
        }

        public static DataSet getStudentByLevelReport(string schoolYear,  int level)
        {
            DBSqlConnect dbConn = new DBSqlConnect();
            SqlCommand cmdOwners = null;
            SqlConnection dbWeekendSchoolConn = dbConn.getSqlConnection();

            DataSet dsStudentsList = null;

            try
            {
                dbWeekendSchoolConn.Open();

                //Instantiate and inialize command
                cmdOwners = new SqlCommand("Proc_Get_Students_By_Level", dbWeekendSchoolConn);
                cmdOwners.CommandType = CommandType.StoredProcedure;



                SqlParameter prmEnrollementYear = cmdOwners.Parameters.Add("p_enrollementyear", SqlDbType.VarChar, 4);
                prmEnrollementYear.Direction = ParameterDirection.Input;
                if ((schoolYear != null) && (!schoolYear.Trim().Equals("")))
                    prmEnrollementYear.Value = schoolYear;
                else
                    prmEnrollementYear.Value = DBNull.Value;



                SqlParameter prmLevel = cmdOwners.Parameters.Add("p_level", SqlDbType.VarChar, 2);
                prmLevel.Direction = ParameterDirection.Input;
                prmLevel.Value = level;



                //Retrieve Rows
                dsStudentsList = new DataSet();
                SqlDataAdapter daStudentList = new SqlDataAdapter(cmdOwners);
                daStudentList.Fill(dsStudentsList, "StudentByLevelList");


            }
            catch (Exception pbisException)
            {
                throw pbisException;
            }
            finally
            {
                if (dbWeekendSchoolConn != null)
                {
                    dbWeekendSchoolConn.Close();
                    dbWeekendSchoolConn.Dispose();
                }
            }


            return dsStudentsList;
        }




    }
}