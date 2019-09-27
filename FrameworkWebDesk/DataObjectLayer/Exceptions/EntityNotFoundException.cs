using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityNotFoundException : Exception
    {
        const string entityNotFoundMessage = "Entity n�o encontrato !";

        public EntityNotFoundException()
            : base(entityNotFoundMessage)
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
