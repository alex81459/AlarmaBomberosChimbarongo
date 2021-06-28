using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    public partial class CoordenadasEmergenciaBuscar : Form
    {
        public CoordenadasEmergenciaBuscar()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            gMapControl1.Dispose();
            this.Close();
        }

        public String CoordenadasEmer { get; set; }

        //Se crea el marcador y capa de marcador para el mapa
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;

        //Longitud y Latitus Inicial par el mapa
        public double LatIncialFinal = -34.7088493761234;
        public double LngInicialFinal = -71.0404300689697;

        public void CargarMapaLimpio()
        {
            String SLatConvertir = txtLatitud.Text;
            String SLngConvertir = txtLonguitud.Text;
            SLatConvertir = SLatConvertir.Replace(".", ",");

            LatIncialFinal = Convert.ToDouble(SLatConvertir);
            LngInicialFinal = Convert.ToDouble(SLngConvertir);

            //Se establece las propiedades del Mapa
            //El moton para mover el mapa
            gMapControl1.DragButton = MouseButtons.Left;
            //que se pueda mover el mapa
            gMapControl1.CanDragMap = true;
            //Se establece como proveedor de mapa GoogleMaps
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            //Las cordenadas de la posicion Inical
            gMapControl1.Position = new PointLatLng(LatIncialFinal, LngInicialFinal);
            //El Zoom, Minimo, el Maximo, y el Zoom Actual
            gMapControl1.MinZoom = 4;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 10;
            //que NO pueda realizar Scrooll con el maus sobre el mapa
            gMapControl1.AutoScroll = false;

            //Se configura la capa del marcador y el marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatIncialFinal, LngInicialFinal), GMarkerGoogleType.green);
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

        private void CoordenadasEmergenciaBuscar_Load(object sender, EventArgs e)
        {
            CargarMapaLimpio();
            trackZoom.Value = 12;
        }
    }
}
