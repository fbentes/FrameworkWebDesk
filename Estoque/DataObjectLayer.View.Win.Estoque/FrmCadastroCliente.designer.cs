namespace DataObjectLayer.View.Win.Estoque
{
    partial class FrmCadastroCliente
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadastroCliente));
            this.btnFechar = new System.Windows.Forms.Button();
            this.gridCliente = new DataObjectLayer.View.Win.DataGridViewEntity();
            this.dtpDataNascimento = new DataObjectLayer.View.Win.DateTimeEntity(this.components);
            this.label1 = new DataObjectLayer.View.Win.LabelEntity();
            this.cmbSexo = new DataObjectLayer.View.Win.ComboBoxCustom(this.components);
            this.lblSexo = new DataObjectLayer.View.Win.LabelEntity();
            this.txtRg = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.txtCpf = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.lblRg = new DataObjectLayer.View.Win.LabelEntity();
            this.lblCpf = new DataObjectLayer.View.Win.LabelEntity();
            this.lblNome = new DataObjectLayer.View.Win.LabelEntity();
            this.txtNome = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.lblAtivo = new DataObjectLayer.View.Win.LabelEntity();
            this.cmbAtivo = new DataObjectLayer.View.Win.ComboBoxCustom(this.components);
            this.cmbUf = new DataObjectLayer.View.Win.ComboBoxCustom(this.components);
            this.lblCidade = new DataObjectLayer.View.Win.LabelEntity();
            this.txtCidade = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.txtCep = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.txtTelefone = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.lblEmail = new DataObjectLayer.View.Win.LabelEntity();
            this.txtEmail = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.lblTelefone = new DataObjectLayer.View.Win.LabelEntity();
            this.lblCep = new DataObjectLayer.View.Win.LabelEntity();
            this.lblUf = new DataObjectLayer.View.Win.LabelEntity();
            this.lblBairro = new DataObjectLayer.View.Win.LabelEntity();
            this.txtBairro = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.lblEndereco = new DataObjectLayer.View.Win.LabelEntity();
            this.txtEndereco = new DataObjectLayer.View.Win.TextBoxEntity(this.components);
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFechar
            // 
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.ForeColor = System.Drawing.Color.Black;
            this.btnFechar.Location = new System.Drawing.Point(193, 350);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 28;
            this.btnFechar.Text = "&Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // gridCliente
            // 
            this.gridCliente.AllowUserToAddRows = false;
            this.gridCliente.AllowUserToDeleteRows = false;
            this.gridCliente.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSlateGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.gridCliente.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCliente.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gridCliente.ButtonDeleteEntity = "btnExcluir";
            this.gridCliente.ButtonNewEntity = "btnNovo";
            this.gridCliente.ButtonSaveEntity = "btnSalvar";
            this.gridCliente.EntityManagerNamespaceSource = "DataObjectLayer.Estoque";
            this.gridCliente.EntityManagerSource = "EntityManagerCliente";
            this.gridCliente.EntityNamespaceSource = "DataObjectLayer.Estoque";
            this.gridCliente.EntitySource = "Cliente";
            this.gridCliente.Location = new System.Drawing.Point(11, 387);
            this.gridCliente.MultiSelect = false;
            this.gridCliente.Name = "gridCliente";
            this.gridCliente.ReadOnly = true;
            this.gridCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCliente.Size = new System.Drawing.Size(441, 167);
            this.gridCliente.TabIndex = 0;
            this.gridCliente.Click += new System.EventHandler(this.gridCliente_Click);
            // 
            // dtpDataNascimento
            // 
            this.dtpDataNascimento.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.dtpDataNascimento.BackColorValidate = System.Drawing.Color.White;
            this.dtpDataNascimento.EntityProperty = "DataNascimento";
            this.dtpDataNascimento.EntitySource = "Cliente";
            this.dtpDataNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataNascimento.IsSetEntityFromControl = true;
            this.dtpDataNascimento.Location = new System.Drawing.Point(284, 61);
            this.dtpDataNascimento.Name = "dtpDataNascimento";
            this.dtpDataNascimento.Size = new System.Drawing.Size(107, 20);
            this.dtpDataNascimento.TabIndex = 7;
            this.dtpDataNascimento.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.EntityProperty = null;
            this.label1.EntitySource = "Cliente";
            this.label1.Location = new System.Drawing.Point(214, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Data Nasc.:";
            this.label1.Visible = false;
            // 
            // cmbSexo
            // 
            this.cmbSexo.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.cmbSexo.BackColorValidate = System.Drawing.Color.White;
            this.cmbSexo.DataSource = null;
            this.cmbSexo.DisplayMember = "Value";
            this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexo.EntityProperty = "Sexo";
            this.cmbSexo.EntitySource = "Cliente";
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.HasNullValue = false;
            this.cmbSexo.IsSetEntityFromControl = true;
            this.cmbSexo.Items.AddRange(new object[] {
            ((object)(resources.GetObject("cmbSexo.Items"))),
            ((object)(resources.GetObject("cmbSexo.Items1")))});
            this.cmbSexo.ItemSelected = ((object)(resources.GetObject("cmbSexo.ItemSelected")));
            this.cmbSexo.ListNamespaceSource = "DataObjectLayer.Business";
            this.cmbSexo.ListSource = "SexoCollection";
            this.cmbSexo.Location = new System.Drawing.Point(71, 84);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(107, 21);
            this.cmbSexo.TabIndex = 9;
            this.cmbSexo.ValueMember = "Key";
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.EntityProperty = null;
            this.lblSexo.EntitySource = "Cliente";
            this.lblSexo.ForeColor = System.Drawing.Color.Black;
            this.lblSexo.Location = new System.Drawing.Point(34, 87);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(31, 13);
            this.lblSexo.TabIndex = 8;
            this.lblSexo.Text = "Sexo";
            // 
            // txtRg
            // 
            this.txtRg.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtRg.BackColorValidate = System.Drawing.Color.White;
            this.txtRg.EntityProperty = "Rg";
            this.txtRg.EntitySource = "Cliente";
            this.txtRg.IsSetEntityFromControl = true;
            this.txtRg.Location = new System.Drawing.Point(71, 58);
            this.txtRg.Mask = "99999999-9";
            this.txtRg.Name = "txtRg";
            this.txtRg.Size = new System.Drawing.Size(107, 20);
            this.txtRg.TabIndex = 5;
            // 
            // txtCpf
            // 
            this.txtCpf.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtCpf.BackColorValidate = System.Drawing.Color.White;
            this.txtCpf.EntityProperty = "Cpf";
            this.txtCpf.EntitySource = "Cliente";
            this.txtCpf.IsSetEntityFromControl = true;
            this.txtCpf.Location = new System.Drawing.Point(71, 32);
            this.txtCpf.Mask = "999.999.999-99";
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.Size = new System.Drawing.Size(107, 20);
            this.txtCpf.TabIndex = 3;
            // 
            // lblRg
            // 
            this.lblRg.AutoSize = true;
            this.lblRg.EntityProperty = null;
            this.lblRg.EntitySource = "Cliente";
            this.lblRg.ForeColor = System.Drawing.Color.Black;
            this.lblRg.Location = new System.Drawing.Point(40, 61);
            this.lblRg.Name = "lblRg";
            this.lblRg.Size = new System.Drawing.Size(23, 13);
            this.lblRg.TabIndex = 4;
            this.lblRg.Text = "RG";
            // 
            // lblCpf
            // 
            this.lblCpf.AutoSize = true;
            this.lblCpf.EntityProperty = null;
            this.lblCpf.EntitySource = "Cliente";
            this.lblCpf.ForeColor = System.Drawing.Color.Black;
            this.lblCpf.Location = new System.Drawing.Point(36, 35);
            this.lblCpf.Name = "lblCpf";
            this.lblCpf.Size = new System.Drawing.Size(27, 13);
            this.lblCpf.TabIndex = 2;
            this.lblCpf.Text = "CPF";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.EntityProperty = null;
            this.lblNome.EntitySource = "Cliente";
            this.lblNome.ForeColor = System.Drawing.Color.Black;
            this.lblNome.Location = new System.Drawing.Point(28, 9);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtNome.BackColorValidate = System.Drawing.Color.White;
            this.txtNome.EntityProperty = "Nome";
            this.txtNome.EntitySource = "Cliente";
            this.txtNome.IsSetEntityFromControl = true;
            this.txtNome.Location = new System.Drawing.Point(71, 6);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(326, 20);
            this.txtNome.TabIndex = 1;
            // 
            // lblAtivo
            // 
            this.lblAtivo.AutoSize = true;
            this.lblAtivo.EntityProperty = null;
            this.lblAtivo.EntitySource = "Cliente";
            this.lblAtivo.ForeColor = System.Drawing.Color.Black;
            this.lblAtivo.Location = new System.Drawing.Point(34, 308);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Size = new System.Drawing.Size(31, 13);
            this.lblAtivo.TabIndex = 24;
            this.lblAtivo.Text = "Ativo";
            // 
            // cmbAtivo
            // 
            this.cmbAtivo.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.cmbAtivo.BackColorValidate = System.Drawing.Color.White;
            this.cmbAtivo.DataSource = null;
            this.cmbAtivo.DisplayMember = "Value";
            this.cmbAtivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAtivo.EntityProperty = "Ativo";
            this.cmbAtivo.EntitySource = "Cliente";
            this.cmbAtivo.FormattingEnabled = true;
            this.cmbAtivo.HasNullValue = false;
            this.cmbAtivo.IsSetEntityFromControl = true;
            this.cmbAtivo.Items.AddRange(new object[] {
            ((object)(resources.GetObject("cmbAtivo.Items"))),
            ((object)(resources.GetObject("cmbAtivo.Items1")))});
            this.cmbAtivo.ItemSelected = ((object)(resources.GetObject("cmbAtivo.ItemSelected")));
            this.cmbAtivo.ListNamespaceSource = "DataObjectLayer.Business";
            this.cmbAtivo.ListSource = "SimNaoCollection";
            this.cmbAtivo.Location = new System.Drawing.Point(71, 305);
            this.cmbAtivo.Name = "cmbAtivo";
            this.cmbAtivo.Size = new System.Drawing.Size(100, 21);
            this.cmbAtivo.TabIndex = 25;
            this.cmbAtivo.ValueMember = "Key";
            // 
            // cmbUf
            // 
            this.cmbUf.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.cmbUf.BackColorValidate = System.Drawing.Color.White;
            this.cmbUf.DataSource = null;
            this.cmbUf.DisplayMember = "Value";
            this.cmbUf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUf.EntityProperty = "Uf";
            this.cmbUf.EntitySource = "Cliente";
            this.cmbUf.FormattingEnabled = true;
            this.cmbUf.HasNullValue = false;
            this.cmbUf.IsSetEntityFromControl = true;
            this.cmbUf.Items.AddRange(new object[] {
            ((object)(resources.GetObject("cmbUf.Items"))),
            ((object)(resources.GetObject("cmbUf.Items1"))),
            ((object)(resources.GetObject("cmbUf.Items2"))),
            ((object)(resources.GetObject("cmbUf.Items3"))),
            ((object)(resources.GetObject("cmbUf.Items4"))),
            ((object)(resources.GetObject("cmbUf.Items5"))),
            ((object)(resources.GetObject("cmbUf.Items6"))),
            ((object)(resources.GetObject("cmbUf.Items7"))),
            ((object)(resources.GetObject("cmbUf.Items8"))),
            ((object)(resources.GetObject("cmbUf.Items9"))),
            ((object)(resources.GetObject("cmbUf.Items10"))),
            ((object)(resources.GetObject("cmbUf.Items11"))),
            ((object)(resources.GetObject("cmbUf.Items12"))),
            ((object)(resources.GetObject("cmbUf.Items13"))),
            ((object)(resources.GetObject("cmbUf.Items14"))),
            ((object)(resources.GetObject("cmbUf.Items15"))),
            ((object)(resources.GetObject("cmbUf.Items16"))),
            ((object)(resources.GetObject("cmbUf.Items17"))),
            ((object)(resources.GetObject("cmbUf.Items18"))),
            ((object)(resources.GetObject("cmbUf.Items19"))),
            ((object)(resources.GetObject("cmbUf.Items20"))),
            ((object)(resources.GetObject("cmbUf.Items21"))),
            ((object)(resources.GetObject("cmbUf.Items22"))),
            ((object)(resources.GetObject("cmbUf.Items23"))),
            ((object)(resources.GetObject("cmbUf.Items24"))),
            ((object)(resources.GetObject("cmbUf.Items25"))),
            ((object)(resources.GetObject("cmbUf.Items26")))});
            this.cmbUf.ItemSelected = ((object)(resources.GetObject("cmbUf.ItemSelected")));
            this.cmbUf.ListNamespaceSource = "DataObjectLayer.Business";
            this.cmbUf.ListSource = "UfCollection";
            this.cmbUf.Location = new System.Drawing.Point(71, 193);
            this.cmbUf.Name = "cmbUf";
            this.cmbUf.Size = new System.Drawing.Size(107, 21);
            this.cmbUf.TabIndex = 17;
            this.cmbUf.ValueMember = "Key";
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.EntityProperty = null;
            this.lblCidade.EntitySource = "Cliente";
            this.lblCidade.ForeColor = System.Drawing.Color.Black;
            this.lblCidade.Location = new System.Drawing.Point(23, 167);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(40, 13);
            this.lblCidade.TabIndex = 14;
            this.lblCidade.Text = "Cidade";
            // 
            // txtCidade
            // 
            this.txtCidade.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtCidade.BackColorValidate = System.Drawing.Color.White;
            this.txtCidade.EntityProperty = "Cidade";
            this.txtCidade.EntitySource = "Cliente";
            this.txtCidade.IsSetEntityFromControl = true;
            this.txtCidade.Location = new System.Drawing.Point(71, 167);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(234, 20);
            this.txtCidade.TabIndex = 15;
            // 
            // txtCep
            // 
            this.txtCep.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtCep.BackColorValidate = System.Drawing.Color.White;
            this.txtCep.EntityProperty = "Cep";
            this.txtCep.EntitySource = "Cliente";
            this.txtCep.IsSetEntityFromControl = true;
            this.txtCep.Location = new System.Drawing.Point(71, 221);
            this.txtCep.Mask = "99.999-999";
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(98, 20);
            this.txtCep.TabIndex = 19;
            // 
            // txtTelefone
            // 
            this.txtTelefone.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtTelefone.BackColorValidate = System.Drawing.Color.White;
            this.txtTelefone.EntityProperty = "Telefone";
            this.txtTelefone.EntitySource = "Cliente";
            this.txtTelefone.IsSetEntityFromControl = true;
            this.txtTelefone.Location = new System.Drawing.Point(71, 250);
            this.txtTelefone.Mask = "(99)9999-9999";
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(98, 20);
            this.txtTelefone.TabIndex = 21;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.EntityProperty = null;
            this.lblEmail.EntitySource = "Cliente";
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(31, 282);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 22;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtEmail.BackColorValidate = System.Drawing.Color.White;
            this.txtEmail.EntityProperty = "Email";
            this.txtEmail.EntitySource = "Cliente";
            this.txtEmail.IsSetEntityFromControl = true;
            this.txtEmail.Location = new System.Drawing.Point(71, 279);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320, 20);
            this.txtEmail.TabIndex = 23;
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.EntityProperty = null;
            this.lblTelefone.EntitySource = "Cliente";
            this.lblTelefone.ForeColor = System.Drawing.Color.Black;
            this.lblTelefone.Location = new System.Drawing.Point(14, 253);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(49, 13);
            this.lblTelefone.TabIndex = 20;
            this.lblTelefone.Text = "Telefone";
            // 
            // lblCep
            // 
            this.lblCep.AutoSize = true;
            this.lblCep.EntityProperty = null;
            this.lblCep.EntitySource = "Cliente";
            this.lblCep.ForeColor = System.Drawing.Color.Black;
            this.lblCep.Location = new System.Drawing.Point(35, 224);
            this.lblCep.Name = "lblCep";
            this.lblCep.Size = new System.Drawing.Size(28, 13);
            this.lblCep.TabIndex = 18;
            this.lblCep.Text = "CEP";
            // 
            // lblUf
            // 
            this.lblUf.AutoSize = true;
            this.lblUf.EntityProperty = null;
            this.lblUf.EntitySource = "Cliente";
            this.lblUf.ForeColor = System.Drawing.Color.Black;
            this.lblUf.Location = new System.Drawing.Point(42, 196);
            this.lblUf.Name = "lblUf";
            this.lblUf.Size = new System.Drawing.Size(21, 13);
            this.lblUf.TabIndex = 16;
            this.lblUf.Text = "UF";
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.EntityProperty = null;
            this.lblBairro.EntitySource = "Cliente";
            this.lblBairro.ForeColor = System.Drawing.Color.Black;
            this.lblBairro.Location = new System.Drawing.Point(29, 138);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(34, 13);
            this.lblBairro.TabIndex = 12;
            this.lblBairro.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtBairro.BackColorValidate = System.Drawing.Color.White;
            this.txtBairro.EntityProperty = "Bairro";
            this.txtBairro.EntitySource = "Cliente";
            this.txtBairro.IsSetEntityFromControl = true;
            this.txtBairro.Location = new System.Drawing.Point(71, 138);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(234, 20);
            this.txtBairro.TabIndex = 13;
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.EntityProperty = null;
            this.lblEndereco.EntitySource = "Cliente";
            this.lblEndereco.ForeColor = System.Drawing.Color.Black;
            this.lblEndereco.Location = new System.Drawing.Point(10, 115);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(53, 13);
            this.lblEndereco.TabIndex = 10;
            this.lblEndereco.Text = "Endereço";
            // 
            // txtEndereco
            // 
            this.txtEndereco.BackColorInvalidate = System.Drawing.Color.Yellow;
            this.txtEndereco.BackColorValidate = System.Drawing.Color.White;
            this.txtEndereco.EntityProperty = "Endereco";
            this.txtEndereco.EntitySource = "Cliente";
            this.txtEndereco.IsSetEntityFromControl = true;
            this.txtEndereco.Location = new System.Drawing.Point(71, 112);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(326, 20);
            this.txtEndereco.TabIndex = 11;
            // 
            // btnNovo
            // 
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.ForeColor = System.Drawing.Color.Black;
            this.btnNovo.Location = new System.Drawing.Point(17, 350);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(75, 23);
            this.btnNovo.TabIndex = 29;
            this.btnNovo.Text = "&Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.Black;
            this.btnSalvar.Location = new System.Drawing.Point(103, 350);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 30;
            this.btnSalvar.Text = "&Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // FrmCadastroCliente
            // 
            this.ClientSize = new System.Drawing.Size(464, 566);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.gridCliente);
            this.Controls.Add(this.dtpDataNascimento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSexo);
            this.Controls.Add(this.lblSexo);
            this.Controls.Add(this.txtRg);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.lblRg);
            this.Controls.Add(this.lblCpf);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblAtivo);
            this.Controls.Add(this.cmbAtivo);
            this.Controls.Add(this.cmbUf);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.txtCep);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.lblCep);
            this.Controls.Add(this.lblUf);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.txtEndereco);
            this.Name = "FrmCadastroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            ((System.ComponentModel.ISupportInitialize)(this.gridCliente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected DataObjectLayer.View.Win.LabelEntity lblBairro;
        protected DataObjectLayer.View.Win.TextBoxEntity txtBairro;
        protected DataObjectLayer.View.Win.LabelEntity lblEndereco;
        protected DataObjectLayer.View.Win.TextBoxEntity txtEndereco;
        protected DataObjectLayer.View.Win.LabelEntity lblTelefone;
        protected DataObjectLayer.View.Win.LabelEntity lblCep;
        protected DataObjectLayer.View.Win.LabelEntity lblUf;
        protected DataObjectLayer.View.Win.LabelEntity lblEmail;
        protected DataObjectLayer.View.Win.TextBoxEntity txtEmail;
        protected DataObjectLayer.View.Win.TextBoxEntity txtTelefone;
        protected DataObjectLayer.View.Win.TextBoxEntity txtCep;
        protected DataObjectLayer.View.Win.LabelEntity lblCidade;
        protected DataObjectLayer.View.Win.TextBoxEntity txtCidade;
        protected DataObjectLayer.View.Win.ComboBoxCustom cmbUf;
        protected DataObjectLayer.View.Win.LabelEntity lblAtivo;
        protected DataObjectLayer.View.Win.ComboBoxCustom cmbAtivo;
        private DataObjectLayer.View.Win.DateTimeEntity dtpDataNascimento;
        protected DataObjectLayer.View.Win.LabelEntity label1;
        protected DataObjectLayer.View.Win.ComboBoxCustom cmbSexo;
        protected DataObjectLayer.View.Win.LabelEntity lblSexo;
        protected DataObjectLayer.View.Win.TextBoxEntity txtRg;
        protected DataObjectLayer.View.Win.TextBoxEntity txtCpf;
        protected DataObjectLayer.View.Win.LabelEntity lblRg;
        protected DataObjectLayer.View.Win.LabelEntity lblCpf;
        protected DataObjectLayer.View.Win.LabelEntity lblNome;
        protected DataObjectLayer.View.Win.TextBoxEntity txtNome;
        private DataObjectLayer.View.Win.DataGridViewEntity gridCliente;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
    }
}
