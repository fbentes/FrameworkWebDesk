using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class NotExistSessionKeyException : Exception
    {
        const string notExistSessionKeyMessage = "Este identificador de sess�o n�o j� existe. Tente outro !";

        public NotExistSessionKeyException()
            : base(notExistSessionKeyMessage)
        {
        }

        public NotExistSessionKeyException(string message)
            : base(message)
        {
        }
    }
}
