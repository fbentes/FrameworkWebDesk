using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class AddEntityChildException : Exception
    {
        public AddEntityChildException()
            : base("Para adicionar um entity child na lista, o m�todo AddChild de seu parent deve ser invocado !")
        {
        }
    }
}
