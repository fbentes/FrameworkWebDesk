using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DataObjectLayer.View.Web.Estoque
{
    public class Global : System.Web.HttpApplication
    {
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["NHibernateManager"] = NHibernateManager.Instance;

            string nhibernateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"hibernate.cfg.xml");

            (Session["NHibernateManager"] as NHibernateManager).CreateConfiguration(nhibernateFile);
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}