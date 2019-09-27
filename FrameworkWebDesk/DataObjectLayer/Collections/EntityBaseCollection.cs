using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public class EntityBaseCollection: List<KeyValuePair<object, object>>
    {
        public string GetValue(object key)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (key.ToString() == this[i].Key.ToString())
                {
                    return this[i].Value.ToString();
                }
            }

            return string.Empty;
        }
    }
}
