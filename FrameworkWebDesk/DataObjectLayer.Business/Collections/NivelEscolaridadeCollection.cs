using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class NivelEscolaridadeCollection : EntityBaseCollection
    {
        private static NivelEscolaridadeCollection instance = null;

        [Singleton]
        public static NivelEscolaridadeCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new NivelEscolaridadeCollection();

                return instance;
            }
        }

        private NivelEscolaridadeCollection()
        {
            this.Add(new KeyValuePair<object, object>("NF", "N�vel Fundamental"));
            this.Add(new KeyValuePair<object, object>("NM", "N�vel M�dio"));
            this.Add(new KeyValuePair<object, object>("NG", "Gradua��o"));
            this.Add(new KeyValuePair<object, object>("PG", "P�s Gradua��o"));
            this.Add(new KeyValuePair<object, object>("ME", "Mestrado"));
            this.Add(new KeyValuePair<object, object>("ND", "Doutorado"));
            this.Add(new KeyValuePair<object, object>("PD", "P�s Doutorado"));
        }
    }
}