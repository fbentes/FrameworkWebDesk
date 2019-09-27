using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class OrderOperationInvalidException : Exception
    {
        const string message = "A ordem informada do operador l�gico AND ou OR est� incorreta !";

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
