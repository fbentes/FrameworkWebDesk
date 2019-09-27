using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public class PropertyView
    {
        private string name;
        private string displayName;
        private int length;
        private int position;

        [Category("Entity")]
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;

                if (displayName == string.Empty)
                {
                    displayName = name;
                }
            }
        }

        [Category("Entity")]
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        [Category("Entity")]
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        [Category("Entity")]
        public int Position
        {
            get { return position; }
            set 
            {
                if (position < 0)
                {
                    position = 0;
                }
                else
                {
                    position = value;
                }
           }
        }

        public PropertyView()
        {
            this.name = string.Empty;
            this.displayName = string.Empty;
            this.length = 0;
            this.position = 0;
        }

        public PropertyView(string name, string displayName, int length, int position)
        {
            this.name = name;
            this.displayName = displayName;
            this.length = length;
            this.position = position;
        }
    }
}
