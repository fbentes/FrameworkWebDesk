using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class PropertyListException : Exception
    {
        public static List<PropertyListException> propertyListExceptions;

        public static List<PropertyListException> PropertyListExceptions
        {
            set
            {
                propertyListExceptions = value;
            }
        }

        private PropertyException[] propertyExceptions;

        public PropertyException[] PropertyExceptions
        {
            get { return propertyExceptions; }
        }

        public PropertyListException(PropertyException[] propertyExceptions)
        {
            this.propertyExceptions = propertyExceptions;
        }

        public static string MessageUnion
        {
            get
            {
                string messages = string.Empty;

                foreach (PropertyListException propertyListException in propertyListExceptions)
                {
                    foreach (PropertyException propertyException in propertyListException.PropertyExceptions)
                    {
                        if (messages != string.Empty)
                        {
                            messages += "\\n";
                        }

                        messages += propertyException.Message;
                    }
                }

                return messages;
            }
        }

        public override string Message
        {
            get
            {
                string messages = string.Empty;
                
                foreach (PropertyException propertyException in propertyExceptions)
                {
                    if (messages != string.Empty)
                    {
                        messages += "\\n";
                    }

                    messages += propertyException.Message;
                }

                return messages;
            }
        }
    }
}