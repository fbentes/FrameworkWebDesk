using System;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class EstadoCivilCollection : EntityBaseCollection
    {
        private static EstadoCivilCollection instance = null;

        [Singleton]
        public static EstadoCivilCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new EstadoCivilCollection();

                return instance;
            }
        }

        private EstadoCivilCollection()
        {
            this.Add(new KeyValuePair<object, object>("S", "Solteiro(a)"));
            this.Add(new KeyValuePair<object, object>("C", "Casado(a)"));
            this.Add(new KeyValuePair<object, object>("D", "Divorciado(a)"));
            this.Add(new KeyValuePair<object, object>("V", "Viúvo(a)"));
            this.Add(new KeyValuePair<object, object>("Q", "Desquitado(a)"));
        }
    }
}