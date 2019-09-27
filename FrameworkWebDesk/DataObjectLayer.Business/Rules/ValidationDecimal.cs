using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class ValidationDecimal: IValidationBusiness
    {
        private static ValidationDecimal instance = null;

        public static ValidationDecimal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidationDecimal();
                }
                return instance;
            }
        }

        private ValidationDecimal()
        {
        }

        /// <summary>
        /// Valida um número decimal.
        /// </summary>
        /// <param name="value">String que contém uma número decimal.</param>
        /// <returns>Retorna true se for possível converter o parâmetro value para um tipo decimal, e false, caso contrário; os formatos aceitáveis são 9999,99 ou 9999.99 ou 9999 e assim por diante. </returns>
        public bool IsValid(string value)
        {
            decimal numberOut;

            return decimal.TryParse(value, out numberOut);
        }
    }
}
