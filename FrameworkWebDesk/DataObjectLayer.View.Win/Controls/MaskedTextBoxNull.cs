using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;

namespace DataObjectLayer.View.Win.Controls
{
    public partial class TextBoxEntityDate : TextBoxEntity
    {
        public TextBoxEntityDate()
        {
            InitializeComponent();

            setPropertiesDefault();
        }

        public TextBoxEntityDate(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void setPropertiesDefault()
        {
            Mask = "99/99/9999";
        }

        private EntityPersistence entity
        {
            get
            {
                return DataBindings[0].DataSource as EntityPersistence;
            }
        }

        private DateTime? getValueProperty()
        {
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (prop.Name == DataBindings[0].PropertyName)
                {
                    return prop.GetValue(entity, null) as DateTime?;
                }
            }

            return null;
        }

        public new DateTime? Value
        {
            get
            {
                if (Text != string.Empty && Text != "  /  /")
                {
                    return Convert.ToDateTime(Text);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value == null)
                {
                    Text = string.Empty;
                }
                else
                {
                    Text = getDateFormat(value.Value);
                }
            }
        }

        private string getDateFormat(DateTime data)
        {
            string dia = string.Empty;
            string mes = string.Empty;

            if (data.Day.ToString().Length == 1)
            {
                dia = "0";
            }

            dia += data.Day.ToString();

            if (data.Month.ToString().Length == 1)
            {
                mes = "0";
            }

            mes += data.Month.ToString();

            return dia + "/" + mes + "/" + data.Year.ToString();
        }
    }
}