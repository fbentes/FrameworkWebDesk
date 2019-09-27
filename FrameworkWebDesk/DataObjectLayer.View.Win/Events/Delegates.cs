using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View.Win
{
    #region Only in Button Save

    public delegate void BeforeSaveClickEvent(EntitySaveEventArgs e);

    public delegate void AfterSaveClickEvent(EntitySaveEventArgs e);

    public delegate void BeforeRefreshListEvent(EntityEventArgs e);

    #endregion

    #region Only in ComboBoxCustom

    public delegate void BeforeSelectedIndexChanged(EntityEventArgs e);

    public delegate void AfterSelectedIndexChanged(EntityEventArgs e);

    #endregion
}
