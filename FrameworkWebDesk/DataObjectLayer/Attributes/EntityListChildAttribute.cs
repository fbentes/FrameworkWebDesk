using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityListChildAttribute: Attribute
    {
        public Type EntityListDetail;

        private EntityListChildAttribute()
        {
        }

        public EntityListChildAttribute(Type entityListDetail)
        {
            this.EntityListDetail = entityListDetail;
        }
    }
}
