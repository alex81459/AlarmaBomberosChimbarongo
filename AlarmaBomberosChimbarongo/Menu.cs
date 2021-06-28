using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace AlarmaBomberosChimbarongo
{
    public partial class Menu : Form
    {
        //Lista para almacenar las rutas de los arhivos de los sonidos de alerta
        List<String> RutaArchivosCompañias = new List<String>();
        List<String> RutaArchivosClaves = new List<String>();
        List<String> RutaAlarmas = new List<String>();
        List<String> RutaArchivosTimbres = new List<String>();
        SerialPort ArduinoPort;
        Boolean ReproduciendoSonidos = false;
        Boolean ReproduciendoAlarma = false;

        public Menu()
        {
            InitializeComponent();
            //Comienza a contar el tiempo y fecha
            tiempo.Enabled = true;
            //Se carga el rut del Usuario que inicio sesion
            txtUsuarioActual.Text = FuncionesAplicacion.RutLogin;
            txtNombreOperador.Text = FuncionesAplicacion.NombreLogin;

            try
            {
                //Se crea y configura el puerto para comunicarse con arduino
                ArduinoPort = new SerialPort();
                ArduinoPort.PortName = Properties.Settings.Default.PuertoSerial;
                ArduinoPort.BaudRate = Convert.ToInt16(Properties.Settings.Default.VelocidadPuertoSerial);
                //Se habre la conexion para probarla
                ArduinoPort.Open();
                ArduinoPort.Close();
                MessageBox.Show("Placa Arduino Lista para Funcionar", "Arduino", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al intentar comunicar con la Placa Arduino, por favor revise la configuracion en Ajustes y que el S.O tiene el Driver correcto para controlar la Placa ERROR: " + e.Message + "", "Error Comunicacion Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ArduinoPort.Close();
            }
        }

        //Metodo para Activar el rele con arduino
        private void ActivarRele()
        {
            try
            {
                ArduinoPort.Close();
                ArduinoPort.Open();
                //Se envia b como intrucion para el arduino, para que active el rele
                ArduinoPort.Write("b");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al intentar Activar el Rele, mediante la placa Arduino ERROR: " + e.Message + "", "Error Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //Metodo para Desactivar el rele con arduino
        private void DesactivarRele()
        {
            try
            {
                ArduinoPort.Close();
                ArduinoPort.Open();
                //Se envia a como intrucion para el arduino, para que active el rele
                ArduinoPort.Write("a");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al intentar Desactivar el Rele, mediante la placa Arduino ERROR: " + e.Message + "", "Error Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Variables para controlar y mover el Form desde la barra
        private Point pos = Point.Empty;
        private bool move = false;

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //Se minimiza el form
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            //Se llama al metodo para cerrar la sesion
            CerrarSesion();
        }

        public void CerrarSesion()
        {
            //Se notifica y se borra el RUT actual
            MessageBox.Show("La sesión se cerró correctamente del RUT: " + FuncionesAplicacion.RutLogin + "", "Sesion Terminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FuncionesAplicacion.RutLogin = "";
            FuncionesAplicacion.NombreLogin = "";

            //Se cierra el puerto de Arduino si esta abierto
            if (ArduinoPort.IsOpen)
            {
                ArduinoPort.Close();
            }

            //Se cierra el Form y se abre el login
            this.Close();
            Login AbrirLogin = new Login();
            AbrirLogin.Show();
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
            if (ArduinoPort.IsOpen == true)
            {
                ArduinoPort.Close();
            }

            ClaveMaestra verificarClave = new ClaveMaestra();

            if (verificarClave.ShowDialog() == DialogResult.OK)
            {
                this.Close();
                Ajustes abrirAjustes = new Ajustes();
                abrirAjustes.ShowDialog();
                ArduinoPort.Close();
            }
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
                MessageBox.Show("Error al intentar abrir la GRE - GUÍA DE RESPUESTA EN CASO DE EMERGENCIA ERROR: " + ex.Message, "Error Abrir GRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            for (int i = 0; i < clbClavesComunes.Items.Count; i++)
            {
                clbClavesComunes.SetItemChecked(i, false);
            }

            for (int i = 0; i < clbCompañiaBomberos.Items.Count; i++)
            {
                clbCompañiaBomberos.SetItemChecked(i, false);
            }

            for (int i = 0; i < clbTimbres.Items.Count; i++)
            {
                clbTimbres.SetItemChecked(i, false);
            }

            txtCoordenadas.Text = "";
            cbPublicar.Checked = false;
            txtLugar.Text = "";
            txtDescripcion.Text = "";
        }

        private void AlmacenarDireccionesAlertas() {

            foreach (String itemr in clbCompañiaBomberos.CheckedItems)
            {
                if (itemr == "Primera")
                {
                    RutaArchivosCompañias.Add(Application.StartupPath + @"\Sonidos\PrimeraCompania.wav");
                }
                if (itemr == "Segunda")
                {
                    RutaArchivosCompañias.Add(Application.StartupPath + @"\Sonidos\SegundaCompania.wav");
                }
                if (itemr == "Tercera")
                {
                    RutaArchivosCompañias.Add(Application.StartupPath + @"\Sonidos\TerceraCompania.wav");
                }
                if (itemr == "Cuarta")
                {
                    RutaArchivosCompañias.Add(Application.StartupPath + @"\Sonidos\CuartaCompania.wav");
                }
                if (itemr == "Quinta")
                {
                    RutaArchivosCompañias.Add(Application.StartupPath + @"\Sonidos\QuintaCompania.wav");
                }
            }

            foreach (String itemt in clbClavesComunes.CheckedItems)
            {
                if (itemt == "10-0-LlamadoEstructural")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-0-LlamadoEstructural.wav");
                }
                if (itemt == "10-1-IncendioVehicular")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-1-IncendioVehicular.wav");
                }
                if (itemt == "10-2-PastizaloBasura")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-2-PastizaloBasura.wav");
                }
                if (itemt == "10-3-PersonaAtrapada")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-3-PersonaAtrapada.wav");
                }
                if (itemt == "10-4-RescateVehicular")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-4-RescateVehicular.wav");
                }
                if (itemt == "10-5-MaterialPeligroso")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-5-MaterialPeligroso.wav");
                }
                if (itemt == "10-6-EmanacióndeGas")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-6-EmanacióndeGas.wav");
                }
                if (itemt == "10-7-Electrico")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-7-Electrico.wav");
                }
                if (itemt == "10-8-NoClasificado")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-8-NoClasificado.wav");
                }
                if (itemt == "10-9-OtrosServicios")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-9-OtrosServicios.wav");
                }
                if (itemt == "10-12-ApoyoOtrosCuerpos")
                {
                    RutaArchivosClaves.Add(Application.StartupPath + @"\Sonidos\10-12-ApoyoOtrosCuerpos.wav");
                }
            }

            foreach (String item in clbTimbres.CheckedItems)
            {
                if (item == "Primera")
                {
                    RutaArchivosTimbres.Add(Application.StartupPath + @"\Sonidos\Timbre1.wav");
                }
                if (item == "Segunda")
                {
                    RutaArchivosTimbres.Add(Application.StartupPath + @"\Sonidos\Timbre2.wav");
                }
                if (item == "Tercera")
                {
                    RutaArchivosTimbres.Add(Application.StartupPath + @"\Sonidos\Timbre3.wav");
                }
                if (item == "Cuarta")
                {
                    RutaArchivosTimbres.Add(Application.StartupPath + @"\Sonidos\Timbre4.wav");
                }
                if (item == "Quinta")
                {
                    RutaArchivosTimbres.Add(Application.StartupPath + @"\Sonidos\Timbre5.wav");
                }
            }
        }

        private void PublicarFacebook()
        {
            //Biblioteca using Facebook;
            /*
            dynamic messagePost = new ExpandoObject();
            messagePost.name = "Alerta de Despacho de Bomberos";
            // "{*actor*} " + "posted news...";
            //<---{*actor*} is the user (i.e.: Alex) 
            messagePost.caption = "[caption] Facebook caption";
            messagePost.description = "Lugar: " + txtLugar.Text + "Descripcion: " + txtDescripcion.Text;
            messagePost.message = "Lugar: " + txtLugar.Text + "Descripcion: " + txtDescripcion.Text;
            string acccessToken = "EAAEXZAbEkNF8BAGZBDZBRki1j1kKwfKl0KILKqrCqYJYOfUnZC93GQsB8RJ4hhL6dop3tX7nZBBCNlgcDGMZClPnn5atL39l0GqfWTsqgt1ZCZBhSzad17ZB86KZBmWXCyeqy6TGRWDmPi1HnuKQ02gb9jQfmGfyljOyEdCxzmI8DQuFlDgHCvcmEOwP7aobGeaKwL7vDJzCuqTILsn4LMKsiWHXDusn5lgPw9k9dvYnOB6jVoaREfBzhTuXyGVz3ZA1AQZD";

            FacebookClient appp = new FacebookClient(acccessToken);
            try
            {
               var postId = appp.Post("307200507786335" + "/feed", messagePost);
            }
              catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception
               MessageBox.Show(ex.Message);
            }
            */
        }

        private void LimpiarReproduccion()
        {
            //Se limpia la lista de reproducion, para no reproducir sonidos anteriores
            RutaArchivosCompañias.Clear();
            for (int i = 0; i < RutaArchivosCompañias.Count; i++)
            {
                RutaArchivosCompañias.RemoveAt(i);
                i--;
            }
            RutaArchivosClaves.Clear();
            for (int i = 0; i < RutaArchivosClaves.Count; i++)
            {
                RutaArchivosTimbres.RemoveAt(i);
                i--;
            }
            RutaArchivosTimbres.Clear();
            for (int i = 0; i < RutaArchivosTimbres.Count; i++)
            {
                RutaArchivosTimbres.RemoveAt(i);
                i--;
            }
            RutaAlarmas.Clear();
            for (int i = 0; i < RutaAlarmas.Count; i++)
            {
                RutaArchivosTimbres.RemoveAt(i);
                i--;
            }

            Reproductor.currentPlaylist.clear();
            try
            {
                Reproductor.playlistCollection.remove(Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0));
                Reproductor.playlistCollection.setDeleted(Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0), true);
            }
            catch (Exception)
            {
            }

            try
            {
                Reproductor.playlistCollection.remove(Reproductor.playlistCollection.getByName("1Alarma").Item(0));
                Reproductor.playlistCollection.setDeleted(Reproductor.playlistCollection.getByName("1Alarma").Item(0), true);
            }
            catch (Exception)
            {
            }

            try
            {
                Reproductor.playlistCollection.remove(Reproductor.playlistCollection.getByName("2Alarma").Item(0));
                Reproductor.playlistCollection.setDeleted(Reproductor.playlistCollection.getByName("2Alarma").Item(0), true);
            }
            catch (Exception)
            {
            }

            try
            {
                Reproductor.playlistCollection.remove(Reproductor.playlistCollection.getByName("Aviso").Item(0));
                Reproductor.playlistCollection.setDeleted(Reproductor.playlistCollection.getByName("Aviso").Item(0), true);
            }
            catch (Exception)
            {
            }

            //Reproductor.playlistCollection.remove("AlarmaCarros");
            Thread.Sleep(500);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clbClavesComunes.CheckedIndices.Count != 0 && clbCompañiaBomberos.CheckedIndices.Count != 0 && txtLugar.Text != "" && txtDescripcion.Text != "")
            {
                ReproduciendoSonidos = false;
                //Se llama al metodo limpiar reproducion para que no reproduzca alertas anteriores
                LimpiarReproduccion();

                //Se pregunta si esta seguro de continuar
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Despachar la siguiente Emergencia: " + txtDescripcion.Text + "? y ¿Esta seguro que los datos estan correctamente ingresados?", "Confirmar Emergencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                //Si la respuesta es positiva
                if (resultadoMensaje == DialogResult.Yes)
                {
                    try
                    {
                        //Se Guardan en un String las claves seleccionadas
                        string ClavesSeleccionadas = "";
                        for (int x = 0; x < clbClavesComunes.CheckedItems.Count; x++)
                        {
                            ClavesSeleccionadas = ClavesSeleccionadas + " " + clbClavesComunes.CheckedItems[x].ToString();
                        }

                        //Se Guardan en un String las Compañias Seleccionadas
                        string CompañiasSeleccionadas = "";
                        for (int x = 0; x < clbCompañiaBomberos.CheckedItems.Count; x++)
                        {
                            CompañiasSeleccionadas = CompañiasSeleccionadas + " " + clbCompañiaBomberos.CheckedItems[x].ToString();
                        }

                        //Se Guardan en un Sring los Timbres Seleccionados
                        string TimbresSeleccionados = "";
                        for (int x = 0; x < clbTimbres.CheckedItems.Count; x++)
                        {
                            TimbresSeleccionados = TimbresSeleccionados + " " + clbTimbres.CheckedItems[x].ToString();
                        }

                        //Si se escoje en publicar en redes sociales
                        if (cbPublicar.Checked)
                        {
                            //Se llama al metodo de publicar en facebook
                            //PublicarFacebook();
                            Process.Start("https://m.facebook.com/ComandanciaCBCH/?ref=bookmarks&soft=composer");
                        }

                        //Se llama al metodo de almacenar las dirrecciones de las alertas que se reproduciran
                        AlmacenarDireccionesAlertas();

                        //Se crea la lista de reproducion
                        Reproductor.playlistCollection.newPlaylist("AlarmaCarros");

                        //Se Agregan al reproductor los sondos de las alarmas a reproducir, dentro de un ciclo
                        //Que se repitra segun la cantidad de veces que se configuro sonar las alarmas
                        //en (Properties.Settings.Default.RepeticionAlarma)
                        int CantidadRepeticionAlarma = (Properties.Settings.Default.RepeticionAlarma);
                        int f = 0;
                        while (f < CantidadRepeticionAlarma)
                        {
                            foreach (String itemco in RutaArchivosCompañias)
                            {
                                Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0).appendItem(Reproductor.newMedia(itemco));
                                Thread.Sleep(500);
                            }

                            foreach (String itemc in RutaArchivosClaves)
                            {
                                Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0).appendItem(Reproductor.newMedia(itemc));
                                Thread.Sleep(500);
                            }

                            foreach (String itemt in RutaArchivosTimbres)
                            {
                                Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0).appendItem(Reproductor.newMedia(itemt));
                                Thread.Sleep(500);
                            }
                            //Aumenta en uno el ciclo de repeticion de alarma
                            f++;
                        }

                        //Se registra la emergencias
                        ControlSQLite guardarEmergencia = new ControlSQLite();
                        guardarEmergencia.EjecutarConsulta("INSERT INTO main.Emergencias (Claves, Compañias, Coordenadas, Lugar, Descripcion, Fecha) VALUES ('" + ClavesSeleccionadas + "', '" + CompañiasSeleccionadas + "', '" + txtCoordenadas.Text + "','" + txtLugar.Text + "', '" + txtDescripcion.Text + "', '" + txtFechayHora.Text + "');");

                        //Se registra quien registro la emergencia
                        ControlSQLite guardarRegistro = new ControlSQLite();
                        guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Despacho','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Genero un Despacho con los siguientes datos, Claves: " + ClavesSeleccionadas + " Compañias: " + CompañiasSeleccionadas + " Coordenadas: " + txtCoordenadas.Text + " Lugar: " + txtLugar.Text + " Descripcion: " + txtDescripcion.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Despachos');");

                        MessageBox.Show("Se Guardo Correctamente la Emergencia con Descripcion: " + txtDescripcion.Text, "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Se activa el Arduino, se envia B al Arduino para activar el rele
                        ActivarRele();

                        //Se espera 1 segundo para ejecutar el codigo
                        Thread.Sleep(1500);

                        //Se inica la Reproducion de las alarmas, en la lista "AlarmasCarros"
                        Reproductor.currentPlaylist = Reproductor.playlistCollection.getByName("AlarmaCarros").Item(0);
                        ReproduciendoSonidos = true;
                    }
                    catch (Exception ef)
                    {
                        MessageBox.Show("Error al intentar realizar el proceso de dar la Alerta, el registro del despacho no ha sido registrado ERROR " + ef.Message + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Muy bien falsa Alarma", "Siempre se puede retractar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Falta llenar Informacion, Debe Selecionar al MENOS una Clave, una Compañia de bomberos, ESCRIBIR el lugar y la descripcion de la emergencia", "Faltas Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CamposActivos(Boolean CamposDisponibles)
        {
            clbCompañiaBomberos.Enabled = CamposDisponibles;
            clbClavesComunes.Enabled = CamposDisponibles;
            clbTimbres.Enabled = CamposDisponibles;
            btnCoordenadas.Enabled = CamposDisponibles;
            cbPublicar.Enabled = CamposDisponibles;
            btnAlarma1.Enabled = CamposDisponibles;
            btnAlarma2.Enabled = CamposDisponibles;
            button1.Enabled = CamposDisponibles;
            btnAviso.Enabled = CamposDisponibles;
        }

        private void btnCoordenadas_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Proximamente :)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnRegistros_Click(object sender, EventArgs e)
        {
            Registros abrirRegistros = new Registros();
            abrirRegistros.ShowDialog();
        }

        private void btnAlarma1_Click(object sender, EventArgs e)
        {
            //Se pregunta si esta seguro de continuar
            DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Activar la 1º Alarma?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //Si la respuesta es positiva
            if (resultadoMensaje == DialogResult.Yes)
            {
                //Se limpia el reproductor
                LimpiarReproduccion();

                try
                {
                    ControlSQLite guardarRegistro = new ControlSQLite();
                    guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Despacho','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " hizo sonar la Primera Alarma ','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Despachos');");
                }
                catch (Exception)
                {
                }

                RutaAlarmas.Add(Application.StartupPath + @"\Sonidos\1Alarma.wav");

                Reproductor.currentPlaylist.clear();
                Reproductor.playlistCollection.newPlaylist("1Alarma");

                int CantidadRepeticionAlarma = (Properties.Settings.Default.RepeticionAlarma);
                int f = 0;
                while (f < CantidadRepeticionAlarma)
                {
                    foreach (String itemco in RutaAlarmas)
                    {
                        Reproductor.playlistCollection.getByName("1Alarma").Item(0).appendItem(Reproductor.newMedia(itemco));
                        Thread.Sleep(500);
                    }
                    //Aumenta en uno el ciclo de repeticion de alarma
                    f++;
                }

                //Se activa el Arduino, se envia B al Arduino para activar el rele
                ActivarRele();

                //Se espera 1,5 segundos para ejecutar el codigo
                Thread.Sleep(1500);

                Reproductor.currentPlaylist = Reproductor.playlistCollection.getByName("1Alarma").Item(0);
                ReproduciendoAlarma = true;
            }
        }

        private void btnAlarma2_Click(object sender, EventArgs e)
        {
            //Se pregunta si esta seguro de continuar
            DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Activar la 2º Alarma?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //Si la respuesta es positiva
            if (resultadoMensaje == DialogResult.Yes)
            {
                //Se limpia el reproductor y se crea la lista de reproducion "AlarmaCarros"
                LimpiarReproduccion();

                try
                {
                    ControlSQLite guardarRegistro = new ControlSQLite();
                    guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Despacho','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " hizo sonar la Segunda Alarma ','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Despachos');");
                }
                catch (Exception)
                {
                }

                RutaAlarmas.Add(Application.StartupPath + @"\Sonidos\2Alarma.wav");

                Reproductor.currentPlaylist.clear();
                Reproductor.playlistCollection.newPlaylist("2Alarma");

                int CantidadRepeticionAlarma = (Properties.Settings.Default.RepeticionAlarma);
                int f = 0;
                while (f < CantidadRepeticionAlarma)
                {
                    foreach (String itemco in RutaAlarmas)
                    {
                        Reproductor.playlistCollection.getByName("2Alarma").Item(0).appendItem(Reproductor.newMedia(itemco));
                        Thread.Sleep(500);
                    }
                    //Aumenta en uno el ciclo de repeticion de alarma
                    f++;
                }

                //Se activa el Arduino, se envia B al Arduino para activar el rele
                ActivarRele();

                //Se espera 1,5 segundos para ejecutar el codigo
                Thread.Sleep(1500);

                Reproductor.currentPlaylist = Reproductor.playlistCollection.getByName("2Alarma").Item(0);
                ReproduciendoAlarma = true;
            }
        }

        private void btnAviso_Click(object sender, EventArgs e)
        {
            //Se pregunta si esta seguro de continuar
            DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Activar el Aviso de Alarma?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //Si la respuesta es positiva
            if (resultadoMensaje == DialogResult.Yes)
            {
                //Se limpia el reproductor y se crea la lista de reproducion "AlarmaCarros"
                LimpiarReproduccion();

                try
                {
                    ControlSQLite guardarRegistro = new ControlSQLite();
                    guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Despacho','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " hizo sonar la Alarma de Aviso','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Despachos');");
                }
                catch (Exception)
                {
                }

                RutaAlarmas.Add(Application.StartupPath + @"\Sonidos\Aviso.wav");

                Reproductor.currentPlaylist.clear();
                Reproductor.playlistCollection.newPlaylist("Aviso");

                int CantidadRepeticionAlarma = (Properties.Settings.Default.RepeticionAlarma);
                int f = 0;
                while (f < CantidadRepeticionAlarma)
                {
                    foreach (String itemco in RutaAlarmas)
                    {
                        Reproductor.playlistCollection.getByName("Aviso").Item(0).appendItem(Reproductor.newMedia(itemco));
                        Thread.Sleep(500);
                    }
                    //Aumenta en uno el ciclo de repeticion de alarma
                    f++;
                }

                //Se activa el Arduino, se envia B al Arduino para activar el rele
                ActivarRele();

                //Se espera 1,5 segundos para ejecutar el codigo
                Thread.Sleep(1500);

                Reproductor.currentPlaylist = Reproductor.playlistCollection.getByName("Aviso").Item(0);
                ReproduciendoAlarma = true;
            }
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Se cierra el puerto de Arduino
            ArduinoPort.Close();
        }

        private void Reproductor_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 1:    // Stopped
                    if (ReproduciendoAlarma == true)
                    {
                        try
                        {
                            //Se desactiva el Rele de arduino
                            ArduinoPort.Write("a");
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error al intentar comunicar con la Placa Arduino, por favor revise la configuracion en Ajustes y que el S.O tiene el Driver correcto para controlar la Placa ERROR: " + error.Message + "", "Error Comunicacion Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ReproduciendoAlarma = false;
                    }
                    break;

                case 10:   // Ready
                    if (ReproduciendoSonidos == true)
                    {
                        //Se limpian los campos
                        LimpiarCampos();
                        try
                        {
                            //Se desactiva el Rele de arduino
                            ArduinoPort.Write("a");   
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show("Error al intentar comunicar con la Placa Arduino, por favor revise la configuracion en Ajustes y que el S.O tiene el Driver correcto para controlar la Placa ERROR: " + error.Message + "", "Error Comunicacion Arduino", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ReproduciendoSonidos = false;
                    }
                    if (ReproduciendoAlarma == true)
                    {
                        ArduinoPort.Write("a");
                        ReproduciendoAlarma = false;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DesactivarRele();
        }

        int SeleccionAnterior = 0;

        private void clbClavesComunes_Click(object sender, EventArgs e)
        {
            if (clbClavesComunes.CheckedItems.Count >= 1)
            {
                MessageBox.Show("Solo puede Seleccionar una Clave", "Seleccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clbClavesComunes.SetItemChecked(SeleccionAnterior, false);
                SeleccionAnterior = clbClavesComunes.SelectedIndex;
            }
            else
            {
                SeleccionAnterior = clbClavesComunes.SelectedIndex;
            }
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            AcercaDe AbrirAcerca = new AcercaDe();
            AbrirAcerca.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
