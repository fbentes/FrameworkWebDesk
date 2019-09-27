using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class ValidationCep: IValidationBusiness
    {
        private static ValidationCep instance = null;

        public static ValidationCep Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidationCep();
                }

                return instance;
            }
        }

        private ValidationCep()
        {
        }

        /// <summary>
        /// Valida um cpf.
        /// </summary>
        /// <param name="value">String que contém um valor no formato 999.999.999-99 ou 99999999999.</param>
        /// <returns>Retorna true se o parâmetro value tiver um cpf válido.</returns>
        public bool IsValid(string value)
        {
            string valueMask = value.Replace(",", ".");

            return Regex.IsMatch(valueMask, @"\d{2}\.\d{3}\-\d{3}");
        }

        public bool CepIsNull(string value)
        {
            string valueMask = value.Replace(",", ".");

            return string.IsNullOrEmpty(valueMask) || valueMask == "  .   -   ";
        }
    }
}
