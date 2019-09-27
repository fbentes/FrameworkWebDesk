using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using DataObjectLayer.Reflection;

namespace DataObjectLayer
{
    [Serializable]
    public class EntityBase
    {
        #region Fields

        protected int unsavedValue = -1;

        protected int id;

        #endregion

        #region Properties

        [PropertyInvisible()]
        public virtual int Id
        {
            get
            {
                return id;
            }
            set
            {
                SetId(value);
            }
        }

        [PropertyInvisible()]
        public virtual bool IsNew
        {
            get
            {
                return id <= unsavedValue;
            }
        }

        [PropertyInvisible()]
        public virtual string Name
        {
            get
            {
                return ToString();
            }
        }

        #endregion

        #region Constructor

        protected EntityBase()
        {
            this.id = -1;
        }

        #endregion

        #region Methods

        protected virtual void SetId(Int32 value)
        {
            id = value;
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            EntityBase other = obj as EntityBase;

            if (other == null)
                return false;

            if (this.Id == unsavedValue && other.Id == unsavedValue)
                return false;

            return this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            if (Id == unsavedValue)
                return this.GetType().GetHashCode();
            else
                return this.GetType().GetHashCode() * Id.GetHashCode();
        }

        #endregion
    }
}