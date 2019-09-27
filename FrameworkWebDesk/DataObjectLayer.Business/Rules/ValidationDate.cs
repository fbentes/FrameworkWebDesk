using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public class ValidationDate: IValidationBusiness
    {
        private static ValidationDate instance = null;

        public static ValidationDate Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidationDate();
                }
                return instance;
            }
        }

        private ValidationDate()
        {
        }

        /// <summary>
        /// Valida uma data.
        /// </summary>
        /// <param name="value">String que contém uma data.</param>
        /// <returns>Retorna true se o parâmetro value estiver nos formatos d/m/yyyy, dd/m/yyyy ou dd/mm/yyyy, e false, caso contrário.</returns>
        public bool IsValid(string value)
        {
            string expressao = @"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|"+
                               @"(([1-9]|[12][0-9]|3[01])([/])([13578]|10|12)([/])(\d{4}))|" +
                               @"(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|"+
                               @"((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|"+
                               @"((29)([/])(02)([/])([13579][26]00))|"+
                               @"((29)([/])(02)([/])([0-9][0-9][0][48]))|"+
                               @"((29)([/])(02)([/])([0-9][0-9][2468][048]))|"+
                               @"((29)([/])(02)([/])([0-9][0-9][13579][26])))";

            return Regex.IsMatch(value, expressao);
        }

        /// <summary>
        /// Retorna stringDate formatado como dd/mm/aaaa.
        /// </summary>
        /// <param name="stringDate">Data no formato d/m/aa, dd/m/aa, d/mm/aa, dd/mm/aa, dd/mm/aa, d/m/aaaa, d/mm/aaaa, dd/m/aaaa ou dd/mm/aaaa.</param>
        /// <returns></returns>
        public string GetDateFormated(string stringDate)
        {
            string[] dateParts = stringDate.Split("/".ToCharArray());

            string day = dateParts[0];

            if (day.Length == 1)
            {
                day = "0" + day;
            }

            string month = dateParts[1];

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            string year = dateParts[2];

            if (year.Length < 2)
            {
                throw new ArgumentException("O ano deve ter no mínimo dois dígitos para o parâmetro stringDate do método GetDateFormated().");
            }

            if (year.Length == 2)
            {
                if (Convert.ToInt32(year[0]) >= 3 && Convert.ToInt32(year[0]) <= 9)
                {
                    year = "19" + year;
                }
                else
                {
                    year = "20" + year;
                }
            }

            return day + "/" + month + "/" + year;
        }

        public bool DateIsNull(object value)
        {
            return value == null || string.IsNullOrEmpty(value.ToString()) || value.ToString() == "  /  /";
        }
    }
}