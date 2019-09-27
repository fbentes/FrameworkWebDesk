using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataObjectLayer.Reflection;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Web
{
    public class DropDownListEntity : DropDownListCustom
    {
        private string propertyOrderList;
        
        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        [Category("Entity")]
        public event AfterSetEntityPropertyToControlDelegate AfterSetEntityPropertyToControl = null;

        [Bindable(true)]
        [Category("Entity")]
        public string PropertyOrderList
        {
            set
            {
                propertyOrderList = value;
            }
            get
            {
                return propertyOrderList;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public ListFilterEntity ListFilterEntity
        {
            set
            {
                ViewState[this.UniqueID + "ListFilterEntity"] = value;
            }
            get
            {
                return ViewState[this.UniqueID + "ListFilterEntity"] as ListFilterEntity;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerSource
        {
            set { entityManagerSource = value; }
            get { return entityManagerSource; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerNamespaceSource
        {
            get { return entityManagerNamespaceSource; }
            set { entityManagerNamespaceSource = value; }
        }

        private Type typeEntityManager
        {
            get
            {
                if (EntityManagerSource == string.Empty)
                {
                    return null;
                }

                return EntityReflection.Instance.GetType(EntityManagerNamespaceSource, EntityManagerSource);
            }
        }

        private IListFilterRegister entityManagerSourceInstance
        {
            get
            {
                if (typeEntityManager != null)
                {
                    PropertyInfo prope = EntityReflection.Instance.GetFirstProperty(typeEntityManager, typeof(SingletonAttribute));

                    if (prope == null)
                    {
                        throw new EntityManagerNoConstructorException("A classe " + EntityManagerSource + " deve conter uma propriedade pública com o atributo [Singleton] retornando uma instância de si própria !", null);
                    }

                    return prope.GetValue(null, null) as IListFilterRegister;
                }

                return null;
            }
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
            }
        }

        private IListFilterRegister entityManagerSourceList;

        public IListFilterRegister EntityManagerSourceList
        {
            set
            {
                entityManagerSourceList = value;

                if (Items.Count == 0)
                {
                    setDataSourceWithList(value as IListFilterRegister);
                }
            }
        }

        private void setDataSourceWithList(IListFilterRegister entityManager)
        {
            if (entityManager == null)
            {
                DataSource = null;
                DataBind();

                return;
            }

            OrderEntity[] orders = null;

            if (PropertyOrderList != string.Empty)
            {
                orders = new OrderEntity[] { new OrderEntity(PropertyOrderList, true) };
            }

            IList list = entityManager.List(orders, ListFilterEntity);

            base.SetDataSource(list, HasNullValue);
        }

        private void setCurrentItem(IEntityPersistence entity)
        {
            object value = EntityReflection.Instance.GetValueProperty(entity, EntityProperty);

            if (value != null)
            {
                SetCurrentItem(value as IEntityPersistence);
            }
            else
            {
                SetCurrentItem(string.Empty);
            }
        }

        public void SetCurrentItem(IEntityPersistence entity)
        {
            if (entity != null)
            {
                SetCurrentItem(entity.Id.ToString());
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            setDataSourceWithList(entityManagerSourceInstance);
        }

        /// <summary>
        /// Atribui o valor da propriedade EntityProperty do EntitySource para a propriedade Text do TextBoxEntity.
        /// </summary>
        public override void SetValueToControl(object value)
        {
            if (!(value is IEntityPersistence))
            {
                if (value == null)
                {
                    SelectedIndex = -1;
                }

                return;
            }

            if (!isCorrectTypeEntity(value as IEntityPersistence))
            {
                return;
            }

            setCurrentItem(value as IEntityPersistence);

            if (AfterSetEntityPropertyToControl != null)
            {
                AfterSetEntityPropertyToControl();
            }
        }

        #region ISetEntityFromControl Members

        public override object Value
        {
            get 
            { 
                if(SelectedItem == null || SelectedItem.Value.Trim() == string.Empty)
                {
                    return null;
                }

                return entityManagerSourceInstance.Read(Convert.ToInt32(SelectedItem.Value));
            }
        }

        #endregion
    }
}
