using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class HQLGeneratorException : Exception
    {
        public HQLGeneratorException(string message)
            : base(message)
        {
        }
    }
}
