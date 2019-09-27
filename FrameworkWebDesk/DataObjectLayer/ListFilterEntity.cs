using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    [Serializable]
    public class ListFilterEntity: List<FilterEntity>
    {
        #region Fields

        public bool HasOperation;

        #endregion

        #region Methods

        public new void Add(FilterEntity item)
        {
            if (!HasOperation)
                HasOperation = item.FilterOperation != FilterOperation.None;

            base.Add(item);
        }

        #endregion
    }
}
