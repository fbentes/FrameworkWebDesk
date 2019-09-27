using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace DataObjectLayer.Business
{
    public static class Criptography
    {
        public static byte[] ComputeHash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            SHA1 sha1 = SHA1.Create();

            return sha1.ComputeHash(Convert.FromBase64String(text));
        }
    }
}