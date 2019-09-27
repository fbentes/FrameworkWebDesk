using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View
{
    public delegate void AfterSetEntityPropertyToControlDelegate();

    public delegate void AfterClickEvent(EntityEventArgs e);

    public delegate void BeforeClickEvent(EntityEventArgs e);

    public delegate void AfterDeleteClickEvent(EntityDeleteEventArgs e);

    public delegate void BeforeDeleteClickEvent(EntityDeleteEventArgs e);
}
