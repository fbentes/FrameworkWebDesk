using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class EntitySaveOrUpdateException : Exception
    {
        const string entitySaveOrUpdateMessage = "Problema ao persistir o entity !";

        public EntitySaveOrUpdateException()
            : base(entitySaveOrUpdateMessage)
        {
        }

        public EntitySaveOrUpdateException(string message)
            : base(message)
        {
        }
    }
}
