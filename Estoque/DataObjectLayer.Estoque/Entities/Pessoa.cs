using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataObjectLayer;
using DataObjectLayer.Business;

namespace DataObjectLayer.Estoque
{
	[Serializable]
	public class Pessoa : EntityPersistence
	{
		#region private fields

		private string nome;
        private string endereco;
        private string bairro;
        private string cidade;
        private string uf;
        private string cep;
        private string telefone;
        private string email;

        #endregion private fields

        #region constructor

        /// <summary>
		/// Construtor default.
		/// </summary>
        public Pessoa()
		{
			nome = string.Empty;
            endereco = string.Empty;
            bairro = string.Empty;
            cidade = string.Empty;
            uf = string.Empty;
            cep = string.Empty;
            telefone = string.Empty;
            email = string.Empty;
        }

        #endregion constructor

        #region Properties

        [OrderProperty(0), WidthProperty(250), EntityPropertyOrder(OrderFiled.Ascending)]
        public virtual string Nome
        {
            get{  return nome;  }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new PropertyException(new Property(this, "Nome", value), "O Nome deve ser preenchido !");
                }

                this.SetValue("nome", value); 
            }
        }

        [OrderProperty(1), WidthProperty(150), DisplayNameProperty("Endereço")]
        public virtual string Endereco
        {
            get { return endereco; }
            set { this.SetValue("endereco", value); }
        }

        [OrderProperty(2), WidthProperty(150), DisplayNameProperty("Bairro")]
        public virtual string Bairro
        {
            get { return bairro; }
            set { this.SetValue("bairro", value); }
        }

        [OrderProperty(3), WidthProperty(150), DisplayNameProperty("Cidade")]
        public virtual string Cidade
        {
            get { return cidade; }
            set { this.SetValue("cidade", value); }
        }

        [PropertyInvisible()]
        public virtual string Uf
        {
            get { return uf; }
            set { this.SetValue("uf", value); }
        }

        [OrderProperty(4), WidthProperty(100), DisplayNameProperty("UF")]
        public virtual string Estado
        {
            get 
            {
                return UfCollection.Instance.GetValue(Uf);
            }
        }

        [OrderProperty(5), WidthProperty(80), DisplayNameProperty("CEP")]
        public virtual string Cep
        {
            get { return cep; }
            set 
            {
                if (!ValidationCep.Instance.CepIsNull(value) && !ValidationCep.Instance.IsValid(value))
                {
                    throw new PropertyException(new Property("Cep", value), "O Cep está num formato inválido !");
                }

                this.SetValue("cep", value); 
            }
        }

        [OrderProperty(6), WidthProperty(80)]
        public virtual string Telefone
        {
            get { return telefone; }
            set { this.SetValue("telefone", value); }
        }

        [OrderProperty(7), WidthProperty(100), DisplayNameProperty("E-mail")]
        public virtual string Email
        {
            get { return email; }
            set { this.SetValue("email", value); }
        }

        #endregion Properties

        public override string ToString()
        {
            return Nome; 
        }
    }
}
