using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class HQLGeneratorException : Exception
    {
        public HQLGeneratorException(string message)
            : base(message)
        {
        }
    }
}
