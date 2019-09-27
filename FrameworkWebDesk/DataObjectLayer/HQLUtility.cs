using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;

using DataObjectLayer.Reflection;

namespace DataObjectLayer
{
    public class HQLUtility
    {
        private static HQLUtility instance = null;

        private HQLUtility()
        {
        }

        public static HQLUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HQLUtility();
                }

                return instance;
            }
        }

        /// <summary>
        /// Retorna uma string HQL com a cláusula 'in' contendo todos os id's dos entities informados no parâmetro.
        /// </summary>
        /// <param name="entities">Entities a serem incluídos no string HQL de saída.</param>
        /// <returns></returns>
        public string GetQueryEntities(IEntityPersistence[] entities)
        {
            if (entities == null || entities.Length == 0)
            {
                throw new HQLGeneratorException("O parâmetro entities de HQLGenerator.GetQueryEntities deve conter pelo menos um entity na lista !");
            }

            Type entityType = entities[0].GetType();

            StringBuilder strQuery = new StringBuilder();

            strQuery.Append("from " + entityType.Name + " as A ");
            strQuery.Append("where A.Id in (" + entities[0].Id.ToString());

            for(int i = 1; i < entities.Length; i++)
            {
                strQuery.Append(", "+entities[i].Id.ToString());
            }

            strQuery.Append(")");

            return strQuery.ToString();
        }
    }
}
