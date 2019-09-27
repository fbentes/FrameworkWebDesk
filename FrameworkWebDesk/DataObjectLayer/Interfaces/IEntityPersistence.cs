using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IEntityPersistence
    {
        int Id
        {
            set;
            get;
        }

        bool IsNew
        {
            get;
        }

        bool HasChanged
        {
            set;
            get;
        }

        bool IsChild
        {
            set;
            get;
        }
        object Clone();
    }
}
