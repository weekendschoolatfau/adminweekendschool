using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminweekendschool.WeekendSchool.Props
{
    public class EmailUserProps
    {
        private string fromUser;
        private string toUser;
        private string ccUser;
        private string bccUser;
        private string subject;
        private string body;
        private string emailServer;
        private string attachment;

        public EmailUserProps()
        {
            fromUser = null;
            toUser = null;
            ccUser = null;
            bccUser = null;
            subject = null;
            body = null;
            emailServer = null;
            attachment = null;
        }

        public string Attachment
        {
            get
            {
                return attachment;
            }
            set
            {
                attachment = value;
            }
        }


        public string FromUser
        {
            get
            {
                return fromUser;
            }
            set
            {
                fromUser = value;
            }
        }

        public string ToUser
        {
            get
            {
                return toUser;
            }
            set
            {
                toUser = value;
            }
        }

        public string CcUser
        {
            get
            {
                return ccUser;
            }
            set
            {
                ccUser = value;
            }
        }

        public string BccUser
        {
            get
            {
                return bccUser;
            }
            set
            {
                bccUser = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }

        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }

        public string EmailServer
        {
            get
            {
                return emailServer;
            }
            set
            {
                emailServer = value;
            }
        }
    }

}