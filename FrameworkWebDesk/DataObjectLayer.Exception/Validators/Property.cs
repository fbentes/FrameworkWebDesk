using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Exceptions
{
    public class Property
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private object value;

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Property(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
