using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public static class ListCache<T> where T: EntityPersistence
    {
        public static bool SetEntityInList(T entity, IList<T> listSource)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("O parâmetro entity não pode ser nulo !");
            }

            bool doSet = false;

            for (int i = 0; i < listSource.Count; i++)
            {
                if (entity.Id == listSource[i].Id)
                {
                    listSource[i] = entity;

                    doSet = true;

                    break;
                }
            }

            return doSet;
        }

        public static T GetEntity(object id, IList<T> listSource)
        {
            if (id == null)
            {
                throw new ArgumentNullException("O parâmetro id não pode ser nulo !");
            }

            if (listSource == null)
            {
                throw new ArgumentNullException("O parâmetro listSource não pode ser nulo !");
            }

            for (int i = 0; i < listSource.Count; i++)
            {
                if (id.ToString() == (listSource[i] as T).Id.ToString())
                {
                    return listSource[i];
                }
            }

            return default(T);
        }

        /// <summary>
        /// Exlui uma lista de entities de uma só vez.
        /// </summary>
        public static void Delete(object id, IList<T> listSource)
        {
            T entity = GetEntity(id, listSource);

            Delete(entity, listSource);
        }

        /// <summary>
        /// Exlui uma lista de entities de uma só vez.
        /// </summary>
        public static void Delete(EntityPersistence entity, IList<T> listSource)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("O parâmetro entity não pode ser nulo !");
            }

            if (listSource == null)
            {
                throw new ArgumentNullException("O parâmetro listSource não pode ser nulo !");
            }

            for (int i = 0; i < listSource.Count; i++)
            {
                if (entity.Id == (listSource[i] as EntityPersistence).Id)
                {
                    listSource.Remove(listSource[i]);
                }
            }
        }

    }
}
