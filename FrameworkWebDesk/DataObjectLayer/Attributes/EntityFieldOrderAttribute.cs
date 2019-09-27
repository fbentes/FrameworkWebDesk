using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityPropertyOrderAttribute : Attribute
    {
        public OrderFiled OrderFiled = OrderFiled.Ascending;

        private EntityPropertyOrderAttribute()
        {
        }

        public EntityPropertyOrderAttribute(OrderFiled orderField)
        {
            this.OrderFiled = orderField;
        }
    }
}
