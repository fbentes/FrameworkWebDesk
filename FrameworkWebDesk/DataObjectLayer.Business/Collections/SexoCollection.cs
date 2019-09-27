using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class SexoCollection : EntityBaseCollection
    {
        private static SexoCollection instance = null;

        [Singleton]
        public static SexoCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new SexoCollection();

                return instance;
            }
        }

        private SexoCollection()
        {
            this.Add(new KeyValuePair<object, object>("M", "Masculino"));
            this.Add(new KeyValuePair<object, object>("F", "Feminino"));
        }
    }
}
