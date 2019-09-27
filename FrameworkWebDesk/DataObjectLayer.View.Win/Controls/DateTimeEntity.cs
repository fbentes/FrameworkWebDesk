using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.Reflection;
using DataObjectLayer.Business;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Win
{
    public partial class DateTimeEntity : DateTimePicker, IViewControlEntity, IChangeBackColor
    {
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

        public DateTimeEntity()
        {
            InitializeComponent();

            Format = DateTimePickerFormat.Short;
        }

        public DateTimeEntity(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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

                if (valor.GetType().Name.ToLower() == "datetime")
                {
                    valorFormatado = ValidationDate.Instance.GetDateFormated(Convert.ToDateTime(valor).ToShortDateString());
                }

                Text = valorFormatado;
            }
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

        public new object Value
        {
            get { return Text; }
        }

        #endregion

        #region IChangeBackColor Members

        public void ChangeBackColorInvalidate()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void ChangeBackColorValidate()
        {
            BackColor = backColorInvalidate;
        }

        #endregion
    }
}
