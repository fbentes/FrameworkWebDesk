using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class NotExistSessionKeyException : Exception
    {
        const string notExistSessionKeyMessage = "Este identificador de sessão não já existe. Tente outro !";

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
