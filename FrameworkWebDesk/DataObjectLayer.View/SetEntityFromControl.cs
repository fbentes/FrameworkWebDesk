using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DataObjectLayer;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View
{
    public class SetEntityFromControl
    {
        private List<PropertyException> propertyExceptionList;

        private static SetEntityFromControl instance = null;

        public static SetEntityFromControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SetEntityFromControl();
                }

                instance.propertyExceptionList = new List<PropertyException>();

                return instance;
            }
        }

        private SetEntityFromControl()
        {
            
        }

        public void Execute(IEntityPersistence entity, IComponent parent)
        {
            if (parent is System.Web.UI.Control)
            {
                execute(entity, parent as System.Web.UI.Control);
            }
            else
            {
                execute(entity, parent as System.Windows.Forms.Control);
            }
        }

        private void execute(IEntityPersistence entity, System.Web.UI.Control parent)
        {
            IViewControlEntity entityControl;

            foreach (System.Web.UI.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(entity, control);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        entityControl = control as IViewControlEntity;

                        if (entityControl.IsSetEntityFromControl && entityControl.Visible && entityControl.EntitySource == entity.GetType().Name)
                        {
                            try
                            {
                                EntityReflection.Instance.SetValuePropertyEntity(entity, entityControl.EntityProperty, entityControl.Value);

                                if (entityControl is IChangeBackColor)
                                {
                                    (entityControl as IChangeBackColor).ChangeBackColorValidate();
                                }
                            }
                            catch (PropertyException E)
                            {
                                propertyExceptionList.Add(E);

                                if (entityControl is IChangeBackColor)
                                {
                                    (entityControl as IChangeBackColor).ChangeBackColorInvalidate();
                                }
                            }
                        }
                    }
            }

            if (propertyExceptionList.Count > 0)
            {
                throw new PropertyListException(propertyExceptionList.ToArray());
            }
        }

        private void execute(IEntityPersistence entity, System.Windows.Forms.Control parent)
        {
            IViewControlEntity entityControl;

            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                {
                    execute(entity, control);
                }
                else
                    if (control is IViewControlEntity)
                    {
                        entityControl = control as IViewControlEntity;

                        if (entityControl.IsSetEntityFromControl && entityControl.Visible && entityControl.EntitySource == entity.GetType().Name)
                        {
                            try
                            {
                                EntityReflection.Instance.SetValuePropertyEntity(entity, entityControl.EntityProperty, entityControl.Value);

                                if (entityControl is IChangeBackColor)
                                {
                                    (entityControl as IChangeBackColor).ChangeBackColorValidate();
                                }
                            }
                            catch (PropertyException E)
                            {
                                propertyExceptionList.Add(E);

                                if (entityControl is IChangeBackColor)
                                {
                                    (entityControl as IChangeBackColor).ChangeBackColorInvalidate();
                                }
                            }
                        }
                    }
            }

            if (propertyExceptionList.Count > 0)
            {
                throw new PropertyListException(propertyExceptionList.ToArray());
            }
        }
    }
}
