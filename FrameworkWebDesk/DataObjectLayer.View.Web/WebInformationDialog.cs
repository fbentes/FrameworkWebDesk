using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DataObjectLayer.View.Web
{
    public class WebInformationDialog
    {
        public static void Show(Page page, string menssagem)
        {
            Random random = new Random();
            random.Next();

            page.ClientScript.RegisterStartupScript(page.GetType(), random.ToString(), "<script> alert('" + menssagem + "'); </script>");
        }
    }
}
