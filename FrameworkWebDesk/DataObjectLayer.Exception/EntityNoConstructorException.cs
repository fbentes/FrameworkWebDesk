using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class EntityNoConstructorException : System.Exception
    {
        public EntityNoConstructorException(string message): base(message)
        {
        }

        public EntityNoConstructorException(): this("Entity sem construtor definido")
        {
        }
    }
}
