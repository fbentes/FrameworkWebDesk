using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityParentException : System.Exception
    {
        public EntityParentException()
            : base("Só pode haver um entity parent para cada entity child !")
        {
        }

        public EntityParentException(string message)
            : base(message)
        {
        }

        public EntityParentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
