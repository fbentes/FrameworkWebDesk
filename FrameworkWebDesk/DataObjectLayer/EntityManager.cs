using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Expression;

using DataObjectLayer.Reflection;

namespace DataObjectLayer
{
    [Serializable]
    public class EntityManager<T> : IRegister where T : EntityPersistence
    {
        #region Fields

        protected static EntityManager<T> instance;

        private ISession session;

        protected ITransaction transaction;

        protected IValidatorEntity<T> validatorEntity;

        private EntityPersistence entity;

        private EntityPersistence entityClone;

        #endregion

        #region Properties

        public EntityPersistence EntityClone
        {
            get { return entityClone; }
            set { entityClone = value; }
        }
        
        /// <summary>
        /// Singleton do EntityManager.
        /// </summary>
        [Singleton]
        public static EntityManager<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager<T>();
                }

                return instance;
            }
        }

        /// <summary>
        /// Entity corrente do EntityManager.
        /// </summary>
        public virtual IEntityPersistence Entity
        {
            get { return entity; }
            set 
            { 
                entity = value as EntityPersistence;

                if (!entity.IsNew)
                {
                    entityClone = entity.Clone() as EntityPersistence;
                }
            }
        }

        #endregion

        #region Constructor

        protected EntityManager()
        {
            validatorEntity = ValidatorEntity<T>.Instance;
        }

        #endregion

        #region Public Methods

        public virtual void SetNewSession()
        {
            session = NHibernateManager.Instance.GetSession(typeof(T).Name);
        }

        public virtual void CreateNewEntity()
        {
            entity = EntityReflection.Instance.GetNewEntity(typeof(T)) as T;

            entityClone = entity.Clone() as EntityPersistence;
        }

        public virtual string[] GetMessagesValidators()
        {
            return validatorEntity.Messages;
        }

        public void RollBackEdit(EntityPersistence entity)
        {
            RollBackEdit(entityClone, entity);
        }

        public void RollBackEdit(EntityPersistence entityClone, EntityPersistence entity)
        {
            if (entityClone == null)
                throw new ArgumentNullException("RollBack() não funciona com a variável entityClone == null !");

            if (entityClone.Id != entity.Id)
            {
                return;
            }

            PropertyInfo[] properties = entity.GetType().GetProperties();

            object sourceValue;

            MethodInfo setAccessor;

            foreach (PropertyInfo property in properties)
            {
                setAccessor = property.GetSetMethod();

                if(setAccessor != null)
                {
                    if (property.PropertyType.Name != "IList")
                    {
                        EntityParentAttribute entityParentAttribute = EntityReflection.Instance.GetAttribute(property, typeof(EntityParentAttribute)) as EntityParentAttribute;

                        if (entityParentAttribute == null)
                        {
                            sourceValue = property.GetValue(entityClone, null);

                            property.SetValue(entity, sourceValue, null);
                        }
                    }
                    else
                    {
                        IList entityClonePropertyValueList = property.GetValue(entityClone, null) as IList;

                        IList entityPropertyValueList = property.GetValue(entity, null) as IList;

                        if (entityClonePropertyValueList.Count == 0)
                            entityPropertyValueList.Clear();
                        else
                        {

                            // Se foi adicionado um item novo, então remova-o !

                            Array tempEntityPropertyValueList = System.Array.CreateInstance(typeof(EntityPersistence), entityPropertyValueList.Count);

                            entityPropertyValueList.CopyTo(tempEntityPropertyValueList, 0);

                            foreach (EntityPersistence entityChild in tempEntityPropertyValueList)
                            {
                                if (entityChild.Id == -2)
                                {
                                    entityPropertyValueList.Remove(entityChild);
                                }
                            }

                            // Se foi alterado algum item da lista, então retorna os valores anteriores
                            // Se foi excluído algum item, então readiciona este item
                            
                            bool execRollBack;

                            foreach (EntityPersistence entityCloneChild in entityClonePropertyValueList)
                            {
                                execRollBack = false;

                                foreach (EntityPersistence entityChild in entityPropertyValueList)
                                {
                                    if (entityCloneChild.Id == entityChild.Id)
                                    {
                                        RollBackEdit(entityCloneChild, entityChild);

                                        execRollBack = true;

                                        break;
                                    }
                                }

                                if (!execRollBack)
                                {
                                    entityPropertyValueList.Add(entityCloneChild);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region ICRUD Members

        /// <summary>
        /// Persiste Entity corrente.
        /// </summary>
        public virtual void Save()
        {
            save(entity);
        }

        protected virtual void save(EntityPersistence entity)
        {
            session.SaveOrUpdate(entity);
        }

        /// <summary>
        ///  Insere um novo Entity no banco se o seu Id for igual a -1, caso contrário só altera.
        /// </summary>
        public virtual void Post()
        {
            if (entity == null)
            {
                throw new OrderOperationInvalidException();
            }

            validatorEntity.Validate(entity as T);

            if (validatorEntity.Messages.Length > 0)
                return;

            if (entity.IsChild)
            {
                return;
            }

            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                transaction = session.BeginTransaction();

                bool refreshSession = entity.IsNew;

                Save();

                transaction.Commit();

                if (refreshSession)
                    session.Refresh(entity);
            }
            catch (Exception E)
            {
                transaction.Rollback();

                throw new EntitySaveOrUpdateException(E.Message);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }
        }

        /// Retorna um List de entities.
        /// </summary>
        /// <returns></returns>
        public virtual IList List()
        {
            OrderEntity orderEntity = getOrderEntity();

            if (orderEntity != null)
                return ListWithOrder(orderEntity.FieldName, orderEntity.Ascending);
            else
                return List(null, null);
        }

        private OrderEntity getOrderEntity()
        {
            PropertyInfo[] properties = EntityReflection.Instance.Properties(typeof(T), typeof(EntityPropertyOrderAttribute));

            if (properties.Length > 0)
            {
                bool ascending = true;

                foreach (Attribute attribute in Attribute.GetCustomAttributes(properties[0]))
                {
                    if (attribute.GetType() == typeof(EntityPropertyOrderAttribute))
                    {
                        ascending = (attribute as EntityPropertyOrderAttribute).OrderFiled == OrderFiled.Ascending;

                        break;
                    }
                }

                return new OrderEntity(properties[0].Name, ascending);
            }

            return null;
        }

        /// <summary>
        /// O mesmo que List(), incluindo o campo a ser ordenado e a ordem deste campo.
        /// </summary>
        /// <param name="propertyNameOrder">Nome do campo mapeado do entity.</param>
        /// <param name="ascending">True se for ascendente e False, caso seja descendente.</param>
        /// <returns></returns>
        public IList ListWithOrder(string propertyNameOrder, Nullable<bool> ascending)
        {
            return ListEntityManager<T>.Instance.List(new OrderEntity[] { new OrderEntity(propertyNameOrder, ascending.Value) }, null);
        }

        /// <summary>
        /// O mesmo que List(), incluindo um array do tipo NHibernate.Expression.Order para ordenação da lista.
        /// </summary>
        /// <param name="orders">Array com instâncias de Order.</param>
        /// <returns></returns>
        public virtual IList List(OrderEntity[] orders, ListFilterEntity filterEntities)
        {
            return ListEntityManager<T>.Instance.List(orders, filterEntities);
        }

        /// <summary>
        /// Retorna uma lista de entities executados pelo HQL.
        /// </summary>
        /// <param name="queryString">HQL string.</param>
        /// <returns></returns>
        public List<T> ListEntities(string queryString)
        {
            List<T> resultList = new List<T>();

            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                IQuery query = session.CreateQuery(queryString);

                query.List(resultList);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }

            return resultList;
        }

        /// <summary>
        /// Retorna uma lista de entities executados pelo HQL.
        /// </summary>
        /// <param name="queryString">HQL string.</param>
        /// <returns></returns>
        public IList ListQuery(string queryString)
        {
            return ListEntityManager<T>.Instance.List(queryString);
        }

        /// <summary>
        /// Retorna uma lista de entities executados pelo HQL com um determinado critério.
        /// </summary>
        /// <param name="queryString">HQL string.</param>
        /// <param name="listParams">Lista do par(propriedade,valor) para o HQL.</param>
        /// <returns></returns>
        public IList ListQuery(string queryString, IDictionary<string, object> listParams)
        {
            return ListEntityManager<T>.Instance.List(queryString, listParams);
        }

        /// <summary>
        /// Retorna um Entity baseado em seu Id.
        /// </summary>
        /// <param name="id">Id do entity mapeado.</param>
        /// <returns></returns>
        public virtual IEntityPersistence Read(int id)
        {
            T entity = default(T);

            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                entity = session.Get<T>(id);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }

            return entity;
        }

        /// <summary>
        /// Retorna um Entity baseado em seu Id na lista especificada.
        /// </summary>
        /// <param name="id">Id do entity mapeado.</param>
        /// <returns></returns>
        public virtual IEntityPersistence Read(int id, IList listSource)
        {
            if (listSource == null)
            {
                return Read(id);
            }

            foreach (object entityCurrent in listSource)
            {
                if ((entityCurrent as IEntityPersistence).Id == id)
                {
                    return (entityCurrent as EntityPersistence);
                }
            }

            return null;
        }

        /// <summary>
        /// Exlui uma lista de entities de uma só vez.
        /// </summary>
        public virtual void Delete(IEntityPersistence[] entities)
        {
            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                transaction = session.BeginTransaction();

                string queryEntities;

                try
                {
                    queryEntities = HQLUtility.Instance.GetQueryEntities(entities);
                }
                catch (HQLGeneratorException E)
                {
                    throw new HQLGeneratorException(E.Message);
                }

                session.Delete(queryEntities);

                transaction.Commit();
            }
            catch (HQLGeneratorException E)
            {
                transaction.Rollback();

                throw new HQLGeneratorException(E.Message);
            }
            catch (Exception  E)
            {
                transaction.Rollback();

                throw new Exception(E.Message);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }
        }

        /// <summary>
        /// Exlui uma lista de entities de uma só vez.
        /// </summary>
        public virtual void Delete(IEntityPersistence[] entities, IList listSource)
        {
            if (listSource == null)
            {
                Delete(entities);

                return;
            }

            foreach (EntityPersistence entity in entities)
            {
                for (int i = 0; i < listSource.Count; i++)
                {
                    if (entity.Id == (listSource[i] as EntityPersistence).Id)
                    {
                        listSource.Remove(listSource[i]);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Exlui o Entity corrente.
        /// </summary>
        public virtual void Delete()
        {
            if (entity == null)
            {
                throw new OrderOperationInvalidException();
            }

            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                transaction = session.BeginTransaction();

                session.Delete(entity);

                transaction.Commit();
            }
            catch (Exception E)
            {
                transaction.Rollback();

                throw new Exception(E.Message);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }
        }

        /// <summary>
        /// Exclui um Entity baseado em seu Id.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            T entity = default(T);

            try
            {
                if (session == null || !session.IsOpen)
                    session = NHibernateManager.Instance.GetSession(typeof(T).Name);

                transaction = session.BeginTransaction();

                entity = session.Get<T>(id);

                if (entity == null)
                {
                    throw new EntityNotFoundException();
                }

                session.Delete(entity);

                transaction.Commit();
            }
            catch (Exception E)
            {
                transaction.Rollback();

                throw new Exception(E.Message);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession(typeof(T).Name);
            }
        }

        #endregion

    }
}
