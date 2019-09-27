using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.WebView.Controls
{
    public  class MessageBox: WebControl, IPostBackEventHandler
    {
        public delegate void YesChoosed(object sender, string Key);
        public delegate void NoChoosed(object sender, string Key);

        public event YesChoosed OnYesChoosed;
        public event NoChoosed OnNoChoosed;

        private string message;
        private string key;
        private bool postBackOnYes;
        private bool postBackOnNo;

        public void ShowConfirmation(string message,string key,bool postBackOnYes,bool postBackOnNo)
        {
            this.message = "Conf" + message;
            this.key = key;
            this.postBackOnYes = postBackOnYes;
            this.postBackOnNo = postBackOnNo;
        }

        public void ShowMessage(string message)
        {
            this.message = message;
        }

        protected override void OnPreRender(EventArgs e)
        {
            if(!base.Page.IsClientScriptBlockRegistered("MessageBox"))
            {
                Page.RegisterClientScriptBlock("MessageBox", functionJava1());
            }
        }

        private string functionJava1()
        {
            string meuPostBackOnYes  = "MessageBoxTextoMensagem=\"\";";
            string meuPostBackOnNo = "MessageBoxTextoMensagem=\"\";";

            if(postBackOnYes)
            {
                meuPostBackOnYes = Page.GetPostBackEventReference(this, "Yes" + key);
            }

            if(postBackOnNo)
            {
                meuPostBackOnNo = Page.GetPostBackEventReference(this, "No_" + key);
            }

            string strJs = "<script language=\"javascript\"> " +
               "var MessageBoxTipoMensagem; " +
               "var MessageBoxTextoMensagem; " +
               "if (document.all && window.attachEvent) { " +
    "window.attachEvent(\"onfocus\", MessageBoxMostrarMensagem); " +
               "} else if (window.addEventListener) {  " +
               "window.addEventListener(\"load\"," + "MessageBoxMostrarMensagem,false); }" +
               "function MessageBoxMostrarMensagem() { " +
               "if (MessageBoxTextoMensagem) { " +
               "if (MessageBoxTextoMensagem != \"\") { " +
               "if (MessageBoxTipoMensagem == 2) {" +
               " alert(MessageBoxTextoMensagem); " +
               "} else {" +
               "if (confirm(MessageBoxTextoMensagem)) { " +
               meuPostBackOnYes +
               "} else { " +
               meuPostBackOnNo +
               "}} MessageBoxTextoMensagem=\"\"; " +
               " }}} </script>";

            return strJs;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if(modoDesign(this))
                writer.Write(this.ID);
            else
                if(message != string.Empty)
                {
                    StringBuilder miSB = new StringBuilder(message);

                    miSB.Replace("\"\"", "'");

                    if(miSB.ToString().StartsWith("Conf"))
                        this.Page.Response.Write("<script>MessageBoxTipoMensagem=1; MessageBoxTextoMensagem=\"" + miSB.ToString().Substring(4) + "\";</script>");
                    else
                        this.Page.Response.Write("<script>MessageBoxTipoMensagem=2; MessageBoxTextoMensagem=\"" + miSB.ToString() + "\";</script>");

                    miSB = null;
                }
        }

        private bool modoDesign(WebControl QueControl) 
        {
            bool DesignMode = false;

            try
            {
                DesignMode = QueControl.Site.DesignMode;
            }
            catch
            {
            }

            return DesignMode;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            switch(eventArgument.Substring(0, 3))
            {
                case "Yes": OnYesChoosed(this, eventArgument.Substring(3)); break;
                case "No": OnNoChoosed(this, eventArgument.Substring(3)); break;
            }       
        }
    }
}
