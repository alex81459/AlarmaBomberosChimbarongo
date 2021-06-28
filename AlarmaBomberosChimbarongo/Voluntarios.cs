using System;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Voluntarios : Form
    {
        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Voluntarios ORDER by ID DESC;");
        }

        public bool validarRut(string Stringrut)
        {
            //Proceso para verificar el rut
            bool validacioncorrecta = false;
            try
            //try... por si acaso
            {
                //Se pasan las letras a mayusculas,se quitan . y -
                Stringrut = Stringrut.ToUpper();
                Stringrut = Stringrut.Replace(".", "");
                Stringrut = Stringrut.Replace("-", "");
                int rutLimpio = int.Parse(Stringrut.Substring(0, Stringrut.Length - 1));
                //Se extrae el verificador
                char verificador = char.Parse(Stringrut.Substring(Stringrut.Length - 1, 1));
                //Se recorre el Rut y se calculan sus numeros, de atras en adelante multiplicandolos +1
                int m = 0, s = 1;
                for (; rutLimpio != 0; rutLimpio /= 10)
                {
                    s = (s + rutLimpio % 10 * (9 - m++ % 6)) % 11;
                }
                //Se revisa si el verificador final es correcto
                if (verificador == (char)(s != 0 ? s + 47 : 75))
                {
                    validacioncorrecta = true;
                }
            }
            catch (Exception)
            {//no hay respuesta si hay excepcion, porsiacaso
                validacioncorrecta = false;
            }
            return validacioncorrecta;
        }

        public void LimpiarCampos()
        {
            CargarTabla();
            txtID.Text = "¿?";
            txtRut.Text = "";
            txtNombreVoluntario.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEstado.Text = "";
            txtNumeroVoluntario.Text = "";
            txtCompañia.SelectedIndex = -1;
            txtCargo.SelectedIndex = -1;
            txtEstado.SelectedIndex = -1;
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnEliminar.Enabled = false;
            txtBuscarEn.Text = "";
        }

        public Voluntarios()
        {
            InitializeComponent();
            CargarTabla();
            cmbBuscarEn.Text = "RUT";
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45) || (e.KeyChar == 43) || (e.KeyChar == 40) || (e.KeyChar == 41) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si son numeros o - (guion) o + o ( o ) (parentecis) o borrar se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNombreContacto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44) || (e.KeyChar == 45) || (e.KeyChar == 43))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros, - (Guion) y + , se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Voluntarios Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER by ID DESC;");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRut(txtRut.Text))
            {
                if (txtID.Text == "¿?")
                {
                    if (txtNumeroVoluntario.Text != "" && txtRut.Text != "" && txtNombreVoluntario.Text != "" && txtTelefono.Text != "" && txtDireccion.Text !="" && txtCompañia.Text !="" && txtCargo.Text !="" && txtEstado.Text != "")
                    {
                        ControlSQLite ExistenciaDatos = new ControlSQLite();
                        Boolean DatoExiste = ExistenciaDatos.ExistenciaDatoEnTabla("select * from Voluntarios where NumeroVoluntario = '"+ txtNumeroVoluntario.Text+ "'");

                        if (DatoExiste)
                        {
                            MessageBox.Show("El Numero de Voluntario " + txtNumeroVoluntario.Text + " Ya se encuentra Registrado en el Sistema", "Registro ya Existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            ControlSQLite ExistenciaDatosRUT = new ControlSQLite();
                            Boolean ExistenciaRut = ExistenciaDatos.ExistenciaDatoEnTabla("select * from Voluntarios where RUT = '" + txtRut.Text + "'");

                            if (ExistenciaRut)
                            {
                                MessageBox.Show("El RUT  del Voluntario " + txtRut.Text + " Ya se encuentra Registrado en el Sistema", "Registro ya Existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                ControlSQLite guardarClaveRadial = new ControlSQLite();
                                guardarClaveRadial.EjecutarConsulta("INSERT INTO main.Voluntarios(NumeroVoluntario, RUT, NombreVoluntario, Telefono, Direccion, CompañiaBomberos, Cargo, Estado) VALUES ('" + txtNumeroVoluntario.Text + "', '" + txtRut.Text + "', '" + txtNombreVoluntario.Text + "', '" + txtTelefono.Text + "', '" + txtDireccion.Text + "', '" + txtCompañia.Text + "', '" + txtCargo.Text + "', '" + txtEstado.Text + "');");

                                MessageBox.Show("Se guardo Correctamente el Voluntario de RUT: " + txtRut.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                try
                                {
                                    ControlSQLite guardarRegistro = new ControlSQLite();
                                    guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Guardar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Registro un nuevo Voluntario con los siguientes datos, N° Voluntario: " + txtNumeroVoluntario.Text + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreVoluntario.Text + " Telefono: "+ txtTelefono.Text+ " Direccion: "+ txtDireccion.Text+ " Compañia Bomberos: "+ txtCompañia.Text+ " Cargo: "+ txtCargo.Text+ " Estado: "+ txtEstado.Text+ "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Voluntarios');");
                                }
                                catch (Exception)
                                {
                                    throw;
                                }

                                LimpiarCampos();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Ingresar los Datos Obligarorios para registrar a un voluntario, N° de Voluntario, RUT, Nombre del Voluntario, Telefono, Direccion, Compañia de Bomberos, Cargo y Estado", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (txtRut.Text != "" && txtNombreVoluntario.Text != "" && txtTelefono.Text != "" && txtDireccion.Text != "" && txtEstado.Text != "")
                    {
                        ClaveMaestra verificarClave = new ClaveMaestra();

                        if (verificarClave.ShowDialog() == DialogResult.OK)
                        {
                            ControlSQLite modificarClaveRadial = new ControlSQLite();
                            modificarClaveRadial.EjecutarConsulta("UPDATE main.Voluntarios SET 'NumeroVoluntario'='" + txtNumeroVoluntario.Text + "','RUT'='" + txtRut.Text + "','NombreVoluntario'='" + txtNombreVoluntario.Text + "','Telefono'='" + txtTelefono.Text + "','Direccion'='" + txtDireccion.Text + "','CompañiaBomberos'='" + txtCompañia.Text + "','Cargo'='" + txtCargo.Text + "','Estado'='" + txtEstado.Text + "' Where ID= '" + txtID.Text + "' ");

                            MessageBox.Show("Se Modifico Correctamente el Voluntario con el RUT: " + txtRut.Text + "", "Modificacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            try
                            {
                                ControlSQLite guardarRegistro = new ControlSQLite();
                                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Modificar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Modifico un Voluntario con los siguientes datos, N° Voluntario: " + txtNumeroVoluntario.Text + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreVoluntario.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Compañia Bomberos: " + txtCompañia.Text + " Cargo: " + txtCargo.Text + " Estado: " + txtEstado.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Voluntarios');");
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Ingresar los Datos Obligarorios de Numero de Telefono, Categoria y Nombre de Contacto para Modificar el Actual", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("El RUT ingresado no es valido, por favor verifique que este bien escrito","Error RUT",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }  
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtRut.Text = Convert.ToString(fila.Cells["RUT"].Value);
                txtNombreVoluntario.Text = Convert.ToString(fila.Cells["NombreVoluntario"].Value);
                txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
                txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
                txtEstado.Text = Convert.ToString(fila.Cells["Estado"].Value);
                txtNumeroVoluntario.Text = Convert.ToString(fila.Cells["NumeroVoluntario"].Value);
                txtCompañia.Text = Convert.ToString(fila.Cells["CompañiaBomberos"].Value);
                txtCargo.Text = Convert.ToString(fila.Cells["Cargo"].Value);
                btnEliminar.Enabled = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                MessageBox.Show("Debe SELECCIONAR Un Voluntario para Eliminarlo de los Registros, para seleccionar debe darle Doble Click", "Voluntario NO Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Eliminar al Voluntario RUT: " + txtRut.Text + " ?", "Confirmacion Eliminar", MessageBoxButtons.YesNo);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    ClaveMaestra verificarClave = new ClaveMaestra();

                    if (verificarClave.ShowDialog() == DialogResult.OK)
                    {
                        ControlSQLite eliminarClaveRadial = new ControlSQLite();
                        eliminarClaveRadial.EjecutarConsulta("DELETE FROM main.Voluntarios WHERE _rowid_ IN ('" + txtID.Text + "');");

                        MessageBox.Show("El Voluntario Rut: " + txtRut.Text + " se Elimino Correctamente", "Eliminado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            ControlSQLite guardarRegistro = new ControlSQLite();
                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Eliminar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Elimino un Voluntario con los siguientes datos, N° Voluntario: " + txtNumeroVoluntario.Text + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreVoluntario.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Compañia Bomberos: " + txtCompañia.Text + " Cargo: " + txtCargo.Text + " Estado: " + txtEstado.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Voluntarios');");
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        LimpiarCampos();
                    }   
                }
                else
                {
                    MessageBox.Show("Muy Bien Falsa Alarma el Voluntario Rut: " + txtRut.Text + " No se ha Eliminado", "Eliminacion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtNumeroVoluntario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si son numeros o borrar se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
