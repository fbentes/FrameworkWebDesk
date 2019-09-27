using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer.Reflection;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Web
{
    public class LabelEntity : Label, IViewControlEntity
    {
        private string entitySource;

        private string entityProperty;

        private bool isSetEntityFromControl;

        [Category("Entity")]
        public event AfterSetEntityPropertyToControlDelegate OnAfterSetEntityPropertyToControl = null;

        [Bindable(true)]
        [Category("Entity")]
        public string EntitySource
        {
            set
            {
                entitySource = value;
            }
            get
            {
                return entitySource;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityProperty
        {
            set
            {
                entityProperty = value;
            }
            get
            {
                return entityProperty;
            }
        }

        [Bindable(true)]
        [Description("True se o valor da propriedade do entity será atribuído para o Text do Label.")]
        [Category("Entity")]
        [DefaultValue(false)]
        public bool IsSetEntityFromControl
        {
            set
            {
                isSetEntityFromControl = value;
            }
            get
            {
                return isSetEntityFromControl;
            }
        }

        private bool isCorrectTypeEntity(IEntityPersistence entity)
        {
            return entitySource == entity.GetType().Name;
        }

        private void setText(IEntityPersistence entity)
        {
            if (!isCorrectTypeEntity(entity))
            {
                return;
            }

            object valor = EntityReflection.Instance.GetValueProperty(entity, EntityProperty);
            
            if(valor != null)
            {
                Text = valor.ToString();
            }
            else
            {
                Text = string.Empty;
            }
        }

        public void SetValueToControl(object value)
        {
            if (!(value is IEntityPersistence))
            {
                return;
            }

            if (isSetEntityFromControl)
            {
                setText(value as IEntityPersistence );

                if (OnAfterSetEntityPropertyToControl != null)
                    OnAfterSetEntityPropertyToControl();
            }
        }

        public object Value
        {
            get { return Text; }
        }
    }
}