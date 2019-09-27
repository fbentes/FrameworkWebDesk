using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class Logradouro
    {
        private string endereco;

        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        private string bairro;

        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }
        private string cidade;

        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private string cep;

        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        public Logradouro()
        {
        }

        public Logradouro(string endereco, string bairro, string cidade, string estado, string cep)
        {
            this.endereco = endereco;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
            this.cep = cep;
        }
    }
}
