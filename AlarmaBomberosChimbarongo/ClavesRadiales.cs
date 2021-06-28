using System;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class ClavesRadiales : Form
    {
        public ClavesRadiales()
        {
            InitializeComponent();
            cmbBuscarEn.Text = "ClaveRadial";
            CargarTabla();
            txtCategoria.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From ClavesRadiales ORDER by ID DESC;");
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ClaveMaestra verificarClave = new ClaveMaestra();

            if (txtID.Text == "¿?")
            {
                MessageBox.Show("Debe SELECCIONAR Una Clave Radial para Eliminarla de los Registros, para seleccionar debe darle Doble Click", "Clave Radial NO Seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                    DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Eliminar la Clave Radial " + txtClaveRadial.Text + " ?", "Confirmacion Eliminar", MessageBoxButtons.YesNo);

                    if (resultadoMensaje == DialogResult.Yes)
                    {
                    if (verificarClave.ShowDialog() == DialogResult.OK)
                    {
                        ControlSQLite eliminarClaveRadial = new ControlSQLite();
                        eliminarClaveRadial.EjecutarConsulta("DELETE FROM main.ClavesRadiales WHERE _rowid_ IN ('" + txtID.Text + "');");

                        MessageBox.Show("La Clave Radial " + txtClaveRadial.Text + " se Elimino Correctamente", "Eliminado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            ControlSQLite guardarRegistro = new ControlSQLite();
                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Eliminar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Elimino una Clave Radial con los siguientes datos, Clave: " + txtClaveRadial.Text + " Categoria: " + txtCategoria.Text + " Descripcion: " + txtDescripcion.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Claves Radiales');");
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
                        MessageBox.Show("Muy Bien Falsa Alarma la Clave Radial " + txtClaveRadial.Text + " No se ha Eliminado", "Eliminacion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                if (txtClaveRadial.Text != "" && txtCategoria.Text != "" && txtDescripcion.Text != "")
                {
                    ControlSQLite ExistenciaDatos = new ControlSQLite();
                    Boolean DatoExiste = ExistenciaDatos.ExistenciaDatoEnTabla("select * from ClavesRadiales where ClaveRadial = '"+ txtClaveRadial.Text+ "'");

                    if (DatoExiste)
                    {
                        MessageBox.Show("La Clave Radial "+ txtClaveRadial.Text+ " Ya se encuentra Registrada en el Sistema","Registro ya Existe",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        ControlSQLite guardarClaveRadial = new ControlSQLite();
                        guardarClaveRadial.EjecutarConsulta("INSERT INTO main.ClavesRadiales(ClaveRadial, Categoria, Descripcion)VALUES ('" + txtClaveRadial.Text + "', '" + txtCategoria.Text + "', '" + txtDescripcion.Text + "');");

                        MessageBox.Show("Se guardo Correctamente la Clave Radial " + txtClaveRadial.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            ControlSQLite guardarRegistro = new ControlSQLite();
                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Guardar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Registro una nueva Clave Radial con los siguientes datos, Clave: "+txtClaveRadial.Text+" Categoria: "+ txtCategoria.Text+ " Descripcion: "+ txtDescripcion.Text+ "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Claves Radiales');");
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
                    MessageBox.Show("Debe Ingresar los Datos Obligarorios de Clave Radial, Categoria y Descripcion","Datos Faltantes",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }  
            }
            else
            {
                ClaveMaestra verificarClave = new ClaveMaestra();

                if (verificarClave.ShowDialog() == DialogResult.OK)
                {
                    if (txtClaveRadial.Text != "" && txtCategoria.Text != "" && txtDescripcion.Text != "")
                    {
                        ControlSQLite modificarClaveRadial = new ControlSQLite();
                        modificarClaveRadial.EjecutarConsulta("UPDATE main.ClavesRadiales SET 'ClaveRadial'='" + txtClaveRadial.Text + "','Categoria'='" + txtCategoria.Text + "','Descripcion'='" + txtDescripcion.Text + "' Where ID= '" + txtID.Text + "' ");

                        MessageBox.Show("Se Modifico Correctamente la Clave Radial " + txtClaveRadial.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);  

                        try
                        {
                            ControlSQLite guardarRegistro = new ControlSQLite();
                            guardarRegistro.EjecutarConsulta("INSERT INTO main.Registros(UsuarioRUT,Accion,Descripcion,Fecha,Lugar) VALUES ('" + FuncionesAplicacion.RutLogin + "','Modificar','El Usuario con RUT: " + FuncionesAplicacion.RutLogin + " Modifico una Clave Radial con los siguientes datos, Clave: " + txtClaveRadial.Text + " Categoria: " + txtCategoria.Text + " Descripcion: " + txtDescripcion.Text + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToString("hh:mm:ss") + "','Claves Radiales');");
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("NO Debe debe dejar ninguno de los campos vacios para modificar la Clave Radial", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                } 
            }
        }

        public void LimpiarCampos(){
            CargarTabla();
            txtID.Text = "¿?";
            txtClaveRadial.Text = "";
            txtCategoria.SelectedIndex = -1;
            txtDescripcion.Text = "";
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnEliminar.Enabled = false;
            txtBuscarEn.Text = "";
       }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44) || (e.KeyChar == 45))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y - (Guion) , se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtClaveRadial.Text = Convert.ToString(fila.Cells["ClaveRadial"].Value);
                txtCategoria.Text = Convert.ToString(fila.Cells["Categoria"].Value);
                txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value);
                btnEliminar.Enabled = true;
            }      
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros y , se mantienen en el textbox
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtClaveRadial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 45) || (e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                //Si se numeros o - (guion) se mantienen en el textbox
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
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From ClavesRadiales Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER by ID DESC;");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
