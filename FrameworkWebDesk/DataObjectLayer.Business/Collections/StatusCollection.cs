using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class StatusCollection : EntityBaseCollection
    {
        private static StatusCollection instance = null;

        [Singleton]
        public static StatusCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new StatusCollection();

                return instance;
            }
        }

        private StatusCollection()
        {
            this.Add(new KeyValuePair<object, object>("A", "Ativo"));
            this.Add(new KeyValuePair<object, object>("I", "Inativo"));
        }
    }
}
