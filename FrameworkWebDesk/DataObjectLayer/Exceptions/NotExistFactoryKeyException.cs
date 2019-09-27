using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class NotExistFactoryKeyException : Exception
    {
        const string notExistFactoryKeyMessage = "Este identificador de fábrica de sessões não existe. Tente outro existente !";

        public NotExistFactoryKeyException()
            : base(notExistFactoryKeyMessage)
        {
        }

        public NotExistFactoryKeyException(string message)
            : base(message)
        {
        }
    }
}
