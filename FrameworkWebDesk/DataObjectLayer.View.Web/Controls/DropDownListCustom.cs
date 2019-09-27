using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using DataObjectLayer.Reflection;
using DataObjectLayer.View;
using DataObjectLayer.Business;

namespace DataObjectLayer.View.Web
{
    public class DropDownListCustom : DropDownList, IViewControlEntity, IChangeBackColor
    {
        #region Fields

        private bool hasNullValue = true;

        private string textNullValue = string.Empty;

        private string listSource = string.Empty;

        private string listNamespaceSource = string.Empty;

        private string entitySource = string.Empty;

        private string entityProperty = string.Empty;

        private Color backColorValidate = Color.White;

        private Color backColorInvalidate = Color.Yellow;

        #endregion
      
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

        [Bindable(true)]
        [Category("Entity")]
        [DefaultValue(true)]
        public bool HasNullValue
        {
            set
            {
                hasNullValue = value;
            }
            get
            {
                return hasNullValue;
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

        public virtual object Value
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem.Value;
                }
                return null;
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            object valueSelected = null;

            if (Value != null)
            {
                valueSelected = Value;
            }

            SetDataSource(listSourceInstance);

            if (valueSelected != null)
            {
                SetCurrentItem(valueSelected.ToString());
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            BackColor = BackColorValidate;
        }

        public virtual void SetDataSource(IList list)
        {
            SetDataSource(list, false);
        }

        public virtual void SetDataSource(IList list, bool hasNullValue)
        {
            SetDataSource(list, "", "", hasNullValue);
        }

        public virtual void SetDataSource(IList list, string text, string value)
        {
            SetDataSource(list, text, value, false);
        }

        public virtual void SetDataSource(IList list, string text, string value, bool hasNullValue)
        {
            if (list == null || list.Count == 0)
            {
                DataSource = null;
            }
            else
            {
                Items.Clear();

                if (hasNullValue)
                {
                    Items.Add(new ListItem(textNullValue, string.Empty));
                }

                if (list[0].GetType() == typeof(KeyValuePair<object,object>))
                {
                    foreach (KeyValuePair<object, object> item in list)
                    {
                        Items.Add(new ListItem(item.Value.ToString(), item.Key.ToString()));
                    }
                }
                else
                    if (list[0].GetType().GetInterface(typeof(IEntityPersistence).Name) != null)
                    {
                        foreach (IEntityPersistence entity in list)
                        {
                            Items.Add(new ListItem(entity.ToString(), entity.Id.ToString()));
                        }
                    }
            }

            DataBind();
        }

        public void SetCurrentItem(string value)
        {
            if (value == string.Empty)
            {
                SelectedIndex = -1;
                return;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Value == value)
                {
                    SelectedIndex = i;
                    break;
                }
            }
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

            if (valueProperty != null)
            {
                SetCurrentItem(valueProperty.ToString());
            }
            else
            {
                SetCurrentItem(string.Empty);
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