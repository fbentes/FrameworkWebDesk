using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace DataObjectLayer
{
    public interface IRegister : IBaseRegister, INewEntity, IPostRegister, IListRegister, IListDeleteRegister
    {
        string[] GetMessagesValidators();

        void RollBackEdit(EntityPersistence entity);
    }
}
