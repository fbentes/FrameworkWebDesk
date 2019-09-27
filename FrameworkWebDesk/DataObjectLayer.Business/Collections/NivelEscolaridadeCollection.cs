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
            this.Add(new KeyValuePair<object, object>("NF", "Nível Fundamental"));
            this.Add(new KeyValuePair<object, object>("NM", "Nível Médio"));
            this.Add(new KeyValuePair<object, object>("NG", "Graduação"));
            this.Add(new KeyValuePair<object, object>("PG", "Pós Graduação"));
            this.Add(new KeyValuePair<object, object>("ME", "Mestrado"));
            this.Add(new KeyValuePair<object, object>("ND", "Doutorado"));
            this.Add(new KeyValuePair<object, object>("PD", "Pós Doutorado"));
        }
    }
}