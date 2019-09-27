using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.View;

namespace DataObjectLayer.View
{
    public partial class FrmBaseQuery: Form
    {
        private Type typeEntity;

        private IBaseRegister entityManager;

        private FrmBaseRegister frmBaseRegister;

        public Type TypeEntity
        {
            get { return typeEntity; }
            set { typeEntity = value; }
        }

        public IBaseRegister EntityManager
        {
            get { return entityManager; }
            set 
            {
                if (frmBaseRegister != null && frmBaseRegister.EntityManager == null)
                {
                    frmBaseRegister.EntityManager = value;
                }

                entityManager = value; 
            }
        }

        public FrmBaseRegister FrmBaseRegister
        {
            get { return frmBaseRegister; }
            set { frmBaseRegister = value; }
        }

        public FrmBaseQuery()
        {
            InitializeComponent();
        }

        private void excluir<T>() where T : EntityPersistence
        {
            List<T> lstEntities = new List<T>(dgvLista.SelectedRows.Count);

            DataGridViewSelectedRowCollection selectedRows = dgvLista.SelectedRows;

            IEnumerator enumerator = selectedRows.GetEnumerator();

            while (enumerator.MoveNext())
            {
                lstEntities.Add(enumerator as T);
            }

            entityManager.Delete(lstEntities.ToArray());
        }

        private void openRegister()
        {
            if (frmBaseRegister == null)
            {
                throw new Exception("FrmBaseRegister não atribuído para a consulta !");
            }

            if (frmBaseRegister.ShowDialog(this) == DialogResult.OK)
            {
                entityManager.List();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            MethodInfo metodo = typeof(FrmBaseQuery).GetMethod("excluir");
            MethodInfo metodoGenerico = metodo.MakeGenericMethod(new Type[]{typeEntity.MemberType.GetType()});
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            openRegister();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            entityManager.CreateNewEntity();

            openRegister();
        }

        protected virtual void FrmBaseQuery_Shown(object sender, EventArgs e)
        {
            if (entityManager != null)
            {
                dgvLista.DataSource = entityManager.List();
            }
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            entityManager.Entity = dgvLista.SelectedRows[0].DataBoundItem as EntityPersistence;
        }
    }
}