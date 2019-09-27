using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class RemoveEntityChildException : Exception
    {
        public RemoveEntityChildException()
            : base("Para remover um entity child da lista, o m�todo RemoveChild de seu parent deve ser invocado !")
        {
        }
    }
}
