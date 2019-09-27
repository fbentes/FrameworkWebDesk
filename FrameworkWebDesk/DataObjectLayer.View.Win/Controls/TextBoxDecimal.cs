using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public partial class TextBoxDecimal : TextBoxEntity
    {
        private int tamanoDaParteInteira = 10;

        private int tamanoDaParteDecimal = 2;

        private char separadorDecimal = ',';

        private decimal maxValue = -1;

        public decimal MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public char SeparadorDecimal
        {
            get { return separadorDecimal; }
            set { separadorDecimal = value; }
        }

        public int TamanoDaParteInteira
        {
            get { return tamanoDaParteInteira; }
            set { tamanoDaParteInteira = value; }
        }

        public int TamanoDaParteDecimal
        {
            get { return tamanoDaParteDecimal; }
            set { tamanoDaParteDecimal = value; }
        }

        public TextBoxDecimal()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (maxValue > -1 && maxValue < Convert.ToDecimal(this.Text))
            {
                MessageBoxInformation.Show(this, "O número máximo permitido é " + maxValue.ToString());

                this.Focus();
            }
        }

        protected virtual void TextBoxDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strCampo = ((TextBox)sender).Text;

            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                if (e.KeyChar == separadorDecimal && ((TextBox)sender).Text.IndexOf(separadorDecimal) == -1)
                {
                    if (strCampo.Trim() == String.Empty)
                    {
                        ((TextBox)sender).Text += "0";

                        ((TextBox)sender).SelectionStart = 2;
                    }
                }
                else
                {
                    if (!(e.KeyChar == '-' && strCampo.Trim().Length == 0))
                    {
                        e.Handled = true;
                    }
                }
            }
            else
                if (Char.IsNumber(e.KeyChar))
                {
                    bool encontrouSeparador = strCampo.IndexOf(separadorDecimal) != -1;

                    if (strCampo.Replace(separadorDecimal.ToString(), String.Empty).Length == tamanoDaParteInteira + (encontrouSeparador ? tamanoDaParteDecimal : 0))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (this.SelectedText != string.Empty)
                           strCampo = strCampo.Replace(this.SelectedText, "");

                        if (encontrouSeparador && strCampo.Length - this.SelectionLength - strCampo.IndexOf(separadorDecimal) > tamanoDaParteDecimal)
                        {
                            e.Handled = true;
                        }
                    }
                }
        }
    }
}