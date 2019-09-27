using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace DataObjectLayer.View.Web
{
    public class EditTemplateField : ITemplate
    {
        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            ImageButton imgEdit = new ImageButton();
            imgEdit.ID = "imgEdit";
            imgEdit.CommandName = "Edit";
            imgEdit.ToolTip = "Editar registro selecionado.";

            container.Controls.Add(imgEdit);
        }

        #endregion
    }
}
