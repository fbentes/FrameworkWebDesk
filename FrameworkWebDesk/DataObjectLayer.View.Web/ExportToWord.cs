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
    public class ExportToWord<T> : ExportFromWeb<T> where T : BaseDataBoundControl, new()
    {
        private static ExportToWord<T> instance = null;

        public static ExportToWord<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportToWord<T>();
                }
                return instance;
            }
        }

        protected override string fileNameExtension
        {
            get
            {
                return "doc";
            }
        }

        protected override string contentType
        {
            get
            {
                return "application/msword";
            }
        }
    }
}
