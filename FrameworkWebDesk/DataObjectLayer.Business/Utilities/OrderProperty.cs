using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class OrderProperty
    {
        private string propertyName;

        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }
        private bool asc;

        public bool Asc
        {
            get { return asc; }
            set { asc = value; }
        }

        public OrderProperty(string propertyName, bool asc)
        {
            this.propertyName = propertyName;
            this.asc = asc;
        }
    }
}
