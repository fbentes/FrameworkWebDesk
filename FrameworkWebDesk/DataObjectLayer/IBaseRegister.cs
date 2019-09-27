using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IBaseRegister
    {
        void SaveOrUpdate();
        void Delete(int codigo);
        void Delete();
        void CreateNewEntity();
    }
}
