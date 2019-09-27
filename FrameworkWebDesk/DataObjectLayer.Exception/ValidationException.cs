using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class ValidationException : Exception
    {
        const string entityNullMessage = "Validação Inválida !";

        public ValidationException()
            : base(entityNullMessage)
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }
    }
}
