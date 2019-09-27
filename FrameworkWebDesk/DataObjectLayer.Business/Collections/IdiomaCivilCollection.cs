using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class IdiomaCollection : EntityBaseCollection
    {
        private static IdiomaCollection instance = null;

        [Singleton]
        public static IdiomaCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new IdiomaCollection();

                return instance;
            }
        }

        private IdiomaCollection()
        {
            this.Add(new KeyValuePair<object, object>("AL", "Alem�o"));
            this.Add(new KeyValuePair<object, object>("CH", "Chin�s"));
            this.Add(new KeyValuePair<object, object>("ES", "Espanhol"));
            this.Add(new KeyValuePair<object, object>("FR", "Franc�s"));
            this.Add(new KeyValuePair<object, object>("IN", "Ingl�s"));
            this.Add(new KeyValuePair<object, object>("IT", "Italiano"));
            this.Add(new KeyValuePair<object, object>("JA", "Japon�s"));
            this.Add(new KeyValuePair<object, object>("NO", "Noruegu�s"));
            this.Add(new KeyValuePair<object, object>("PO", "Portugu�s"));
            this.Add(new KeyValuePair<object, object>("RU", "Russo"));
        }
    }
}