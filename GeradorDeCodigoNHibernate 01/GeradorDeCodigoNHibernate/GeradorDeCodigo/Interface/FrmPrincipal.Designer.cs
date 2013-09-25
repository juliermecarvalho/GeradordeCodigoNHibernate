namespace GeradorDeCodigo
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.btnGerar = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkEntidades = new System.Windows.Forms.CheckBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.chkWindwsAuthentication = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Authentication = new System.Windows.Forms.GroupBox();
            this.chkRepositorio = new System.Windows.Forms.CheckBox();
            this.btnTesteConexao = new System.Windows.Forms.Button();
            this.btnServeName = new System.Windows.Forms.Button();
            this.cmbServeName = new System.Windows.Forms.ComboBox();
            this.cmbDataBase = new System.Windows.Forms.ComboBox();
            this.chkGeraNHirbenateHelper = new System.Windows.Forms.CheckBox();
            this.cmbLazy = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRetiraS = new System.Windows.Forms.CheckBox();
            this.btnListarTabelas = new System.Windows.Forms.Button();
            this.chkTabelas = new System.Windows.Forms.CheckedListBox();
            this.btnTodos = new System.Windows.Forms.Button();
            this.btnNenhum = new System.Windows.Forms.Button();
            this.chkPropriedadesAutomaticas = new System.Windows.Forms.CheckBox();
            this.tbcBD = new System.Windows.Forms.TabControl();
            this.tbpSqlServer = new System.Windows.Forms.TabPage();
            this.tcpOracle = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSenhaOralce = new System.Windows.Forms.TextBox();
            this.txtNomeUsuario = new System.Windows.Forms.TextBox();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.txtLocalHost = new System.Windows.Forms.TextBox();
            this.Authentication.SuspendLayout();
            this.tbcBD.SuspendLayout();
            this.tbpSqlServer.SuspendLayout();
            this.tcpOracle.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerar.Location = new System.Drawing.Point(699, 608);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(140, 22);
            this.btnGerar.TabIndex = 1;
            this.btnGerar.Text = "Gerar Arquivos";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(1, 637);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(837, 21);
            this.progressBar.TabIndex = 1;
            // 
            // chkEntidades
            // 
            this.chkEntidades.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEntidades.AutoSize = true;
            this.chkEntidades.Checked = true;
            this.chkEntidades.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEntidades.Location = new System.Drawing.Point(683, 56);
            this.chkEntidades.Name = "chkEntidades";
            this.chkEntidades.Size = new System.Drawing.Size(99, 17);
            this.chkEntidades.TabIndex = 13;
            this.chkEntidades.Text = "Gera Entidades";
            this.chkEntidades.UseVisualStyleBackColor = true;
            // 
            // txtLogin
            // 
            this.txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogin.Enabled = false;
            this.txtLogin.Location = new System.Drawing.Point(11, 56);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(610, 20);
            this.txtLogin.TabIndex = 4;
            // 
            // txtSenha
            // 
            this.txtSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenha.Enabled = false;
            this.txtSenha.Location = new System.Drawing.Point(11, 102);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(610, 20);
            this.txtSenha.TabIndex = 5;
            // 
            // chkWindwsAuthentication
            // 
            this.chkWindwsAuthentication.AutoSize = true;
            this.chkWindwsAuthentication.Checked = true;
            this.chkWindwsAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindwsAuthentication.Location = new System.Drawing.Point(11, 20);
            this.chkWindwsAuthentication.Name = "chkWindwsAuthentication";
            this.chkWindwsAuthentication.Size = new System.Drawing.Size(163, 17);
            this.chkWindwsAuthentication.TabIndex = 3;
            this.chkWindwsAuthentication.Text = "Use Windows Authentication";
            this.chkWindwsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindwsAuthentication.CheckedChanged += new System.EventHandler(this.chkWindwsAuthentication_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sever Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Senha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Data Base Name";
            // 
            // Authentication
            // 
            this.Authentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Authentication.Controls.Add(this.txtLogin);
            this.Authentication.Controls.Add(this.txtSenha);
            this.Authentication.Controls.Add(this.label3);
            this.Authentication.Controls.Add(this.chkWindwsAuthentication);
            this.Authentication.Controls.Add(this.label2);
            this.Authentication.Location = new System.Drawing.Point(10, 47);
            this.Authentication.Name = "Authentication";
            this.Authentication.Size = new System.Drawing.Size(637, 145);
            this.Authentication.TabIndex = 6;
            this.Authentication.TabStop = false;
            this.Authentication.Text = "Authentication";
            // 
            // chkRepositorio
            // 
            this.chkRepositorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRepositorio.AutoSize = true;
            this.chkRepositorio.Checked = true;
            this.chkRepositorio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRepositorio.Location = new System.Drawing.Point(683, 76);
            this.chkRepositorio.Name = "chkRepositorio";
            this.chkRepositorio.Size = new System.Drawing.Size(105, 17);
            this.chkRepositorio.TabIndex = 14;
            this.chkRepositorio.Text = "Gera Repositório";
            this.chkRepositorio.UseVisualStyleBackColor = true;
            // 
            // btnTesteConexao
            // 
            this.btnTesteConexao.Location = new System.Drawing.Point(4, 284);
            this.btnTesteConexao.Name = "btnTesteConexao";
            this.btnTesteConexao.Size = new System.Drawing.Size(127, 23);
            this.btnTesteConexao.TabIndex = 7;
            this.btnTesteConexao.Text = "Testa Conexão";
            this.btnTesteConexao.UseVisualStyleBackColor = true;
            this.btnTesteConexao.Click += new System.EventHandler(this.btnTesteConexao_Click);
            // 
            // btnServeName
            // 
            this.btnServeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServeName.Location = new System.Drawing.Point(553, 18);
            this.btnServeName.Name = "btnServeName";
            this.btnServeName.Size = new System.Drawing.Size(94, 23);
            this.btnServeName.TabIndex = 1;
            this.btnServeName.Text = "Refresh";
            this.btnServeName.UseVisualStyleBackColor = true;
            this.btnServeName.Click += new System.EventHandler(this.btnServeName_Click);
            // 
            // cmbServeName
            // 
            this.cmbServeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbServeName.FormattingEnabled = true;
            this.cmbServeName.Location = new System.Drawing.Point(10, 20);
            this.cmbServeName.Name = "cmbServeName";
            this.cmbServeName.Size = new System.Drawing.Size(538, 21);
            this.cmbServeName.TabIndex = 2;
            this.cmbServeName.SelectedIndexChanged += new System.EventHandler(this.cmbServeName_SelectedIndexChanged);
            // 
            // cmbDataBase
            // 
            this.cmbDataBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDataBase.FormattingEnabled = true;
            this.cmbDataBase.Location = new System.Drawing.Point(10, 212);
            this.cmbDataBase.Name = "cmbDataBase";
            this.cmbDataBase.Size = new System.Drawing.Size(639, 21);
            this.cmbDataBase.TabIndex = 6;
            // 
            // chkGeraNHirbenateHelper
            // 
            this.chkGeraNHirbenateHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGeraNHirbenateHelper.AutoSize = true;
            this.chkGeraNHirbenateHelper.Checked = true;
            this.chkGeraNHirbenateHelper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGeraNHirbenateHelper.Location = new System.Drawing.Point(683, 33);
            this.chkGeraNHirbenateHelper.Name = "chkGeraNHirbenateHelper";
            this.chkGeraNHirbenateHelper.Size = new System.Drawing.Size(122, 17);
            this.chkGeraNHirbenateHelper.TabIndex = 12;
            this.chkGeraNHirbenateHelper.Text = "Gera NHiberna Help";
            this.chkGeraNHirbenateHelper.UseVisualStyleBackColor = true;
            // 
            // cmbLazy
            // 
            this.cmbLazy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLazy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLazy.FormattingEnabled = true;
            this.cmbLazy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbLazy.Items.AddRange(new object[] {
            "TRUE",
            "FALSE"});
            this.cmbLazy.Location = new System.Drawing.Point(683, 120);
            this.cmbLazy.Name = "cmbLazy";
            this.cmbLazy.Size = new System.Drawing.Size(121, 21);
            this.cmbLazy.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(680, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Lazy";
            // 
            // chkRetiraS
            // 
            this.chkRetiraS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRetiraS.AutoSize = true;
            this.chkRetiraS.Checked = true;
            this.chkRetiraS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRetiraS.Location = new System.Drawing.Point(683, 146);
            this.chkRetiraS.Name = "chkRetiraS";
            this.chkRetiraS.Size = new System.Drawing.Size(154, 17);
            this.chkRetiraS.TabIndex = 16;
            this.chkRetiraS.Text = "Usar Entidades no Singular";
            this.chkRetiraS.UseVisualStyleBackColor = true;
            // 
            // btnListarTabelas
            // 
            this.btnListarTabelas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnListarTabelas.Location = new System.Drawing.Point(1, 609);
            this.btnListarTabelas.Name = "btnListarTabelas";
            this.btnListarTabelas.Size = new System.Drawing.Size(140, 21);
            this.btnListarTabelas.TabIndex = 0;
            this.btnListarTabelas.Text = "Listar Tabelas";
            this.btnListarTabelas.UseVisualStyleBackColor = true;
            this.btnListarTabelas.Click += new System.EventHandler(this.btnListarTabelas_Click);
            // 
            // chkTabelas
            // 
            this.chkTabelas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTabelas.CheckOnClick = true;
            this.chkTabelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTabelas.FormattingEnabled = true;
            this.chkTabelas.Location = new System.Drawing.Point(4, 328);
            this.chkTabelas.Name = "chkTabelas";
            this.chkTabelas.Size = new System.Drawing.Size(746, 260);
            this.chkTabelas.TabIndex = 18;
            this.chkTabelas.ThreeDCheckBoxes = true;
            // 
            // btnTodos
            // 
            this.btnTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTodos.Location = new System.Drawing.Point(756, 328);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(84, 23);
            this.btnTodos.TabIndex = 18;
            this.btnTodos.Text = "Todos";
            this.btnTodos.UseVisualStyleBackColor = true;
            this.btnTodos.Click += new System.EventHandler(this.btnTodos_Click);
            // 
            // btnNenhum
            // 
            this.btnNenhum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNenhum.Location = new System.Drawing.Point(756, 357);
            this.btnNenhum.Name = "btnNenhum";
            this.btnNenhum.Size = new System.Drawing.Size(84, 23);
            this.btnNenhum.TabIndex = 19;
            this.btnNenhum.Text = "Nenhum";
            this.btnNenhum.UseVisualStyleBackColor = true;
            this.btnNenhum.Click += new System.EventHandler(this.Nenhum_Click);
            // 
            // chkPropriedadesAutomaticas
            // 
            this.chkPropriedadesAutomaticas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPropriedadesAutomaticas.AutoSize = true;
            this.chkPropriedadesAutomaticas.Checked = true;
            this.chkPropriedadesAutomaticas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPropriedadesAutomaticas.Location = new System.Drawing.Point(682, 170);
            this.chkPropriedadesAutomaticas.Name = "chkPropriedadesAutomaticas";
            this.chkPropriedadesAutomaticas.Size = new System.Drawing.Size(149, 17);
            this.chkPropriedadesAutomaticas.TabIndex = 17;
            this.chkPropriedadesAutomaticas.Text = "Propriedades Automáticas";
            this.chkPropriedadesAutomaticas.UseVisualStyleBackColor = true;
            // 
            // tbcBD
            // 
            this.tbcBD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcBD.Controls.Add(this.tbpSqlServer);
            this.tbcBD.Controls.Add(this.tcpOracle);
            this.tbcBD.Location = new System.Drawing.Point(4, 11);
            this.tbcBD.Name = "tbcBD";
            this.tbcBD.SelectedIndex = 0;
            this.tbcBD.Size = new System.Drawing.Size(668, 267);
            this.tbcBD.TabIndex = 0;
            // 
            // tbpSqlServer
            // 
            this.tbpSqlServer.Controls.Add(this.label1);
            this.tbpSqlServer.Controls.Add(this.label4);
            this.tbpSqlServer.Controls.Add(this.Authentication);
            this.tbpSqlServer.Controls.Add(this.cmbServeName);
            this.tbpSqlServer.Controls.Add(this.cmbDataBase);
            this.tbpSqlServer.Controls.Add(this.btnServeName);
            this.tbpSqlServer.Location = new System.Drawing.Point(4, 22);
            this.tbpSqlServer.Name = "tbpSqlServer";
            this.tbpSqlServer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSqlServer.Size = new System.Drawing.Size(660, 241);
            this.tbpSqlServer.TabIndex = 0;
            this.tbpSqlServer.Text = "SqlServer";
            this.tbpSqlServer.UseVisualStyleBackColor = true;
            // 
            // tcpOracle
            // 
            this.tcpOracle.Controls.Add(this.label11);
            this.tcpOracle.Controls.Add(this.label9);
            this.tcpOracle.Controls.Add(this.label8);
            this.tcpOracle.Controls.Add(this.label7);
            this.tcpOracle.Controls.Add(this.label6);
            this.tcpOracle.Controls.Add(this.txtSenhaOralce);
            this.tcpOracle.Controls.Add(this.txtNomeUsuario);
            this.tcpOracle.Controls.Add(this.txtSID);
            this.tcpOracle.Controls.Add(this.txtPorta);
            this.tcpOracle.Controls.Add(this.txtLocalHost);
            this.tcpOracle.Location = new System.Drawing.Point(4, 22);
            this.tcpOracle.Name = "tcpOracle";
            this.tcpOracle.Padding = new System.Windows.Forms.Padding(3);
            this.tcpOracle.Size = new System.Drawing.Size(660, 241);
            this.tcpOracle.TabIndex = 1;
            this.tcpOracle.Text = "Oracle";
            this.tcpOracle.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "SID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Porta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Senha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nome do Host";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Nome do Usuário";
            // 
            // txtSenhaOralce
            // 
            this.txtSenhaOralce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenhaOralce.Location = new System.Drawing.Point(9, 198);
            this.txtSenhaOralce.Name = "txtSenhaOralce";
            this.txtSenhaOralce.PasswordChar = '*';
            this.txtSenhaOralce.Size = new System.Drawing.Size(637, 20);
            this.txtSenhaOralce.TabIndex = 4;
            // 
            // txtNomeUsuario
            // 
            this.txtNomeUsuario.AcceptsTab = true;
            this.txtNomeUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeUsuario.Location = new System.Drawing.Point(9, 157);
            this.txtNomeUsuario.Name = "txtNomeUsuario";
            this.txtNomeUsuario.Size = new System.Drawing.Size(637, 20);
            this.txtNomeUsuario.TabIndex = 3;
            // 
            // txtSID
            // 
            this.txtSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSID.Location = new System.Drawing.Point(9, 110);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(637, 20);
            this.txtSID.TabIndex = 2;
            this.txtSID.Text = "xe";
            // 
            // txtPorta
            // 
            this.txtPorta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPorta.Location = new System.Drawing.Point(9, 64);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(637, 20);
            this.txtPorta.TabIndex = 1;
            this.txtPorta.Text = "1521";
            // 
            // txtLocalHost
            // 
            this.txtLocalHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalHost.Location = new System.Drawing.Point(9, 21);
            this.txtLocalHost.Name = "txtLocalHost";
            this.txtLocalHost.Size = new System.Drawing.Size(637, 20);
            this.txtLocalHost.TabIndex = 0;
            this.txtLocalHost.Text = "localhost";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 673);
            this.Controls.Add(this.tbcBD);
            this.Controls.Add(this.chkPropriedadesAutomaticas);
            this.Controls.Add(this.btnNenhum);
            this.Controls.Add(this.btnTesteConexao);
            this.Controls.Add(this.btnTodos);
            this.Controls.Add(this.chkTabelas);
            this.Controls.Add(this.btnListarTabelas);
            this.Controls.Add(this.chkRetiraS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbLazy);
            this.Controls.Add(this.chkGeraNHirbenateHelper);
            this.Controls.Add(this.chkRepositorio);
            this.Controls.Add(this.chkEntidades);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnGerar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NHGeneration - Geração Automática de Código para NHibernate";
            this.Authentication.ResumeLayout(false);
            this.Authentication.PerformLayout();
            this.tbcBD.ResumeLayout(false);
            this.tbpSqlServer.ResumeLayout(false);
            this.tbpSqlServer.PerformLayout();
            this.tcpOracle.ResumeLayout(false);
            this.tcpOracle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox chkEntidades;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.CheckBox chkWindwsAuthentication;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox Authentication;
        private System.Windows.Forms.CheckBox chkRepositorio;
        private System.Windows.Forms.Button btnTesteConexao;
        private System.Windows.Forms.Button btnServeName;
        private System.Windows.Forms.ComboBox cmbServeName;
        private System.Windows.Forms.ComboBox cmbDataBase;
        private System.Windows.Forms.CheckBox chkGeraNHirbenateHelper;
        private System.Windows.Forms.ComboBox cmbLazy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkRetiraS;
        private System.Windows.Forms.Button btnListarTabelas;
        private System.Windows.Forms.CheckedListBox chkTabelas;
        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.Button btnNenhum;
        private System.Windows.Forms.CheckBox chkPropriedadesAutomaticas;
        private System.Windows.Forms.TabControl tbcBD;
        private System.Windows.Forms.TabPage tbpSqlServer;
        private System.Windows.Forms.TabPage tcpOracle;
        private System.Windows.Forms.TextBox txtLocalHost;
        private System.Windows.Forms.TextBox txtNomeUsuario;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSenhaOralce;
    }
}