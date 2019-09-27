using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DataObjectLayer.Business
{
    [Serializable]
    public class UfCollection : EntityBaseCollection
    {
        private static UfCollection instance = null;

        [Singleton]
        public static UfCollection Instance
        {
            get
            {
                if (instance == null)
                    instance = new UfCollection();

                return instance;
            }
        }

        private UfCollection()
        {
            this.Add(new KeyValuePair<object, object>("AC", "Acre"));
            this.Add(new KeyValuePair<object, object>("AL", "Alagoas"));
            this.Add(new KeyValuePair<object, object>("AP", "Amap�"));
            this.Add(new KeyValuePair<object, object>("AM", "Amazonas"));
            this.Add(new KeyValuePair<object, object>("BA", "Bahia"));
            this.Add(new KeyValuePair<object, object>("CE", "Cear�"));
            this.Add(new KeyValuePair<object, object>("DF", "Distrito Federal"));
            this.Add(new KeyValuePair<object, object>("ES", "Esp�rito Santo"));
            this.Add(new KeyValuePair<object, object>("GO", "Goi�s"));
            this.Add(new KeyValuePair<object, object>("MA", "Maranh�o"));
            this.Add(new KeyValuePair<object, object>("MT", "Mato Grosso"));
            this.Add(new KeyValuePair<object, object>("MS", "Mato Grosso do Sul"));
            this.Add(new KeyValuePair<object, object>("MG", "Minas Gerais"));
            this.Add(new KeyValuePair<object, object>("PA", "Par�"));
            this.Add(new KeyValuePair<object, object>("PB", "Para�ba"));
            this.Add(new KeyValuePair<object, object>("PR", "Paran�"));
            this.Add(new KeyValuePair<object, object>("PE", "Pernambuco"));
            this.Add(new KeyValuePair<object, object>("PI", "Piau�"));
            this.Add(new KeyValuePair<object, object>("RJ", "Rio de Janeiro"));
            this.Add(new KeyValuePair<object, object>("RN", "Rio Grande do Norte"));
            this.Add(new KeyValuePair<object, object>("RS", "Rio Grande do Sul"));
            this.Add(new KeyValuePair<object, object>("RO", "Rond�nia"));
            this.Add(new KeyValuePair<object, object>("RR", "Roraima"));
            this.Add(new KeyValuePair<object, object>("SC", "Santa Catarina"));
            this.Add(new KeyValuePair<object, object>("SP", "S�o Paulo"));
            this.Add(new KeyValuePair<object, object>("SE", "Sergipe"));
            this.Add(new KeyValuePair<object, object>("TO", "Tocantins"));
        }
    }
}
