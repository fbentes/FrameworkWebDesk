using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
	[Serializable]
    public class Produto : EntityPersistence
	{
		#region fields

        private GrupoProduto grupoProduto;
        private string nome;
        private decimal precoUnitario;
        private int quantidadeEstoque;
        private bool ativo;

        #endregion 

        #region constructor

        /// <summary>
		/// Construtor default.
		/// </summary>
        public Produto()
		{
            grupoProduto = null;
            nome = string.Empty;
            precoUnitario = 0;
            quantidadeEstoque = 0;
            ativo = true;
        }

        #endregion constructor

        #region Properties

        [OrderProperty(0), DisplayNameProperty("Grupo Produto"), WidthProperty(200)]
        public virtual GrupoProduto GrupoProduto
        {
            get { return grupoProduto; }
            set { this.SetValue("grupoProduto", value); }
        }

        [OrderProperty(1), WidthProperty(280)]
        public virtual string Nome
        {
            get { return nome; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new PropertyException(new Property(this, "Nome", value), "O Nome deve ser preenchido !");
                }

                this.SetValue("nome", value); 
            }
        }

        [OrderProperty(2), DisplayNameProperty("Preço Unit."), WidthProperty(100)]
        public virtual decimal PrecoUnitario
        {
            get { return precoUnitario; }
            set { this.SetValue("precoUnitario", value); }
        }

        [OrderProperty(3), DisplayNameProperty("Quant. Estoque"), WidthProperty(100)]
        public virtual int QuantidadeEstoque
        {
            get { return quantidadeEstoque; }
            set { this.SetValue("quantidadeEstoque", value); }
        }

        [PropertyInvisible]
        public virtual bool Ativo
        {
            get { return ativo; }
            set { this.SetValue("ativo", value); }
        }

        #endregion Properties

        public override string ToString()
        {
            return Nome;
        }
    }
}
