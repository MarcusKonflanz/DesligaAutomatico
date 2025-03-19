using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DesligaAutomatico
{
    public partial class FmrDesliga : Form
    {
        public int cancelTimer { get; set; }

        public FmrDesliga()
        {
            InitializeComponent();
        }

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            cancelTimer = 0;
            btnAgendar.Visible = false;
            btnAgendar.Enabled = false;
            btnCancelar.Visible = true;
            txtTime.Enabled = false;

            int time = 0;
            int currentTime = 0;
            int error = 0;

            try
            {
                time = Convert.ToInt32(txtTime.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insira um número válido para o tempo.");
                error++;
            }

            if (time >= 0)
            {

                int timeMin = 0;
                timeMin = time * 60;

                pbTimer.Minimum = 0;
                pbTimer.Maximum = timeMin;
                pbTimer.Value = 0;


                while (currentTime != timeMin)
                {
                    currentTime++;
                    Thread.Sleep(500);
                    string tempoRestante = Convert.ToString((timeMin - currentTime) / 60);
                    if (tempoRestante == "0")
                        lblTempoRestante.Text = "o computador será desligado em: 00:" + Convert.ToString(timeMin - currentTime) + " segundos.";
                    else
                        lblTempoRestante.Text = "o computador será desligado em: " + Convert.ToString(tempoRestante) + " minutos.";

                    pbTimer.Value = currentTime;
                    Application.DoEvents(); // Processa eventos da UI

                    if (cancelTimer > 0)
                    {
                        timeMin = 0;
                        currentTime = 0;
                        pbTimer.Value = 0;
                    }
                    Thread.Sleep(500);
                }
                lblTempoRestante.Visible = false;
                if (cancelTimer > 0)
                {
                    cancelaTimer();
                }
                else
                {
                    if (error == 0)
                        Process.Start("shutdown", "/s /t 0");

                    else
                        cancelaTimer();

                }
            }
            else
            {
                MessageBox.Show("Insira um número válido para o tempo.");
                cancelaTimer();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelTimer++;
        }

        private void cancelaTimer()
        {
            btnAgendar.Visible = true;
            btnAgendar.Enabled = true;
            btnCancelar.Visible = false;
            txtTime.Enabled = true;
        }
    }
}
