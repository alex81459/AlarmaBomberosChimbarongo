using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //Para controlar el Form
        private Point pos = Point.Empty;
        private bool move = false;

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45) || (e.KeyChar == 107) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se ingresa los numeros, el guion -, la k permite que se mantenga en el textbox, y borrar
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "19876248-2";
            txtClave.Text = "12345";
            IniciarSesion();
        }

        public void IniciarSesion()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            DataTable ResultadoBusquedaRUT = new DataTable();
            ResultadoBusquedaRUT = cargarTabla.CargarTabla("SELECT RUT,Nombre From Operadores Where RUT = '"+txtUsuario.Text+"' and Clave = '"+txtClave.Text+"'");

            try
            {
                if (ResultadoBusquedaRUT.Rows[0][0].ToString() != "")
                {
                    MessageBox.Show("Sesion Iniciada Correctamente RUT: " + ResultadoBusquedaRUT.Rows[0][0].ToString() + "","Sesion Iniciada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    FuncionesAplicacion.RutLogin = ResultadoBusquedaRUT.Rows[0][0].ToString();
                    FuncionesAplicacion.NombreLogin = ResultadoBusquedaRUT.Rows[0][1].ToString();

                    try
                    {
                        ControlSQLite guardarRegistro = new ControlSQLite();
                        guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Iniciar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Inicio Sesion','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Login');");
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    this.Close();
                    Menu AbrirMenu = new Menu();
                    AbrirMenu.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Iniciar Sesion, el Rut ingresado es incorrecto y/o la clave", "Credenciales Erroneas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Barra_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Barra_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.Location = new Point((this.Left + e.X - pos.X), (this.Top + e.Y - pos.Y));
            }
        }

        private void Barra_MouseDown(object sender, MouseEventArgs e)
        {
            pos = new Point(e.X, e.Y);
            move = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AcercaDe abrirAcerca = new AcercaDe();
            abrirAcerca.ShowDialog();
        }
    }
}
