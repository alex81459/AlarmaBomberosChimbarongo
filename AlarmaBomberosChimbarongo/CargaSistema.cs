using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Menu abrirMenu = new Menu();
            timer1.Stop();
            abrirMenu.Show();
            this.Hide();
        }
    }
}
