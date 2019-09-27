using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public class ButtonSaveEntity: Button
    {
        #region Fields

        private bool showMessageExceptions = true;

        private bool showMessageSucess = true;
        private string messageSucess = "Dados salvos com sucesso !";

        private bool showMessageConfirmation = true;
        private string messageConfirmation = "Deseja salvar os dados ?";

        private string buttonDeleteEntity = string.Empty;
        private string gridViewEntity = string.Empty;
        private string buttonNewEntity = string.Empty;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        #endregion

        [Category("Entity")]
        public event BeforeSaveClickEvent BeforeSaveClick;

        [Category("Entity")]
        public event AfterSaveClickEvent AfterSaveClick;

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

        private ButtonDeleteEntity buttonDeleteEntityInstance
        {
            get
            {
                return Parent.Controls[buttonDeleteEntity] as ButtonDeleteEntity;
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

        private IPostRegister entityManagerSourceInstance
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

                    return prope.GetValue(null, null) as IPostRegister;
                }

                return null;
            }
        }

        #endregion

        #region Save Events

        protected virtual void OnBeforeSaveClick(EntitySaveEventArgs e)
        {
            if (showMessageConfirmation && !string.IsNullOrEmpty(messageConfirmation))
            {
                if (!MessageBoxConfirmation.Show(Parent, messageConfirmation))
                {
                    e.CancelAction = true;

                    return;
                }
            }

            if (BeforeSaveClick != null)
            {
                BeforeSaveClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            EntitySaveEventArgs entitySaveEventArgs = new EntitySaveEventArgs();

            OnBeforeSaveClick(entitySaveEventArgs);

            // Testa se o cliente não cancelou o OnClick explicitamente.
            if (entitySaveEventArgs.CancelAction)
            {
                return;
            }

            entitySaveEventArgs.Entity = getEntityToPersist();

            if (entitySaveEventArgs.Entity == null)
            {
                throw new EntityParentException("A propriedade EntityCurrent do GridViewEntity está nula ! ");
            }

            setEntityFromControl(entitySaveEventArgs);

            //Testa pela segunda vez para assegurar que não houve problemas nos sets para as propriedades.
            if (entitySaveEventArgs.CancelAction)
            {
                return;
            }

            entitySaveEventArgs.EntityManager = entityManagerSourceInstance as IPostRegister;

            entitySaveEventArgs.EntityManager.Entity = entitySaveEventArgs.Entity;
            entitySaveEventArgs.EntityManager.Post();

            refreshButtonNewEntity();

            refreshGridViewEntity();

            refreshButtonDeleteEntity();

            OnAfterSaveClick(entitySaveEventArgs);
        }

        protected virtual void OnAfterSaveClick(EntitySaveEventArgs e)
        {
            if (AfterSaveClick != null)
            {
                AfterSaveClick(e);
            }

            if (!e.CancelAction && showMessageSucess && !string.IsNullOrEmpty(messageSucess))
            {
                MessageBoxInformation.Show(Parent, messageSucess);
            }
        }

        #endregion

        #region Private Methods

        private void setEntityFromControl(EntitySaveEventArgs e)
        {
            try
            {
                SetEntityFromControl.Instance.Execute(e.Entity, Parent);
            }
            catch (PropertyListException E)
            {
                if (ShowMessageExceptions)
                {
                    MessageBoxInformation.Show(Parent, E.Message);
                }
                else
                {
                    throw new PropertyListException(E.PropertyExceptions);
                }

                e.CancelAction = true;
            }
        }

        private IEntityPersistence getEntityToPersist()
        {
            IEntityPersistence entity = getEntityFromGridViewEntity();

            if (entity == null)
            {
                if (!string.IsNullOrEmpty(buttonNewEntity) && buttonNewEntityInstance != null)
                {
                    entity = buttonNewEntityInstance.EntityNew;
                }
            }

            return entity;
        }

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
                buttonNewEntityInstance.Enabled = true;
            }
        }

        private void refreshGridViewEntity()
        {
            if (GridViewEntityInstance != null)
            {
                int selectedIndex = -1;

                if (GridViewEntityInstance.SelectedRows.Count > 0)
                {
                    selectedIndex = GridViewEntityInstance.SelectedRows[0].Index;
                }

                GridViewEntityInstance.RefreshList();

                if (selectedIndex > -1)
                {
                    GridViewEntityInstance.Rows[selectedIndex].Selected = true;
                }
            }
        }

        private void refreshButtonDeleteEntity()
        {
            if (buttonDeleteEntityInstance != null)
            {
                buttonDeleteEntityInstance.Enabled = false;
            }
        }
        
        #endregion
    }
}