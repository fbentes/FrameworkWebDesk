using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityNullException : Exception
    {
        const string entityNullMessage = "Entidade nula encontrada !";

        public EntityNullException()
            : base(entityNullMessage)
        {
        }

        public EntityNullException(string message)
            : base(message)
        {
        }
    }
}
