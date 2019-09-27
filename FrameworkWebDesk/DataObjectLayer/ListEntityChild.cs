using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class ListEntityChild<T>: List<T> where T: EntityPersistence
    {
        #region Fields

        private EntityPersistence parent;

        #endregion

        #region Fields

        private ListEntityChild()
        {
        }

        public ListEntityChild(EntityPersistence parent)
        {
            this.parent = parent;
        }

        #endregion
    
        #region Methods

        public new void AddRange(IEnumerable<T> collection)
        {
        }

        public new void Add(T item)
        {
            parent.AddChild(item);
        }

        public new void Remove(T item)
        {
            parent.RemoveChild(item);
        }

        #endregion
    }
}
