using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DataObjectLayer.Reflection;

namespace DataObjectLayer
{
    [Serializable]
    public class ValidatorEntity<T> : IValidatorEntity<T> where T : EntityPersistence
    {
        private static ValidatorEntity<T> instance = null;

        protected List<string> listMessages = new List<string>();

        public static ValidatorEntity<T> Instance
        {
            get
            {
                if (instance == null)
                    instance = new ValidatorEntity<T>();

                return instance;
            }
        }

        public string[] Messages
        {
            get
            {
                return listMessages.ToArray();
            }
        }

        protected ValidatorEntity()
        {
        }

        public virtual void Validate(T entity)
        {
            listMessages.Clear();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach(PropertyInfo property in properties)
            {
                validateNullProperty(entity, property);
            }
        }

        private void validateNullProperty(T entity, PropertyInfo property)
        {
            string displayNameProperty = property.Name;

            DisplayNamePropertyAttribute displayNamePropertyAttribute = EntityReflection.Instance.GetAttribute(property, typeof(DisplayNamePropertyAttribute)) as DisplayNamePropertyAttribute;

            if(displayNamePropertyAttribute != null)
                displayNameProperty = displayNamePropertyAttribute.DisplayName;

            NotNullPropertyAttribute notNullPropertyAttribute = EntityReflection.Instance.GetAttribute(property, typeof(NotNullPropertyAttribute)) as NotNullPropertyAttribute;

            if (notNullPropertyAttribute != null)
            {
                object value = property.GetValue(entity, null);

                if (value == null || value.ToString() == string.Empty)
                    listMessages.Add("O campo \"" + displayNameProperty + "\" não pode ser nulo !");
            }
        }
    }
}
