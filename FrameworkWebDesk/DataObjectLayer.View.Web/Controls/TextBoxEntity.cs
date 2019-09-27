using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer.Reflection;
using System.Globalization;
using DataObjectLayer.Business;
using DataObjectLayer.View;
using eWorld.UI;

namespace DataObjectLayer.View.Web
{
    public class TextBoxEntity : MaskedTextBox, IViewControlEntity, IChangeBackColor
    {
        [Category("Entity")]
        public event AfterSetEntityPropertyToControlDelegate OnAfterSetEntityPropertyToControl = null;

        private string entitySource;

        private string entityProperty;

        private Color backColorValidate = Color.White;

        private Color backColorInvalidate = Color.Yellow;

        [Bindable(true)]
        [Category("Appearance")]
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
        [Category("Appearance")]
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

        public TextBoxEntity()
        {
            //Implementar limitação do número de caracteres e atribuir para this.MaxLength.
        }

        protected override void OnLoad(EventArgs e)
        {
            BackColor = BackColorValidate;
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

            if (valor != null)
            {
                string valorFormatado = valor.ToString();

                CultureInfo culture = new CultureInfo("pt-BR", false);

                if (valor.GetType().Name.ToLower() == "decimal")
                {
                    NumberFormatInfo numberFormatInfo = culture.NumberFormat;
                    
                    valorFormatado = Convert.ToDecimal(valor).ToString("N", numberFormatInfo);
                }
                else
                    if (valor.GetType().Name.ToLower() == "datetime" && Mask.Trim()=="99/99/9999")
                    {
                        valorFormatado = ValidationDate.Instance.GetDateFormated(Convert.ToDateTime(valor).ToShortDateString());
                    }

                Text = valorFormatado;
            }
            else
            {
                Text = string.Empty;
            }
        }

        /// <summary>
        /// Atribui o valor da propriedade EntityProperty do EntitySource para a propriedade Text do TextBoxEntity.
        /// </summary>
        public void SetValueToControl(object value)
        {
            if (value == null)
            {
                Text = string.Empty;
                
                return;
            }

            if (!(value is IEntityPersistence))
            {
                return;
            }

            setText(value as IEntityPersistence);

            if (OnAfterSetEntityPropertyToControl != null)
                OnAfterSetEntityPropertyToControl();
        }

        public object Value
        {
            get 
            {
                return Text;
            }
        }

        public bool IsSetEntityFromControl
        {
            set{}
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