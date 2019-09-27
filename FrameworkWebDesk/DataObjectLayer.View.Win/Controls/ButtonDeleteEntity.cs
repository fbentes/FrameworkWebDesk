using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public class ButtonDeleteEntity : Button
    {
        #region Fields

        private bool showMessageExceptions = true;
        
        private bool showMessageSucess = true;
        private string messageSucess = "Dados excluídos com sucesso !";

        private bool showMessageConfirmation = true;
        private string messageConfirmation = "Deseja excluir o registro ?";

        private string buttonNewEntity = string.Empty;
        private string gridViewEntity = string.Empty;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        #endregion

        [Category("Entity")]
        public event BeforeDeleteClickEvent BeforeDeleteClick;

        [Category("Entity")]
        public event AfterDeleteClickEvent AfterDeleteClick;

        #region Properties

        private ButtonNewEntity buttonNewEntityInstance
        {
            get
            {
                return Parent.Controls[buttonNewEntity] as ButtonNewEntity;
            }
        }

        public DataGridViewEntity GridViewEntityInstance
        {
            get
            {
                return Parent.Controls[gridViewEntity] as DataGridViewEntity;
            }
        }

        [Category("Entity")]
        [DefaultValue(true)]
        public bool ShowMessageExceptions
        {
            set
            {
                showMessageExceptions = value;
            }
            get
            {
                return showMessageExceptions;
            }
        }

        [Category("Entity")]
        [DefaultValue(true)]
        public bool ShowMessageSucess
        {
            set
            {
                showMessageSucess = value;
            }
            get
            {
                return showMessageSucess;
            }
        }

        [Category("Entity")]
        public string MessageSucess
        {
            set
            {
                messageSucess = value;
            }
            get
            {
                return messageSucess;
            }
        }

        [Category("Entity")]
        [DefaultValue(true)]
        public bool ShowMessageConfirmation
        {
            set
            {
                showMessageConfirmation = value;
            }
            get
            {
                return showMessageConfirmation;
            }
        }

        [Category("Entity")]
        public string MessageConfirmation
        {
            set
            {
                messageConfirmation = value;
            }
            get
            {
                return messageConfirmation;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string ButtonNewEntity
        {
            set
            {
                buttonNewEntity = value;
            }
            get
            {
                return buttonNewEntity;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string GridViewEntity
        {
            set
            {
                gridViewEntity = value;
            }
            get
            {
                return gridViewEntity;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerSource
        {
            set { entityManagerSource = value; }
            get { return entityManagerSource; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerNamespaceSource
        {
            get { return entityManagerNamespaceSource; }
            set { entityManagerNamespaceSource = value; }
        }

        private Type typeEntityManager
        {
            get
            {
                if (EntityManagerSource == string.Empty)
                {
                    return null;
                }

                return EntityReflection.Instance.GetType(EntityManagerNamespaceSource, EntityManagerSource);
            }
        }

        private IDeleteRegister entityManagerSourceInstance
        {
            get
            {
                if (typeEntityManager != null)
                {
                    PropertyInfo prope = EntityReflection.Instance.GetFirstProperty(typeEntityManager, typeof(SingletonAttribute));

                    if (prope == null)
                    {
                        throw new EntityManagerNoConstructorException("A classe " + EntityManagerSource + " deve conter uma propriedade pública com o atributo [Singleton] retornando uma instância de si própria !", null);
                    }

                    return prope.GetValue(null, null) as IDeleteRegister;
                }

                return null;
            }
        }

        #endregion

        #region Delete Events

        protected virtual void OnBeforeDeleteClick(EntityDeleteEventArgs e)
        {
            if (showMessageConfirmation && !string.IsNullOrEmpty(messageConfirmation))
            {
                if (!MessageBoxConfirmation.Show(Parent, messageConfirmation))
                {
                    e.CancelAction = true;

                    return;
                }
            }

            if (BeforeDeleteClick != null)
            {
                BeforeDeleteClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            EntityDeleteEventArgs EntityDeleteEventArgs = new EntityDeleteEventArgs();

            OnBeforeDeleteClick(EntityDeleteEventArgs);

            // Testa se o cliente não cancelou o OnClick explicitamente.
            if (EntityDeleteEventArgs.CancelAction)
            {
                return;
            }

            EntityDeleteEventArgs.Entity = getEntityFromGridViewEntity();

            if (EntityDeleteEventArgs.Entity == null)
            {
                throw new EntityParentException("A propriedade EntityCurrent do DataGridViewEntity está nula ! ");
            }

            EntityDeleteEventArgs.EntityManager = entityManagerSourceInstance as IDeleteRegister;

            EntityDeleteEventArgs.EntityManager.Delete(EntityDeleteEventArgs.Entity.Id);

            refreshGridViewEntity();

            refreshButtonNewEntity();

            OnAfterDeleteClick(EntityDeleteEventArgs);
        }

        protected virtual void OnAfterDeleteClick(EntityDeleteEventArgs e)
        {
            if (AfterDeleteClick != null)
            {
                AfterDeleteClick(e);
            }
            if (!e.CancelAction && showMessageSucess && !string.IsNullOrEmpty(messageSucess))
            {
                MessageBoxInformation.Show(Parent, messageSucess);
            }
        }

        #endregion

        #region Private Methods

        private IEntityPersistence getEntityFromGridViewEntity()
        {
            if (!string.IsNullOrEmpty(GridViewEntity))
            {
                if (GridViewEntityInstance != null)
                {
                    return GridViewEntityInstance.EntityCurrent;
                }
            }

            return null;
        }

        private void refreshButtonNewEntity()
        {
            if (buttonNewEntityInstance != null)
            {
                buttonNewEntityInstance.ExecuteClick();
            }
        }

        private void refreshGridViewEntity()
        {
            if (GridViewEntityInstance != null)
            {
                GridViewEntityInstance.RefreshList();
            }
        }

        #endregion
    }
}