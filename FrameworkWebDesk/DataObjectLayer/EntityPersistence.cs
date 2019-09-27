using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using DataObjectLayer.Reflection;

namespace DataObjectLayer
{
    [Serializable]
    public class EntityPersistence : EntityBase, ICloneable, IEntityPersistence
    {
        enum OperationListChild
        {
            Add,
            Set,
            Remove
        }

        #region Fields

        private int idDefaultInList = -3;

        private bool hasChanged;

        private event EventHandler Changed;

        private bool isChild;

        #endregion

        #region Properties

        [PropertyInvisible()]
        public virtual int IdDefaultInList
        {
            get { return idDefaultInList; }
        }

        [PropertyInvisible()]
        public virtual int IdDefault
        {
            get
            {
                return unsavedValue;
            }
        }

        [PropertyInvisible()]
        public virtual bool HasChanged
        {
            get
            {
                return hasChanged;
            }
            set
            {
                hasChanged = value;
            }
        }

        [PropertyInvisible()]
        public virtual bool IsChild
        {
            set
            {
                isChild = value;
            }
            get
            {
                return isChild;
            }
        }

        #endregion

        #region Constructor
        
        protected EntityPersistence()
        {
            this.id = -1;
            this.unsavedValue = -1;

            PropertyInfo[] properties = EntityReflection.Instance.Properties(this.GetType(), typeof(EntityParentAttribute));

            isChild = properties.Length > 0;
        }

        #endregion

        #region Methods

        protected override void SetId(Int32 value)
        {
            this.SetValue("id", value);
        }

        protected virtual void SetValue(string field, object value)
        {
            FieldInfo f = this.getField(field);

            if (f == null)
            {
                throw new EntityFieldNotFoundException();
            }
            else
            {
                if (f.GetValue(this) != value)
                {
                    this.hasChanged = true;

                    f.SetValue(this, value);

                    this.onChange();
                }
            }
        }

        protected virtual FieldInfo getField(string name)
        {
            Type type = this.GetType();

            FieldInfo field = null;

            while (type != typeof(object))
            {
                field = type.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (field != null)
                {
                    break;
                }

                type = type.BaseType;
            }

            return field;
        }

        protected virtual void onChange()
        {
            if (this.Changed != null)
                this.Changed(this, EventArgs.Empty);
        }

        private void setEntityParent(EntityPersistence entityChild)
        {
            PropertyInfo[] properties = EntityReflection.Instance.Properties(entityChild.GetType(), typeof(EntityParentAttribute));

            if (properties.Length == 1)
                properties[0].SetValue(entityChild, this, null);
            else
                if (properties.Length == 0)
                    throw new EntityParentException("Deve haver pelo menos um entity parent para o entity child " + entityChild.GetType().Name);
                else
                {
                    foreach (PropertyInfo prop in properties)
                    {
                        if (prop.PropertyType == this.GetType())
                        {
                            prop.SetValue(entityChild, this, null);

                            break;
                        }
                    }
                }
        }

        private IList operationListChild(EntityPersistence entityChild, OperationListChild operation)
        {
            PropertyInfo[] properties = EntityReflection.Instance.Properties(this.GetType(), typeof(EntityListChildAttribute));

            IList listEntity = null;

            if (properties.Length > 0)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (EntityReflection.Instance.GetTypeEntityChild(property) == entityChild.GetType())
                    {
                        listEntity = property.GetValue(this, null) as IList;

                        if (listEntity != null)
                        {
                            if (operation == OperationListChild.Add)
                            {
                                listEntity.Add(entityChild);

                                setEntityParent(entityChild);
                            }
                            else
                                if (operation == OperationListChild.Remove)
                                {
                                    listEntity.Remove(entityChild);
                                }
                                else
                                {
                                    for (int i = 0; i < listEntity.Count; i++ )
                                    {
                                        if ((listEntity[i] as EntityPersistence).Id == entityChild.Id)
                                        {
                                            listEntity[i] = entityChild;
                                            break;
                                        }
                                    }
                                }
                        }
                    }
                }
            }

            return listEntity;
        }

        public virtual IList AddChild(EntityPersistence entityChild)
        {
            return operationListChild(entityChild, OperationListChild.Add);
        }

        public virtual IList SetEntityUpdatedInList(EntityPersistence entityChild)
        {
            return operationListChild(entityChild, OperationListChild.Set);
        }

        public virtual IList RemoveChild(EntityPersistence entityChild)
        {
            return operationListChild(entityChild, OperationListChild.Remove);
        }

        public virtual object Clone()
        {
            EntityPersistence entityClone = MemberwiseClone() as EntityPersistence;

            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach(PropertyInfo property in properties)
            {
                if (EntityReflection.Instance.GetAttribute(property, typeof(NotClonablePropertyAttribute)) != null)
                {
                    continue;
                }

                if (property.PropertyType.IsSubclassOf(typeof(EntityPersistence)))
                {
                    PropertyInvisibleAttribute propertyInvisibleAttribute = EntityReflection.Instance.GetAttribute(property, typeof(PropertyInvisibleAttribute)) as PropertyInvisibleAttribute;

                    if (propertyInvisibleAttribute == null)
                    {
                        EntityPersistence entityPropertyValue = property.GetValue(this, null) as EntityPersistence;

                        if (entityPropertyValue != null)
                            property.SetValue(entityClone, entityPropertyValue.Clone(), null);
                    }
                }
                else
                    if (property.PropertyType.Name == "IList")
                    {
                        IList entityPropertyValueList = property.GetValue(this, null) as IList;

                        if (entityPropertyValueList != null)
                        {
                            List<EntityPersistence> listTarget = new List<EntityPersistence>(entityPropertyValueList.Count);

                            property.SetValue(entityClone, listTarget, null);

                            foreach (EntityPersistence objeto in entityPropertyValueList)
                            {
                                listTarget.Add((objeto as EntityPersistence).Clone() as EntityPersistence);
                            }
                        }
                    }
            }

            return entityClone;
        }

        #endregion
    }
}