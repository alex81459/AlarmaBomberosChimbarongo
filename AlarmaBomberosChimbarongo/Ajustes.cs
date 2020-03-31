using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace AlarmaBomberosChimbarongo
{
    public partial class Ajustes : Form
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

        public Ajustes()
        {
            InitializeComponent();
            btnConectar.Enabled = false;
            btnGuardarCambiosPuerto.Enabled = false;
            CboPuertos.Text = Properties.Settings.Default.PuertoSerial;
            CboBautRate.Text = Properties.Settings.Default.VelocidadPuertoSerial;
            txtRepeticionAlarma.Text = Properties.Settings.Default.RepeticionAlarma.ToString();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRealizarCopiaBD_Click(object sender, EventArgs e)
        {
            ClaveMaestra verificarClave = new ClaveMaestra();

            if (verificarClave.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.Filter = "Archivos de Base de Datos DB(*.db)| *.db";
                DialogoGuardar.Title = "Seleccione la Ubicacion de Guardado de la Copia de Seguridad";

                DateTime fecha = DateTime.Now;
                String FechaCorta = fecha.ToShortDateString();

                DialogoGuardar.FileName = FechaCorta + "CopiaSeguridad" + "AlarmaBomberosChimbarongo.db";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        String UbicacionGuardar = DialogoGuardar.FileName;
                        String UbicacionBD = (Application.StartupPath + @"\AlarmaBomberosChimbarongo.db");
                        File.Delete(UbicacionGuardar);
                        File.Copy(UbicacionBD, UbicacionGuardar);

                        MessageBox.Show("Se realizo correctamente la Copia de Seguridad de los Datos", "Copia Seguridad Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar realizar la Copia de Seguridad ERROR: " + ex.Message, "Error Copia Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            ClaveMaestra verificarClave = new ClaveMaestra();

            if (verificarClave.ShowDialog() == DialogResult.OK)
            {
                OpenFileDialog BuscarCopiaSegurudad = new OpenFileDialog();

                BuscarCopiaSegurudad.Filter = "Archivos de Base de Datos DB(*.db)| *.db";
                BuscarCopiaSegurudad.Title = "Seleccione la Ubicacion de la Copia de Seguridad a Restaurar";
                if (BuscarCopiaSegurudad.ShowDialog() == DialogResult.OK)
                {
                    String UbicacionBDRespaldo = BuscarCopiaSegurudad.FileName;

                    String UbicacionBD = (Application.StartupPath + @"\AlarmaBomberosChimbarongo.db");


                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta seguro que desea restaurar los datos del programa?, ADVERTENCIA todos los datos actuales seran eliminados y remplazados por los datos de la copia de seguridad que selecciono", "Confirmacion Restauracion", MessageBoxButtons.YesNo);

                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        try
                        {
                            File.Delete(UbicacionBD);
                            File.Copy(UbicacionBDRespaldo, UbicacionBD);

                            MessageBox.Show("Se realizo correctamente la restaurancion de los datos del programa", "Restauracion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ha ocurrido un error al intentar restaurar la copia de seguridad ERROR: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Muy bien falsa alarma, los datos actuales se mantendran y no seran restaurados de una copia anterior", "Restauracion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }    
        }

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            string[] PuertosDisponibles = SerialPort.GetPortNames();
            CboPuertos.Items.Clear();

            foreach (string puerto_simple in PuertosDisponibles)
            {
                CboPuertos.Items.Add(puerto_simple);
            }

            if (CboPuertos.Items.Count > 0)
            {
                MessageBox.Show("Puertos Libres Detectados, Seleccione el Puerto a Usar para Trabajar","Puertos Dectectados",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnConectar.Enabled = true;

            }
            else
            {
                MessageBox.Show("NO se han detectado Puertos Libres disponibles","No hay Puertos Disponibles",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                CboPuertos.Items.Clear();
                CboPuertos.Text = "";
                btnConectar.Enabled = false;
                btnGuardarCambiosPuerto.Enabled = false;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (CboPuertos.Text != "" && CboBautRate.Text != "")
            {
                SpPuertos.BaudRate = Int32.Parse(CboBautRate.Text);
                SpPuertos.DataBits = 8;
                SpPuertos.Parity = Parity.None;
                SpPuertos.StopBits = StopBits.One;
                SpPuertos.Handshake = Handshake.None;
                SpPuertos.PortName = CboPuertos.Text;

                try
                {
                    SpPuertos.Open();
                    MessageBox.Show("Conexion Establecida Correctamente con el Puerto","Conexion Correcta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    btnGuardarCambiosPuerto.Enabled = true;
                    SpPuertos.Close();
                }
                catch (Exception ex)
                {
                    SpPuertos.Close();
                    MessageBox.Show("ERROR al Extablecer Conexion con el Puerto ERROR: "+ex.Message.ToString(), "Conexion Erronea", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGuardarCambiosPuerto.Enabled = false;
                } 
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un Puerto y velocidad de transmision para realizar la Conexion","Faltan Parametros Conexion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarCambiosPuerto_Click(object sender, EventArgs e)
        {
            ClaveMaestra verificarClave = new ClaveMaestra();

            if (verificarClave.ShowDialog() == DialogResult.OK) {
                String PuertoGuardar = CboPuertos.Text;

                Properties.Settings.Default.PuertoSerial = PuertoGuardar;
                Properties.Settings.Default.VelocidadPuertoSerial = CboBautRate.Text;
                Properties.Settings.Default.RepeticionAlarma = Convert.ToInt32(txtRepeticionAlarma.Value);
                Properties.Settings.Default.Save();
                MessageBox.Show("Se Guardo Correctamente la Configuracion del Puerto Serie", "Configuracion Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
