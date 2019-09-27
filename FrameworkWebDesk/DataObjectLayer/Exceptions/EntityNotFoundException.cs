using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityNotFoundException : Exception
    {
        const string entityNotFoundMessage = "Entity não encontrato !";

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
