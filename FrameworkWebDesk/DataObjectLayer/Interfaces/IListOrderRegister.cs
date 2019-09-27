using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IListOrderRegister
    {
        IList ListWithOrder(string propertyNameOrder, Nullable<bool> ascending);
    }
}
