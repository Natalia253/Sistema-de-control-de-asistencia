namespace CapaPresentacion.Formularios
{
    partial class Usuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlListado = new System.Windows.Forms.Panel();
            this.dGVUsuarios = new System.Windows.Forms.DataGridView();
            this.pnlEditarUsuario = new System.Windows.Forms.Panel();
            this.tblEditar = new System.Windows.Forms.TableLayoutPanel();
            this.lblEditarTitulo = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.txtDepartamento = new System.Windows.Forms.TextBox();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVUsuarios)).BeginInit();
            this.pnlEditarUsuario.SuspendLayout();
            this.tblEditar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(12);
            this.pnlHeader.Size = new System.Drawing.Size(1060, 64);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(210, 28);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios Registrados";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Controls.Add(this.pnlListado);
            this.pnlContent.Controls.Add(this.pnlEditarUsuario);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12);
            this.pnlContent.Size = new System.Drawing.Size(1060, 516);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlListado
            // 
            this.pnlListado.Controls.Add(this.dGVUsuarios);
            this.pnlListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListado.Location = new System.Drawing.Point(12, 12);
            this.pnlListado.Name = "pnlListado";
            this.pnlListado.Padding = new System.Windows.Forms.Padding(8);
            this.pnlListado.Size = new System.Drawing.Size(712, 492);
            this.pnlListado.TabIndex = 0;
            // 
            // dGVUsuarios
            // 
            this.dGVUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dGVUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGVUsuarios.ColumnHeadersHeight = 29;
            this.dGVUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dGVUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVUsuarios.EnableHeadersVisualStyles = false;
            this.dGVUsuarios.Location = new System.Drawing.Point(8, 8);
            this.dGVUsuarios.Name = "dGVUsuarios";
            this.dGVUsuarios.ReadOnly = true;
            this.dGVUsuarios.RowHeadersVisible = false;
            this.dGVUsuarios.RowHeadersWidth = 51;
            this.dGVUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVUsuarios.Size = new System.Drawing.Size(696, 476);
            this.dGVUsuarios.TabIndex = 0;
            this.dGVUsuarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVUsuarios_CellDoubleClick);
            // 
            // pnlEditarUsuario
            // 
            this.pnlEditarUsuario.BackColor = System.Drawing.Color.White;
            this.pnlEditarUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditarUsuario.Controls.Add(this.tblEditar);
            this.pnlEditarUsuario.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlEditarUsuario.Location = new System.Drawing.Point(724, 12);
            this.pnlEditarUsuario.Name = "pnlEditarUsuario";
            this.pnlEditarUsuario.Padding = new System.Windows.Forms.Padding(12);
            this.pnlEditarUsuario.Size = new System.Drawing.Size(324, 492);
            this.pnlEditarUsuario.TabIndex = 1;
            // 
            // tblEditar
            // 
            this.tblEditar.ColumnCount = 1;
            this.tblEditar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditar.Controls.Add(this.lblEditarTitulo, 0, 0);
            this.tblEditar.Controls.Add(this.lblUserID, 0, 1);
            this.tblEditar.Controls.Add(this.txtUserID, 0, 2);
            this.tblEditar.Controls.Add(this.lblNombre, 0, 3);
            this.tblEditar.Controls.Add(this.txtNombre, 0, 4);
            this.tblEditar.Controls.Add(this.lblDepartamento, 0, 5);
            this.tblEditar.Controls.Add(this.txtDepartamento, 0, 6);
            this.tblEditar.Controls.Add(this.btnGuardarCambios, 0, 8);
            this.tblEditar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditar.Location = new System.Drawing.Point(12, 12);
            this.tblEditar.Margin = new System.Windows.Forms.Padding(0);
            this.tblEditar.Name = "tblEditar";
            this.tblEditar.RowCount = 10;
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblEditar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditar.Size = new System.Drawing.Size(298, 466);
            this.tblEditar.TabIndex = 0;
            // 
            // lblEditarTitulo
            // 
            this.lblEditarTitulo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEditarTitulo.AutoSize = true;
            this.lblEditarTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEditarTitulo.Location = new System.Drawing.Point(3, 6);
            this.lblEditarTitulo.Name = "lblEditarTitulo";
            this.lblEditarTitulo.Size = new System.Drawing.Size(123, 23);
            this.lblEditarTitulo.TabIndex = 0;
            this.lblEditarTitulo.Text = "Editar Usuario";
            // 
            // lblUserID
            // 
            this.lblUserID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(3, 38);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(52, 16);
            this.lblUserID.TabIndex = 1;
            this.lblUserID.Text = "User ID";
            // 
            // txtUserID
            // 
            this.txtUserID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserID.Location = new System.Drawing.Point(3, 61);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(292, 22);
            this.txtUserID.TabIndex = 2;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(3, 90);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(56, 16);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.Location = new System.Drawing.Point(3, 113);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(292, 22);
            this.txtNombre.TabIndex = 4;
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDepartamento.AutoSize = true;
            this.lblDepartamento.Location = new System.Drawing.Point(3, 142);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(93, 16);
            this.lblDepartamento.TabIndex = 5;
            this.lblDepartamento.Text = "Departamento";
            // 
            // txtDepartamento
            // 
            this.txtDepartamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepartamento.Location = new System.Drawing.Point(3, 165);
            this.txtDepartamento.Name = "txtDepartamento";
            this.txtDepartamento.Size = new System.Drawing.Size(292, 22);
            this.txtDepartamento.TabIndex = 6;
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGuardarCambios.FlatAppearance.BorderSize = 0;
            this.btnGuardarCambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarCambios.ForeColor = System.Drawing.Color.White;
            this.btnGuardarCambios.Location = new System.Drawing.Point(49, 220);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(200, 36);
            this.btnGuardarCambios.TabIndex = 7;
            this.btnGuardarCambios.Text = "GUARDAR CAMBIOS";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // Usuarios
            // 
            this.ClientSize = new System.Drawing.Size(1060, 580);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Usuarios";
            this.Text = "Usuarios";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVUsuarios)).EndInit();
            this.pnlEditarUsuario.ResumeLayout(false);
            this.tblEditar.ResumeLayout(false);
            this.tblEditar.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlListado;
        private System.Windows.Forms.DataGridView dGVUsuarios;
        private System.Windows.Forms.Panel pnlEditarUsuario;
        private System.Windows.Forms.TableLayoutPanel tblEditar;
        private System.Windows.Forms.Label lblEditarTitulo;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDepartamento;
        private System.Windows.Forms.Button btnGuardarCambios;
    }
}