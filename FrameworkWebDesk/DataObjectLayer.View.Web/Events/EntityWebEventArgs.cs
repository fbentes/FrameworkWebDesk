using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;
using System.Web.UI;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Web
{
    public class EntityWebEventArgs: EntityEventArgs
    {
        public string PageName = string.Empty;

        public EntityWebEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity, string pageName, IList listSource)
            : base(entityManager, entity, listSource)
        {
            if (pageName != string.Empty)
            {
                if (pageName.ToLower().IndexOf(".aspx") == -1)
                {
                    pageName += pageName + ".aspx";
                }

                PageName = pageName;
            }
        }

        public EntityWebEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity, string pageName)
            : this(entityManager, entity, pageName, null)
        {
        }

        public EntityWebEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity)
            : base(entityManager, entity)
        {
        }

        public EntityWebEventArgs(IListDeleteRegister entityManager, IEntityPersistence entity, IList listSource)
            : base(entityManager, entity, listSource)
        {
        }
    }
}
