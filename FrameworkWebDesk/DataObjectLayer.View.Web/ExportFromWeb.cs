using System;
using System.Reflection;
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
    public abstract class ExportFromWeb<T> where T : BaseDataBoundControl, new()
    {
        public event BeforeExport OnBeforeExport = null;

        protected virtual string contentType
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string fileNameExtension
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual T getNewControl(T Control)
        {
            return new T();
        }

        protected virtual void setListSource(T control, IList listSource)
        {
            control.DataSource = listSource;
            control.DataBind();
        }

        public virtual void ExecuteExport(T control)
        {
            ExecuteExport(control, null);
        }

        public virtual void ExecuteExport(T control, IList listSource)
        {
            HttpResponse response = control.Page.Response;

            if (listSource == null)
            {
                throw new ArgumentNullException("O parâmetro listSource não pode ser nulo !");
            }

            try
            {
                T controlToExport = getNewControl(control);

                if (OnBeforeExport != null)
                {
                    BeforeExportEventArgs beforeExportEventArgs = new BeforeExportEventArgs(controlToExport);

                    OnBeforeExport(beforeExportEventArgs);

                    if (!string.IsNullOrEmpty(beforeExportEventArgs.JavaScript))
                    {
                        response.Write(beforeExportEventArgs.JavaScript);
                    }
                }

                response.Clear();
                response.ContentType = contentType;
                response.AddHeader("Content-Disposition", "attachment; filename=Exportador_" + DateTime.Now.Ticks.ToString() + "." + fileNameExtension);
                response.Charset = "";

                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                setListSource(controlToExport, listSource);

                controlToExport.RenderControl(htmlWrite);

                response.Write(stringWrite.ToString());

                response.End();
            }
            catch (Exception)
            {
                // Erro desconhecido. A exportação funciona mesmo com a exceção disparada e não tratada.
            }
        }
    }
}
