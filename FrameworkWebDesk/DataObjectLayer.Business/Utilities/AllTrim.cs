using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataObjectLayer.Business
{
    public static class AllTrim
    {
        // Remove os espaços em excesso.

        public static void Execute(ref string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Regex regex = new Regex(@"\s * | * \s", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            value = regex.Replace(value, " ").Trim();
        }
    }
}
