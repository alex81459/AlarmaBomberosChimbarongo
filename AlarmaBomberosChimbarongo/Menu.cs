using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace AlarmaBomberosChimbarongo
{
    public partial class Menu : Form
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        string strbufferOut;

        public Menu()
        {
            InitializeComponent();
            tiempo.Enabled = true;

            SpPuertos.BaudRate = Int32.Parse(Properties.Settings.Default.VelocidadPuertoSerial);
            SpPuertos.DataBits = 8;
            SpPuertos.Parity = Parity.None;
            SpPuertos.StopBits = StopBits.One;
            SpPuertos.Handshake = Handshake.None;
            SpPuertos.PortName = Properties.Settings.Default.PuertoSerial;
            strbufferOut = "";
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

        private void txtSituacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 35))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculasn ñ, borrar , numerosy espacio se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLugar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 35))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculasn ñ, borrar , numerosy espacio se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 35))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculasn ñ, borrar , numerosy espacio se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtOficialAcargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculasn ñ, borrar y espacio se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void LimpiarCampos()
        {
            for(int i = 0; i < clbClavesComunes.Items.Count; i++){
                clbClavesComunes.SetItemChecked(i, false);
            }

            for (int i = 0; i < clbCompañiaBomberos.Items.Count; i++)
            {
                clbCompañiaBomberos.SetItemChecked(i, false);
            }

            txtCoordenadas.Text = "";
            cbPublicar.Checked = false;
            txtSituacion.Text = "";
            txtOficialAcargo.Text = "";
            txtLugar.Text = "";
            txtDescripcion.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clbClavesComunes.CheckedIndices.Count != 0 && clbCompañiaBomberos.CheckedIndices.Count != 0 && txtSituacion.Text != "" && txtOficialAcargo.Text != "" && txtLugar.Text != "" && txtDescripcion.Text != "")
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Despachar a la Situacion de Emergencia: " + txtSituacion.Text + "? y ¿Esta seguro que los datos estan correctamente ingresados?", "Confirmacion Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    string ClavesSeleccionadas = "";
                    for (int x = 0; x < clbClavesComunes.CheckedItems.Count; x++)
                    {
                        ClavesSeleccionadas = ClavesSeleccionadas + " " + clbClavesComunes.CheckedItems[x].ToString();
                    }

                    string CompañiasSeleccionadas = "";
                    for (int x = 0; x < clbCompañiaBomberos.CheckedItems.Count; x++)
                    {
                        CompañiasSeleccionadas = CompañiasSeleccionadas + " " + clbCompañiaBomberos.CheckedItems[x].ToString();
                    }

                    ControlSQLite guardarEmergencia = new ControlSQLite();
                    guardarEmergencia.EjecutarConsulta("INSERT INTO main.Emergencias (Claves, Compañias, Coordenadas, Situacion, OficialAcargo, Lugar, Descripcion, Fecha) VALUES ('" + ClavesSeleccionadas + "', '" + CompañiasSeleccionadas + "', '" + txtCoordenadas.Text + "', '" + txtSituacion.Text + "', '" + txtOficialAcargo.Text + "', '" + txtLugar.Text + "', '" + txtDescripcion.Text + "', '" + txtFechayHora.Text + "');");

                    MessageBox.Show("Se Guardo Correctamente la Emergencia " + txtSituacion.Text, "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (cbPublicar.Checked == true)
                    {
                        System.Diagnostics.Process.Start("https://twitter.com/intent/tweet?text=SITUACION EMERGENCIA: " + txtSituacion.Text + " LUGAR: " + txtLugar.Text + ". Bomberos Acude a la Emergencia");
                    }    

                    List<String> RutaArchivos = new List<String>();

                    for (int x = 0; x < clbCompañiaBomberos.CheckedItems.Count; x++)
                    {
                        if (clbCompañiaBomberos.CheckedItems[x].ToString() == "Primera")
                        {
                            RutaArchivos.Add(Application.StartupPath + @"\Sonidos\PrimeraCompañia.wav");
                        }
                        if (clbCompañiaBomberos.CheckedItems[x].ToString() == "Segunda")
                        {
                            RutaArchivos.Add(Application.StartupPath + @"\Sonidos\SegundaCompañia.wav");
                        }
                        if (clbCompañiaBomberos.CheckedItems[x].ToString() == "Tercera")
                        {
                            RutaArchivos.Add(Application.StartupPath + @"\Sonidos\TerceraCompañia.wav");
                        }
                        if (clbCompañiaBomberos.CheckedItems[x].ToString() == "Cuarta")
                        {
                            RutaArchivos.Add(Application.StartupPath + @"\Sonidos\CuartaCompañia.wav");
                        }
                        if (clbCompañiaBomberos.CheckedItems[x].ToString() == "Quinta")
                        {
                            RutaArchivos.Add(Application.StartupPath + @"\Sonidos\QuintaCompañia.wav");
                        }
                    }
                    Reproductor.URL = RutaArchivos[0];

                    




                        /*  try
                          {
                              SpPuertos.Open();
                              SpPuertos.DiscardInBuffer();
                              */

                        // SoundPlayer player = new SoundPlayer();
                        // player.SoundLocation = Application.StartupPath + @"\Sonidos\PrimeraCompañia.wav";
                        // player.Play();




                        /*
                        strbufferOut = "Datos a enviar";
                        SpPuertos.Write(strbufferOut);
                        */

                        /*
                            SpPuertos.Close();
                            MessageBox.Show("Se Ejecuto Correctamente las Instrucciones de la Alerta","Alertas Ejecutadas",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
    ;                         MessageBox.Show("Error al Intentar Ejecutar las Intrucciones de la Alerta, revise los ajustes de conexion ERROR: "+ex.Message,"ERROR Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                              SpPuertos.Close();
                        }
                        */

                        LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Muy bien falsa Alarma","Siempre se puede retractar",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Falta llenar Informacion, Debe Selecionar al MENOS una Clave, una Compañia de bomberos, ESCRIBIR la situacion de emergencia, el ofical a cargo, el lugar y la descripcion de la emergencia","Faltas Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnCoordenadas_Click(object sender, EventArgs e)
        {
            CoordenadasEmergencia buscarCoordendas = new CoordenadasEmergencia();
            if (buscarCoordendas.ShowDialog() == DialogResult.OK)
            {
                String Coordendas = buscarCoordendas.CoordenadasEmer;
                txtCoordenadas.Text = Coordendas;
            }
        }

        private void btnVerEmergencias_Click(object sender, EventArgs e)
        {
            Emergencias abrirEmergencias = new Emergencias();
            abrirEmergencias.ShowDialog();
        }
    }
}
