using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataObjectLayer;

namespace DataObjectLayer.Estoque
{
	[Serializable]
	public class GrupoProduto : EntityPersistence
	{
		#region private fields

		private string nome;

        #endregion private fields

        #region constructor

        /// <summary>
		/// Construtor default.
		/// </summary>
        public GrupoProduto()
		{
            nome = string.Empty;
        }

        #endregion constructor

        #region Properties

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

        #endregion Properties

        public override string ToString()
        {
            return Nome;
        }
    }
}
