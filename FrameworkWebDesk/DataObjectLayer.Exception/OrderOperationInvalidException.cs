using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class OrderOperationInvalidException : Exception
    {
        const string message = "A ordem informada do operador lógico AND ou OR está incorreta !";

        public OrderOperationInvalidException()
            : base(message)
        {
        }

        public OrderOperationInvalidException(string message)
            : base(message)
        {
        }
    }
}
