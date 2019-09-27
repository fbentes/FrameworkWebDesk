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
    public class ExportToExcel<T> : ExportFromWeb<T> where T : BaseDataBoundControl, new()
    {
        private static ExportToExcel<T> instance = null;

        public static ExportToExcel<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportToExcel<T>();
                }
                return instance;
            }
        }

        protected override string fileNameExtension
        {
            get
            {
                return "xls";
            }
        }

        protected override string contentType
        {
            get
            {
                return "application/vnd.ms-excel";
            }
        }
    }
}