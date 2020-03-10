using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
    public partial class Grifos : Form
    {
        //Se crea el marcador y capa de marcador para el mapa
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;

        //Longitud y Latitus Inicial par el mapa
        double LatIncial = -34.7088493761234;
        double LngInicial = -71.0404300689697;

        public void CargarTabla()
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Grifos;");
        }

        public void CargarMapaLimpio()
        {
            //Se establece las propiedades del Mapa
            //El moton para mover el mapa
            gMapControl1.DragButton = MouseButtons.Left;
            //que se pueda mover el mapa
            gMapControl1.CanDragMap = true;
            //Se establece como proveedor de mapa GoogleMaps
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            //Las cordenadas de la posicion Inical
            gMapControl1.Position = new PointLatLng(LatIncial, LngInicial);
            //El Zoom, Minimo, el Maximo, y el Zoom Actual
            gMapControl1.MinZoom = 4;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 10;
            //que NO pueda realizar Scrooll con el maus sobre el mapa
            gMapControl1.AutoScroll = false;

            //Se configura la capa del marcador y el marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatIncial, LngInicial), GMarkerGoogleType.green);
            //Se agrega el marcador al Mapa
            markerOverlay.Markers.Add(marker);

            //se agrega el mapa y el amrcador al map control
            gMapControl1.Overlays.Add(markerOverlay);
        }

        public Grifos()
        {
            InitializeComponent();
            CargarMapaLimpio();
            CargarTabla();
            cmbBuscarEn.Text = "NumeroGrifo";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //se obtienen los datos de la longitud y latitud del mapa haciendo doble click sobre alguna ubicacion
            double latitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double longitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            //Se agregan las coordenadas Unidas para la base de Datos
            txtCordenadasUbicacion.Text = latitud + "/" + longitud;

            //se crea al marcador para la nueva posicion seleccionada
            marker.Position = new PointLatLng(latitud, longitud);
            //se agrega el mensaje al marcador (tooltip)
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", latitud, longitud);

            //Se agrega el mapa y el marcador al map control
            gMapControl1.Overlays.Add(markerOverlay);

        }

        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            // Se cambia la cantidad de zoom en el mapa con el trackBar
            gMapControl1.Zoom = trackZoom.Value;
        }

        private void btnSatelite_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }

        private void btnPlano_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se revisa si el index de el DataGridView empieza en 0, para evitar que los datos se extraigan mal
            if (e.RowIndex >= 0)
            {
                
                //Se extraen los datos de el DataGridView
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];
                txtID.Text = Convert.ToString(fila.Cells["ID"].Value);
                txtNumero.Text = Convert.ToString(fila.Cells["NumeroGrifo"].Value);
                txtDireccionGrifo.Text = Convert.ToString(fila.Cells["DireccionGrifo"].Value);
                txtEstado.Text = Convert.ToString(fila.Cells["Estado"].Value);
                txtCordenadasUbicacion.Text = Convert.ToString(fila.Cells["CoordenadasUbicacion"].Value);
                btnEliminar.Enabled = true;
                
                if (Convert.ToString(fila.Cells["CoordenadasUbicacion"]) != "")
                {

                    //Se separa la longitud de la latidud
                    String CoordendaSerparar = txtCordenadasUbicacion.Text;
                    //Se gaurdan las separacioned entro de un arreglo
                    String[] Separador = CoordendaSerparar.Split('/');

                    //Se extraen desde el arreglo
                    String Latidud = Separador[0];
                    String Longitud = Separador[1];

                    //Se posiciona la Ubicacion de la tienda
                    marker.Position = new PointLatLng(Convert.ToDouble(Latidud), Convert.ToDouble(Longitud));
                    marker.ToolTipText = string.Format("Grifo Numero: " + txtNumero.Text + " Latitud: {0} \n Longitud: {1}", Convert.ToDouble(Latidud), Convert.ToDouble(Longitud));
                    gMapControl1.Position = new PointLatLng(Convert.ToDouble(Latidud), Convert.ToDouble(Longitud));
                    gMapControl1.Zoom = 17;
                    trackZoom.Value = 17;
                }
            }
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

        private void Grifos_Load(object sender, EventArgs e)
        {

        }

        public void LimpiarCampos()
        {
            CargarTabla();
            txtID.Text = "¿?";
            txtNumero.Text = "";
            txtDireccionGrifo.Text = "";
            txtEstado.Text = "";
            txtEstado.Text = "";
            txtCordenadasUbicacion.Text = "";
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnEliminar.Enabled = false;
            txtBuscarEn.Text = "";
        }

        private void txtBuscarEn_KeyUp(object sender, KeyEventArgs e)
        {
            ControlSQLite cargarTabla = new ControlSQLite();
            dataGridView1.DataSource = cargarTabla.CargarTabla("SELECT * From Grifos Where " + cmbBuscarEn.Text + " like '%" + txtBuscarEn.Text + "%';");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "¿?")
            {
                if (txtNumero.Text != "" && txtDireccionGrifo.Text != "" && txtEstado.Text != "")
                {
                    ControlSQLite guardarClaveRadial = new ControlSQLite();
                    guardarClaveRadial.EjecutarConsulta("INSERT INTO main.Grifos(NumeroGrifo,DireccionGrifo,Estado,CoordenadasUbicacion)VALUES ('" + txtNumero.Text + "','" + txtDireccionGrifo.Text + "', '" + txtEstado.Text + "', '" + txtCordenadasUbicacion.Text + "');");

                    MessageBox.Show("Se guardo Correctamente el Grifo Numero: " + txtNumero.Text + "", "Guardado Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar los Datos Obligarorios para registrar un grifo, Numero de Grifo, Direccion del Grifo y el Estado del Grifo", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (txtNumero.Text != "" && txtDireccionGrifo.Text != "" && txtEstado.Text != "")
                {
                    ControlSQLite modificarClaveRadial = new ControlSQLite();
                    modificarClaveRadial.EjecutarConsulta("UPDATE main.Grifos SET 'NumeroGrifo'='" + txtNumero.Text + "','DireccionGrifo'='" + txtDireccionGrifo.Text + "','Estado'='" + txtEstado.Text + "','CoordenadasUbicacion'='" + txtCordenadasUbicacion.Text + "' Where ID= '" + txtID.Text + "' ");

                    MessageBox.Show("Se Modifico Correctamente el Grifo Numero: " + txtNumero.Text + "", "Modificacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar los Datos Obligarorios para modificar un grifo, Numero de Grifo, Direccion del Grifo y el Estado del Grifo", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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
                MessageBox.Show("Debe SELECCIONAR Un Grifo para Eliminarlo de los Registros, para seleccionar debe darle Doble Click", "Grifo NO Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult resultadoMensaje = MessageBox.Show("¿Esta Seguro que Desea Eliminar al Grifo Numero: " + txtNumero.Text + " ?", "Confirmacion Eliminar", MessageBoxButtons.YesNo);

                if (resultadoMensaje == DialogResult.Yes)
                {
                    ControlSQLite eliminarClaveRadial = new ControlSQLite();
                    eliminarClaveRadial.EjecutarConsulta("DELETE FROM main.Grifos WHERE _rowid_ IN ('" + txtID.Text + "');");

                    MessageBox.Show("El Grifo Numero: " + txtNumero.Text + " se Elimino Correctamente", "Eliminado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Muy Bien Falsa Alarma el Grifo Numero: " + txtNumero.Text + " No se ha Eliminado", "Eliminacion Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
