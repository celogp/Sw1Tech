using Sw1Tech.WinF.Integracao.Controllers;
using System;
using System.Windows.Forms;

namespace Sw1Tech.WinF.Integracao
{
    public partial class FrmApp : Form
    {
        //1 min = 60000 milliseconds.
        private ExportacaoPHDController ctrlExp;
        private ImportacaoPHDController ctrlImp;

        public FrmApp()
        {
            InitializeComponent();
            TimerExp.Interval = ((int) UDTempoExportacao.Value * 60000);;
            TimerImp.Interval = ((int) UDTempoImportacao.Value * 60000);
            ctrlExp = new ExportacaoPHDController();
            ctrlImp = new ImportacaoPHDController();

        }

        private void BtnLigaExportacao_Click(object sender, System.EventArgs e)
        {
            if (TimerExp.Enabled == false)
            {
                TimerExp.Enabled = true;
                BtnLigaExportacao.Text = "Desligar a exportação";
            }
            else
            {
                TimerExp.Enabled = false;
                BtnLigaExportacao.Text = "Ligar a exportação";
            }
        }

        private void BtnLigaImportacao_Click(object sender, System.EventArgs e)
        {
            if (TimerImp.Enabled == false)
            {
                TimerImp.Enabled = true;
                BtnLigaImportacao.Text = "Desligar a importação";
            }
            else
            {
                TimerImp.Enabled = false;
                BtnLigaImportacao.Text = "Ligar a importação";
            }
        }

        private void TimerExp_Tick(object sender, EventArgs e)
        {
            lbHoraUltimoCicloExp.Text = "Hora : " + DateTime.Now.ToLongTimeString();
            lbDataUltimoCicloExp.Text = "Data : " + DateTime.Now.ToLongDateString();
            if (cbParceiros.Checked)
            {
                Logger.LogThisLine("Exportando os parceiros");
                ctrlExp.DoExportarParceiro();
            }
            if (cbProdutos.Checked)
            {
                Logger.LogThisLine("Exportando os produtos");
                ctrlExp.DoExportarProduto();
            }
        }

        private void TimerImp_Tick(object sender, EventArgs e)
        {
            lbHoraUltimoCicloImp.Text = "Hora : " + DateTime.Now.ToLongTimeString();
            lbDataUltimoCicloImp.Text = "Data : " + DateTime.Now.ToLongDateString();
            Logger.LogThisLine("Importando parceiro");
            ctrlImp.DoImportarParceiro();
        }

        private void BtnExportarManual_Click(object sender, EventArgs e)
        {
            if (cbParceiros.Checked)
            {
                Logger.LogThisLine("Exportando manual os parceiros");
                ctrlExp.DoExportarParceiro();
            }
            if (cbProdutos.Checked)
            {
                Logger.LogThisLine("Exportando manual os produtos");
                ctrlExp.DoExportarProduto();
            }

        }

        private void BtnImportarManual_Click(object sender, EventArgs e)
        {
            ctrlImp.DoImportarParceiro();
        }

        private void FrmApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.CloseLogger();
        }
    }
}
