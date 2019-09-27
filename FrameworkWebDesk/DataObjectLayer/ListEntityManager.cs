using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

using NHibernate;
using NHibernate.Expression;

namespace DataObjectLayer
{
    public class ListEntityManager<T> where T: EntityPersistence
    {
        private static ListEntityManager<T> instance;

        public ISession Session;

        public static ListEntityManager<T> Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListEntityManager<T>();

                return instance;
            }
        }

        private ListEntityManager()
        {
        }

        private object[] objectValues(Array values)
        {
            List<object> retValues = new List<object>();

            foreach (object value in values)
            {
                retValues.Add(value);
            }

            return retValues.ToArray();
        }

        private void validatorFilterEntity(FilterEntity filterEntity)
        {
            if (filterEntity.FilterCriteria != FilterCriteria.Equal && filterEntity.FilterCriteria != FilterCriteria.Different)
            {
                if (filterEntity.Value == null)
                    throw new FilterEntityException("A propriedade Value não pode ser null para FilterEntity !");
            }

            if (filterEntity.FilterCriteria == FilterCriteria.Between || filterEntity.FilterCriteria == FilterCriteria.In || filterEntity.FilterCriteria == FilterCriteria.NotIn)
            {
                if (filterEntity.Value.GetType().ToString().IndexOf("[]") == -1)
                    throw new FilterEntityException("O tipo da propriedade Value do FilterEntity deve ser um Array para FilterCriteria.Between e FilterCriteria.In !");

                filterEntity.Value =(object[])objectValues(filterEntity.Value as Array);

                if (((object[])filterEntity.Value).Length == 0)
                    throw new FilterEntityException("A propriedade Value do FilterEntity deve ter pelo menos um item !");

                if (filterEntity.FilterCriteria == FilterCriteria.Between && ((object[])filterEntity.Value).Length < 2)
                {
                    throw new FilterEntityException("A propriedade Value do FilterEntity para uso com FilterCriteria.Between deve ser um Array com dois itens !");
                }
            }
        }

        private ICriterion criterionEntity(FilterEntity filterEntity)
        {
            if (filterEntity.FilterCriteria !=  FilterCriteria.None)
            {
                validatorFilterEntity(filterEntity);

                switch (filterEntity.FilterCriteria)
                {
                    case FilterCriteria.Equal: if (filterEntity.Value == null)
                                                {
                                                    return Expression.IsNull(filterEntity.FieldName);
                                                }
                                               return Expression.Eq(filterEntity.FieldName, filterEntity.Value);
                    case FilterCriteria.Greater: return Expression.Gt(filterEntity.FieldName, filterEntity.Value);
                    case FilterCriteria.GreaterOrEqual: return Expression.Ge(filterEntity.FieldName, filterEntity.Value);
                    case FilterCriteria.Smaller: return Expression.Lt(filterEntity.FieldName, filterEntity.Value);
                    case FilterCriteria.SmallerOrEqual: return Expression.Le(filterEntity.FieldName, filterEntity.Value);
                    case FilterCriteria.Different: if (filterEntity.Value == null)
                                                    {
                                                        return Expression.IsNotNull(filterEntity.FieldName);
                                                    } 
                                                    return Expression.Not(Expression.Eq(filterEntity.FieldName, filterEntity.Value));
                    case FilterCriteria.In: return Expression.In(filterEntity.FieldName, (object[])filterEntity.Value);
                    case FilterCriteria.NotIn: return Expression.Not(Expression.In(filterEntity.FieldName, (object[])filterEntity.Value));
                    case FilterCriteria.Between: return Expression.Between(filterEntity.FieldName, ((object[])filterEntity.Value)[0], ((object[])filterEntity.Value)[1]);
                    case FilterCriteria.StartLike: return Expression.Like(filterEntity.FieldName, filterEntity.Value.ToString(), MatchMode.Start);
                    case FilterCriteria.EndLike: return Expression.Like(filterEntity.FieldName, filterEntity.Value.ToString(), MatchMode.End);
                    case FilterCriteria.AllLike: return Expression.Like(filterEntity.FieldName, filterEntity.Value.ToString(), MatchMode.Anywhere);
                    default: return null;
                }
            }

            return null;
        }

        private void addCriterias(ref ICriteria criteria, ListFilterEntity filterEntities)
        {
            if (filterEntities != null && filterEntities.Count > 0)
            {
                if (!filterEntities.HasOperation)
                {
                    ICriterion criterion;

                    for (int i = 0; i < filterEntities.Count; i++)
                    {
                        criterion = criterionEntity(filterEntities[i]);

                        criteria.Add(criterion);
                    }
                }
                else
                {
                    ICriterion leftCriterion, rightCriterion;

                    for (int i = 1; i < filterEntities.Count - 1; i += 2)
                    {
                        leftCriterion = criterionEntity(filterEntities[i - 1]);
                        rightCriterion = criterionEntity(filterEntities[i + 1]);

                        switch (filterEntities[i].FilterOperation)
                        {
                            case FilterOperation.And: criteria.Add(Expression.And(leftCriterion, rightCriterion)); break;
                            case FilterOperation.Or: criteria.Add(Expression.Or(leftCriterion, rightCriterion)); break;
                            default: throw new OrderOperationInvalidException();
                        }
                    }
                }
            }
        }

        private void addOrder(ref ICriteria criteria, OrderEntity[] orders)
        {
            if (orders != null && orders.Length > 0)
            {
                foreach (OrderEntity order in orders)
                {
                    criteria.AddOrder(new Order(order.FieldName, order.Ascending));
                }
            }
        }

        public List<T> List(OrderEntity[] orders, ListFilterEntity filterCriterias)
        {
            List<T> resultList = new List<T>();

            try
            {
                if (Session == null || !Session.IsOpen)
                    Session = NHibernateManager.Instance.GetSession();

                ICriteria criteria = Session.CreateCriteria(typeof(T));

                addCriterias(ref criteria, filterCriterias);

                addOrder(ref criteria, orders);

                criteria.List(resultList);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession();
            }

            return resultList;
        }

        private void setParamns(ref IQuery query, IDictionary<string, object> listParams)
        {
            foreach (KeyValuePair<string, object> item in listParams)
            {
                if(item.Value != null)
                    query.SetParameter(item.Key, item.Value);
                else
                    query.SetParameter(item.Key, item.Value, NHibernateUtil.Class);
            }
        }

        public IList List(string queryString)
        {
            return List(queryString, null);
        }

        public IList List(string queryString, IDictionary<string, object> listParams)
        {
            IList resultList = new ArrayList();

            try
            {
                if (Session == null || !Session.IsOpen)
                    Session = NHibernateManager.Instance.GetSession();

                IQuery query;

                if (queryString.IndexOf('{') == -1)
                {
                    query = Session.CreateQuery(queryString);
                }
                else
                {
                    query = Session.CreateSQLQuery(queryString);
                }

                if (listParams != null)
                    setParamns(ref query, listParams);

                query.List(resultList);
            }
            finally
            {
                NHibernateManager.Instance.CloseSession();
            }

            return resultList;
        }
    }
}