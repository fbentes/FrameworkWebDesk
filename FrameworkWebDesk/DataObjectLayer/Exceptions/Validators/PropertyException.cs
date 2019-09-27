using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class PropertyException : Exception
    {
        private Property property;

        public Property Property
        {
            get
            {
                return property;
            }
        }

        public PropertyException(string message)
            : base(message)
        {
        }

        public PropertyException(Property property, string message)
            : base(message)
        {
            this.property = property;
        }

        public PropertyException(Property property, string message, Exception innerException)
            : base(message, innerException)
        {
            this.property = property;
        }
    }
}