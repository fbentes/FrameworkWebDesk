using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IListFilterRegister : IReadRegister
    {
        IList List(OrderEntity[] orders, ListFilterEntity filterEntities);
    }
}
