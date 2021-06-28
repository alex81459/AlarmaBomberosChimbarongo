using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class CargaSistema : Form
    {
        public CargaSistema()
        {
            InitializeComponent();
            revisarInstancia();
        }

        public void revisarInstancia(){
                if (Process.GetProcessesByName("Alarma Bomberos Chimbarongo").Length > 1) {
                    MessageBox.Show("Solo se puede mantener abierta una instancia de la aplicacion a la vez", "Aplicacion ya abierta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    Application.Exit();
                }
                else
                {
                timer1.Start();
                }  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Login abrirLogin = new Login();
            timer1.Stop();
            abrirLogin.Show();
            this.Hide();
        }
    }
}
