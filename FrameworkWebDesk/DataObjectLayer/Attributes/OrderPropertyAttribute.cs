using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class OrderPropertyAttribute : Attribute
    {
        public int Order;

        private OrderPropertyAttribute()
        {
        }

        public OrderPropertyAttribute(int order)
        {
            this.Order = order;
        }
    }
}
