namespace AlarmaBomberosChimbarongo
{
    partial class CoordenadasEmergencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoordenadasEmergencia));
            this.Barra = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.btnSatelite = new System.Windows.Forms.Button();
            this.btnPlano = new System.Windows.Forms.Button();
            this.trackZoom = new System.Windows.Forms.TrackBar();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCoordenadas = new System.Windows.Forms.Label();
            this.Barra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Barra
            // 
            this.Barra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Barra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Barra.Controls.Add(this.pictureBox2);
            this.Barra.Controls.Add(this.pictureBox12);
            this.Barra.Controls.Add(this.label7);
            this.Barra.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Barra.Dock = System.Windows.Forms.DockStyle.Top;
            this.Barra.Location = new System.Drawing.Point(0, 0);
            this.Barra.Name = "Barra";
            this.Barra.Size = new System.Drawing.Size(953, 22);
            this.Barra.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.Font = new System.Drawing.Font("Orbitron", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(349, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Coordendas Emergencia";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::AlarmaBomberosChimbarongo.Properties.Resources.icons8_cerrar_ventana_48;
            this.pictureBox2.Location = new System.Drawing.Point(923, -4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox12
            // 
            this.pictureBox12.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox12.Image = global::AlarmaBomberosChimbarongo.Properties.Resources.alert_alarm_22185;
            this.pictureBox12.Location = new System.Drawing.Point(1, -1);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(21, 21);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 25;
            this.pictureBox12.TabStop = false;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(11, 77);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = false;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(929, 450);
            this.gMapControl1.TabIndex = 86;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseDoubleClick);
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::AlarmaBomberosChimbarongo.Properties.Resources.mapa;
            this.pictureBox9.Location = new System.Drawing.Point(13, 1);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(46, 46);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 87;
            this.pictureBox9.TabStop = false;
            // 
            // btnSatelite
            // 
            this.btnSatelite.ForeColor = System.Drawing.Color.Black;
            this.btnSatelite.Location = new System.Drawing.Point(557, 51);
            this.btnSatelite.Name = "btnSatelite";
            this.btnSatelite.Size = new System.Drawing.Size(70, 20);
            this.btnSatelite.TabIndex = 88;
            this.btnSatelite.Text = "Satelite";
            this.btnSatelite.UseVisualStyleBackColor = true;
            this.btnSatelite.Click += new System.EventHandler(this.btnSatelite_Click);
            // 
            // btnPlano
            // 
            this.btnPlano.ForeColor = System.Drawing.Color.Black;
            this.btnPlano.Location = new System.Drawing.Point(483, 51);
            this.btnPlano.Name = "btnPlano";
            this.btnPlano.Size = new System.Drawing.Size(70, 20);
            this.btnPlano.TabIndex = 89;
            this.btnPlano.Text = "Plano";
            this.btnPlano.UseVisualStyleBackColor = true;
            this.btnPlano.Click += new System.EventHandler(this.btnPlano_Click);
            // 
            // trackZoom
            // 
            this.trackZoom.LargeChange = 2;
            this.trackZoom.Location = new System.Drawing.Point(724, 29);
            this.trackZoom.Maximum = 20;
            this.trackZoom.Minimum = 4;
            this.trackZoom.Name = "trackZoom";
            this.trackZoom.Size = new System.Drawing.Size(216, 45);
            this.trackZoom.TabIndex = 90;
            this.trackZoom.Value = 10;
            this.trackZoom.ValueChanged += new System.EventHandler(this.trackZoom_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Orbitron", 9.75F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(678, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 16);
            this.label17.TabIndex = 91;
            this.label17.Text = "Zoom";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Orbitron", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(65, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(208, 16);
            this.label11.TabIndex = 92;
            this.label11.Text = "Ubicacion de la Emergencia";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(65, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(235, 12);
            this.label14.TabIndex = 93;
            this.label14.Text = "*Para Marcar una Ubicación en el Mapa Haga Doble Clic";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::AlarmaBomberosChimbarongo.Properties.Resources.Satellite_icon_icons_com_74901;
            this.pictureBox10.Location = new System.Drawing.Point(574, 11);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(40, 40);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 94;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::AlarmaBomberosChimbarongo.Properties.Resources.mapa2;
            this.pictureBox11.Location = new System.Drawing.Point(499, 11);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(40, 40);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 95;
            this.pictureBox11.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtCoordenadas);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox11);
            this.panel1.Controls.Add(this.pictureBox10);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.trackZoom);
            this.panel1.Controls.Add(this.btnPlano);
            this.panel1.Controls.Add(this.btnSatelite);
            this.panel1.Controls.Add(this.pictureBox9);
            this.panel1.Controls.Add(this.gMapControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 540);
            this.panel1.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Orbitron", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 96;
            this.label1.Text = "Coodendas:";
            // 
            // txtCoordenadas
            // 
            this.txtCoordenadas.AutoSize = true;
            this.txtCoordenadas.Font = new System.Drawing.Font("Orbitron", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoordenadas.Location = new System.Drawing.Point(104, 53);
            this.txtCoordenadas.Name = "txtCoordenadas";
            this.txtCoordenadas.Size = new System.Drawing.Size(26, 16);
            this.txtCoordenadas.TabIndex = 97;
            this.txtCoordenadas.Text = "¿?";
            // 
            // CoordenadasEmergencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(953, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Barra);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CoordenadasEmergencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coordenadas Emergencia";
            this.Barra.ResumeLayout(false);
            this.Barra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Barra;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label label7;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Button btnSatelite;
        private System.Windows.Forms.Button btnPlano;
        private System.Windows.Forms.TrackBar trackZoom;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label txtCoordenadas;
        private System.Windows.Forms.Label label1;
    }
}