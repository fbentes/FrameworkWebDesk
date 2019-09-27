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
            this.Add(new KeyValuePair<object, object>("AP", "Amapá"));
            this.Add(new KeyValuePair<object, object>("AM", "Amazonas"));
            this.Add(new KeyValuePair<object, object>("BA", "Bahia"));
            this.Add(new KeyValuePair<object, object>("CE", "Ceará"));
            this.Add(new KeyValuePair<object, object>("DF", "Distrito Federal"));
            this.Add(new KeyValuePair<object, object>("ES", "Espírito Santo"));
            this.Add(new KeyValuePair<object, object>("GO", "Goiás"));
            this.Add(new KeyValuePair<object, object>("MA", "Maranhão"));
            this.Add(new KeyValuePair<object, object>("MT", "Mato Grosso"));
            this.Add(new KeyValuePair<object, object>("MS", "Mato Grosso do Sul"));
            this.Add(new KeyValuePair<object, object>("MG", "Minas Gerais"));
            this.Add(new KeyValuePair<object, object>("PA", "Pará"));
            this.Add(new KeyValuePair<object, object>("PB", "Paraíba"));
            this.Add(new KeyValuePair<object, object>("PR", "Paraná"));
            this.Add(new KeyValuePair<object, object>("PE", "Pernambuco"));
            this.Add(new KeyValuePair<object, object>("PI", "Piauí"));
            this.Add(new KeyValuePair<object, object>("RJ", "Rio de Janeiro"));
            this.Add(new KeyValuePair<object, object>("RN", "Rio Grande do Norte"));
            this.Add(new KeyValuePair<object, object>("RS", "Rio Grande do Sul"));
            this.Add(new KeyValuePair<object, object>("RO", "Rondônia"));
            this.Add(new KeyValuePair<object, object>("RR", "Roraima"));
            this.Add(new KeyValuePair<object, object>("SC", "Santa Catarina"));
            this.Add(new KeyValuePair<object, object>("SP", "São Paulo"));
            this.Add(new KeyValuePair<object, object>("SE", "Sergipe"));
            this.Add(new KeyValuePair<object, object>("TO", "Tocantins"));
        }
    }
}
