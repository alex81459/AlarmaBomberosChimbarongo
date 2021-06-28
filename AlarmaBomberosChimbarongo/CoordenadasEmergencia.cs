using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class CoordenadasEmergencia : Form
    {
        public String CoordenadasEmer { get; set; }

        //Se crea el marcador y capa de marcador para el mapa
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;

        //Longitud y Latitus Inicial par el mapa
        public double LatIncial = -34.7088493761234;
        public double LngInicial = -71.0404300689697;

        public CoordenadasEmergencia()
        {
            InitializeComponent();
            CargarMapaLimpio();
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

        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            // Se cambia la cantidad de zoom en el mapa con el trackBar
            gMapControl1.Zoom = trackZoom.Value;
        }

        private void btnPlano_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void btnSatelite_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //se obtienen los datos de la longitud y latitud del mapa haciendo doble click sobre alguna ubicacion
            double latitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double longitud = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            //Se agregan las coordenadas Unidas para la base de Datos
            txtCoordenadas.Text = latitud + "/" + longitud;

            //se crea al marcador para la nueva posicion seleccionada
            marker.Position = new PointLatLng(latitud, longitud);
            //se agrega el mensaje al marcador (tooltip)
            marker.ToolTipText = string.Format("Ubicacion Emergencia: \n Latitud:{0} \n Longitud:{1}", latitud, longitud);

            //Se agrega el mapa y el marcador al map control
            gMapControl1.Overlays.Add(markerOverlay);

            CoordenadasEmer = txtCoordenadas.Text;
            this.DialogResult = DialogResult.OK;
            gMapControl1.Dispose();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            gMapControl1.Dispose();
            this.Close();
        }
    }
}
