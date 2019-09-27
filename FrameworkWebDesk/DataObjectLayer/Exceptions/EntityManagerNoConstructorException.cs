using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class EntityManagerNoConstructorException : Exception
    {
        private const string message = "EntityManager sem construtor definido !";

        public EntityManagerNoConstructorException()
            : base(message)
        {
        }

        public EntityManagerNoConstructorException(Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityManagerNoConstructorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
