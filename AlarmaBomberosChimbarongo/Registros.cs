using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Registros : Form
    {
        public Registros()
        {
            InitializeComponent();
            CargarTabla();
            cmbBuscarEn.Text = "ID";
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            txtCantidadRegistro.Text = cargarTabla.CargarTabla("SELECT Count(ID) From Registros;").Rows[0][0].ToString();
            txtPaginasDisponibles.Text = (Convert.ToInt32(txtCantidadRegistro.Text) / 50).ToString();
            nudPaginaActual.Maximum = Convert.ToDecimal(txtPaginasDisponibles.Text);
            nudPaginaActualBuscar.Refresh();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Registros ORDER by ID DESC Limit 50;");
        }

        public void LimpiarCampos()
        {
            CargarTabla();
            txtIDRegistro.Text = "";
            txtLugar.Text = "";
            txtUsuarioRUt.Text = "";
            txtAccion.Text = "";
            txtFecha.Text = "";
            txtDescripcion.Text = "";
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtIDRegistro.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtLugar.Text = Convert.ToString(fila.Cells["Lugar"].Value);
                txtUsuarioRUt.Text = Convert.ToString(fila.Cells["UsuarioRUT"].Value);
                txtAccion.Text = Convert.ToString(fila.Cells["Accion"].Value);
                txtFecha.Text = Convert.ToString(fila.Cells["Fecha"].Value);
                txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value);
            }
        }

        private void txtBuscarEn_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void btnCambiarPagina_Click(object sender, EventArgs e)
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Registros ORDER by ID DESC Limit " + Convert.ToInt32((nudPaginaActual.Value * 50)) + ",50;");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            HacerInvisiblesyLimpiarCampos();
        }

        private void HacerInvisiblesyLimpiarCampos()
        {
            txtBuscarEn.Visible = false;
            nudPaginaActualBuscar.Visible = false;
            txtPaginasDisponiblesBusqueda.Visible = false;
            txtBuscarEn.Text = "";
            nudPaginaActualBuscar.Value = 0;
            txtPaginasDisponiblesBusqueda.Text = "?????????";
            txtPaginasDisponiblesBusqueda.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtBuscarEn.Visible = true;
            lnlParametrosABuscar.Visible = true;
            lblPaginaActualBusqueda.Visible = false;
            lblPaginasDisponibles.Visible = false;
            lblRegistrosEncontrados.Visible = false;
            txtRegistrosEncontradosSuperior.Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEn.Text == "")
            {
                MessageBox.Show("Debe ingresar algun dato a buscar en el campo 'Parametros a Buscar'", "Faltan Datos para la Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ControlSQLite BuscarRegistros = new ControlSQLite();
                String CantidadRegistrosDetectados = (BuscarRegistros.CargarTabla("SELECT COUNT(ID) From Registros Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';").Rows[0][0].ToString());
                txtRegistrosEncontradosSuperior.Text = CantidadRegistrosDetectados;
                txtPaginasDisponiblesBusqueda.Text = (Convert.ToInt32(CantidadRegistrosDetectados) / 50).ToString();
                nudPaginaActualBuscar.Maximum = (Convert.ToInt32(CantidadRegistrosDetectados) / 50);
                ActivarControlpaginas();
                dataGridView1.DataSource = BuscarRegistros.CargarTabla("SELECT * From Registros Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%' ORDER by ID DESC Limit 50 OFFSET " + Convert.ToUInt32(nudPaginaActualBuscar.Value * 50).ToString() + ";");
                lblRegistrosEncontrados.Visible = true;
                txtRegistrosEncontradosSuperior.Visible = true;
            }
        }

        private void ActivarControlpaginas()
        {
            nudPaginaActualBuscar.Visible = true;
            lblBuscarPor.Visible = true;
            txtPaginasDisponiblesBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
            lblPaginaActualBusqueda.Visible = true;
            lblPaginasDisponibles.Visible = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtIDRegistro.Text != "")
            {
                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.FileName = "EmergenciaBomberosChimbarongoID" + txtIDRegistro.Text + ".pdf";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Document doc = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"" + DialogoGuardar.FileName + "", FileMode.Create));

                        doc.AddTitle("Registro Bomberos ID:" + txtIDRegistro.Text);
                        doc.AddCreator("Registro Bomberos ©Alex Salinas Ponce");

                        doc.Open();

                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        doc.Add(new Paragraph("ID " + txtIDRegistro.Text + "  Registro Bomberos Chimbarongo  Fecha Registro " + txtFecha.Text));
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\bomberoschimbarongo.png");
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_CENTER;
                        float percentage = 0.0f;
                        percentage = 150 / imagen.Width;
                        imagen.ScalePercent(percentage * 90);
                        doc.Add(imagen);
                        doc.Add(new Paragraph("-------Lugar-------"));
                        doc.Add(new Paragraph(txtLugar.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------RUT Usuario-------"));
                        doc.Add(new Paragraph(txtUsuarioRUt.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Accion-------"));
                        doc.Add(new Paragraph(txtAccion.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Fecha-------"));
                        doc.Add(new Paragraph(txtFecha.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Descripcion-------"));
                        doc.Add(new Paragraph(txtDescripcion.Text));
                        doc.Add(new Paragraph(" "));

                        doc.Close();
                        writer.Close();

                        MessageBox.Show("Se realizo correctamente la Generacion del Documento", "Documento Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = @"" + DialogoGuardar.FileName + "";
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar abrir el documento Generado ERROR: " + ex.Message, "Error Abrir Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar realizar la generacion del Documento ERROR: " + ex.Message, "Error Creacion Docuemento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar un registro para Imprimir", "No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
