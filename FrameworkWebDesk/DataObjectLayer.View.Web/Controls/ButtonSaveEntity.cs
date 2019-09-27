using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer.Reflection;


namespace DataObjectLayer.View.Web
{
    public class ButtonSaveEntity: Button
    {
        #region Fields

        private bool showMessageExceptions = true;

        private string gridViewEntity = string.Empty;
        private string buttonNewEntity = string.Empty;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        #endregion

        [Category("Entity")]
        public event BeforeSaveWebClickEvent BeforeSaveClick;

        [Category("Entity")]
        public event AfterSaveWebClickEvent AfterSaveClick;

        #region Properties
        #endregion

        private ButtonNewEntity buttonNewEntityInstance
        {
            get
            {
                return Page.FindControl(ButtonNewEntity) as ButtonNewEntity;
            }
        }

        public GridViewEntity GridViewEntityInstance
        {
            get
            {
                return Page.FindControl(GridViewEntity) as GridViewEntity;
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

        #region Save Events

        protected virtual void OnBeforeSaveClick(EntitySaveWebEventArgs e)
        {
            if (BeforeSaveClick != null)
            {
                BeforeSaveClick(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            EntitySaveWebEventArgs entitySaveEventArgs = new EntitySaveWebEventArgs();

            OnBeforeSaveClick(entitySaveEventArgs);

            // Testa se o cliente não cancelou o OnClick explicitamente.
            if (entitySaveEventArgs.CancelAction)
            {
                return;
            }

            entitySaveEventArgs.Entity = getEntityFromGridViewEntity();

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

            OnAfterSaveClick(entitySaveEventArgs);
        }

        protected virtual void OnAfterSaveClick(EntitySaveWebEventArgs e)
        {
            if (AfterSaveClick != null)
            {
                AfterSaveClick(e);
            }
        }

        #endregion

        #region Private Methods

        private void setEntityFromControl(EntitySaveWebEventArgs e)
        {
            try
            {
                SetEntityFromControl.Instance.Execute(e.Entity, Page);
            }
            catch (PropertyListException E)
            {
                if (ShowMessageExceptions)
                {
                    WebInformationDialog.Show(Page, E.Message);
                }
                else
                {
                    throw new PropertyListException(E.PropertyExceptions);
                }

                e.CancelAction = true;
            }
        }

        private IEntityPersistence getEntityFromGridViewEntity()
        {
            if (!string.IsNullOrEmpty(GridViewEntity))
            {
                DataObjectLayer.View.Web.GridViewEntity gridViewEntity = Page.FindControl(GridViewEntity) as DataObjectLayer.View.Web.GridViewEntity;

                if (gridViewEntity != null)
                {
                    return gridViewEntity.EntityCurrent;
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
                GridViewEntityInstance.RefreshList(true);
            }
        }

        #endregion
    }
}