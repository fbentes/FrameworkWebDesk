using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using DataObjectLayer;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

namespace DataObjectLayer.View.Web
{
    public enum TypeOpenPage
    {
        NewPage,
        SamePage
    }

    public static class OpenFile
    {
        public static void Execute(Page page, byte[] fileBytes)
        {
            Execute(page, fileBytes, DateTime.Now.Ticks.ToString(), TypeOpenPage.NewPage);
        }

        public static void Execute(Page page, byte[] fileBytes, string fileName)
        {
            Execute(page, fileBytes, fileName, TypeOpenPage.NewPage);
        }

        public static void Execute(Page page, byte[] fileBytes, string fileName, TypeOpenPage typeOpenPage)
        {
            File file = new File();

            file.Bytes = fileBytes;

            file.Extension = Path.GetExtension(fileName).ToLower();

            if (typeOpenPage == TypeOpenPage.NewPage)
            {
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            }

            if ((file.Extension == ".jpg") || (file.Extension == ".bmp") || (file.Extension == ".jpeg"))
                page.Response.ContentType = "image/jpeg";
            if (file.Extension == ".pdf")
                page.Response.ContentType = "application/PDF";
            if (file.Extension == ".doc")
                page.Response.ContentType = "application/msword";
            if (file.Extension == ".xls")
                page.Response.ContentType = "application/vnd.ms-excel";
            /*
            if (file.Extension == ".txt")
                page.Response.ContentType = "text/plain";
            */
            page.Response.BinaryWrite(file.Bytes);

            page.Response.End();
        }

        private class File
        {
            private string extension;
            private byte[] bytes;

            public string Extension
            {
                get { return extension; }
                set { extension = value; }
            }

            public byte[] Bytes
            {
                get { return bytes; }
                set { bytes = value; }
            }
        }
    }
}
