using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataObjectLayer;

namespace DataObjectLayer
{
	public class KeyValue
	{
		#region private fields

		private object key;
        private object value;

        #endregion private fields

        #region constructor

        /// <summary>
		/// Construtor default.
		/// </summary>
        public KeyValue():this(null, null)
		{
        }

        public KeyValue(object key, object value)
        {
            this.key = key;
            this.value = value;
        }

        #endregion constructor

        #region Properties

        public object Key
        {
            get { return key; }
            set { key = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion Properties
    }
}
