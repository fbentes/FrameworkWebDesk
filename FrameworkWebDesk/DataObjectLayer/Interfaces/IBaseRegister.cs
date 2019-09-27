using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IBaseRegister
    {
        IEntityPersistence Entity
        {
            set;
            get;
        }
    }
}
