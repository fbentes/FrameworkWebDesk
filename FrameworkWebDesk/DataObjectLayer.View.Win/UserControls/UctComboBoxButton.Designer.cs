namespace DataObjectLayer.View.Win
{
    partial class UctComboBoxButton
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
            this.components = new System.ComponentModel.Container();
            this.btnAcao = new System.Windows.Forms.Button();
            this.comboBoxCustomEntity = new DataObjectLayer.View.Win.ComboBoxCustom(this.components);
            this.SuspendLayout();
            // 
            // btnAcao
            // 
            this.btnAcao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAcao.Location = new System.Drawing.Point(237, 1);
            this.btnAcao.Name = "btnAcao";
            this.btnAcao.Size = new System.Drawing.Size(26, 21);
            this.btnAcao.TabIndex = 1;
            this.btnAcao.UseVisualStyleBackColor = true;
            this.btnAcao.Click += new System.EventHandler(this.btnClick);
            // 
            // comboBoxCustomEntity
            // 
            this.comboBoxCustomEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCustomEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustomEntity.FormattingEnabled = true;
            this.comboBoxCustomEntity.HasNullValue = false;
            this.comboBoxCustomEntity.ItemSelected = null;
            this.comboBoxCustomEntity.Location = new System.Drawing.Point(3, 2);
            this.comboBoxCustomEntity.Name = "comboBoxCustomEntity";
            this.comboBoxCustomEntity.Size = new System.Drawing.Size(232, 21);
            this.comboBoxCustomEntity.TabIndex = 0;
            // 
            // UctComboBoxButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAcao);
            this.Controls.Add(this.comboBoxCustomEntity);
            this.Name = "UctComboBoxButton";
            this.Size = new System.Drawing.Size(265, 24);
            this.ResumeLayout(false);

        }

        #endregion

        protected ComboBoxCustom comboBoxCustomEntity;
        protected System.Windows.Forms.Button btnAcao;

    }
}
