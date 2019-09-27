using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;
using System.Web.UI;

namespace DataObjectLayer.View
{
    public class EntityEventArgs: EventArgs
    {
        public bool CancelAction = false;

        public IList ListSource;

        public IListDeleteRegister EntityManager;

        public IEntityPersistence Entity;

        public EntityEventArgs()
        {
        }

        public EntityEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity)
        {
            EntityManager = entityManager;
            Entity = entity;
        }

        public EntityEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity, IList listSource)
            : this(entityManager, entity)
        {
            ListSource = listSource;
        }
    }
}
