using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataObjectLayer.View.Win
{
    public partial class UctPeriodoDatas : UserControl
    {
        private string mensagemValidacao;

        public string MensagemValidacao
        {
            get { return mensagemValidacao; }
        }

        public string Caption
        {
            set
            {
                gpbPeriodoDatas.Text = value;
            }
            get
            {
                return gpbPeriodoDatas.Text;
            }
        }

        public DateTime DataInicial
        {
            get
            {
                return dtpDataInicial.Value.Date;
            }
        }

        public DateTime DataFinal
        {
            get
            {
                return dtpDataFinal.Value.Date;
            }
        }

        public UctPeriodoDatas()
        {
            InitializeComponent();
        }

        public bool PeriodoValido
        {
            get
            {
                validaDatas();

                return mensagemValidacao == string.Empty;
            }
        }

        private void validaDatas()
        {
            mensagemValidacao = string.Empty;

            if (dtpDataInicial.Value > dtpDataFinal.Value)
                mensagemValidacao = "Data Inicial não pode ser maior que a Data Final !";
        }
    }
}
