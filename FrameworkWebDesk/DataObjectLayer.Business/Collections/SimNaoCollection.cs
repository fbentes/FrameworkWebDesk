using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class SimNaoCollection : EntityBaseCollection
    {
        private static SimNaoCollection instance = null;

        [Singleton]
        public static SimNaoCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new SimNaoCollection();

                return instance;
            }
        }

        private SimNaoCollection()
        {
            Add(new KeyValuePair<object, object>(true, "Sim"));
            Add(new KeyValuePair<object, object>(false, "Não"));
        }
    }
}
