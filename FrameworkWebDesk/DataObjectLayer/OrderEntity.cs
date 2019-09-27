using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public class OrderEntity
    {
        private string fieldName;

        private bool ascending;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public bool Ascending
        {
            get { return ascending; }
            set { ascending = value; }
        }

        public OrderEntity()
        {
        }

        public OrderEntity(string fieldName, bool ascending)
        {
            this.fieldName = fieldName;
            this.ascending = ascending;
        }
    }
}