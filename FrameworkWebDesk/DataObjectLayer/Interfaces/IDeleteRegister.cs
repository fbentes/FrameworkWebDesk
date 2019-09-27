using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IDeleteRegister: IDeleteManyRegister
    {
        void Delete(object id);
    }
}
