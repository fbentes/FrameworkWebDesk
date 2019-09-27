using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataObjectLayer;
using System.Web.UI;

namespace DataObjectLayer.View.Web
{
    public class EntitySaveWebEventArgs : EntitySaveEventArgs
    {
        private string pageName = string.Empty;

        public string PageName
        {
            get { return pageName; }
            set { pageName = value; }
        }

        public EntitySaveWebEventArgs()
        {
        }

        public EntitySaveWebEventArgs(IPostRegister entityManager, IEntityPersistence entity, string pageName, IList listSource)
            : base(entityManager, entity, listSource)
        {
            if (pageName != string.Empty)
            {
                if (pageName.ToLower().IndexOf(".aspx") == -1)
                {
                    pageName += pageName + ".aspx";
                }

                PageName = pageName;
            }
        }
    }
}
