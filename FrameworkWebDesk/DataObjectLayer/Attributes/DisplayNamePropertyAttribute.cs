using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class DisplayNamePropertyAttribute : Attribute
    {
        public string DisplayName;

        private DisplayNamePropertyAttribute()
        {
        }

        public DisplayNamePropertyAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }
    }
}
