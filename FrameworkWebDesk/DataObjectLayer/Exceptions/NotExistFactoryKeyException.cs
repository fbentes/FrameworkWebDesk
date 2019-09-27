using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class NotExistFactoryKeyException : Exception
    {
        const string notExistFactoryKeyMessage = "Este identificador de f�brica de sess�es n�o existe. Tente outro existente !";

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
