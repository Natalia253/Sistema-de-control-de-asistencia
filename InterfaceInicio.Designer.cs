using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    partial class InterfaceInicio
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.Panel panelUsuarios;
        private System.Windows.Forms.Panel panelAsistencias;
        private System.Windows.Forms.Panel panelPresentes;
        private System.Windows.Forms.Panel panelAusentes;
        private System.Windows.Forms.Panel panelReloj;

        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblAsistencias;
        private System.Windows.Forms.Label lblPresentes;
        private System.Windows.Forms.Label lblAusentes;
        private System.Windows.Forms.Label lblEstadoReloj;
        private System.Windows.Forms.Label lblEstadoTxt;

        private System.Windows.Forms.Button btnResincronizar;

        private System.Windows.Forms.DataGridView dgvUltimos;

        private System.Windows.Forms.TableLayoutPanel layoutPrincipal;
        private System.Windows.Forms.TableLayoutPanel layoutTarjetas;
        private System.Windows.Forms.TableLayoutPanel layoutInferior;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelUsuarios = new System.Windows.Forms.Panel();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.panelAsistencias = new System.Windows.Forms.Panel();
            this.lblAsistencias = new System.Windows.Forms.Label();
            this.panelPresentes = new System.Windows.Forms.Panel();
            this.lblPresentes = new System.Windows.Forms.Label();
            this.panelAusentes = new System.Windows.Forms.Panel();
            this.lblAusentes = new System.Windows.Forms.Label();
            this.panelReloj = new System.Windows.Forms.Panel();
            this.lblEstadoReloj = new System.Windows.Forms.Label();
            this.lblEstadoTxt = new System.Windows.Forms.Label();
            this.btnResincronizar = new System.Windows.Forms.Button();
            this.dgvUltimos = new System.Windows.Forms.DataGridView();
            this.layoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.layoutTarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.layoutInferior = new System.Windows.Forms.TableLayoutPanel();
            this.panelUsuarios.SuspendLayout();
            this.panelAsistencias.SuspendLayout();
            this.panelPresentes.SuspendLayout();
            this.panelAusentes.SuspendLayout();
            this.panelReloj.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimos)).BeginInit();
            this.layoutPrincipal.SuspendLayout();
            this.layoutTarjetas.SuspendLayout();
            this.layoutInferior.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblTitulo.Size = new System.Drawing.Size(967, 60);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Dashboard de Asistencia";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelUsuarios
            // 
            this.panelUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.panelUsuarios.Controls.Add(this.lblUsuarios);
            this.panelUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUsuarios.Location = new System.Drawing.Point(10, 10);
            this.panelUsuarios.Margin = new System.Windows.Forms.Padding(10);
            this.panelUsuarios.Name = "panelUsuarios";
            this.panelUsuarios.Size = new System.Drawing.Size(221, 124);
            this.panelUsuarios.TabIndex = 0;
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUsuarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblUsuarios.Location = new System.Drawing.Point(0, 0);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(221, 124);
            this.lblUsuarios.TabIndex = 0;
            this.lblUsuarios.Text = "Usuarios";
            this.lblUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAsistencias
            // 
            this.panelAsistencias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.panelAsistencias.Controls.Add(this.lblAsistencias);
            this.panelAsistencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAsistencias.Location = new System.Drawing.Point(251, 10);
            this.panelAsistencias.Margin = new System.Windows.Forms.Padding(10);
            this.panelAsistencias.Name = "panelAsistencias";
            this.panelAsistencias.Size = new System.Drawing.Size(221, 124);
            this.panelAsistencias.TabIndex = 1;
            // 
            // lblAsistencias
            // 
            this.lblAsistencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAsistencias.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAsistencias.ForeColor = System.Drawing.Color.White;
            this.lblAsistencias.Location = new System.Drawing.Point(0, 0);
            this.lblAsistencias.Name = "lblAsistencias";
            this.lblAsistencias.Size = new System.Drawing.Size(221, 124);
            this.lblAsistencias.TabIndex = 0;
            this.lblAsistencias.Text = "Asistencias Hoy";
            this.lblAsistencias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPresentes
            // 
            this.panelPresentes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.panelPresentes.Controls.Add(this.lblPresentes);
            this.panelPresentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPresentes.Location = new System.Drawing.Point(492, 10);
            this.panelPresentes.Margin = new System.Windows.Forms.Padding(10);
            this.panelPresentes.Name = "panelPresentes";
            this.panelPresentes.Size = new System.Drawing.Size(221, 124);
            this.panelPresentes.TabIndex = 2;
            // 
            // lblPresentes
            // 
            this.lblPresentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPresentes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPresentes.ForeColor = System.Drawing.Color.White;
            this.lblPresentes.Location = new System.Drawing.Point(0, 0);
            this.lblPresentes.Name = "lblPresentes";
            this.lblPresentes.Size = new System.Drawing.Size(221, 124);
            this.lblPresentes.TabIndex = 0;
            this.lblPresentes.Text = "Presentes";
            this.lblPresentes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAusentes
            // 
            this.panelAusentes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.panelAusentes.Controls.Add(this.lblAusentes);
            this.panelAusentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAusentes.Location = new System.Drawing.Point(733, 10);
            this.panelAusentes.Margin = new System.Windows.Forms.Padding(10);
            this.panelAusentes.Name = "panelAusentes";
            this.panelAusentes.Size = new System.Drawing.Size(224, 124);
            this.panelAusentes.TabIndex = 3;
            // 
            // lblAusentes
            // 
            this.lblAusentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAusentes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAusentes.ForeColor = System.Drawing.Color.White;
            this.lblAusentes.Location = new System.Drawing.Point(0, 0);
            this.lblAusentes.Name = "lblAusentes";
            this.lblAusentes.Size = new System.Drawing.Size(224, 124);
            this.lblAusentes.TabIndex = 0;
            this.lblAusentes.Text = "Ausentes";
            this.lblAusentes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelReloj
            // 
            this.panelReloj.BackColor = System.Drawing.Color.White;
            this.panelReloj.Controls.Add(this.btnResincronizar);
            this.panelReloj.Controls.Add(this.lblEstadoReloj);
            this.panelReloj.Controls.Add(this.lblEstadoTxt);
            this.panelReloj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReloj.Location = new System.Drawing.Point(3, 3);
            this.panelReloj.Name = "panelReloj";
            this.panelReloj.Size = new System.Drawing.Size(332, 368);
            this.panelReloj.TabIndex = 0;
            // 
            // lblEstadoReloj
            // 
            this.lblEstadoReloj.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEstadoReloj.Location = new System.Drawing.Point(15, 15);
            this.lblEstadoReloj.Name = "lblEstadoReloj";
            this.lblEstadoReloj.Size = new System.Drawing.Size(185, 23);
            this.lblEstadoReloj.TabIndex = 0;
            this.lblEstadoReloj.Text = "Estado del Reloj";
            // 
            // lblEstadoTxt
            // 
            this.lblEstadoTxt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEstadoTxt.Location = new System.Drawing.Point(15, 50);
            this.lblEstadoTxt.Name = "lblEstadoTxt";
            this.lblEstadoTxt.Size = new System.Drawing.Size(300, 31);
            this.lblEstadoTxt.TabIndex = 1;
            // 
            // btnResincronizar
            // 
            this.btnResincronizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(182)))));
            this.btnResincronizar.FlatAppearance.BorderSize = 0;
            this.btnResincronizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResincronizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResincronizar.ForeColor = System.Drawing.Color.White;
            this.btnResincronizar.Location = new System.Drawing.Point(15, 95);
            this.btnResincronizar.Name = "btnResincronizar";
            this.btnResincronizar.Size = new System.Drawing.Size(300, 38);
            this.btnResincronizar.TabIndex = 2;
            this.btnResincronizar.Text = "🔄 Volver a sincronizar";
            this.btnResincronizar.UseVisualStyleBackColor = false;
            this.btnResincronizar.Click += new System.EventHandler(this.btnResincronizar_Click);
            // 
            // dgvUltimos
            // 
            this.dgvUltimos.AllowUserToAddRows = false;
            this.dgvUltimos.AllowUserToDeleteRows = false;
            this.dgvUltimos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUltimos.ColumnHeadersHeight = 29;
            this.dgvUltimos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUltimos.Location = new System.Drawing.Point(341, 3);
            this.dgvUltimos.Name = "dgvUltimos";
            this.dgvUltimos.ReadOnly = true;
            this.dgvUltimos.RowHeadersWidth = 51;
            this.dgvUltimos.Size = new System.Drawing.Size(623, 368);
            this.dgvUltimos.TabIndex = 1;
            // 
            // layoutPrincipal
            // 
            this.layoutPrincipal.ColumnCount = 1;
            this.layoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPrincipal.Controls.Add(this.lblTitulo, 0, 0);
            this.layoutPrincipal.Controls.Add(this.layoutTarjetas, 0, 1);
            this.layoutPrincipal.Controls.Add(this.layoutInferior, 0, 2);
            this.layoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPrincipal.Location = new System.Drawing.Point(0, 0);
            this.layoutPrincipal.Name = "layoutPrincipal";
            this.layoutPrincipal.RowCount = 3;
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.layoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPrincipal.Size = new System.Drawing.Size(973, 590);
            this.layoutPrincipal.TabIndex = 0;
            // 
            // layoutTarjetas
            // 
            this.layoutTarjetas.ColumnCount = 4;
            this.layoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutTarjetas.Controls.Add(this.panelUsuarios, 0, 0);
            this.layoutTarjetas.Controls.Add(this.panelAsistencias, 1, 0);
            this.layoutTarjetas.Controls.Add(this.panelPresentes, 2, 0);
            this.layoutTarjetas.Controls.Add(this.panelAusentes, 3, 0);
            this.layoutTarjetas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTarjetas.Location = new System.Drawing.Point(3, 63);
            this.layoutTarjetas.Name = "layoutTarjetas";
            this.layoutTarjetas.RowCount = 1;
            this.layoutTarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTarjetas.Size = new System.Drawing.Size(967, 144);
            this.layoutTarjetas.TabIndex = 1;
            // 
            // layoutInferior
            // 
            this.layoutInferior.ColumnCount = 2;
            this.layoutInferior.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.layoutInferior.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.layoutInferior.Controls.Add(this.panelReloj, 0, 0);
            this.layoutInferior.Controls.Add(this.dgvUltimos, 1, 0);
            this.layoutInferior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutInferior.Location = new System.Drawing.Point(3, 213);
            this.layoutInferior.Name = "layoutInferior";
            this.layoutInferior.RowCount = 1;
            this.layoutInferior.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutInferior.Size = new System.Drawing.Size(967, 374);
            this.layoutInferior.TabIndex = 2;
            // 
            // InterfaceInicio
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(973, 590);
            this.Controls.Add(this.layoutPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InterfaceInicio";
            this.panelUsuarios.ResumeLayout(false);
            this.panelAsistencias.ResumeLayout(false);
            this.panelPresentes.ResumeLayout(false);
            this.panelAusentes.ResumeLayout(false);
            this.panelReloj.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimos)).EndInit();
            this.layoutPrincipal.ResumeLayout(false);
            this.layoutTarjetas.ResumeLayout(false);
            this.layoutInferior.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}