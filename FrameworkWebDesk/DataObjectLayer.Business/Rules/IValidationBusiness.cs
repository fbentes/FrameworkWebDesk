using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public interface IValidationBusiness
    {
        bool IsValid(string value);
    }
}
