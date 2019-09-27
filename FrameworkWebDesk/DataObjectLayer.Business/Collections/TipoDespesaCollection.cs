using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class TipoDespesaCollection : EntityBaseCollection
    {
        private static TipoDespesaCollection instance = null;

        [Singleton]
        public static TipoDespesaCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new TipoDespesaCollection();

                return instance;
            }
        }

        private TipoDespesaCollection()
        {
            this.Add(new KeyValuePair<object,object>("F", "Fixa"));
            this.Add(new KeyValuePair<object, object>("V", "Variável"));
        }
    }
}
