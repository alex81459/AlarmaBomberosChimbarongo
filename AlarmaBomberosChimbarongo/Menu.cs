using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            tiempo.Enabled = true;
            /*int LocalizacionPanel1 = panel8.Location.X;
            int AnchoPanel1 = panel8.Size.Width;
            int AnchoFechaHora1 = txtFechayHora.Width;
            int PosicionMedio1 = LocalizacionPanel1+((AnchoPanel1/2)-(AnchoFechaHora1/2));

            txtFechayHora.Location = new Point(PosicionMedio1, panel8.Location.Y);*/

        }

        Boolean TemaColorOscuro = Properties.Settings.Default.FondoOscuro;

        private Point pos = Point.Empty;
        private bool move = false;

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Barra_MouseDown(object sender, MouseEventArgs e)
        {
            pos = new Point(e.X, e.Y);
            move = true;
        }

        private void Barra_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.Location = new Point((this.Left + e.X - pos.X), (this.Top + e.Y - pos.Y));
            }
        }

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void tiempo_Tick(object sender, EventArgs e)
        {
            txtFechayHora.Text = DateTime.Now.ToString();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void txtFechayHora_Click(object sender, EventArgs e)
        {

        }

        private void btnGrifos_Click(object sender, EventArgs e)
        {
            Grifos abrirGrifos = new Grifos();
            abrirGrifos.ShowDialog();
            
        }

        private void btnVoluntarios_Click(object sender, EventArgs e)
        {
            Voluntarios abrirVoluntarios = new Voluntarios();
            abrirVoluntarios.ShowDialog();
        }

        private void btnGuiaTelefonica_Click(object sender, EventArgs e)
        {
            GuiaTelefonica abrirGuiaTelefonica = new GuiaTelefonica();
            abrirGuiaTelefonica.ShowDialog();
        }

        private void btnCLavesRadiales_Click(object sender, EventArgs e)
        {
            ClavesRadiales abrirClavesRadiales = new ClavesRadiales();
            abrirClavesRadiales.ShowDialog();
        }

        private void btnAJustes_Click(object sender, EventArgs e)
        {
            Ajustes abrirAjustes = new Ajustes();
            abrirAjustes.ShowDialog();
        }
    }
}
