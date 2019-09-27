using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public static class RandomValue
    {
        public static string GetString(int length)
        {
            byte[] buffer = new byte[length];

            Random random = new Random();

            random.NextBytes(buffer);

            string result = Convert.ToBase64String(buffer);

            if (result.Length > length)
            {
                result = result.Substring(0, length);
            }

            return result;
        }
    }
}
