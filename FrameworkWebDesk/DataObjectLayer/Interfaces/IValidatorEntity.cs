using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace DataObjectLayer
{
    public interface IValidatorEntity<T> where T : EntityPersistence
    {
        void Validate(T entity);
        
        string[] Messages
        {
            get;
        }

    }

}
