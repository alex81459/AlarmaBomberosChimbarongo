using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private void btnCLavesRadiales_Click(object sender, EventArgs e)
        {
            ClavesRadiales abrirClaves = new ClavesRadiales();
            abrirClaves.ShowDialog();
        }

        private void Menu_Resize(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnAJustes_Click(object sender, EventArgs e)
        {
            Ajustes abrirAjustes = new Ajustes();
            abrirAjustes.ShowDialog();
        }

        private void btnCLavesRadiales_Click_1(object sender, EventArgs e)
        {
            ClavesRadiales abrirClaves = new ClavesRadiales();
            abrirClaves.ShowDialog();
        }

        private void btnGrifos_Click(object sender, EventArgs e)
        {
            Grifos abrirGrifos = new Grifos();
            abrirGrifos.ShowDialog();
        }

        private void btnGuiaTelefonica_Click(object sender, EventArgs e)
        {
            GuiaTelefonica abrirGuia = new GuiaTelefonica();
            abrirGuia.ShowDialog();
        }

        private void btnVoluntarios_Click(object sender, EventArgs e)
        {
            Voluntarios abrirVoluntarios = new Voluntarios();
            abrirVoluntarios.ShowDialog();
        }

        private void btnMaterialesPeligroso_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = @".\GRE.pdf";
                p.Start();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Error al intentar abrir la GRE - GUÍA DE RESPUESTA EN CASO DE EMERGENCIA ERROR: "+ex.Message,"Error Abrir GRE",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
