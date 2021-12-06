using adminweekendschool.WeekendSchool.Props;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Web;
using System.Web.Mail;

namespace adminweekendschool.WeekendSchool.Utils
{
    public static class Utility
    {
        public static string getMachineName()
        {
            string MachineName = System.Environment.MachineName;
            if (MachineName.Trim().ToUpper().Equals("SVINTRANET3"))
                MachineName = "http://" + MachineName.Trim().ToLower() + ".bcpa.local";
            else if (MachineName.Trim().ToUpper().Equals("SVWEBSTAGING"))
                MachineName = "https://" + MachineName.Trim().ToLower() + ".bcpa.net";
            else if (MachineName.Trim().ToUpper().Equals("IT6127DTTR"))
                MachineName = "http://localhost";
            else
                MachineName = "http://localhost";

            return MachineName;

        }

        public static string getFileFullPath()
        {
            string fileFullPath = @"C:\PDF\";
            if (!Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(fileFullPath);
            }



            fileFullPath = @"C:\PDF\";
            if (!Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(fileFullPath);
            }

            string MachineName = System.Environment.MachineName;
            if (MachineName.Trim().ToUpper().Equals("SVINTRANET3"))
                fileFullPath = @"C:\wwwroot\inetpub\onlineweekendschool\files\";


            return fileFullPath;
        }

        public static void KillAllRunningProcess(string processToKill)
        {


            Process[] localByName = Process.GetProcessesByName(processToKill);

            int i = 0;

            while (i < localByName.Length)
            {
                localByName[i].Kill();
                localByName[i].WaitForExit();
                i++;
            }
        }


        public static void KillRunningProcess(string processToKill)
        {


            Process[] localByName = Process.GetProcessesByName(processToKill);

            int i = 0;

            while (i < localByName.Length - 1)
            {
                localByName[i].Kill();
                localByName[i].WaitForExit();
                i++;
            }
        }


        //**************************************************************************/
        //***					  Send an Email to the user                      ***/
        //**************************************************************************/
        public static bool sendEmail(EmailUserProps emailObj)
        {
            bool isEmailSent = true;
            string EmailServer = null;

            MailMessage emailUser = new MailMessage();
            if (emailObj.FromUser != null)
                emailUser.From = emailObj.FromUser;
            else
                return false;

            if (emailObj.ToUser != null)
                emailUser.To = emailObj.ToUser;
            else
                return false;

            if (emailObj.CcUser != null)
                emailUser.Cc = emailObj.CcUser;

            if (emailObj.BccUser != null)
                emailUser.Bcc = emailObj.BccUser;

            if (emailObj.Attachment != null)
            {
                MailAttachment attachment = new MailAttachment(emailObj.Attachment);
                emailUser.Attachments.Add(attachment);
            }

            emailUser.Subject = emailObj.Subject;
            emailUser.Body = emailObj.Body;

            //EmailServer = DBConnect.getEmailServer();
            //if (EmailServer != null)
            //    SmtpMail.SmtpServer = DBConnect.getEmailServer();
            //else
            //    SmtpMail.SmtpServer = "localhost";

            //try
            //{
            //    SmtpMail.Send(emailUser);
            //}
            //catch (Exception e)
            //{
            //    isEmailSent = false;
            //    throw e;
            //}

            //emailUser = null;
            return isEmailSent;
        }

    }
}