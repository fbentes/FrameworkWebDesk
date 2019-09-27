using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace DataObjectLayer.View.Web
{
    #region Only in Button Save

    public delegate void BeforeSaveWebClickEvent(EntitySaveWebEventArgs e);

    public delegate void AfterSaveWebClickEvent(EntitySaveWebEventArgs e);

    public delegate void BeforeExport(BeforeExportEventArgs e);

    #endregion

    #region Only in GridViewEntity

    public delegate void AfterRowEditingDelegate(EntityWebEventArgs e);

    public delegate void BeforeRowEditingDelegate(EntityWebEventArgs e);

    public delegate void BeforeRowDeletingDelegate(EntityWebEventArgs e);

    public delegate void AfterRowDeletingDelegate(EntityWebEventArgs e);

    #endregion
}
