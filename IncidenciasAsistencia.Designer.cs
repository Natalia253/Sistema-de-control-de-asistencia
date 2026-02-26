namespace CapaPresentacion.Formularios
{
    partial class IncidenciasAsistencia
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox txtUserIdFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnLimpiar;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dGVIncidencias;

        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblPanelTitulo;

        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;

        private System.Windows.Forms.Label lblTipoJust;
        private System.Windows.Forms.ComboBox cboTipoJust;

        private System.Windows.Forms.Label lblCodigoAuditoria;
        private System.Windows.Forms.TextBox txtCodigoAuditoria;

        private System.Windows.Forms.Label lblJustificacion;
        private System.Windows.Forms.TextBox txtJustificacion;

        private System.Windows.Forms.Button btnGuardar;

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtUserIdFiltro = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dGVIncidencias = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtJustificacion = new System.Windows.Forms.TextBox();
            this.lblJustificacion = new System.Windows.Forms.Label();
            this.txtCodigoAuditoria = new System.Windows.Forms.TextBox();
            this.lblCodigoAuditoria = new System.Windows.Forms.Label();
            this.cboTipoJust = new System.Windows.Forms.ComboBox();
            this.lblTipoJust = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblPanelTitulo = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVIncidencias)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblTitulo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(18, 14, 18, 10);
            this.pnlTop.Size = new System.Drawing.Size(1180, 58);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(18, 16);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(248, 28);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Incidencias de Asistencia";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.White;
            this.pnlFiltros.Controls.Add(this.btnLimpiar);
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.txtUserIdFiltro);
            this.pnlFiltros.Controls.Add(this.lblUserId);
            this.pnlFiltros.Controls.Add(this.txtBuscar);
            this.pnlFiltros.Controls.Add(this.lblBuscar);
            this.pnlFiltros.Controls.Add(this.dtpHasta);
            this.pnlFiltros.Controls.Add(this.lblHasta);
            this.pnlFiltros.Controls.Add(this.dtpDesde);
            this.pnlFiltros.Controls.Add(this.lblDesde);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 58);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            this.pnlFiltros.Size = new System.Drawing.Size(1180, 68);
            this.pnlFiltros.TabIndex = 1;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Location = new System.Drawing.Point(757, 31);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(92, 28);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(655, 31);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(92, 28);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtUserIdFiltro
            // 
            this.txtUserIdFiltro.Location = new System.Drawing.Point(543, 34);
            this.txtUserIdFiltro.Name = "txtUserIdFiltro";
            this.txtUserIdFiltro.Size = new System.Drawing.Size(95, 22);
            this.txtUserIdFiltro.TabIndex = 7;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(540, 13);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(52, 16);
            this.lblUserId.TabIndex = 6;
            this.lblUserId.Text = "UserID:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(305, 34);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(220, 22);
            this.txtBuscar.TabIndex = 5;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(302, 13);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(109, 16);
            this.lblBuscar.TabIndex = 4;
            this.lblBuscar.Text = "Nombre / Depto.:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(163, 34);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(125, 22);
            this.dtpHasta.TabIndex = 3;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(160, 13);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(46, 16);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(23, 34);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(125, 22);
            this.dtpDesde.TabIndex = 1;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(20, 13);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(51, 16);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.pnlGrid);
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 126);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(18, 12, 18, 18);
            this.pnlMain.Size = new System.Drawing.Size(1180, 514);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dGVIncidencias);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(18, 12);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(804, 484);
            this.pnlGrid.TabIndex = 0;
            // 
            // dGVIncidencias
            // 
            this.dGVIncidencias.ColumnHeadersHeight = 32;
            this.dGVIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVIncidencias.Location = new System.Drawing.Point(0, 0);
            this.dGVIncidencias.Name = "dGVIncidencias";
            this.dGVIncidencias.RowHeadersWidth = 51;
            this.dGVIncidencias.Size = new System.Drawing.Size(804, 484);
            this.dGVIncidencias.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.btnGuardar);
            this.pnlRight.Controls.Add(this.txtJustificacion);
            this.pnlRight.Controls.Add(this.lblJustificacion);
            this.pnlRight.Controls.Add(this.txtCodigoAuditoria);
            this.pnlRight.Controls.Add(this.lblCodigoAuditoria);
            this.pnlRight.Controls.Add(this.cboTipoJust);
            this.pnlRight.Controls.Add(this.lblTipoJust);
            this.pnlRight.Controls.Add(this.cboTipo);
            this.pnlRight.Controls.Add(this.lblTipo);
            this.pnlRight.Controls.Add(this.lblPanelTitulo);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(822, 12);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14);
            this.pnlRight.Size = new System.Drawing.Size(340, 484);
            this.pnlRight.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(19, 410);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(300, 40);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar justificación";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // txtJustificacion
            // 
            this.txtJustificacion.Location = new System.Drawing.Point(19, 234);
            this.txtJustificacion.Multiline = true;
            this.txtJustificacion.Name = "txtJustificacion";
            this.txtJustificacion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJustificacion.Size = new System.Drawing.Size(300, 162);
            this.txtJustificacion.TabIndex = 8;
            // 
            // lblJustificacion
            // 
            this.lblJustificacion.AutoSize = true;
            this.lblJustificacion.Location = new System.Drawing.Point(16, 214);
            this.lblJustificacion.Name = "lblJustificacion";
            this.lblJustificacion.Size = new System.Drawing.Size(83, 16);
            this.lblJustificacion.TabIndex = 7;
            this.lblJustificacion.Text = "Justificación:";
            // 
            // txtCodigoAuditoria
            // 
            this.txtCodigoAuditoria.Location = new System.Drawing.Point(19, 186);
            this.txtCodigoAuditoria.Name = "txtCodigoAuditoria";
            this.txtCodigoAuditoria.Size = new System.Drawing.Size(300, 22);
            this.txtCodigoAuditoria.TabIndex = 6;
            // 
            // lblCodigoAuditoria
            // 
            this.lblCodigoAuditoria.AutoSize = true;
            this.lblCodigoAuditoria.Location = new System.Drawing.Point(16, 166);
            this.lblCodigoAuditoria.Name = "lblCodigoAuditoria";
            this.lblCodigoAuditoria.Size = new System.Drawing.Size(125, 16);
            this.lblCodigoAuditoria.TabIndex = 5;
            this.lblCodigoAuditoria.Text = "Código auditoría (*):";
            // 
            // cboTipoJust
            // 
            this.cboTipoJust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoJust.FormattingEnabled = true;
            this.cboTipoJust.Location = new System.Drawing.Point(19, 136);
            this.cboTipoJust.Name = "cboTipoJust";
            this.cboTipoJust.Size = new System.Drawing.Size(300, 24);
            this.cboTipoJust.TabIndex = 4;
            // 
            // lblTipoJust
            // 
            this.lblTipoJust.AutoSize = true;
            this.lblTipoJust.Location = new System.Drawing.Point(16, 116);
            this.lblTipoJust.Name = "lblTipoJust";
            this.lblTipoJust.Size = new System.Drawing.Size(129, 16);
            this.lblTipoJust.TabIndex = 3;
            this.lblTipoJust.Text = "Tipo de justificación:";

            // 
            // cboTipo
            // 
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(19, 76);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(300, 24);
            this.cboTipo.TabIndex = 2;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(16, 56);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(38, 16);
            this.lblTipo.TabIndex = 1;
            this.lblTipo.Text = "Tipo:";
            // 
            // lblPanelTitulo
            // 
            this.lblPanelTitulo.AutoSize = true;
            this.lblPanelTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPanelTitulo.Location = new System.Drawing.Point(14, 14);
            this.lblPanelTitulo.Name = "lblPanelTitulo";
            this.lblPanelTitulo.Size = new System.Drawing.Size(184, 23);
            this.lblPanelTitulo.TabIndex = 0;
            this.lblPanelTitulo.Text = "Registrar justificación";
            // 
            // IncidenciasAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1180, 640);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IncidenciasAsistencia";
            this.Text = "Incidencias de Asistencia";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGVIncidencias)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}