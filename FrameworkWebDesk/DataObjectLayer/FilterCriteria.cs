using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public enum FilterCriteria
    {
        Equal,
        Greater,
        Smaller,
        GreaterOrEqual,
        SmallerOrEqual,
        Different,
        StartLike,
        EndLike,
        AllLike,
        In,
        NotIn,
        Between,
        IsNull,
        IsNotNull,
        None
    }
}
