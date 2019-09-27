using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DataObjectLayer.View.Web
{
    public class DeleteTemplateField : ITemplate
    {
        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            ImageButton imgDelete = new ImageButton();
            imgDelete.ID = "imgDelete";
            imgDelete.CommandName = "Delete";
            imgDelete.ToolTip = "Excluir registro selecionado.";

            container.Controls.Add(imgDelete);
        }

        #endregion
    }
}
