using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public class SetBindingControl
    {
        private static SetBindingControl instance = null;

        public static SetBindingControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SetBindingControl();
                }
                
                return instance;
            }
        }

        private SetBindingControl()
        {
        }

        public void Execute(Control control, string propertyName, object dataSource, string dataMember)
        {
            control.DataBindings.Clear();
            control.DataBindings.Add(propertyName, dataSource, dataMember);
        }

        public void Execute(ComboBox comboBox, EntityBaseCollection dataSource)
        {
            Execute(comboBox, dataSource, null, null);
        }

        public void Execute(ComboBox comboBox, EntityBaseCollection dataSource, EntityPersistence entityTarget, string propertyName)
        {
            comboBox.DataSource = dataSource;

            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";

            object value = EntityReflection.Instance.GetValueProperty(entityTarget, propertyName);

            if (value != null && !string.IsNullOrEmpty(value.ToString()))
                comboBox.SelectedValue = value;

            Execute(comboBox, "SelectedValue", entityTarget, propertyName);
        }
    }
}
