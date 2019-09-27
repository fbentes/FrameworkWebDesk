using System;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DataObjectLayer;
using DataObjectLayer.Reflection;
using DataObjectLayer.Business;

namespace DataObjectLayer.View.Win
{
    public class DataGridViewEntity : DataGridView
    {
        public event BeforeRefreshListEvent BeforeRefreshListEvent;

        private string entitySource = string.Empty;
        private string entityNamespaceSource = string.Empty;

        private string entityManagerSource = string.Empty;
        private string entityManagerNamespaceSource = string.Empty;

        private string buttonNewEntity = string.Empty;
        private string buttonDeleteEntity = string.Empty;
        private string buttonSaveEntity = string.Empty;

        private bool sortAsc = false;

        private Type typeEntityOld;

        private List<OrderEntity> orderList = new List<OrderEntity>();

        private ListFilterEntity listFilterEntity = new ListFilterEntity();

        private PropertyViewCollection propertyViewEntities = new PropertyViewCollection();

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

        [Obsolete()]
        public Type TypeEntity        
        {
            set
            {
                typeEntityOld = value;
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

        [Bindable(true)]
        [Category("Entity")]
        public string EntityManagerNamespaceSource
        {
            get { return entityManagerNamespaceSource; }
            set { entityManagerNamespaceSource = value; }
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

        [Bindable(true)]
        [Category("Entity")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<OrderEntity> OrderList
        {
            get
            {
                if (orderList == null)
                {
                    orderList = new List<OrderEntity>();
                }

                return orderList;
            }
            set
            {
                orderList = value;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListFilterEntity ListFilterEntity
        {
            get
            {
                if (listFilterEntity == null)
                {
                    listFilterEntity = new ListFilterEntity();
                }

                return listFilterEntity;
            }
            set
            {
                listFilterEntity = value;
            }
        }

        [Bindable(true)]
        [Category("Entity")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PropertyViewCollection PropertyViewEntities
        {
            set
            {
                if (value != null)
                {
                    propertyViewEntities = value;
                }
            }
            get
            {
                if (propertyViewEntities == null)
                {
                    propertyViewEntities = new PropertyViewCollection();
                }

                return propertyViewEntities;
            }
        }

        private ButtonNewEntity buttonNewEntityInstance
        {
            get
            {
                return Parent.Controls[buttonNewEntity] as ButtonNewEntity;
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

        public IEntityPersistence EntityCurrent
        {
            get
            {
                if (SelectedRows.Count == 0)
                {
                    return null;
                }

                return SelectedRows[0].DataBoundItem as IEntityPersistence;
            }
        }

        public DataGridViewEntity()
        {
            this.ReadOnly = true;
            this.AllowUserToResizeRows = true;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = true;
            this.AllowUserToOrderColumns = true;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ForeColor = Color.Black;
            this.BackgroundColor = Color.White;
            this.MultiSelect = false;

            configColor();
        }

        private void configColor()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;

            this.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        }

        [Obsolete()]
        public void SetDataSource(IList list)
        {
            DataSource = list;
        }

        private new object DataSource
        {
            set
            {
                base.DataSource = null;              
                
                base.DataSource = value;

                if (value != null)
                {
                    configGridEntity();
                }
            }
            get
            {
                return base.DataSource;
            }
        }

        private void setEntityCurrent()
        {
            bool rowSelected = SelectedRows.Count > 0;

            if (rowSelected)
            {
                SetControlFromEntity.Execute(EntityCurrent, Parent);
            }

            if (buttonNewEntityInstance != null)
            {
                buttonNewEntityInstance.Enabled = rowSelected;
            }

            if (buttonSaveEntityInstance != null)
            {
                buttonSaveEntityInstance.Enabled = rowSelected;
            }

            if (buttonDeleteEntityInstance != null)
            {
                buttonDeleteEntityInstance.Enabled = rowSelected;
            }

            setControlsToValid();
        }

        private void setControlsToValid()
        {
            foreach (Control control in Parent.Controls)
            {
                if (control is IChangeBackColor)
                {
                    (control as IChangeBackColor).ChangeBackColorValidate();
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            setEntityCurrent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            setEntityCurrent();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            setEntityCurrent();
        }

        private void configEntityColumn(PropertyInfo propertyInfo)
        {
            foreach (Attribute attr in Attribute.GetCustomAttributes(propertyInfo))
            {
                if (attr.GetType() == typeof(PropertyInvisibleAttribute))
                {
                    setColumnInVisivle(propertyInfo.Name);
                }
                else
                    if (attr.GetType() == typeof(OrderPropertyAttribute))
                    {
                        setOrderColumn(propertyInfo.Name, (attr as OrderPropertyAttribute).Order);
                    }
                    else
                        if (attr.GetType() == typeof(DisplayNamePropertyAttribute))
                        {
                            setDisplayNameColumn(propertyInfo.Name, (attr as DisplayNamePropertyAttribute).DisplayName);
                        }
                        else
                            if (attr.GetType() == typeof(WidthPropertyAttribute))
                            {
                                setDisplayNameColumn(propertyInfo.Name, (attr as WidthPropertyAttribute).Width);
                            }
            }
        }

        private void configPropertyView(PropertyView propView)
        {
            Columns[propView.Name].DisplayIndex = propView.Position;
            Columns[propView.Name].HeaderText = propView.DisplayName;
            Columns[propView.Name].Width = propView.Length;
        }

        private void configGridEntity()
        {
            if (typeEntity == null)
                return;

            if (propertyViewEntities.Count > 0)
            {
                foreach (PropertyInfo pInfo in typeEntity.GetProperties())
                {
                    bool isColumnConfig = false;

                    foreach (PropertyView propView in propertyViewEntities)
                    {
                        if (pInfo.Name == propView.Name)
                        {
                            configPropertyView(propView);
                            
                            isColumnConfig = true;
                            
                            break;
                        }
                    }

                    if (!isColumnConfig)
                    {
                        setColumnInVisivle(pInfo.Name);
                    }
                }              
            }
            else
            {
                foreach (PropertyInfo pInfo in typeEntity.GetProperties())
                {
                    configEntityColumn(pInfo);
                }
            }
        }

        private void setColumnInVisivle(string columnName)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.Name == columnName)
                {
                    column.Visible = false;
                    break;
                }
            }
        }

        private void setOrderColumn(string columnName, int order)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.Name == columnName)
                {
                    column.DisplayIndex = order;
                    break;
                }
            }
        }

        private void setDisplayNameColumn(string columnName, string displayNameColumn)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.Name == columnName)
                {
                    column.HeaderText = displayNameColumn;
                    break;
                }
            }
        }

        private void setDisplayNameColumn(string columnName, int width)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.Name == columnName)
                {
                    column.Width = width;
                    break;
                }
            }
        }

        public void ExportToExcel()
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.SelectAll();

            Process procExcel = new Process();
            procExcel.StartInfo = new ProcessStartInfo("Excel");
            procExcel.Start();
        }

        public void RefreshList()
        {
            EntityEventArgs entityEventArgs = null;

            if (BeforeRefreshListEvent != null)
            {
                entityEventArgs = new EntityEventArgs(entityManagerSourceInstance, EntityCurrent);

                BeforeRefreshListEvent(entityEventArgs);
            }

            if (entityEventArgs == null || !entityEventArgs.CancelAction)
            {
                DataSource = entityManagerSourceInstance.List((OrderList != null ? OrderList.ToArray() : null), ListFilterEntity);
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Source == "System.Drawing.Image")
            {
                e.Cancel = false;
            }
        }

        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (OrderList != null)
            {
                OrderList.Clear();
            }
            else
            {
                OrderList = new List<OrderEntity>();
            }

            sortAsc = !sortAsc;

            OrderList.Add(new OrderEntity(Columns[e.ColumnIndex].Name, sortAsc));

            RefreshList();
        }
    }
}
