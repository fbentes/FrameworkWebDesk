using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace DataObjectLayer
{
    public class Cloneable: ICloneable
    {
        private static Cloneable instance = null;

        public static Cloneable Instance
        {
            get
            {
                if (instance == null)
                    instance = new Cloneable();

                return instance;
            }
        }

        private Cloneable()
        {
        }

        #region ICloneable Members

        public object Clone()
        {
           // PropertyInfo[] properties = objeto.GetType().GetProperties();
            return null;
        }

        #endregion
    }
}
