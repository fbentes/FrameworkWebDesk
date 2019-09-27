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
        /// Valida um n�mero decimal.
        /// </summary>
        /// <param name="value">String que cont�m uma n�mero decimal.</param>
        /// <returns>Retorna true se for poss�vel converter o par�metro value para um tipo decimal, e false, caso contr�rio; os formatos aceit�veis s�o 9999,99 ou 9999.99 ou 9999 e assim por diante. </returns>
        public bool IsValid(string value)
        {
            decimal numberOut;

            return decimal.TryParse(value, out numberOut);
        }
    }
}
