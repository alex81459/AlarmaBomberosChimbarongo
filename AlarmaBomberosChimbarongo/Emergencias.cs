using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace AlarmaBomberosChimbarongo
{
    public partial class Emergencias : Form
    {
        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Emergencias;");
        }

        public Emergencias()
        {
            InitializeComponent();
            CargarTabla();
            cmbBuscarEn.Text = "ID";
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtClaves.Text = Convert.ToString(fila.Cells["Claves"].Value);
                txtCompañias.Text = Convert.ToString(fila.Cells["Compañias"].Value); ;
                txtFecha.Text = Convert.ToString(fila.Cells["Fecha"].Value); ;
                txtSituacion.Text = Convert.ToString(fila.Cells["Situacion"].Value); ;
                txtOficialAcargo.Text = Convert.ToString(fila.Cells["OficialAcargo"].Value);
                txtLugar.Text = Convert.ToString(fila.Cells["Lugar"].Value);
                txtDescripcion.Text = Convert.ToString(fila.Cells["Descripcion"].Value); ;
                txtCoordenadas.Text = Convert.ToString(fila.Cells["Coordenadas"].Value); ;
            }
        }

        private void txtBuscarEn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 164) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 165) || (e.KeyChar == Convert.ToChar(Keys.Back)) || (e.KeyChar == 32) || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar == 44) || (e.KeyChar == 45) || (e.KeyChar == 43) || (e.KeyChar == 58))
            {
                //Si se ingresa letras mayuculas, Ñ, minusculas, ñ, borrar, espacio, Numeros, - (Guion), + y :, se mantienen en el textbox
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
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Emergencias Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';");
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtClaves.Text = "";
            txtCompañias.Text = "";
            txtFecha.Text = "";
            txtSituacion.Text = "";
            txtOficialAcargo.Text = "";
            txtLugar.Text = "";
            txtDescripcion.Text = "";
            txtCoordenadas.Text = "";
            txtBuscarEn.Text = "";
            CargarTabla();
            txtID.Text = "¿?";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "¿?")
            {
                /*
                Document doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream("EmergenciaBomberosID"+txtID.Text+".pdf", FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph();
                titulo.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
                titulo.Add("Emergencia Bomberos ID: "+ txtID.Text+ " Fecha: "+ txtFecha.Text+ "");
                doc.Add(titulo);

                doc.Add(new Paragraph("Claves: "+ txtClaves.Text+ ""));
                doc.Add(new Paragraph("Compañias: "+ txtCompañias.Text+ ""));
                doc.Add(new Paragraph("Situacion: "+ txtSituacion.Text+ ""));
                doc.Add(new Paragraph("txtOficialAcargo: " + txtOficialAcargo.Text + ""));
                doc.Add(new Paragraph("Situacion: " + txtSituacion.Text + ""));
                doc.Add(new Paragraph("Descripcion: " + txtDescripcion.Text + ""));
                doc.Add(new Paragraph("Coordenadas: " + txtCoordenadas.Text + ""));
                doc.Close();
                */

                SaveFileDialog DialogoGuardar = new SaveFileDialog();
                DialogoGuardar.FileName = "EmergenciaBomberosChimbarongoID" + txtID.Text + ".pdf";

                if (DialogoGuardar.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Document doc = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"" + DialogoGuardar.FileName + "", FileMode.Create));

                        doc.AddTitle("Emergencia Bomberos ID:" + txtID.Text);
                        doc.AddCreator("Emergencia Bomberos ©Alex Salinas Ponce");

                        doc.Open();

                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        doc.Add(new Paragraph("ID "+ txtID.Text +"  Registro de Emergencia Bomberos Chimbarongo    Fecha Emergencia " + txtFecha.Text));
                        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Application.StartupPath + @"\bomberoschimbarongo.png");
                        imagen.BorderWidth = 0;
                        imagen.Alignment = Element.ALIGN_CENTER;
                        float percentage = 0.0f;
                        percentage = 150 / imagen.Width;
                        imagen.ScalePercent(percentage * 90);
                        doc.Add(imagen);
                        doc.Add(new Paragraph("-------Claves-------"));
                        doc.Add(new Paragraph(txtClaves.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Compañias-------"));
                        doc.Add(new Paragraph(txtCompañias.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Situacion-------"));
                        doc.Add(new Paragraph(" " + txtSituacion.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Oficial a Cargo-------"));
                        doc.Add(new Paragraph(" " + txtOficialAcargo.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Lugar-------"));
                        doc.Add(new Paragraph(" " + txtLugar.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Descripcion-------"));
                        doc.Add(new Paragraph(" " + txtDescripcion.Text));
                        doc.Add(new Paragraph(" "));
                        doc.Add(new Paragraph("-------Coordenadas-------"));

                        if (txtCoordenadas.Text == "")
                        {
                            doc.Add(new Paragraph(" No Especificadas"));
                        }
                        else
                        {
                            doc.Add(new Paragraph(" " + txtCoordenadas.Text));
                        }

                        doc.Close();
                        writer.Close();

                        MessageBox.Show("Se realizo correctamente la Generacion del Documento", "Documento Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = @""+ DialogoGuardar.FileName+ "";
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
                MessageBox.Show("Debe Seleccionar una emergencia para Imprimir","No hay Datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
    }
}
