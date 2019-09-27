using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer.Reflection;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Web
{
    public class CheckBoxEntity : CheckBox, IViewControlEntity, IChangeBackColor
    {
        private string entitySource;

        private string entityProperty;

        private Color backColorValidate = Color.White;

        private Color backColorInvalidate = Color.Yellow;

        [Bindable(true)]
        [Category("Entity")]
        public Color BackColorInvalidate
        {
            set
            {
                backColorInvalidate = value;
            }
            get
            {
                return backColorInvalidate;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public Color BackColorValidate
        {
            set
            {
                backColorValidate = value;
            }
            get
            {
                return backColorValidate;
            }
        }

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

        private bool isCorrectTypeEntity(EntityPersistence entity)
        {
            return entitySource == entity.GetType().Name;
        }

        private void setCheck(EntityPersistence entity)
        {
            if (!isCorrectTypeEntity(entity))
            {
                return;
            }

            object valor = EntityReflection.Instance.GetValueProperty(entity, EntityProperty);

            if(valor != null)
            {
                Checked = Convert.ToBoolean(valor);
            }
            else
            {
                Checked = false;
            }
        }

        /// <summary>
        /// Atribui o valor da propriedade EntityProperty do EntitySource para a propriedade Text do TextBoxEntity.
        /// </summary>
        public void SetValueToControl(object value)
        {
            if (!(value is EntityPersistence))
            {
                return;
            }

            setCheck(value as EntityPersistence);

            if (OnAfterSetEntityPropertyToControl != null)
                OnAfterSetEntityPropertyToControl();
        }

        public object Value
        {
            get
            {
                return Checked;
            }
        }

        public bool IsSetEntityFromControl
        {
            set { }
            get { return true; }
        }

        #region IChangeBackColor Members

        public void ChangeBackColorValidate()
        {
            BackColor = backColorValidate;
        }

        public void ChangeBackColorInvalidate()
        {
            BackColor = backColorInvalidate;
        }

        #endregion
    }
}