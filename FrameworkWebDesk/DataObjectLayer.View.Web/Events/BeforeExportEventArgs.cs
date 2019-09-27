using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View.Web
{
    public class BeforeExportEventArgs: EventArgs 
    {
        private BaseDataBoundControl control;

        private string javaScript = string.Empty;

        public string JavaScript
        {
            set { javaScript = value; }
            get 
            {
                return getJavaScriptFormated(); 
            }
        }

        public BaseDataBoundControl Control
        {
            get { return control; }
        }

        public BeforeExportEventArgs(BaseDataBoundControl control)
        {
            this.control = control;
        }

        private string getJavaScriptFormated()
        {
            if (javaScript.IndexOf("</script>") == -1 && javaScript != string.Empty)
            {
                javaScript = "<script language=\"javascript\">" + javaScript + "</script>";
            }

            return javaScript;
        }
    }
}
