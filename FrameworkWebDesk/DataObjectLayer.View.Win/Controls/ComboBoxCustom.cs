using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.Reflection;
using System.Design;

namespace DataObjectLayer.View.Win
{
    public partial class ComboBoxCustom : ComboBox, IViewControlEntity, IChangeBackColor
    {
        public delegate void ExecuteMoreCode();

        [Serializable]
        private class NullItem
        {
            private static NullItem instance = null;

            public static NullItem GetValue(string textNullValue)
            {
                if (instance == null)
                {
                    instance = new NullItem();                  
                }

                instance.textNullValue = textNullValue;

                return instance;
            }

            private string textNullValue = string.Empty;

            private NullItem()
            {

            }

            public string Id
            {
                get
                {
                    return string.Empty;
                }
            }

            public string Name
            {
                get
                {
                    return textNullValue;
                }
            }
            
            public override string ToString()
            {
                return textNullValue;
            }
        }

        public event BeforeSelectedIndexChanged BeforeSelectedIndexChanged = null;

        public event AfterSelectedIndexChanged AfterSelectedIndexChanged = null;

        private bool hasNullValue = true;

        private string textNullValue = string.Empty;

        private string listSource = string.Empty;

        private bool canExecuteSelectedIndexChanged = true;

        private string listNamespaceSource = string.Empty;

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

        [Browsable(true)]
        [Category("Behavior")]
        public bool HasNullValue
        {
            get
            {
                return hasNullValue;
            }
            set
            {
                hasNullValue = value;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        [DefaultValue("")]
        public string TextNullValue
        {
            set
            {
                textNullValue = value;
            }
            get
            {
                return textNullValue;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string ListSource
        {
            set
            {
                listSource = value;

                if (!DesignMode)
                {
                    SetDataSource(listSourceInstance);
                }
            }
            get
            {
                return listSource;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string ListNamespaceSource
        {
            set
            {
                listNamespaceSource = value;
            }
            get
            {
                return listNamespaceSource;
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

        private Type typeListSource
        {
            get
            {
                if (ListSource == string.Empty)
                {
                    return null;
                }

                return EntityReflection.Instance.GetType(ListNamespaceSource, ListSource);
            }
        }

        private IList listSourceInstance
        {
            get
            {
                if (typeListSource != null)
                {
                    PropertyInfo prope = EntityReflection.Instance.GetFirstProperty(typeListSource, typeof(SingletonAttribute));

                    if (prope == null)
                    {
                        throw new EntityManagerNoConstructorException("A classe " + ListSource + " deve conter uma propriedade pública com o atributo [Singleton] retornando uma instância de si própria !", null);
                    }

                    return prope.GetValue(null, null) as IList;
                }

                return null;
            }
        }

        public ComboBoxCustom()
        {
            initialize();

            DisplayMember = "Value";
            ValueMember = "Key";
        }

        public ComboBoxCustom(IContainer container):this()
        {
            container.Add(this);          
        }

        [DefaultValue("")]
        [RefreshProperties( RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (!DesignMode)
                {
                    SetDataSource(value as IEnumerable);
                }
            }
        }

        private void initialize()
        {
            InitializeComponent();

            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void SetDataSource(IEnumerable list)
        {
            if (list == null)
            {
                base.DataSource = null;
                return;
            }

            IList retval = new ArrayList();

            foreach (object o in list)
            {
                retval.Add(o);
            }

            if (this.hasNullValue)
            {
                retval.Insert(0, NullItem.GetValue(textNullValue));
            }

            if (retval.Count > 0)
            {
                canExecuteSelectedIndexChanged = false;

                base.DataSource = retval;

                canExecuteSelectedIndexChanged = true;
            }
            else
            {
                base.DataSource = null;
            }
        }

        public object ItemSelected
        {
            get
            {
                if (this.SelectedItem == NullItem.GetValue(textNullValue))
                {
                    return null;
                }
                else
                    return this.SelectedItem;
            }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    if (this.SelectedItem != NullItem.GetValue(textNullValue))
                        this.SelectedItem = NullItem.GetValue(textNullValue);
                }
                else
                    this.SelectedItem = value;
            }
        }

        protected virtual void OnBeforeSelectedIndexChanged(EntityEventArgs e)
        {
            if (BeforeSelectedIndexChanged != null)
            {
                BeforeSelectedIndexChanged(e);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (canExecuteSelectedIndexChanged)
            {
                EntityEventArgs entityEventArgs = new EntityEventArgs();

                OnBeforeSelectedIndexChanged(entityEventArgs);

                if (entityEventArgs.CancelAction)
                {
                    return;
                }

                base.OnSelectedIndexChanged(e);

                OnAfterSelectedIndexChanged(entityEventArgs);
            }
        }

        protected virtual void OnAfterSelectedIndexChanged(EntityEventArgs e)
        {
            if (AfterSelectedIndexChanged != null)
            {
                AfterSelectedIndexChanged(e);
            }
        }

        public virtual void SetCurrentItem(object value)
        {
            if (value == null || value.ToString().Trim() == string.Empty)
            {
                SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                if (((KeyValuePair<object, object>)Items[i]).Key.ToString() == value.ToString())
                {
                    SelectedIndex = i;
                    break;
                }
            }
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

        #region IViewControlEntity Members

        public bool IsSetEntityFromControl
        {
            set { }
            get { return true; }
        }

        protected bool isCorrectTypeEntity(IEntityPersistence entity)
        {
            return entitySource == entity.GetType().Name;
        }
        
        public virtual void SetValueToControl(object value)
        {
            if (!(value is IEntityPersistence))
            {
                return;
            }

            if (!isCorrectTypeEntity(value as IEntityPersistence))
            {
                return;
            }

            object valueProperty = EntityReflection.Instance.GetValueProperty(value as IEntityPersistence, EntityProperty);

            SetCurrentItem(valueProperty);
        }

        public virtual object Value
        {
            get 
            {
                if (SelectedItem != null)
                {
                    return SelectedValue;
                }
                return null;
            }
        }

        #endregion
    }
}