using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer.Reflection;
using System.Globalization;
using DataObjectLayer.Business;
using DataObjectLayer.View;

namespace DataObjectLayer.View.Web
{
    public class FileUploadEntity : FileUpload, IViewControlEntity, IChangeBackColor
    {
        private string entitySource;

        private string entityProperty;

        private Color backColorValidate = Color.White;

        private Color backColorInvalidate = Color.Yellow;

        [Bindable(true)]
        [Category("Appearance")]
        public Color BackColorInvalidate
        {
            set
            {
                backColorInvalidate = value;
            }
            get
            {
                return backColorInvalidate;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public Color BackColorValidate
        {
            set
            {
                backColorValidate = value;
            }
            get
            {
                return backColorValidate;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntitySource
        {
            set
            {
                entitySource = value;
            }
            get
            {
                return entitySource;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityProperty
        {
            set
            {
                entityProperty = value;
            }
            get
            {
                return entityProperty;
            }
        }

        public FileUploadEntity()
        {
            //Implementar limitação do número de caracteres e atribuir para this.MaxLength.
        }

        protected override void OnLoad(EventArgs e)
        {
            BackColor = BackColorValidate;
        }

        private bool isCorrectTypeEntity(IEntityPersistence entity)
        {
            return entitySource == entity.GetType().Name;
        }

        /// <summary>
        /// Atribui o valor da propriedade EntityProperty do EntitySource para a propriedade Text do TextBoxEntity.
        /// </summary>
        public void SetValueToControl(object value)
        {
        }

        public object Value
        {
            get
            {
                return FileBytes;
            }
        }

        public bool IsSetEntityFromControl
        {
            set { }
            get { return true; }
        }

        #region IChangeBackColor Members

        public void ChangeBackColorValidate()
        {
            BackColor = backColorValidate;
        }

        public void ChangeBackColorInvalidate()
        {
            BackColor = backColorInvalidate;
        }

        #endregion
    }
}