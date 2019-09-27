using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class FormaParcelamentoCollection : EntityBaseCollection
    {
        private static FormaParcelamentoCollection instance = null;

        [Singleton]
        public static FormaParcelamentoCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new FormaParcelamentoCollection();
                
                return instance;
            }
        }

        private FormaParcelamentoCollection()
        {
            this.Add(new KeyValuePair<object, object>(" ", "      "));
            this.Add(new KeyValuePair<object, object>("D", "Dia(s)"));
            this.Add(new KeyValuePair<object, object>("M", "Mes(es)"));
            this.Add(new KeyValuePair<object, object>("A", "Ano(s)"));
        }
    }
}
