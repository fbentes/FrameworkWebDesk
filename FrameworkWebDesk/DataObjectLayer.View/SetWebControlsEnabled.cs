using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DataObjectLayer;
using System.Web;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View
{
    /// <summary>
    /// Torna todos os controles da lista controls habilitados ou não !
    /// </summary>
    public static class SetWebControlsEnabled
    {

        public static void Execute(IComponent parent, bool enabled)
        {
            if (parent is System.Web.UI.Control)
            {
                execute(parent as System.Web.UI.Control, enabled);
            }
            else
            {
                execute(parent as System.Windows.Forms.Control, enabled);
            }
        }

        /// <summary>
        /// Para cada WebControl da collection controls, será atribuído true ou false para a propriedade Enabled.
        /// </summary>
        /// <param name="controls">Lista de controles</param>
        /// <param name="enabled">True para habilitar todos os controles da lista; false caso contrário.</param>        
        private static void execute(System.Web.UI.Control parent, bool enabled)
        {
            foreach (System.Web.UI.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(control, enabled);
                }
                else
                    if (control is WebControl)
                    {
                        (control as WebControl).Enabled = enabled;
                    }
            }
        }

        private static void execute(System.Windows.Forms.Control parent, bool enabled)
        {
            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(control, enabled);
                }
                else
                    (control as System.Windows.Forms.Control).Enabled = enabled;
            }
        }
    }
}
