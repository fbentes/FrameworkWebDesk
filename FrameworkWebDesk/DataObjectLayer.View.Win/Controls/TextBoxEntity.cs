using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DataObjectLayer;
using DataObjectLayer.View;
using DataObjectLayer.Business;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public partial class TextBoxEntity : MaskedTextBox, IViewControlEntity, IChangeBackColor
    {
        #region Fields

        private string entitySource;

        private string entityProperty;

        private Color backColorValidate = Color.White;

        private Color backColorInvalidate = Color.Yellow;

        #endregion

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

        public TextBoxEntity()
        {
            InitializeComponent();
        }

        public TextBoxEntity(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnEnter(EventArgs e)
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
                    if (valor.GetType().Name.ToLower() == "datetime" && Mask.Trim() == "99/99/9999")
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

        #region IViewControlEntity Members

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

        public bool IsSetEntityFromControl
        {
            set { }
            get { return true; }
        }

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
        }

        public object Value
        {
            get
            {
                return Text;
            }
        }


        #endregion

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
