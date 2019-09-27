using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class EntityFieldNotFoundException: Exception
    {
        const string entityFieldNotFoundMessage = "Este identificador de sess�o j� existe. Tente outro !";

        public EntityFieldNotFoundException()
            : base(entityFieldNotFoundMessage)
        {
        }

        public EntityFieldNotFoundException(string message)
            : base(message)
        {
        }
    }
}
