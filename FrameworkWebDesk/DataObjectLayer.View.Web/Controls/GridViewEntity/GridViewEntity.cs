using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using DataObjectLayer;
using DataObjectLayer.Reflection;
using DataObjectLayer.Business;

namespace DataObjectLayer.View.Web
{
    public class GridViewEntity: GridView
    {
        public class DataBoundFieldEntity : BoundField
        {
            public int OrderColumn = OrderDefaultForColumns;
        }

        private const int OrderDefaultForColumns = -1;

        private string messageDelete = string.Empty;

        private string deleteImageUrl = string.Empty;
        private string editImageUrl = string.Empty;

        private bool hasDeleteColumn = true;
        private bool hasEditColumn = true;

        private string buttonNewEntity = string.Empty;
        private string buttonSaveEntity = string.Empty;

        private string entitySource = string.Empty;
        private string entityNamespaceSource = string.Empty;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        [Bindable(true)]
        [Category("Entity")]
        public string EntityNamespaceSource
        {
            get { return entityNamespaceSource; }
            set { entityNamespaceSource = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerNamespaceSource
        {
            get { return entityManagerNamespaceSource; }
            set { entityManagerNamespaceSource = value; }
        }

        private OrderProperty orderProperty
        {
            set
            {
                ViewState[this.UniqueID + "orderProperty"] = value;
            }
            get
            {
                return ViewState[this.UniqueID + "orderProperty"] as OrderProperty;
            }
        }

        private OrderEntity[] orders
        {
            get
            {
                if (orderProperty != null)
                {
                    return new OrderEntity[] { new OrderEntity(orderProperty.PropertyName, orderProperty.Asc) };
                }

                return null;
            }
        }

        public IEntityPersistence EntityCurrent
        {
            set
            {
                ViewState[this.UniqueID + "EntityCurrent"] = value;
            }
            get
            {
                return ViewState[this.UniqueID + "EntityCurrent"] as IEntityPersistence;
            }
        }

        private ButtonNewEntity buttonNewEntityInstance
        {
            get
            {
               return Page.FindControl(ButtonNewEntity) as ButtonNewEntity;
            }
        }

        private ButtonSaveEntity buttonSaveEntityInstance
        {
            get
            {
                return Page.FindControl(ButtonSaveEntity) as ButtonSaveEntity;
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
        public string MessageDelete
        {
            get { return messageDelete; }
            set { messageDelete = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string DeleteImageUrl
        {
            get { return deleteImageUrl; }
            set { deleteImageUrl = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EditImageUrl
        {
            get { return editImageUrl; }
            set { editImageUrl = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public bool HasDeleteColumn
        {
            get { return hasDeleteColumn; }
            set { hasDeleteColumn = value; }
        }

        [Bindable(true)]
        [Category("Entity")]
        public bool HasEditColumn
        {
            get { return hasEditColumn; }
            set { hasEditColumn = value;}
        }

        [Category("Entity")]
        public event AfterRowEditingDelegate AfterRowEditing = null;

        [Category("Entity")]
        public event BeforeRowEditingDelegate BeforeRowEditing = null;

        [Category("Entity")]
        public event BeforeRowDeletingDelegate BeforeRowDeleting = null;

        [Category("Entity")]
        public event AfterRowDeletingDelegate AfterRowDeleting = null;

        [Bindable(true)]
        [Category("Entity")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PropertyViewCollection PropertyViewEntities
        {
            set
            {
                if (value != null)
                {
                    ViewState[this.UniqueID + "PropertyViewEntities"] = value;
                }
            }
            get
            {
                return ViewState[this.UniqueID + "PropertyViewEntities"] as PropertyViewCollection;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public Color BackColorRowSelected
        {
            set
            {
                ViewState[this.UniqueID + "BackColorSelected"] = value;
            }
            get
            {
                if (ViewState[this.UniqueID + "BackColorSelected"] == null)
                {
                    BackColorRowSelected = Color.LightCyan;
                }

                return (Color)ViewState[this.UniqueID + "BackColorSelected"];
            }
        }

        public override int SelectedIndex
        {
            get
            {
                return base.SelectedIndex;
            }
            set
            {
                base.SelectedIndex = value;

                changeBackColorRowSelected(value);
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        public Color BackColorRow
        {
            set
            {
                ViewState[this.UniqueID + "BackColorRow"] = value;
            }
            get
            {
                if (ViewState[this.UniqueID + "BackColorRow"] == null)
                {
                    BackColorRow = Color.LightGray;
                }

                return (Color)ViewState[this.UniqueID + "BackColorRow"];
            }
        }

        public int WidthDefaultForColumns = 100;

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

        private Type typeEntityManager
        {
            get
            {
                if (EntityNamespaceSource == string.Empty || EntityManagerSource == string.Empty)
                {
                    return null;
                }

                return EntityReflection.Instance.GetType(EntityManagerNamespaceSource, EntityManagerSource);
            }
        }

        private IListDeleteRegister entityManagerSourceInstance
        {
            get
            {
                if (typeEntityManager != null)
                {
                    PropertyInfo prope = EntityReflection.Instance.GetFirstProperty(typeEntityManager, typeof(SingletonAttribute));

                    if (prope == null)
                    {
                        throw new EntityManagerNoConstructorException("A classe " + EntityManagerSource + " deve conter uma propriedade pública com o atributo [Singleton] !", null);
                    }

                    return prope.GetValue(null, null) as IListDeleteRegister;
                }

                return null;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerSource
        {
            set
            {
                entityManagerSource = value;
            }
            get
            {
                return entityManagerSource;
            }
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                DataKeyNames = new string[] { "Id" };

                Columns.Clear();

                configGridEntity();

                base.DataSource = value;

                DataBind();
            }
        }

        public GridViewEntity()
        {
            Font.Size = FontUnit.Smaller;
            ForeColor = Color.Black;
            HeaderStyle.BackColor = Color.LightBlue;
            HeaderStyle.ForeColor = Color.Black;
            AllowPaging = true;
            AllowSorting = true;
            AutoGenerateColumns = false;
            BackColor = Color.LightGray;
            Style.Add(HtmlTextWriterStyle.FontFamily, "arial");
            BorderStyle = BorderStyle.Solid;
            BorderColor = Color.LightBlue;
            HeaderStyle.Font.Size = new FontUnit(FontSize.Small);
        }

        #region Private Methods

        private IEntityPersistence getEntitySelecionado(IListDeleteRegister entityManager, int indice, IList listSource)
        {
            int id = Convert.ToInt32(this.DataKeys[indice].Value);

            return entityManager.Read(id, listSource);
        }

        private void changeBackColorRowSelected(int index)
        {
            foreach (GridViewRow row in Rows)
            {
                row.BackColor = BackColorRow;
            }

            if (index > -1)
            {
                Rows[index].BackColor = BackColorRowSelected;
            }
        }

        private void addColCommandButtons()
        {
            if (hasDeleteColumn)
            {
                
                CommandField btnExcluir = new CommandField();
                btnExcluir.ButtonType = ButtonType.Image;
                btnExcluir.DeleteImageUrl = "~/Images/Excluir.ico";
                btnExcluir.HeaderText = "Excluir";
                btnExcluir.DeleteText = "Excluir registro atual.";
                btnExcluir.ShowDeleteButton = true;
                //btnExcluir.Visible = false;

                Columns.Add(btnExcluir);
                 
                /*
                TemplateField delete = new TemplateField();
                delete.ItemTemplate = new DeleteTemplateField();
                delete.HeaderText = "Excluir";

                Columns.Add(delete);     
                 */ 
            }

            if (hasEditColumn)
            {
                CommandField btnEditar = new CommandField();
                btnEditar.ButtonType = ButtonType.Image;
                btnEditar.EditImageUrl = "~/Images/Editar.ico";
                btnEditar.HeaderText = "Editar";
                btnEditar.EditText = "Editar registro atual.";
                btnEditar.ShowEditButton = true;
                //btnEditar.Visible = false;

                Columns.Add(btnEditar);

                /*
                TemplateField edit = new TemplateField();
                edit.ItemTemplate = new EditTemplateField();
                edit.HeaderText = "Editar";

                Columns.Add(edit);
                 */ 
 
            }
        }

        private List<DataBoundFieldEntity> reOrderList(List<DataBoundFieldEntity> listFields)
        {
            List<DataBoundFieldEntity> returnList = new List<DataBoundFieldEntity>(listFields.Count);

            DataBoundFieldEntity fieldMenor = null;

            do
            {
                int orderMenor = 999;

                for (int i = 0; i < listFields.Count; i++)
                {
                    if (orderMenor > listFields[i].OrderColumn)
                    {
                        orderMenor = listFields[i].OrderColumn;
                        fieldMenor = listFields[i];
                    }
                }

                returnList.Add(fieldMenor);

                listFields.Remove(fieldMenor);
            }
            while (listFields.Count > 0);

            return returnList;
        }

        private void addListField(List<DataBoundFieldEntity> listFields)
        {
            int quantidadeColunasFixas = 0;

            if (HasDeleteColumn)
            {
                quantidadeColunasFixas++;
            }

            if (HasEditColumn)
            {
                quantidadeColunasFixas++;
            }

            List<DataBoundFieldEntity> orderList = reOrderList(listFields);

            foreach (DataBoundFieldEntity field in orderList)
            {
                if (field.OrderColumn != -1)
                {
                    Columns.Insert(quantidadeColunasFixas + field.OrderColumn, field);
                }
                else
                {
                    Columns.Add(field);
                }
            }
        }

        private void configGridEntity()
        {
            if (typeEntity == null || Columns.Count > 2)
                return;

            addColCommandButtons();

            Attribute[] attributes;

            bool addProperty;

            string displayName;

            int widthColumn, orderColumn;

            List<DataBoundFieldEntity> listFields = new List<DataBoundFieldEntity>();

            if (PropertyViewEntities != null && PropertyViewEntities.Count > 0)
            {
                foreach (PropertyView propView in PropertyViewEntities)
                {
                    foreach (PropertyInfo pInfo in typeEntity.GetProperties())
                    {
                        if (pInfo.Name == propView.Name)
                        {
                            DataBoundFieldEntity newField = getNewColumn(pInfo.Name, propView.DisplayName, propView.Length, propView.Position);

                            listFields.Add(newField);
                        }
                    }
                }

            }
            else
            {
                foreach (PropertyInfo pInfo in typeEntity.GetProperties())
                {
                    addProperty = true;

                    attributes = Attribute.GetCustomAttributes(pInfo);

                    if (attributes.Length > 0)
                    {
                        foreach (Attribute attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(PropertyInvisibleAttribute))
                            {
                                addProperty = false;
                                break;
                            }
                        }
                    }

                    if (!addProperty)
                    {
                        continue;
                    }

                    displayName = string.Empty;
                    widthColumn = WidthDefaultForColumns;
                    orderColumn = OrderDefaultForColumns;

                    foreach (Attribute attribute in attributes)
                    {
                        if (attribute.GetType() == typeof(DisplayNamePropertyAttribute))
                        {
                            displayName = (attribute as DisplayNamePropertyAttribute).DisplayName;
                        }
                        else
                            if (attribute.GetType() == typeof(WidthPropertyAttribute))
                            {
                                widthColumn = (attribute as WidthPropertyAttribute).Width;
                            }
                            else
                                if (attribute.GetType() == typeof(OrderPropertyAttribute))
                                {
                                    orderColumn = (attribute as OrderPropertyAttribute).Order;
                                }
                    }

                    DataBoundFieldEntity newField = getNewColumn(pInfo.Name, displayName, widthColumn, orderColumn);

                    listFields.Add(newField);
                }
            }

            addListField(listFields);
        }

        private DataBoundFieldEntity getNewColumn(string columnName, string displayNameColumn, int width, int order)
        {
            DataBoundFieldEntity field = new DataBoundFieldEntity();
            field.DataField = columnName;
            field.HeaderText = (displayNameColumn == string.Empty ? columnName : displayNameColumn);
            field.ItemStyle.Width = new Unit(width);
            field.OrderColumn = order;
            field.SortExpression = columnName;
            return field;
        }

        #endregion

        public void RefreshList()
        {
            DataSource = entityManagerSourceInstance.List(orders, null);
        }

        public void RefreshList(bool preserveSelectedIndex)
        {
            int index = SelectedIndex;

            RefreshList();

            if (preserveSelectedIndex)
            {
                SelectedIndex = index;
            }
        }

        #region Editing Events

        protected virtual void OnBeforeRowEditing(EntityWebEventArgs e)
        {
            if (BeforeRowEditing != null)
            {
                BeforeRowEditing(e);
            }
        }

        protected override void OnRowEditing(GridViewEditEventArgs e)
        {
            EntityWebEventArgs entityEventArgs = new EntityWebEventArgs(null, null, (IList)null);

            entityEventArgs.EntityManager = entityManagerSourceInstance;

            OnBeforeRowEditing(entityEventArgs);

            if (entityEventArgs != null && entityEventArgs.PageName != string.Empty)
            {
                Page.Response.Redirect(entityEventArgs.PageName + "?" + DataKeyNames[0] + typeEntity.Name + "=" + DataKeys[e.NewEditIndex].Value.ToString());

                return;
            }

            if (entityEventArgs == null)
            {
                return;
            }

            IEntityPersistence entity = getEntitySelecionado(entityEventArgs.EntityManager, e.NewEditIndex, entityEventArgs.ListSource);

            if (entity != null)
            {
                SetControlFromEntity.Execute(entity, Page, true);

                if (entityEventArgs != null)
                {
                    entityEventArgs.Entity = entity;
                }

                SelectedIndex = e.NewEditIndex;

                EntityCurrent = entity;

                if (buttonSaveEntityInstance != null)
                {
                    buttonSaveEntityInstance.Enabled = true;
                }
            }

            if (entityEventArgs == null)
            {
                entityEventArgs = new EntityWebEventArgs(null, entity);
            }

            if (buttonSaveEntityInstance != null)
            {
                buttonSaveEntityInstance.Enabled = true;
            }

            if (buttonNewEntityInstance != null)
            {
                buttonNewEntityInstance.Enabled = true;
            }

            OnAfterRowEditing(entityEventArgs);
        }

        protected virtual void OnAfterRowEditing(EntityWebEventArgs e)
        {
            if (AfterRowEditing != null)
            {
                AfterRowEditing(e);
            }
        }

        #endregion

        #region Deleting Events

        protected virtual void OnBeforeRowDeleting(EntityWebEventArgs e)
        {
            if (BeforeRowDeleting != null)
            {
                BeforeRowDeleting(e);
            }
        }

        protected override void OnRowDeleting(GridViewDeleteEventArgs e)
        {
            EntityWebEventArgs entityEventArgs = new EntityWebEventArgs(null, null);

            entityEventArgs.EntityManager = entityManagerSourceInstance;

            OnBeforeRowDeleting(entityEventArgs);

            IEntityPersistence entity = getEntitySelecionado(entityEventArgs.EntityManager, e.RowIndex, entityEventArgs.ListSource);

            if (entity != null && entityEventArgs != null)
            {
                if (entityEventArgs.ListSource != null)
                {
                    entityEventArgs.EntityManager.Delete(new EntityPersistence[] { entity as EntityPersistence }, entityEventArgs.ListSource);

                    DataSource = entityEventArgs.ListSource;
                }
                else
                {
                    entityEventArgs.EntityManager.Delete(new EntityPersistence[] { entity as EntityPersistence }, null);
                }
            }

            if (entityEventArgs == null)
            {
                entityEventArgs = new EntityWebEventArgs(null, entity);
            }

            RefreshList();

            OnAfterRowDeleting(entityEventArgs);
        }

        protected virtual void OnAfterRowDeleting(EntityWebEventArgs e)
        {
            if (AfterRowDeleting != null)
            {
                AfterRowDeleting(e);
            }
        }

        #endregion

        #region Others Events

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                if (hasDeleteColumn)
                {
                    ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                    
                    if (messageDelete == string.Empty)
                    {
                        imgDelete.Attributes.Add("onclick", "javascript:return confirm('Deseja excluir a linha selecionada ?");
                    }
                    else
                    {
                        imgDelete.Attributes.Add("onclick", "javascript:return confirm('" + messageDelete + "')");
                    }

                    if (DeleteImageUrl == string.Empty)
                    {
                        imgDelete.ImageUrl = "~/Images/Excluir.ico";
                    }
                    else
                    {
                        imgDelete.ImageUrl = DeleteImageUrl;
                    }
                }

                if (hasEditColumn)
                {
                    ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");

                    if (EditImageUrl == string.Empty)
                    {
                        imgEdit.ImageUrl = "~/Images/Editar.ico";
                    }
                    else
                    {
                        imgEdit.ImageUrl = EditImageUrl;
                    }
                }
                 */ 
            }
             
        }

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;

            RefreshList();
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            bool asc = true;

            if (orderProperty != null)
            {
                if (orderProperty.PropertyName == e.SortExpression)
                {
                    orderProperty.Asc = !orderProperty.Asc;
                }
                else
                {
                    orderProperty.PropertyName = e.SortExpression;
                    orderProperty.Asc = asc;
                }
            }
            else
            {
                orderProperty = new OrderProperty(e.SortExpression, asc);
            }

            RefreshList();
        }
        /*
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in Rows)
            {
                for (int i = 0; i < row.Cells.Count - 1; i++)
                {
                    row.Cells[i].Attributes.Add("onClick", Page.ClientScript.GetPostBackClientHyperlink(row.FindControl("btnEditar"), "", true));
                }
            }

            base.Render(writer);
        }
*/
        #endregion

        /**

   CSS
         * 1: .header {   
         * 2:     font-family:verdana; font-weight:bold;   
         * 3:     font-size:8pt; background-color:red; color:white;   
         * 4: }   
         * 5: .normal {   
         * 6:     font-family:verdana;  font-size:8pt;   
         * 7:     background-color:silver; color:black;   
         * 8: }   
         * 9: .hover {  
         * 10:     font-family:verdana;  font-size:8pt;  
         * 11:     background-color:green;  color:white;  
         * 12: }  
         * 13: .selected {  
         * 14:     font-family:verdana;  font-size:8pt;  
         * 15:     background-color:blue;  color:white;  
         * 16: }   1: protected override void Render(HtmlTextWriter writer)   
         * 2: {   
         * 3:     
         * //Aqui vamos tornar cada linha do grid gvDados clicável: quando clicarmos nela, será executado o código do botão Editar   
         * 4:     foreach (GridViewRow r in gvDados.Rows)   
         * 5:     {   
         * 6:         for (int i = 0; i < r.Cells.Count - 1; i++)   
         * 7:         {    
         * 8:             r.Cells[i].Attributes.Add("onClick", ClientScript.GetPostBackClientHyperlink(r.FindControl("btnEditar"), "", true));   
         * 9:         }  
         * 10:     }  
         * 11:     base.Render(writer);  
         * 12: }
         * 
         ***/

        /*
           1: protected void btnEditar_Click(object sender, EventArgs e)   
         * 2: {   
         * 3:     //Seta a linha selecionada com a classe CSS para destacá-la e guarda seu índice em uma variável auxiliar (no viewstate da página)   
         * 4:     gvDados.Rows[Int32.Parse((sender as Button).CommandArgument)].CssClass = "selected";   
         * 5:     ViewState["AUX"] = (sender as Button).CommandArgument;   
         * 6:        
         * 7:     //Coloca o método do botão Salvar em modo de Atualização   
         * 8:     ViewState["SALVAR"] = "U";   
         * 9:    
         * 10:     //Obtém a(s) chave(s) do registro através do índice da linha do gridview (por isso, é importante SEMPRE setar os DataKeyNames!)  
         * 11:     int key = Int32.Parse(gvDados.DataKeys[Int32.Parse((sender as Button).CommandArgument)].Value.ToString());  
         * 12:       
         * 13:     ...//Carregue o registro nos textboxes...
         * */
    }
}
