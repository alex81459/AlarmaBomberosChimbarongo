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
    public partial class GuiaTelefonica : Form
    {
        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From GuiaTelefonica;");
        }
        public void LimpiarCampos()
        {
            CargarTabla();
            txtID.Text = "¿?";
            txtNumero.Text = "";
            txtCategoria.SelectedIndex = -1;
            txtNombreContacto.Text = "";
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnEliminar.Enabled = false;
            txtBuscarEn.Text = "";
        }


        public GuiaTelefonica()
        {
            InitializeComponent();
            cmbBuscarEn.Text = "Numero";
            CargarTabla();
            txtCategoria.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From GuiaTelefonica Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtNumero.Text = Convert.ToString(fila.Cells["Numero"].Value);
                txtCategoria.Text = Convert.ToString(fila.Cells["Categoria"].Value);
                txtNombreContacto.Text = Convert.ToString(fila.Cells["NombreContacto"].Value);
                btnEliminar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                if (txtNumero.Text != "" && txtCategoria.Text != "" && txtNombreContacto.Text != "")
                {
                    ControlSQLite guardarClaveRadial = new ControlSQLite();
                    guardarClaveRadial.EjecutarConsulta("INSERT INTO main.GuiaTelefonica(Numero, Categoria, NombreContacto)VALUES ('" + txtNumero.Text + "', '" + txtCategoria.Text + "', '" + txtNombreContacto.Text + "');");

                    MessageBox.Show("Se guardo Correctamente el Numero: " + txtNumero.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar los Datos Obligarorios de Numero de Telefono, Categoria y Nombre de Contacto para Guardarlo", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (txtNumero.Text != "" && txtCategoria.Text != "" && txtNombreContacto.Text != "")
                {
                    ClaveMaestra verificarClave = new ClaveMaestra();

                    if (verificarClave.ShowDialog() == DialogResult.OK)
                    {
                        ControlSQLite modificarClaveRadial = new ControlSQLite();
                        modificarClaveRadial.EjecutarConsulta("UPDATE main.GuiaTelefonica SET 'Numero'='" + txtNumero.Text + "','Categoria'='" + txtCategoria.Text + "','NombreContacto'='" + txtNombreContacto.Text + "' Where ID= '" + txtID.Text + "' ");

                        MessageBox.Show("Se Modifico Correctamente el Numero " + txtNumero.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }     
                }
                else
                {
                    MessageBox.Show("Debe Ingresar los Datos Obligarorios de Numero de Telefono, Categoria y Nombre de Contacto para Modificar el Actual", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                    
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                MessageBox.Show("Debe SELECCIONAR Un Numero de Telefono para Eliminarla de los Registros, para seleccionar debe darle Doble Click", "Numero Telefono NO Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Eliminar el Numero de Telefono: " + txtNumero.Text + " ?", "Confirmacion Eliminar", MessageBoxButtons.YesNo);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    ClaveMaestra verificarClave = new ClaveMaestra();

                    if (verificarClave.ShowDialog() == DialogResult.OK)
                    {
                        ControlSQLite eliminarClaveRadial = new ControlSQLite();
                        eliminarClaveRadial.EjecutarConsulta("DELETE FROM main.GuiaTelefonica WHERE _rowid_ IN ('" + txtID.Text + "');");

                        MessageBox.Show("El Numero de Telefono: " + txtNumero.Text + " se Elimino Correctamente", "Eliminado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }    
                }
                else
                {
                    MessageBox.Show("Muy Bien Falsa Alarma el Numero de Telefono: " + txtNumero.Text + " No se ha Eliminado", "Eliminacion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
