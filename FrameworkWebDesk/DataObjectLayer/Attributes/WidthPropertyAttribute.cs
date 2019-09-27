using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class WidthPropertyAttribute : Attribute
    {
        public int Width;

        private WidthPropertyAttribute()
        {
        }

        public WidthPropertyAttribute(int width)
        {
            this.Width = width;
        }
    }
}
