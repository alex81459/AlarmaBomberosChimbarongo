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

            /*if (TemaColorOscuro)
            {
                panel1.BackColor = Color.Black;
                panel1.ForeColor = Color.White;
                //BotonesOscuros();
            }
            else
            {
                panel1.BackColor = Color.White;
                panel1.ForeColor = Color.Black;
                //BotonesClaros();
            }
            */
            tiempo.Enabled = true;
            //BotonesOscuros();
        }
        
        /*
        public void BotonesOscuros(){
            btn100.BackColor = Color.Black;
            btn102.BackColor = Color.Black;
            btn104.BackColor = Color.Black;
            btn101103.BackColor = Color.Black;
            btnOtrosServicios.BackColor = Color.Black;
            btnPrimera1.BackColor = Color.Black;
            btnSegunda2.BackColor = Color.Black;
            btnTercera3.BackColor = Color.Black;
            btnCuarta4.BackColor = Color.Black;
            btnQuinta5.BackColor = Color.Black;
            btnCuarteles.BackColor = Color.Black;
            btnGrifos.BackColor = Color.Black;
            btnMaterialesPeligroso.BackColor = Color.Black;
            btnVoluntarios.BackColor = Color.Black;
            btnGuiaTelefonica.BackColor = Color.Black;
            btnCLavesRadiales.BackColor = Color.Black;
            btn100.ForeColor = Color.WhiteSmoke;
            btn102.ForeColor = Color.WhiteSmoke;
            btn104.ForeColor = Color.WhiteSmoke;
            btn101103.ForeColor = Color.WhiteSmoke;
            btnOtrosServicios.ForeColor = Color.WhiteSmoke;
            btnPrimera1.ForeColor = Color.WhiteSmoke;
            btnSegunda2.ForeColor = Color.WhiteSmoke;
            btnTercera3.ForeColor = Color.WhiteSmoke;
            btnCuarta4.ForeColor = Color.WhiteSmoke;
            btnQuinta5.ForeColor = Color.WhiteSmoke;
            btnCuarteles.ForeColor = Color.WhiteSmoke;
            btnGrifos.ForeColor = Color.WhiteSmoke;
            btnMaterialesPeligroso.ForeColor = Color.WhiteSmoke;
            btnVoluntarios.ForeColor = Color.WhiteSmoke;
            btnGuiaTelefonica.ForeColor = Color.WhiteSmoke;
            btnCLavesRadiales.ForeColor = Color.WhiteSmoke;
            textClaves.BackColor = Color.Black;
            textClaves.ForeColor = Color.White;
        }
        */
        

            /*
        public void BotonesClaros()
        {
            btn100.BackColor = Color.White;
            btn102.BackColor = Color.White;
            btn104.BackColor = Color.White;
            btn101103.BackColor = Color.White;
            btnOtrosServicios.BackColor = Color.White;
            btnPrimera1.BackColor = Color.White;
            btnSegunda2.BackColor = Color.White;
            btnTercera3.BackColor = Color.White;
            btnCuarta4.BackColor = Color.White;
            btnQuinta5.BackColor = Color.White;
            btnCuarteles.BackColor = Color.White;
            btnGrifos.BackColor = Color.White;
            btnMaterialesPeligroso.BackColor = Color.White;
            btnVoluntarios.BackColor = Color.White;
            btnGuiaTelefonica.BackColor = Color.White;
            btnCLavesRadiales.BackColor = Color.White;
            btn100.ForeColor = Color.Black;
            btn102.ForeColor = Color.Black;
            btn104.ForeColor = Color.Black;
            btn101103.ForeColor = Color.Black;
            btnOtrosServicios.ForeColor = Color.Black;
            btnPrimera1.ForeColor = Color.Black;
            btnSegunda2.ForeColor = Color.Black;
            btnTercera3.ForeColor = Color.Black;
            btnCuarta4.ForeColor = Color.Black;
            btnQuinta5.ForeColor = Color.Black;
            btnCuarteles.ForeColor = Color.Black;
            btnGrifos.ForeColor = Color.Black;
            btnMaterialesPeligroso.ForeColor = Color.Black;
            btnVoluntarios.ForeColor = Color.Black;
            btnGuiaTelefonica.ForeColor = Color.Black;
            btnCLavesRadiales.ForeColor = Color.Black;
            textClaves.BackColor = Color.White;
            textClaves.ForeColor = Color.Black;
        }
        */

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

        /*
        private void picCambiarColor_Click(object sender, EventArgs e)
        {
            if (TemaColorOscuro)
            {
                panel1.BackColor = Color.White;
                panel1.ForeColor = Color.Black;
                Properties.Settings.Default.FondoOscuro = false;
                Properties.Settings.Default.Save();
                TemaColorOscuro = Properties.Settings.Default.FondoOscuro;
                //BotonesClaros();
            }
            else
            {
                panel1.BackColor = Color.Black;
                panel1.ForeColor = Color.White;
                Properties.Settings.Default.FondoOscuro = true;
                Properties.Settings.Default.Save();
                TemaColorOscuro = Properties.Settings.Default.FondoOscuro;
                //BotonesOscuros();
            }
        }
        */

        private void tiempo_Tick(object sender, EventArgs e)
        {
            txtFechayHora.Text = DateTime.Now.ToString();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void btnCLavesRadiales_Click(object sender, EventArgs e)
        {
            ClavesRadiales abrirClaves = new ClavesRadiales();
            abrirClaves.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAJustes_Click(object sender, EventArgs e)
        {
            Ajustes abrirAjustes = new Ajustes();
            abrirAjustes.ShowDialog();
        }

        private void btnGuiaTelefonica_Click(object sender, EventArgs e)
        {
            GuiaTelefonica abrirGuiaTelefonica = new GuiaTelefonica();
            abrirGuiaTelefonica.ShowDialog();
        }

        private void btnVoluntarios_Click(object sender, EventArgs e)
        {
            Voluntarios abrirVoluntarios = new Voluntarios();
            abrirVoluntarios.ShowDialog();
        }

        private void btnGrifos_Click(object sender, EventArgs e)
        {
            Grifos abrirGrifos = new Grifos();
            abrirGrifos.ShowDialog();
        }
    }
}
