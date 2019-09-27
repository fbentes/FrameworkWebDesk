using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace DataObjectLayer
{
    public interface IReadRegister
    {
        IEntityPersistence Read(int id);

        IEntityPersistence Read(int id, IList listSource);
    }
}