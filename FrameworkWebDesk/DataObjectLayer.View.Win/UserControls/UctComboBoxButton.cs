using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public partial class UctComboBoxButton : UserControl
    {
        public delegate void ButtonClickMethod();

        private ButtonClickMethod buttonClick;

        public ButtonClickMethod ButtonClick
        {
            set
            {
                buttonClick = value;
            }
        }


        public UctComboBoxButton()
        {
            InitializeComponent();
        }

        
        [Browsable(true)]
        [Category("Appearance")]
        public bool ButtonClickVisible
        {
            set { btnAcao.Visible = value; }
            get { return btnAcao.Visible; }
        }

        [Browsable(true)]
        [Category("Data")]
        public object DataSource
        {
            set { comboBoxCustomEntity.DataSource = value; }
            get { return comboBoxCustomEntity.DataSource; }
        }

        [Browsable(true)]
        [Category("Data")]
        public string DisplayMember
        {
            set { comboBoxCustomEntity.DisplayMember = value; }
            get { return comboBoxCustomEntity.DisplayMember; }
        }

        [Browsable(true)]
        [Category("Data")]
        public string ValueMember
        {
            set { comboBoxCustomEntity.ValueMember = value; }
            get { return comboBoxCustomEntity.ValueMember; }
        }

        [Browsable(false)]
        public object SelectedItem
        {
            set { comboBoxCustomEntity.SelectedItem = value; }
            get { return comboBoxCustomEntity.SelectedItem; }
        }

        [Browsable(false)]
        public new ControlBindingsCollection DataBindings
        {
            get { return comboBoxCustomEntity.DataBindings; }
        }

        [Browsable(true)]
        [Category("Behavior")]
        public bool HasNullValue
        {
            get
            {
                return comboBoxCustomEntity.HasNullValue;
            }
            set
            {
                comboBoxCustomEntity.HasNullValue = value;
            }
        }

        [Browsable(false)]
        public object ItemSelected
        {
            set { comboBoxCustomEntity.ItemSelected = value; }
            get { return comboBoxCustomEntity.ItemSelected; }
        }

        protected virtual void btnClick(object sender, EventArgs e)
        {
            if (buttonClick != null)
                buttonClick();
        }
    }
}
