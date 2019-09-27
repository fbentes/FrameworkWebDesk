using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.Business;
using DataObjectLayer.View;
using DataObjectLayer.View.Win;
using DataObjectLayer.Estoque;

namespace DataObjectLayer.View.Win.Estoque
{
    public partial class FrmCadastroCliente : Form
    {
        private Cliente cliente;

        public FrmCadastroCliente()
        {
            InitializeComponent();

            NHibernateManager.Instance.CreateConfiguration("App.Config");

            setaNovoCliente();

            cmbUf.SetDataSource(UfCollection.Instance);

            cmbSexo.SetDataSource(SexoCollection.Instance);

            cmbAtivo.SetDataSource(SimNaoCollection.Instance);
        }

        private void setaNovoCliente()
        {
            cliente = new Cliente();

            SetControlFromEntity.Execute(cliente, this);

            gridCliente.TypeEntity = typeof(Cliente);
            gridCliente.SetDataSource(EntityManagerCliente.Instance.List());
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            setaNovoCliente();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SetEntityFromControl.Instance.Execute(cliente, this);
            }
            catch (PropertyListException E)
            {
                MessageBoxInformation.Show(this, E.Message);
                return;
            }

            EntityManagerCliente.Instance.Entity = cliente;
            EntityManagerCliente.Instance.Post();

            gridCliente.DataSource = EntityManagerCliente.Instance.List();
        }

        private void gridCliente_Click(object sender, EventArgs e)
        {
            if (gridCliente.SelectedRows.Count > 0)
            {
                cliente = gridCliente.SelectedRows[0].DataBoundItem as Cliente;

                SetControlFromEntity.Execute(cliente, this);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
