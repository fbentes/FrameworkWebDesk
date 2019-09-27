namespace DataObjectLayer.View.Win
{
    partial class UctPeriodoDatas
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpbPeriodoDatas = new System.Windows.Forms.GroupBox();
            this.lblDataInicial = new System.Windows.Forms.Label();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.gpbPeriodoDatas.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbPeriodoDatas
            // 
            this.gpbPeriodoDatas.Controls.Add(this.dtpDataFinal);
            this.gpbPeriodoDatas.Controls.Add(this.label1);
            this.gpbPeriodoDatas.Controls.Add(this.lblDataInicial);
            this.gpbPeriodoDatas.Controls.Add(this.dtpDataInicial);
            this.gpbPeriodoDatas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbPeriodoDatas.Location = new System.Drawing.Point(0, 0);
            this.gpbPeriodoDatas.Name = "gpbPeriodoDatas";
            this.gpbPeriodoDatas.Size = new System.Drawing.Size(176, 81);
            this.gpbPeriodoDatas.TabIndex = 0;
            this.gpbPeriodoDatas.TabStop = false;
            this.gpbPeriodoDatas.Text = "Período de Datas";
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.AutoSize = true;
            this.lblDataInicial.Location = new System.Drawing.Point(6, 26);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(60, 13);
            this.lblDataInicial.TabIndex = 0;
            this.lblDataInicial.Text = "Data Inicial";
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(72, 22);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpDataInicial.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data Final";
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(72, 51);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpDataFinal.TabIndex = 3;
            // 
            // UctPeriodoDatas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbPeriodoDatas);
            this.Name = "UctPeriodoDatas";
            this.Size = new System.Drawing.Size(176, 81);
            this.gpbPeriodoDatas.ResumeLayout(false);
            this.gpbPeriodoDatas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbPeriodoDatas;
        private System.Windows.Forms.Label lblDataInicial;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
    }
}
