using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DataObjectLayer;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public class ButtonNewEntity: Button
    {
        private string entitySource = string.Empty;
        private string entityNamespaceSource = string.Empty;

        private string buttonDeleteEntity = string.Empty;
        private string buttonSaveEntity = string.Empty;

        private IEntityPersistence entityNew = null;

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
                buttonSaveEntity = value;
            }
            get
            {
                return buttonSaveEntity;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string ButtonDeleteEntity
        {
            set
            {
                buttonDeleteEntity = value;
            }
            get
            {
                return buttonDeleteEntity;
            }
        }

        public IEntityPersistence EntityNew
        {
            get
            {
                return entityNew;
            }
        }

        private ButtonDeleteEntity buttonDeleteEntityInstance
        {
            get
            {
                return Parent.Controls[buttonDeleteEntity] as ButtonDeleteEntity;
            }
        }

        private ButtonSaveEntity buttonSaveEntityInstance
        {
            get
            {
                return Parent.Controls[buttonSaveEntity] as ButtonSaveEntity;
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

        protected virtual void OnBeforeClick(EntityEventArgs e)
        {
            if (BeforeClick != null)
            {
                BeforeClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            EntityEventArgs entityEventArgs = new EntityEventArgs(null, null);

            OnBeforeClick(entityEventArgs);

            if (entityEventArgs.CancelAction)
            {
                return;
            }

            Type type = typeEntity;

            entityNew = getNewEntity();

            SetControlFromEntity.Execute(entityNew, Parent, true);

            if (buttonSaveEntityInstance != null)
            {
                buttonSaveEntityInstance.Enabled = true;
                Enabled = false;
            }

            entityEventArgs.Entity = entityNew;

            if (!string.IsNullOrEmpty(ButtonSaveEntity) && !string.IsNullOrEmpty(buttonSaveEntityInstance.GridViewEntity))
            {
                buttonSaveEntityInstance.GridViewEntityInstance.ClearSelection();
            }

            refreshButtonDeleteEntity();

            OnAfterClick(entityEventArgs);
        }

        protected virtual void OnAfterClick(EntityEventArgs e)
        {
            if (AfterClick != null)
            {
                AfterClick(e);
            }
        }

        public void ExecuteClick()
        {
            OnClick(EventArgs.Empty);
        }

        private void refreshButtonDeleteEntity()
        {
            if (buttonDeleteEntityInstance != null)
            {
                buttonDeleteEntityInstance.Enabled = false;
            }
        }
    }
}
