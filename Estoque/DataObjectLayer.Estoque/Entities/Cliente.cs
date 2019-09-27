using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataObjectLayer;
using DataObjectLayer.Business;

namespace DataObjectLayer.Estoque
{
	[Serializable]
    public class Cliente : Pessoa
	{
		#region fields

        private string cpf;
        private string rg;
        private DateTime? dataNascimento;
        private string sexo;
        private SexoCollection sexoCollection;
        private bool ativo;
        private byte[] anexo;

        #endregion private fields

        #region constructor

        /// <summary>
		/// Construtor default.
		/// </summary>
        public Cliente()
        {
            cpf = string.Empty;
            rg = string.Empty;
            sexo = string.Empty;
            sexoCollection = SexoCollection.Instance;
            ativo = true;
            dataNascimento = null;
            anexo = null;
        }

        #endregion constructor

        #region Properties

        [OrderProperty(8), DisplayNameProperty("CPF")]
        public virtual string Cpf
        {
            get
            {
                return cpf;
            }

            set
            {
                if(!ValidationCpf.Instance.CpfIsNull(value) && !ValidationCpf.Instance.IsValid(value))
                {
                    throw new PropertyException(new Property(this, "Cpf", value), "O Cpf está num formato inválido !");
                }

                this.SetValue("cpf", value);
            }
        }

        [OrderProperty(9), DisplayNameProperty("RG")]
        public virtual string Rg
        {
            get
            {
                return rg;
            }

            set
            {
                this.SetValue("rg", value);
            }
        }

        [PropertyInvisible()]
        public virtual DateTime? DataNascimento
        {
            get
            {
                return dataNascimento;
            }

            set
            {
                if (!ValidationDate.Instance.DateIsNull(value) && !ValidationDate.Instance.IsValid(value.ToString()))
                {
                    throw new PropertyException(new Property("Data de nascimento", value), "A data de nascimento está num formato inválido !");
                }

                this.SetValue("dataNascimento", value);
            }
        }

        [OrderProperty(10), DisplayNameProperty("Sexo")]
        public virtual string SexoPessoa
        {
            get
            {
                return sexoCollection.GetValue(Sexo);
            }
        }

        [PropertyInvisible()]
        public virtual string Sexo
        {
            get
            {
                return sexo;
            }

            set
            {
                this.SetValue("sexo", value);
            }
        }

        [PropertyInvisible]
        public virtual bool Ativo
        {
            get { return ativo; }
            set { this.SetValue("ativo", value); }
        }

        [PropertyInvisible]
        public virtual byte[] Anexo
        {
            get { return anexo; }
            set { this.SetValue("anexo", value); }
        }

        #endregion Properties
    }
}