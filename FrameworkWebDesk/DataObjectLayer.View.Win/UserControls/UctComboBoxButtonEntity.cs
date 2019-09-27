using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;

namespace DataObjectLayer.View.Win
{
    public partial class UctComboBoxButtonEntity : UctComboBoxButton
    {
        private IList listEntity = null;

        private FrmBaseRegister frmBaseRegister = null;

        public FrmBaseRegister FrmBaseRegister
        {
            get { return frmBaseRegister; }
            set 
            {
                frmBaseRegister = value;

                if (frmBaseRegister != null && frmBaseRegister.EntityManager != null)
                {
                    if (listEntity == null)
                    {
                        this.DataSource = frmBaseRegister.EntityManager.List();
                    }
                }

                configToolTip();
            }
        }

        public IList ListEntity
        {
            get 
            { 
                return this.DataSource as IList; 
            }
            set 
            {
                listEntity = value;

                this.DataSource = value; 
            }
        }

        public UctComboBoxButtonEntity()
        {
            InitializeComponent();
        }

        private void configToolTip()
        {
            toolTip1.SetToolTip(btnAcao, "Incluir novo ...");
        }

        protected override void btnClick(object sender, EventArgs e)
        {
            if (frmBaseRegister == null)
                throw new ArgumentNullException("frmBaseRegister não pode ser null !");

            openRegister();
        }

        private void openRegister()
        {
            frmBaseRegister.EntityManager.CreateNewEntity();

            frmBaseRegister.SetDataSource(frmBaseRegister.EntityManager.Entity);

            if (frmBaseRegister.ShowDialog(this) == DialogResult.OK)
            {
               this.DataSource = frmBaseRegister.EntityManager.List();

               this.SelectedItem = frmBaseRegister.EntityManager.Entity;
            }
        }
    }
}

