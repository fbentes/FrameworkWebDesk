using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace DataObjectLayer.Business
{
    public class EMail
    {
        #region Fields

        private static EMail instance = null;

        private string fromMailAdress = string.Empty;

        private string toMailAdress = string.Empty;

        private string subject = string.Empty;

        private string body = string.Empty;

        private string[] cc = null;

        private string smtpServer = string.Empty;

        private int port = 25;

        private string userName = string.Empty;

        private string password = string.Empty;

        #endregion

        #region Properties

        public static EMail Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EMail();
                }
                return instance;
            }
        }

        public string FromMailAdress
        {
            get { return fromMailAdress; }
            set { fromMailAdress = value; }
        }

        public string ToMailAdress
        {
            get { return toMailAdress; }
            set { toMailAdress = value; }
        }

        public string[] CC
        {
            set
            {
                cc = value;
            }
            get
            {
                return cc;
            }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        public string SmtpServer
        {
            get { return smtpServer; }
            set { smtpServer = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region Constructor
        
        private EMail()
        {
        }

        #endregion

        #region Methods

        public void Send()
        {
            MailAddress From = new MailAddress(fromMailAdress);
            MailAddress To = new MailAddress(toMailAdress);

            MailMessage mailMessage = new MailMessage(From, To);

            mailMessage.Subject = subject;

            mailMessage.Body = body;

            if(cc != null)
            {
                foreach (string address in cc)
                {
                    mailMessage.CC.Add(new MailAddress(address));
                }
            }
          
            SmtpClient smtp = new SmtpClient(smtpServer, port);

            if (userName == null || userName.Trim() == string.Empty)
            {
                smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                smtp.Credentials = new NetworkCredential(userName, password);
            }

            smtp.Send(mailMessage);
        }

        #endregion
    }
}
