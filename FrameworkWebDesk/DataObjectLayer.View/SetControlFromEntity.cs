using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DataObjectLayer;

namespace DataObjectLayer.View
{
    public static class SetControlFromEntity
    {
        public static void Execute(IEntityPersistence entity, IComponent parent)
        {
            Execute(entity, parent, true);
        }

        public static void Execute(IEntityPersistence entity, IComponent parent, bool enabled)
        {
            if (parent is System.Web.UI.Control)
            {
                execute(entity, parent as System.Web.UI.Control, enabled);
            }
            else
            {
                execute(entity, parent as System.Windows.Forms.Control, enabled);
            }
        }

        private static void execute(IEntityPersistence entity, System.Windows.Forms.Control parent, bool enabled)
        {
            IViewControlEntity entityControl;

            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(entity, control, enabled);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        entityControl = control as IViewControlEntity;

                        if (entityControl.EntitySource == entity.GetType().Name)
                        {
                            entityControl.SetValueToControl(entity);
                            entityControl.Enabled = enabled;
                        }
                    }
            }
        }

        private static void execute(IEntityPersistence entity, System.Web.UI.Control parent, bool enabled)
        {
            IViewControlEntity entityControl;

            foreach (System.Web.UI.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(entity, control, enabled);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        entityControl = control as IViewControlEntity;

                        if (entityControl.EntitySource == entity.GetType().Name)
                        {
                            entityControl.SetValueToControl(entity);
                            entityControl.Enabled = enabled;
                        }
                    }
            }
        }
    }
}
