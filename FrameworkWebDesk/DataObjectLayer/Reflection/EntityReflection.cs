using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;


namespace DataObjectLayer.Reflection
{
    public class EntityReflection
    {
        private static EntityReflection instance;

        public static EntityReflection Instance
        {
            get
            {
                if (instance == null)
                    instance = new EntityReflection();

                return instance;
            }
        }

        private EntityReflection()
        {
        }

        public EntityPersistence GetNewEntity(Type entityType)
        {
            ConstructorInfo construtor = entityType.GetConstructor(Type.EmptyTypes);

            if (construtor == null)
            {
                throw new EntityNoConstructorException();
            }

            return construtor.Invoke(null) as EntityPersistence;
        }

        public PropertyInfo GetFirstProperty(Type type, Type attribute)
        {
            PropertyInfo[] properties = Properties(type, attribute, true);

            if (properties.Length == 0)
            {
                return null;
            }

            return properties[0];
        }

        public PropertyInfo[] Properties(Type type, Type attribute)
        {
            return Properties(type, attribute, false);
        }

        public PropertyInfo[] Properties(Type type, Type attribute, bool breakWhenFound)
        {
            List<PropertyInfo> propertiesInfo = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if(GetAttribute(propertyInfo, attribute) != null)
                {
                    propertiesInfo.Add(propertyInfo);

                    if (breakWhenFound)
                    {
                        break;
                    }
                }
            }

            return propertiesInfo.ToArray();
        }

        public List<Attribute> GetAttributes(PropertyInfo property)
        {
            List<Attribute> listAttribute = new List<Attribute>();

            foreach (Attribute attributeInfo in Attribute.GetCustomAttributes(property))
            {
                listAttribute.Add(attributeInfo);
            }

            return listAttribute;
        }

        public Attribute GetAttribute(PropertyInfo property, Type attribute)
        {
            List<Attribute> listAttribute = GetAttributes(property);

            foreach (Attribute attributeInfo in listAttribute)
            {
                if (attributeInfo.GetType() == attribute)
                    return attributeInfo;
            }

            return null;
        }

        public Type GetTypeEntityChild(PropertyInfo property)
        {
            foreach (Attribute attributeInfo in Attribute.GetCustomAttributes(property))
            {
                if (attributeInfo.GetType() == typeof(EntityListChildAttribute))
                {
                    return (attributeInfo as EntityListChildAttribute).EntityListDetail;
                }
            }

            return Type.Missing as Type;
        }

        public Type GetTypeEntity(string typeName)
        {
            return null;
        }

        public bool IsEntityChild(Type entityType)
        {
            if (!entityType.IsSubclassOf(typeof(EntityPersistence)))
            {
                throw new ArgumentException("O parâmetro entityType deve ser uma classe herdada de EntityPersistence na chamada do método IsEntityChild() !");
            }

            EntityChildAttribute entityChildAttribute = null;

            foreach (Attribute attribute in entityType.GetCustomAttributes(true))
            {
                entityChildAttribute = attribute as EntityChildAttribute;

                if (entityChildAttribute != null)
                    return true;
            }

            return entityChildAttribute != null;
        }

        /// <summary>
        /// Retorna o valor a propriedade do entity.
        /// </summary>
        /// <param name="entity">Instância do entity.</param>
        /// <param name="propertyName">Nome da propriedade do entity que contém o valor.</param>
        /// <returns></returns>
        public object GetValueProperty(IEntityPersistence entity, string propertyName)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();

            object value = null;

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == propertyName)
                {
                    value = property.GetValue(entity, null);

                    break;
                }
            }

            return value;
        }

        public Type GetTypeProperty(IEntityPersistence entity, string propertyName)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();

            Type type = typeof(object);

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == propertyName)
                {
                    type = property.PropertyType;

                    break;
                }
            }

            return type;
        }

        public PropertyInfo GetPropertyInfo(IEntityPersistence entity, string propertyName)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == propertyName)
                {
                    return property;
                }
            }

            return null;
        }

        public void SetValuePropertyEntity(IEntityPersistence entity, string propertyName, object value)
        {
            if (propertyName == string.Empty)
            {
                return;
            }

            PropertyInfo propertyInfo = GetPropertyInfo(entity, propertyName);

            if (propertyInfo == null)
            {
                return;
            }

            Type typeProperty = propertyInfo.PropertyType;

            if (typeProperty.Name.ToLower() == "nullable`1" && value != null)
            {
                if (value.ToString().Trim() != string.Empty)
                {
                    typeProperty = Nullable.GetUnderlyingType(typeProperty);
                }
                else
                    value = null;
            }

            try
            {
                switch (typeProperty.Name.ToLower())
                {
                    case "string": propertyInfo.SetValue(entity, (value != null ? value.ToString() : string.Empty), null); break;
                    case "int16":
                    case "int32":
                    case "int64": propertyInfo.SetValue(entity, Convert.ToInt32(value), null); break;
                    case "boolean": propertyInfo.SetValue(entity, Convert.ToBoolean(value), null); break;
                    case "datetime": propertyInfo.SetValue(entity, Convert.ToDateTime(value), null); break;
                    case "decimal": propertyInfo.SetValue(entity, Convert.ToDecimal(value), null); break;
                    default: propertyInfo.SetValue(entity, value, null); break;
                }
            }
            catch (TargetInvocationException E)
            {
                if (E.InnerException is PropertyException)
                {
                    throw new PropertyException(((PropertyException)E.InnerException).Property, E.InnerException.Message);
                }
            }
            catch (PropertyException E)
            {
                throw new PropertyException(E.Message);
            }
            catch (FormatException E)
            {
                string displayProperty = string.Empty;

                DisplayNamePropertyAttribute attributo = GetAttribute(propertyInfo, typeof(DisplayNamePropertyAttribute)) as DisplayNamePropertyAttribute;

                if (attributo != null)
                {
                    displayProperty = attributo.DisplayName;
                }
                else
                {
                    displayProperty = propertyInfo.Name;
                }

                throw new PropertyException(new Property(propertyInfo.Name, value), "O campo " + displayProperty + " está com valor inválido !", E);
            }
        }

        public Type GetType(string assemblyName, string typeName)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            foreach (Type type in assembly.GetTypes())
            {
                if (type.Name == typeName)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
