using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataObjectLayer;
using DataObjectLayer.Reflection;

namespace DataObjectLayer.View.Win
{
    public partial class FrmBaseRegister : Form
    {
        private IRegister entityManager;

        public IRegister EntityManager
        {
            get { return entityManager; }
            set { entityManager = value; }
        }

        public FrmBaseRegister()
        {
            InitializeComponent();
        }
        /*
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            LinearGradientBrush linear = new LinearGradientBrush(ClientRectangle, Color.Blue, Color.White, LinearGradientMode.Vertical);

            g.FillRectangle(linear, ClientRectangle);
        }
        */
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            mudaTextBtnSalvarParaEntityChild();
        }

        protected virtual void mudaTextBtnSalvarParaEntityChild()
        {
            if (entityManager != null && entityManager.Entity != null)
            {
                //if (EntityReflection.Instance.IsEntityChild(entityManager.Entity.GetType()))
                //{
                    if(EntityManager.Entity.IsChild)
                        btnSalvar.Text = "Ok";
                    else
                        btnSalvar.Text = "Salvar";

                //}
            }
        }

        public virtual void RefreshDataSources()
        {
            SetDataSource(entityManager.Entity);
        }

        public virtual void SetDataSource(IEntityPersistence entity)
        {
        }

        protected virtual void btnSalvar_Click(object sender, EventArgs e)
        {
            entityManager.Post();

            foreach (Control control in this.Controls)
            {
                if (control.DataBindings.Count > 0)
                {
                    control.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                }
            }

            ValidateChildren(ValidationConstraints.Visible);

            string messages = string.Empty; 

            foreach(string message in entityManager.GetMessagesValidators())
            {
                messages += message + "\n";
            }

            if (messages != string.Empty)
            {
                MessageBoxInformation.Show(this, messages);

                DialogResult = DialogResult.None;
            }
        }

        private void FrmBaseRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel)
            {
                entityManager.RollBackEdit(entityManager.Entity as EntityPersistence);
            }
        }
    }
}