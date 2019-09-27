using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IRegister<T> : IBaseRegister where T : EntityPersistence
    {
        T Entity
        {
            set;
        }

        EntityPersistence GetEntityAsEntityPersistence();

        EntityPersistence EntityMaster
        {
            get;
            set;
        }        
        
        T Read(int codigo);

        void Delete(T[] entities);

        List<T> List();

        List<T> List(bool closeSession);

        List<T> List(string propertyNameOrder, Nullable<bool> ascending);

        void CloseSession();
    }
}
