using System;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Operadores : Form
    {
        public Operadores()
        {
            InitializeComponent();
            CargarTabla();
            cmbBuscarEn.Text = "Rut";
        }

        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Operadores ORDER by ID DESC;");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LimpiarCampos()
        {
            CargarTabla();
            txtID.Text = "¿?";
            txtRut.Text = "";
            txtNombreOperador.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEstado.Text = "";
            nNumeroOperador.Text = "";
            txtEstado.SelectedIndex = -1;
            txtClave1.Text = "";
            txtClave2.Text = "";
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnEliminar.Enabled = false;
            txtBuscarEn.Text = "";
        }

        private void txtRut_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNombreVoluntario_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtClave1_KeyPress(object sender, KeyPressEventArgs e)
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
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Operadores Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER by ID DESC;");
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                nNumeroOperador.Value = Convert.ToInt32(Convert.ToString(fila.Cells["ID"].Value));
                txtRut.Text = Convert.ToString(fila.Cells["Rut"].Value);
                txtNombreOperador.Text = Convert.ToString(fila.Cells["Nombre"].Value);
                txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
                txtEstado.Text = Convert.ToString(fila.Cells["Estado"].Value);
                btnEliminar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarRut(txtRut.Text))
            {
                if (txtID.Text == "¿?")
                {
                    if (txtRut.Text != "" && txtNombreOperador.Text != "" && txtTelefono.Text != "" && txtDireccion.Text != "" && txtEstado.Text != "")
                    {
                        ControlSQLite ExistenciaDatos = new ControlSQLite();
                        Boolean DatoExiste = ExistenciaDatos.ExistenciaDatoEnTabla("select * from Operadores where NumeroOperador = '" + nNumeroOperador.Value.ToString() + "'");

                        if (DatoExiste)
                        {
                            MessageBox.Show("El Numero de Operador " + nNumeroOperador.Value.ToString() + " Ya se encuentra Registrado en el Sistema", "Registro ya Existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            ControlSQLite ExistenciaDatosRUT = new ControlSQLite();
                            Boolean ExistenciaRut = ExistenciaDatos.ExistenciaDatoEnTabla("select * from Operadores where Rut = '" + txtRut.Text + "'");

                            if (ExistenciaRut)
                            {
                                MessageBox.Show("El RUT  del Operador " + txtRut.Text + " Ya se encuentra Registrado en el Sistema", "Registro ya Existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if (txtClave1.Text == "" || txtClave2.Text == "")
                                {
                                    MessageBox.Show("No ha ingresado una clave para el operador del sistema", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    if (txtClave1.Text != txtClave2.Text)
                                    {
                                        MessageBox.Show("Las Claves Ingresadas no son Correctas, ambas deben ser identicas para verificarse", "Datos Erroneo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        if (txtClave1.Text.Length >= 8)
                                        {
                                            ControlSQLite guardarClaveRadial = new ControlSQLite();
                                            guardarClaveRadial.EjecutarConsulta("INSERT INTO main.Operadores(Rut, Clave, Nombre, Direccion, Telefono, Estado, NumeroOperador) VALUES ('" + txtRut.Text + "', '" + txtClave1.Text + "', '" + txtNombreOperador.Text + "', '" + txtDireccion.Text + "', '" + txtTelefono.Text + "', '" + txtEstado.Text + "', '" + nNumeroOperador.Value.ToString() + "');");

                                            MessageBox.Show("Se guardo Correctamente el Voluntario de RUT: " + txtRut.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            try
                                            {
                                                ControlSQLite guardarRegistro = new ControlSQLite();
                                                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Guardar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Registro un nuevo Operador con los siguientes datos, N° Operador: " + nNumeroOperador.Value.ToString() + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreOperador.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Estado: " + txtEstado.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Operadores');");
                                            }
                                            catch (Exception)
                                            {
                                                throw;
                                            }
                                            LimpiarCampos();
                                        }
                                        else
                                        {
                                            MessageBox.Show("La clave debe tener 8 caracteres minimo por seguridad","Clave Insegura",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Ingresar los Datos Obligarorios para registrar a un Operador, N° de Operador, RUT, Nombre del Operador, Telefono, Direccion y Estado", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (txtRut.Text != "" && txtNombreOperador.Text != "" && txtTelefono.Text != "" && txtDireccion.Text != "" && txtEstado.Text != "")
                    {
                        ClaveMaestra verificarClave = new ClaveMaestra();

                        if (verificarClave.ShowDialog() == DialogResult.OK)
                        {
                            if (cbCambiarClave.Checked == false)
                            {
                                ControlSQLite modificarClaveRadial = new ControlSQLite();
                                modificarClaveRadial.EjecutarConsulta("UPDATE main.Operadores SET 'NumeroOperador'='" + nNumeroOperador.Value.ToString() + "','RUT'='" + txtRut.Text + "','Nombre'='" + txtNombreOperador.Text + "','Telefono'='" + txtTelefono.Text + "','Direccion'='" + txtDireccion.Text + "','Estado'='" + txtEstado.Text + "' Where ID= '" + txtID.Text + "' ");
                                MessageBox.Show("Se Modifico Correctamente el Voluntario con el RUT: " + txtRut.Text + " Sin Cambiar su Clave de Sesion", "Modificacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ControlSQLite guardarRegistro = new ControlSQLite();
                                guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Modificar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Modifico a un Operador con los siguientes datos, Numero del Operador" + nNumeroOperador.Value.ToString() + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreOperador.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Estado: " + txtEstado.Text + " y NO Cambio su Clave','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Operadores');");

                                LimpiarCampos();
                            }
                            else
                            {
                                if (txtClave1.Text == "" || txtClave2.Text == "")
                                {
                                    MessageBox.Show("No ha ingresado una clave para cambiar al operador del sistema", "Faltan Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    if (txtClave1.Text != txtClave2.Text)
                                    {
                                        MessageBox.Show("Las Claves Ingresadas no son Correctas, ambas deben ser identicas para verificarse", "Datos Erroneo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        if (txtClave1.Text.Length >= 8)
                                        {
                                            ControlSQLite modificarClaveRadial = new ControlSQLite();
                                            modificarClaveRadial.EjecutarConsulta("UPDATE main.Operadores SET 'NumeroOperador'='" + nNumeroOperador.Value.ToString() + "','RUT'='" + txtRut.Text + "','Nombre'='" + txtNombreOperador.Text + "','Telefono'='" + txtTelefono.Text + "','Direccion'='" + txtDireccion.Text + "','Estado'='" + txtEstado.Text + "','Clave'='" + txtClave1.Text + "' Where ID= '" + txtID.Text + "' ");
                                            MessageBox.Show("Se Modifico Correctamente el Voluntario con el RUT: " + txtRut.Text + " y Se Cambo su Clave de Sesion", "Modificacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            ControlSQLite guardarRegistro = new ControlSQLite();
                                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Modificar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Modifico a un Operador con los siguientes datos, Numero del Operador" + nNumeroOperador.Value.ToString() + " Rut: " + txtRut.Text + " Nombre Voluntario: " + txtNombreOperador.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Estado: " + txtEstado.Text + " y Cambio su Clave','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Operadores');");

                                            LimpiarCampos();
                                        }
                                        else
                                        {
                                            MessageBox.Show("La clave debe tener 8 caracteres minimo por seguridad", "Clave Insegura", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                    }
                                }
                            }
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
                MessageBox.Show("El RUT ingresado no es valido, por favor verifique que este bien escrito", "Error RUT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                MessageBox.Show("Debe SELECCIONAR Un Operador para Eliminarlo de los Registros, para seleccionar debe darle Doble Click", "Operador NO Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Eliminar al Operador RUT: " + txtRut.Text + " ?", "Confirmacion Eliminar", MessageBoxButtons.YesNo);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    ClaveMaestra verificarClave = new ClaveMaestra();

                    if (verificarClave.ShowDialog() == DialogResult.OK)
                    {
                        ControlSQLite eliminarClaveRadial = new ControlSQLite();
                        eliminarClaveRadial.EjecutarConsulta("DELETE FROM main.Operadores WHERE _rowid_ IN ('" + txtID.Text + "');");

                        MessageBox.Show("El Operador Rut: " + txtRut.Text + " se Elimino Correctamente", "Eliminado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            ControlSQLite guardarRegistro = new ControlSQLite();
                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Eliminar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Elimino un Operador con los siguientes datos, Rut: " + txtRut.Text + " Nombre Operador: " + txtNombreOperador.Text + " Telefono: " + txtTelefono.Text + " Direccion: " + txtDireccion.Text + " Estado: " + txtEstado.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Operadores');");
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

        private void cbVerClave_CheckedChanged(object sender, EventArgs e)
        {
            String Clave1 = txtClave1.Text; String Clave2 = txtClave2.Text;
            if (cbVerClave.Checked == true)
            {
                txtClave1.UseSystemPasswordChar = false;
                txtClave1.Text = Clave1;
                txtClave2.UseSystemPasswordChar = false;
                txtClave2.Text = Clave1;
            }
            else
            {
                txtClave1.UseSystemPasswordChar = true;
                txtClave1.Text = Clave1;
                txtClave2.UseSystemPasswordChar = true;
                txtClave2.Text = Clave1;
            }
        }
    }
}
