using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Web
{
    public class ButtonNewEntity: Button
    {
        private string entitySource = string.Empty;
        private string entityNamespaceSource = string.Empty;

        [Category("Entity")]
        public event BeforeClickEvent BeforeClick;

        [Category("Entity")]
        public event AfterClickEvent AfterClick;

        private Type typeEntity
        {
            get
            {
                if (EntityNamespaceSource == string.Empty || EntitySource == string.Empty)
                {
                    return null;
                }

                return EntityReflection.Instance.GetType(EntityNamespaceSource, EntitySource);
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
        public string EntityNamespaceSource
        {
            get { return entityNamespaceSource; }
            set { entityNamespaceSource = value; }
        }

        [Category("Entity")]
        public string ButtonSaveEntity
        {
            set
            {
                ViewState[this.UniqueID + "ButtonSaveEntity"] = value;
            }
            get
            {
                if (ViewState[this.UniqueID + "ButtonSaveEntity"] == null)
                {
                    return string.Empty;
                }

                return ViewState[this.UniqueID + "ButtonSaveEntity"].ToString();
            }
        }

        private ButtonSaveEntity buttonSaveEntityInstance
        {
            get
            {
                return Page.FindControl(ButtonSaveEntity) as ButtonSaveEntity;
            }
        }

        private IEntityPersistence getNewEntity()
        {
            ConstructorInfo construtor = typeEntity.GetConstructor(Type.EmptyTypes);

            if (construtor == null)
            {
                throw new EntityNullException("A entidade " + typeEntity.Name + " deve ter um construtor padrão !");
            }

            return construtor.Invoke(null) as IEntityPersistence;
        }

        protected virtual void OnBeforeClick(EntityWebEventArgs e)
        {
            if (BeforeClick != null)
            {
                BeforeClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            EntityWebEventArgs entityEventArgs = new EntityWebEventArgs(null, null);

            OnBeforeClick(entityEventArgs);

            if (entityEventArgs.CancelAction)
            {
                return;
            }

            Type type = typeEntity;

            if (entityEventArgs.PageName != string.Empty)
            {
                HttpContext.Current.Server.Transfer(entityEventArgs.PageName);

                return;
            }

            entityEventArgs.Entity = getNewEntity();

            SetControlFromEntity.Execute(entityEventArgs.Entity, Page, true);

            if (buttonSaveEntityInstance != null)
            {
                buttonSaveEntityInstance.Enabled = true;
                Enabled = false;

                if (buttonSaveEntityInstance.GridViewEntityInstance != null)
                {
                    buttonSaveEntityInstance.GridViewEntityInstance.EntityCurrent = entityEventArgs.Entity;

                    buttonSaveEntityInstance.GridViewEntityInstance.SelectedIndex = -1;
                }
            }

            OnAfterClick(entityEventArgs);
        }

        protected virtual void OnAfterClick(EntityWebEventArgs e)
        {
            if (AfterClick != null)
            {
                AfterClick(e);
            }
        }

        public void ExecuteClick()
        {
            OnClick(EntityWebEventArgs.Empty);
        }
    }
}
