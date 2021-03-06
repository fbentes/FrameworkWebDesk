using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DataObjectLayer.View.Web
{
    public class ExportGridViewEntityToExcel : ExportToExcel<GridViewEntity>
    {
        private static ExportGridViewEntityToExcel instance = null;

        public static new ExportGridViewEntityToExcel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportGridViewEntityToExcel();
                }
                return instance;
            }
        }

        protected override void setListSource(GridViewEntity control, IList listSource)
        {
            GridViewEntity gridEntityExcel = new GridViewEntity();

            gridEntityExcel.ID = DateTime.Now.Ticks.ToString(); ;
            gridEntityExcel.EntitySource = (control as GridViewEntity).EntitySource;
            gridEntityExcel.HasDeleteColumn = false;
            gridEntityExcel.HasEditColumn = false;
            gridEntityExcel.DataSource = listSource;

            control = gridEntityExcel;
        }
    }
}
