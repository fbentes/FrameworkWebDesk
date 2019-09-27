using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer.Business
{
    public static class Correio
    {
        private static readonly string urlBusca = "http://www.polarmacae.com.br/novosite/polarmacae.web/_cep/buscacep.asp?cep=";

        public static Logradouro BuscaCep(string cep)
        {
            Logradouro logradouro = new Logradouro();

            if (string.IsNullOrEmpty(cep))
            {
                return logradouro;
            }

            WebClient webCliente = new WebClient();

            WebRequest request = WebRequest.Create(urlBusca + cep); ;

            WebResponse response = request.GetResponse();;

            StreamReader stream = new StreamReader(response.GetResponseStream());;

            try
            {
                string str = stream.ReadLine();

                logradouro.Endereco = str.Substring(str.IndexOf(":") + 1).Replace("<br>","").Trim();

                str = stream.ReadLine();

                logradouro.Bairro = str.Substring(str.IndexOf(":") + 1).Replace("<br>", "").Trim();

                str = stream.ReadLine();

                logradouro.Cidade = str.Substring(str.IndexOf(":") + 1).Replace("<br>", "").Trim();

                str = stream.ReadLine();

                logradouro.Estado = str.Substring(str.IndexOf(":") + 1).Replace("<br>", "").Trim();

                logradouro.Cep = cep;
            }
            finally
            {
                request = null;

                response.Close();
                response = null;

                stream.Close();
                stream = null;
            }

            return logradouro;
        }
    }
}
