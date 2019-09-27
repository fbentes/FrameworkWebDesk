using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DataObjectLayer.Business
{
    public class Comparer : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(x, y));
        }
    }
}
