using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class Ajustes : Form
    {
        public Ajustes()
        {
            InitializeComponent();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRealizarCopiaBD_Click(object sender, EventArgs e)
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
                catch (Exception)
                {
                    MessageBox.Show("Ha ocurrido un error al intentar realizar la Copia de Seguridad","Erro Copia Seguridad",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestaurarBD_Click(object sender, EventArgs e)
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
                    catch (Exception)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar restaurar la copia de seguridad");
                    }
                }
                else
                {
                    MessageBox.Show("Muy bien falsa alarma, los datos actuales se mantendran y no seran restaurados de una copia anterior", "Restauracion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
