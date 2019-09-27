using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class CloseSessionException : System.Exception
    {
        const string closeSessionExceptionMessage = "Problema ao fechar a sess�o: ";

        public CloseSessionException(string message)
            : base(closeSessionExceptionMessage + " " + message)
        {
        }

        public override string Message
        {
            get
            {
                return closeSessionExceptionMessage;
            }
        }
    }
}
