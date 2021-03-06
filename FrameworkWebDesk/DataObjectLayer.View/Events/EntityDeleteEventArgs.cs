using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;
using System.Web.UI;

namespace DataObjectLayer.View
{
    public class EntityDeleteEventArgs: EventArgs
    {
        #region Fields

        private bool cancelAction = false;
        private IList listSource;
        private IDeleteRegister entityManager;
        private IEntityPersistence entity;

        #endregion

        #region Properties

        public bool CancelAction
        {
            get { return cancelAction; }
            set { cancelAction = value; }
        }

        public IList ListSource
        {
            get { return listSource; }
            set { listSource = value; }
        }

        public IDeleteRegister EntityManager
        {
            get { return entityManager; }
            set { entityManager = value; }
        }

        public IEntityPersistence Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        #endregion

        #region Constructors

        public EntityDeleteEventArgs()
        {
        }

        public EntityDeleteEventArgs(IDeleteRegister entityManager, IEntityPersistence entity, IList listSource)
        {
            this.entityManager = entityManager;

            this.entity = entity;
            
            this.listSource = listSource;
        }
        #endregion

    }
}
