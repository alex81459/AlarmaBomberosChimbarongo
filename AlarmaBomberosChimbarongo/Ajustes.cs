using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace AlarmaBomberosChimbarongo
{
    public partial class Ajustes : Form
    {
        public Ajustes()
        {
            //Se carga el puerto a usar, la velocidad del puerto y el numero de veces que se repite la alarma
            InitializeComponent();
            CboPuertos.Text = Properties.Settings.Default.PuertoSerial;
            CboBautRate.Text = Properties.Settings.Default.VelocidadPuertoSerial;
            nRepeticionAlarma.Value = Convert.ToInt32(Properties.Settings.Default.RepeticionAlarma.ToString());
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu abrirMenu = new Menu();
            abrirMenu.Show();
        }

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            //Se buscan los puertos disponibles
            string[] PuertosDisponibles = SerialPort.GetPortNames();
            CboPuertos.Items.Clear();

            foreach (string puerto_simple in PuertosDisponibles)
            {
                CboPuertos.Items.Add(puerto_simple);
            }

            if (CboPuertos.Items.Count > 0)
            {
                MessageBox.Show("Puertos Libres Detectados, Seleccione el Puerto a Usar para Trabajar con la Placa de Arduino","Puertos Dectectados",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NO se han detectado Puertos Libres disponibles","No hay Puertos Disponibles",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                CboPuertos.Items.Clear();
                CboPuertos.Text = "";
                btnGuardarCambiosPuerto.Enabled = false;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //Se conecta con el puerto para Probarlo
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
                MessageBox.Show("Debe Seleccionar un Puerto y velocidad de transmision para realizar la Conexion","Faltan Parametros de Conexion",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarCambiosPuerto_Click(object sender, EventArgs e)
        {
            //Se registran las nuevos datos de conexion con la placa de Arduino
            if (CboPuertos.Text != "" || CboBautRate.Text != "")
            {
                ClaveMaestra verificarClave = new ClaveMaestra();

                if (verificarClave.ShowDialog() == DialogResult.OK)
                {
                    String PuertoGuardar = CboPuertos.Text;

                    Properties.Settings.Default.PuertoSerial = PuertoGuardar;
                    Properties.Settings.Default.VelocidadPuertoSerial = CboBautRate.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Se Guardo Correctamente la Configuracion del Puerto Serie", "Configuracion Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Necesita Seleccionar algun puerto y Velocidad de Transmision para Guardar los Ajustes", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarVeces_Click(object sender, EventArgs e)
        {
            //Se guarda la cantidad de veces que se repetira la alarma
            Properties.Settings.Default.RepeticionAlarma = Convert.ToInt32(nRepeticionAlarma.Value);
            Properties.Settings.Default.Save();
            MessageBox.Show("Se Guardo Correctamente la cantidad de Veces que se Repetira la Alarma Repetir: "+ Properties.Settings.Default.RepeticionAlarma+ "","Cantidad Guardada",MessageBoxButtons.OK,MessageBoxIcon.Information);

            try
            {
                ControlSQLite guardarRegistro = new ControlSQLite();
                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Repeticion Alarma','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Cambio la Cantidad de veces que se repite la Alarma a "+ nRepeticionAlarma.Value.ToString()+ "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Ajustes');");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Se lanza la direccion para cambiar los sonidos de alarma
            MessageBox.Show("Los Sonidos de Alarma deben de estar en Formato WAV y deben tener exactamente el mismo nombre del sonido que sustituirán para Remplazarlo","Informacion Cambio Sonidos",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Process.Start(Application.StartupPath + @"\Sonidos");

            try
            {
                ControlSQLite guardarRegistro = new ControlSQLite();
                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Archivos de Sonido','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Ejecuto la ventana para remplazar los archivos de sonido de la Alarma','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Ajustes');");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registros abrirRegistros = new Registros();
            abrirRegistros.ShowDialog();
        }

        private void btnOperadores_Click(object sender, EventArgs e)
        {
            Operadores AbrirOperadores = new Operadores();
            AbrirOperadores.ShowDialog();
        }

        private void btnRealizarCopiaBD_Click_1(object sender, EventArgs e)
        {
            //Se realiza una copia de seguridad
            //Se guarda el registro en la BD
            try
            {
                ControlSQLite guardarRegistro = new ControlSQLite();
                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Crear Copia Seguridad','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Creo una Copia de Seguridad de la Base de Datos, la cual fue guardada exitosamente','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Ajustes');");
            }
            catch (Exception)
            {
                throw;
            }

            //Se abre el dialogo para guardar
            SaveFileDialog DialogoGuardar = new SaveFileDialog();
            DialogoGuardar.Filter = "Archivos de Base de Datos DB(*.db)| *.db";
            DialogoGuardar.Title = "Seleccione la Ubicacion de Guardado de la Copia de Seguridad";
            DialogoGuardar.AddExtension = true;
            DialogoGuardar.DefaultExt = ".db";


            //Se extrae la fecha actual, y se tranforma a corta
            DateTime fecha = DateTime.Now;
            String FechaCorta = fecha.ToShortDateString();
            //Se remplazan las / por - para evitar errores
            FechaCorta = FechaCorta.Replace("/", "-").Replace("/", "-").Replace("/", "-");
            //Se crea el nombre de archivo para guardar
            DialogoGuardar.FileName = FechaCorta + "CopiaSeguridad" + "AlarmaBomberosChimbarongo.db";

            //Se lanza el dialogo para guardar
            if (DialogoGuardar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String UbicacionGuardar = DialogoGuardar.FileName;
                    String UbicacionBD = (Application.StartupPath + @"\AlarmaBomberosChimbarongo.db");
                    //Se elimina por si el archivo ya existe
                    File.Delete(UbicacionGuardar);
                    //Se copia la BD a la ubicacion de guardado
                    File.Copy(UbicacionBD, UbicacionGuardar);
                    MessageBox.Show("Se realizo correctamente la Copia de Seguridad de los Datos", "Copia Seguridad Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al intentar realizar la Copia de Seguridad ERROR: " + ex.Message, "Error Copia Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestaurarBD_Click_1(object sender, EventArgs e)
        {
            DialogResult resultadoMensaje = MessageBox.Show("Necesita Ingresar nuevamente la clave maestra para Restaurar la Copia de Seguridad, ADVERTENCIA TODOS LOS DATOS ACTUALES SERÁN REMPLAZADOS POR LOS DE LA COPIA SE SEGURIDAD, si la copia a restaurar es la correcta continúe con el proceso.", "Advertencia Restaurar Copia Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            ClaveMaestra verificarClave = new ClaveMaestra();
            if (verificarClave.ShowDialog() == DialogResult.OK)
            {

                OpenFileDialog BuscarCopiaSeguridad = new OpenFileDialog();

                BuscarCopiaSeguridad.Filter = "Archivos de Base de Datos DB(*.db)| *.db";
                BuscarCopiaSeguridad.Title = "Seleccione la Ubicacion de la Copia de Seguridad a Restaurar";
                if (BuscarCopiaSeguridad.ShowDialog() == DialogResult.OK)
                {
                    String UbicacionBDRespaldo = BuscarCopiaSeguridad.FileName;

                    String UbicacionBD = (Application.StartupPath + @"\AlarmaBomberosChimbarongo.db");

                    if (resultadoMensaje == DialogResult.Yes)
                    {
                        try
                        {
                            File.Delete(UbicacionBD);
                            File.Copy(UbicacionBDRespaldo, UbicacionBD);

                            MessageBox.Show("Se realizo correctamente la restaurancion de los datos del programa", "Restauracion correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            try
                            {
                                ControlSQLite guardarRegistro = new ControlSQLite();
                                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Restaurar Copia Seguridad','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Restauro una Copia de Seguridad de la Base de Datos, la cual fue restaurada exitosamente','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Ajustes');");
                            }
                            catch (Exception)
                            {
                                throw;
                            }

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

        private void btnProbar_Click(object sender, EventArgs e)
        {
            System.IO.Ports.SerialPort ArduinoPort;

            try
            {
                //crear Serial Port de Arduino
                ArduinoPort = new SerialPort();
                ArduinoPort.PortName = CboPuertos.Text;
                ArduinoPort.BaudRate = Convert.ToInt16(CboBautRate.Text);
                ArduinoPort.Open();
                MessageBox.Show("Comunicacion con Placa de Arduino establecida correctamente","Conexion Correcta",MessageBoxButtons.OK,MessageBoxIcon.Information);
                ArduinoPort.Write("b");
                MessageBox.Show("Arduino Debio Activar el Rele","Informacion Prueba",MessageBoxButtons.OK,MessageBoxIcon.Information);
                ArduinoPort.Write("a");
                MessageBox.Show("Arduino Debio Desactivar el Rele", "Informacion Prueba", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ArduinoPort.Write("b");
                Thread.Sleep(700);
                ArduinoPort.Write("a");
                Thread.Sleep(700);
                ArduinoPort.Write("b");
                Thread.Sleep(700);
                ArduinoPort.Write("a");
                Thread.Sleep(700);
                ArduinoPort.Write("b");
                Thread.Sleep(700);
                ArduinoPort.Write("a");
                Thread.Sleep(700);
                MessageBox.Show("Arduino Debio Activar y Desactivar el Rele 3 veces seguidas", "Informacion Prueba", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ArduinoPort.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Error al intentar comuniar con la Placa Arduino, por favor verificar la configuracion establecida y que el S.O tiene el Driver correcto para controlar la Placa ERROR: " + err.Message + "", "Error Comunicacion Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
