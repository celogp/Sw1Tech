namespace Sw1Tech.WinF.Integracao
{
    partial class FrmApp
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BtnLigaExportacao = new System.Windows.Forms.Button();
            this.BtnLigaImportacao = new System.Windows.Forms.Button();
            this.TimerExp = new System.Windows.Forms.Timer(this.components);
            this.TimerImp = new System.Windows.Forms.Timer(this.components);
            this.BtnExportarManual = new System.Windows.Forms.Button();
            this.GbExportacao = new System.Windows.Forms.GroupBox();
            this.gbUltimoCiclo = new System.Windows.Forms.GroupBox();
            this.lbDataUltimoCicloExp = new System.Windows.Forms.Label();
            this.lbHoraUltimoCicloExp = new System.Windows.Forms.Label();
            this.UDTempoExportacao = new System.Windows.Forms.NumericUpDown();
            this.LbTimerExportacao = new System.Windows.Forms.Label();
            this.GbImportacao = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbDataUltimoCicloImp = new System.Windows.Forms.Label();
            this.lbHoraUltimoCicloImp = new System.Windows.Forms.Label();
            this.BtnImportarManual = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UDTempoImportacao = new System.Windows.Forms.NumericUpDown();
            this.cbParceiros = new System.Windows.Forms.CheckBox();
            this.cbProdutos = new System.Windows.Forms.CheckBox();
            this.GbExportacao.SuspendLayout();
            this.gbUltimoCiclo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDTempoExportacao)).BeginInit();
            this.GbImportacao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDTempoImportacao)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnLigaExportacao
            // 
            this.BtnLigaExportacao.Location = new System.Drawing.Point(9, 257);
            this.BtnLigaExportacao.Name = "BtnLigaExportacao";
            this.BtnLigaExportacao.Size = new System.Drawing.Size(152, 23);
            this.BtnLigaExportacao.TabIndex = 0;
            this.BtnLigaExportacao.Text = "Ligar a Exportação";
            this.BtnLigaExportacao.UseVisualStyleBackColor = true;
            this.BtnLigaExportacao.Click += new System.EventHandler(this.BtnLigaExportacao_Click);
            // 
            // BtnLigaImportacao
            // 
            this.BtnLigaImportacao.Location = new System.Drawing.Point(9, 257);
            this.BtnLigaImportacao.Name = "BtnLigaImportacao";
            this.BtnLigaImportacao.Size = new System.Drawing.Size(144, 23);
            this.BtnLigaImportacao.TabIndex = 4;
            this.BtnLigaImportacao.Text = "Ligar a importação";
            this.BtnLigaImportacao.UseVisualStyleBackColor = true;
            this.BtnLigaImportacao.Click += new System.EventHandler(this.BtnLigaImportacao_Click);
            // 
            // TimerExp
            // 
            this.TimerExp.Tick += new System.EventHandler(this.TimerExp_Tick);
            // 
            // TimerImp
            // 
            this.TimerImp.Tick += new System.EventHandler(this.TimerImp_Tick);
            // 
            // BtnExportarManual
            // 
            this.BtnExportarManual.Location = new System.Drawing.Point(167, 257);
            this.BtnExportarManual.Name = "BtnExportarManual";
            this.BtnExportarManual.Size = new System.Drawing.Size(108, 23);
            this.BtnExportarManual.TabIndex = 5;
            this.BtnExportarManual.Text = "Exportar Manual";
            this.BtnExportarManual.UseVisualStyleBackColor = true;
            this.BtnExportarManual.Click += new System.EventHandler(this.BtnExportarManual_Click);
            // 
            // GbExportacao
            // 
            this.GbExportacao.Controls.Add(this.cbProdutos);
            this.GbExportacao.Controls.Add(this.cbParceiros);
            this.GbExportacao.Controls.Add(this.gbUltimoCiclo);
            this.GbExportacao.Controls.Add(this.UDTempoExportacao);
            this.GbExportacao.Controls.Add(this.LbTimerExportacao);
            this.GbExportacao.Controls.Add(this.BtnLigaExportacao);
            this.GbExportacao.Controls.Add(this.BtnExportarManual);
            this.GbExportacao.Location = new System.Drawing.Point(12, 12);
            this.GbExportacao.Name = "GbExportacao";
            this.GbExportacao.Size = new System.Drawing.Size(303, 299);
            this.GbExportacao.TabIndex = 6;
            this.GbExportacao.TabStop = false;
            this.GbExportacao.Text = "Exportação para Sw1Tech : ";
            // 
            // gbUltimoCiclo
            // 
            this.gbUltimoCiclo.Controls.Add(this.lbDataUltimoCicloExp);
            this.gbUltimoCiclo.Controls.Add(this.lbHoraUltimoCicloExp);
            this.gbUltimoCiclo.Location = new System.Drawing.Point(9, 172);
            this.gbUltimoCiclo.Name = "gbUltimoCiclo";
            this.gbUltimoCiclo.Size = new System.Drawing.Size(288, 79);
            this.gbUltimoCiclo.TabIndex = 6;
            this.gbUltimoCiclo.TabStop = false;
            this.gbUltimoCiclo.Text = "Último ciclo exportado ";
            // 
            // lbDataUltimoCicloExp
            // 
            this.lbDataUltimoCicloExp.AutoSize = true;
            this.lbDataUltimoCicloExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataUltimoCicloExp.Location = new System.Drawing.Point(6, 51);
            this.lbDataUltimoCicloExp.Name = "lbDataUltimoCicloExp";
            this.lbDataUltimoCicloExp.Size = new System.Drawing.Size(50, 17);
            this.lbDataUltimoCicloExp.TabIndex = 1;
            this.lbDataUltimoCicloExp.Text = "Data : ";
            // 
            // lbHoraUltimoCicloExp
            // 
            this.lbHoraUltimoCicloExp.AutoSize = true;
            this.lbHoraUltimoCicloExp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoraUltimoCicloExp.Location = new System.Drawing.Point(6, 21);
            this.lbHoraUltimoCicloExp.Name = "lbHoraUltimoCicloExp";
            this.lbHoraUltimoCicloExp.Size = new System.Drawing.Size(51, 17);
            this.lbHoraUltimoCicloExp.TabIndex = 0;
            this.lbHoraUltimoCicloExp.Text = "Hora : ";
            // 
            // UDTempoExportacao
            // 
            this.UDTempoExportacao.Location = new System.Drawing.Point(107, 19);
            this.UDTempoExportacao.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.UDTempoExportacao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UDTempoExportacao.Name = "UDTempoExportacao";
            this.UDTempoExportacao.Size = new System.Drawing.Size(107, 20);
            this.UDTempoExportacao.TabIndex = 2;
            this.UDTempoExportacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDTempoExportacao.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LbTimerExportacao
            // 
            this.LbTimerExportacao.AutoSize = true;
            this.LbTimerExportacao.Location = new System.Drawing.Point(6, 21);
            this.LbTimerExportacao.Name = "LbTimerExportacao";
            this.LbTimerExportacao.Size = new System.Drawing.Size(95, 13);
            this.LbTimerExportacao.TabIndex = 1;
            this.LbTimerExportacao.Text = "Ciclo em minutos : ";
            this.LbTimerExportacao.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // GbImportacao
            // 
            this.GbImportacao.AutoSize = true;
            this.GbImportacao.Controls.Add(this.groupBox1);
            this.GbImportacao.Controls.Add(this.BtnImportarManual);
            this.GbImportacao.Controls.Add(this.label1);
            this.GbImportacao.Controls.Add(this.UDTempoImportacao);
            this.GbImportacao.Controls.Add(this.BtnLigaImportacao);
            this.GbImportacao.Location = new System.Drawing.Point(320, 12);
            this.GbImportacao.Name = "GbImportacao";
            this.GbImportacao.Size = new System.Drawing.Size(303, 299);
            this.GbImportacao.TabIndex = 7;
            this.GbImportacao.TabStop = false;
            this.GbImportacao.Text = "Importação para Phd : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbDataUltimoCicloImp);
            this.groupBox1.Controls.Add(this.lbHoraUltimoCicloImp);
            this.groupBox1.Location = new System.Drawing.Point(9, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 77);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Último ciclo importado ";
            // 
            // lbDataUltimoCicloImp
            // 
            this.lbDataUltimoCicloImp.AutoSize = true;
            this.lbDataUltimoCicloImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataUltimoCicloImp.Location = new System.Drawing.Point(6, 47);
            this.lbDataUltimoCicloImp.Name = "lbDataUltimoCicloImp";
            this.lbDataUltimoCicloImp.Size = new System.Drawing.Size(50, 17);
            this.lbDataUltimoCicloImp.TabIndex = 1;
            this.lbDataUltimoCicloImp.Text = "Data : ";
            // 
            // lbHoraUltimoCicloImp
            // 
            this.lbHoraUltimoCicloImp.AutoSize = true;
            this.lbHoraUltimoCicloImp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoraUltimoCicloImp.Location = new System.Drawing.Point(6, 19);
            this.lbHoraUltimoCicloImp.Name = "lbHoraUltimoCicloImp";
            this.lbHoraUltimoCicloImp.Size = new System.Drawing.Size(51, 17);
            this.lbHoraUltimoCicloImp.TabIndex = 0;
            this.lbHoraUltimoCicloImp.Text = "Hora : ";
            // 
            // BtnImportarManual
            // 
            this.BtnImportarManual.Location = new System.Drawing.Point(159, 257);
            this.BtnImportarManual.Name = "BtnImportarManual";
            this.BtnImportarManual.Size = new System.Drawing.Size(104, 23);
            this.BtnImportarManual.TabIndex = 8;
            this.BtnImportarManual.Text = "Importar Manual";
            this.BtnImportarManual.UseVisualStyleBackColor = true;
            this.BtnImportarManual.Click += new System.EventHandler(this.BtnImportarManual_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cliclo em minutos : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UDTempoImportacao
            // 
            this.UDTempoImportacao.Location = new System.Drawing.Point(109, 23);
            this.UDTempoImportacao.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.UDTempoImportacao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UDTempoImportacao.Name = "UDTempoImportacao";
            this.UDTempoImportacao.Size = new System.Drawing.Size(107, 20);
            this.UDTempoImportacao.TabIndex = 5;
            this.UDTempoImportacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UDTempoImportacao.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbParceiros
            // 
            this.cbParceiros.AutoSize = true;
            this.cbParceiros.Location = new System.Drawing.Point(9, 48);
            this.cbParceiros.Name = "cbParceiros";
            this.cbParceiros.Size = new System.Drawing.Size(70, 17);
            this.cbParceiros.TabIndex = 7;
            this.cbParceiros.Text = "Parceiros";
            this.cbParceiros.UseVisualStyleBackColor = true;
            // 
            // cbProdutos
            // 
            this.cbProdutos.AutoSize = true;
            this.cbProdutos.Location = new System.Drawing.Point(9, 71);
            this.cbProdutos.Name = "cbProdutos";
            this.cbProdutos.Size = new System.Drawing.Size(68, 17);
            this.cbProdutos.TabIndex = 8;
            this.cbProdutos.Text = "Produtos";
            this.cbProdutos.UseVisualStyleBackColor = true;
            // 
            // FrmApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 319);
            this.Controls.Add(this.GbImportacao);
            this.Controls.Add(this.GbExportacao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integração Phd";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmApp_FormClosed);
            this.GbExportacao.ResumeLayout(false);
            this.GbExportacao.PerformLayout();
            this.gbUltimoCiclo.ResumeLayout(false);
            this.gbUltimoCiclo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDTempoExportacao)).EndInit();
            this.GbImportacao.ResumeLayout(false);
            this.GbImportacao.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UDTempoImportacao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnLigaExportacao;
        private System.Windows.Forms.Button BtnLigaImportacao;
        private System.Windows.Forms.Timer TimerExp;
        private System.Windows.Forms.Timer TimerImp;
        private System.Windows.Forms.Button BtnExportarManual;
        private System.Windows.Forms.GroupBox GbExportacao;
        private System.Windows.Forms.NumericUpDown UDTempoExportacao;
        private System.Windows.Forms.Label LbTimerExportacao;
        private System.Windows.Forms.GroupBox GbImportacao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UDTempoImportacao;
        private System.Windows.Forms.Button BtnImportarManual;
        private System.Windows.Forms.GroupBox gbUltimoCiclo;
        private System.Windows.Forms.Label lbHoraUltimoCicloExp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbHoraUltimoCicloImp;
        private System.Windows.Forms.Label lbDataUltimoCicloExp;
        private System.Windows.Forms.Label lbDataUltimoCicloImp;
        private System.Windows.Forms.CheckBox cbParceiros;
        private System.Windows.Forms.CheckBox cbProdutos;
    }
}

