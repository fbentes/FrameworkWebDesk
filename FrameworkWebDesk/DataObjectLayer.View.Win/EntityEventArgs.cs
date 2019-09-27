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
        public EntityPersistence Entity;

        public EntityEventArgs(EntityPersistence entity)
        {
            Entity = entity;
        }
    }
}
