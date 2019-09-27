using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class ValidationException : Exception
    {
        const string entityNullMessage = "Valida��o Inv�lida !";

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
