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
            this.Add(new KeyValuePair<object, object>("AL", "Alemão"));
            this.Add(new KeyValuePair<object, object>("CH", "Chinês"));
            this.Add(new KeyValuePair<object, object>("ES", "Espanhol"));
            this.Add(new KeyValuePair<object, object>("FR", "Francês"));
            this.Add(new KeyValuePair<object, object>("IN", "Inglês"));
            this.Add(new KeyValuePair<object, object>("IT", "Italiano"));
            this.Add(new KeyValuePair<object, object>("JA", "Japonês"));
            this.Add(new KeyValuePair<object, object>("NO", "Norueguês"));
            this.Add(new KeyValuePair<object, object>("PO", "Português"));
            this.Add(new KeyValuePair<object, object>("RU", "Russo"));
        }
    }
}