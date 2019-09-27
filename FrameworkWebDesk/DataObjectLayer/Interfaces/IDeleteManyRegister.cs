using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IDeleteManyRegister
    {
        void Delete(IEntityPersistence[] entities);

        void Delete(IEntityPersistence[] entities, IList listSource);
    }
}
