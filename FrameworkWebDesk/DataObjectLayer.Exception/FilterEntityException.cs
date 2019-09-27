using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class FilterEntityException : Exception
    {
        public FilterEntityException(string message)
            : base(message)
        {
        }
    }
}
