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
    public static class SetControlEntityEnabled
    {
        public static void Execute(IComponent control, bool enabled)
        {
            Execute(control, enabled, false, string.Empty);
        }

        public static void Execute(IComponent parent, bool enabled, bool clear, string entitySource)
        {
            if (parent is System.Web.UI.Control)
            {
                execute(parent as System.Web.UI.Control, enabled, clear, entitySource);
            }
            else
            {
                execute(parent as System.Windows.Forms.Control, enabled, clear, entitySource);
            }
        }

        /// <summary>
        /// Para cada WebControl da collection controls, será atribuído true ou false para a propriedade Enabled.
        /// </summary>
        /// <param name="controls">Lista de controles</param>
        /// <param name="enabled">True para habilitar todos os controles da lista controls; false caso contrário.</param>
        /// <param name="clear">True para limpar todos os controles da lista controls; false caso contrário.</param>
        private static void execute(System.Web.UI.Control parent, bool enabled, bool clear, string entitySource)
        {
            foreach (System.Web.UI.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(control, enabled, clear, entitySource);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        if (entitySource == string.Empty || entitySource == (control as IViewControlEntity).EntitySource)
                        {
                            (control as IViewControlEntity).Enabled = enabled;

                            if (clear)
                            {
                                (control as IViewControlEntity).SetValueToControl(null);
                            }
                        }
                    }
            }
        }

        private static void execute(System.Windows.Forms.Control parent, bool enabled, bool clear, string entitySource)
        {
            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(control, enabled, clear, entitySource);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        if (entitySource == string.Empty || entitySource == (control as IViewControlEntity).EntitySource)
                        {
                            (control as IViewControlEntity).Enabled = enabled;

                            if (clear)
                            {
                                (control as IViewControlEntity).SetValueToControl(null);
                            }
                        }
                    }
            }
        }

    }
}
