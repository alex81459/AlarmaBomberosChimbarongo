using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class ClaveMaestra : Form
    {
        public ClaveMaestra()
        {
            InitializeComponent();
        }

        public String TextoASha256(String Texto)
        {
            //Para transformar el String de la clave en String SHA256
            StringBuilder HASHClave = new StringBuilder();
            using (SHA256 HASH = SHA256Managed.Create())
            {
                Encoding codificacion = Encoding.UTF8;
                Byte[] result = HASH.ComputeHash(codificacion.GetBytes(Texto));
                foreach (Byte bits in result)
                    HASHClave.Append(bits.ToString("x2"));
            }
            return HASHClave.ToString();
        }

        String Valorclavemaestra = Properties.Settings.Default.ClaveMaestra;
       

        private void btnContinuar_Click(object sender, EventArgs e)
        {

            String ClaveIngresadaHASH = TextoASha256(txtClave.Text);

            if (Valorclavemaestra == ClaveIngresadaHASH)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("La Clave Ingresada no es correcta", "Error Clave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
