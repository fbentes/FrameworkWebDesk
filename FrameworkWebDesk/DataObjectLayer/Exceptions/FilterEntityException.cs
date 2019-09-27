using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class FilterEntityException : Exception
    {
        public FilterEntityException(string message)
            : base(message)
        {
        }
    }
}
