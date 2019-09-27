using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public class FilterEntity
    {
        private string fieldName;

        private FilterCriteria filterCriteria = FilterCriteria.None;

        private object value;

        private FilterOperation filterOperation = FilterOperation.None;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public FilterCriteria FilterCriteria
        {
            get { return filterCriteria; }
            set { filterCriteria = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public FilterOperation FilterOperation
        {
            get { return filterOperation; }
            set { filterOperation = value; }
        }

        public FilterEntity()
        {
        }

        public FilterEntity(FilterOperation filterOperation)
        {
            this.filterOperation = filterOperation;
        }

        public FilterEntity(string fieldName, FilterCriteria filterCriteria, object value)
        {
            this.fieldName = fieldName;
            this.filterCriteria = filterCriteria;
            this.value = value;
        }
    }
}