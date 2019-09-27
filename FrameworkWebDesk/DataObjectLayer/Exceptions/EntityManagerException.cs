using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityManagerException : System.Exception
    {
        public EntityManagerException()
            : base("Só pode haver um entity parent para cada entity child !")
        {
        }

        public EntityManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
