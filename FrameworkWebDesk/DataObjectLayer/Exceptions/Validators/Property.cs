using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class Property
    {
        #region Fields

        private object owner;

        private string name;

        private object value;

        #endregion
        
        #region Properties

        public object Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        #endregion

        #region Constructors
        
        public Property(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public Property(object owner, string name, object value)
            : this(name, value)
        {
            this.owner = owner;
        }

        #endregion
    }
}
