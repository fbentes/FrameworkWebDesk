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
    public class ExportGridViewToExcel : ExportToExcel<GridView>
    {
        private static ExportGridViewToExcel instance = null;

        public static new ExportGridViewToExcel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportGridViewToExcel();
                }
                return instance;
            }
        }

        protected override GridView getNewControl(GridView Control)
        {
            GridView grid = base.getNewControl(Control);

            grid.AutoGenerateColumns = false;

            return grid;
        }
    }
}