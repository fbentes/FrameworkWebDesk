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

namespace DataObjectLayer.View.Win
{
    public partial class ComboBoxEntity : ComboBoxCustom
    {
        public event BeforeRefreshListEvent BeforeRefreshListEvent;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        private List<OrderEntity> orderList = new List<OrderEntity>();

        private ListFilterEntity listFilterEntity = new ListFilterEntity();

        private new string ListSource
        {
            set
            {                
            }
            get
            {
                return null;
            }
        }

        private new string ListNamespaceSource
        {
            set
            {
            }
            get
            {
                return null;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerNamespaceSource
        {
            get { return entityManagerNamespaceSource; }
            set { entityManagerNamespaceSource = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerSource
        {
            set
            {
                entityManagerSource = value;
            }
            get
            {
                return entityManagerSource;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public List<OrderEntity> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public ListFilterEntity ListFilterEntity
        {
            get
            {
                return listFilterEntity;
            }
            set
            {
                listFilterEntity = value;
            }
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

        private IListDeleteRegister entityManagerSourceInstance
        {
            get
            {
                if (typeEntityManager != null)
                {
                    PropertyInfo prope = EntityReflection.Instance.GetFirstProperty(typeEntityManager, typeof(SingletonAttribute));

                    if (prope == null)
                    {
                        throw new EntityManagerNoConstructorException("A classe " + EntityManagerSource + " deve conter uma propriedade pública com o atributo [Singleton] !", null);
                    }

                    return prope.GetValue(null, null) as IListDeleteRegister;
                }

                return null;
            }
        }

        public ComboBoxEntity()
        {
            DisplayMember = "Name";
            ValueMember = "Id";
        }

        public ComboBoxEntity(IContainer container)
            : this()
        {
            container.Add(this);          
        }

        public void RefreshList()
        {
            EntityEventArgs entityEventArgs = null;

            if (BeforeRefreshListEvent != null)
            {
                entityEventArgs = new EntityEventArgs(entityManagerSourceInstance, Value as IEntityPersistence);

                BeforeRefreshListEvent(entityEventArgs);
            }

            if (entityEventArgs == null || !entityEventArgs.CancelAction)
            {
                DataSource = entityManagerSourceInstance.List((OrderList != null ? OrderList.ToArray() : null), ListFilterEntity);
            }
        }

        public override void SetCurrentItem(object value)
        {
            if (value == null)
            {
                return;
            }

            if (!(value is IEntityPersistence))
            {
                throw new Exception("O parâmetro value do método " + Name + ".SetCurrentItem não implementa IEntityPersistence !");
            }

            SelectedItem = value;
        }
    
        public override object Value
        {
            get
            {
                return SelectedItem;
            }
        }
    }
}