using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlarmaBomberosChimbarongo
{
    class ControlSQLite
    {
        //SQLiteConnection cadenaConexion = new SQLiteConnection(@"Data Source = c:\AlarmaBomberosChimbarongo.db; Version = 3;");
        SQLiteConnection cadenaConexion = new SQLiteConnection(@"Data Source =.\AlarmaBomberosChimbarongo.db; Version = 3;");

        public void EjecutarConsulta(String SQLConsulta)
        {
            try
            {
                cadenaConexion.Open();

                SQLiteCommand sqlComando = new SQLiteCommand(SQLConsulta, cadenaConexion);

                sqlComando.ExecuteNonQuery();
                cadenaConexion.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Ejecutar el Proceso Solicitado", "Error Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cadenaConexion.Close();
                
            }   
        }


        public DataTable CargarTabla(String ConsultaSQL) {
            try
            {
                cadenaConexion.Open();
                SQLiteDataAdapter db = new SQLiteDataAdapter(ConsultaSQL, cadenaConexion);
                DataSet ds = new DataSet();
                ds.Reset();
                DataTable dt = new DataTable();
                db.Fill(ds);
                dt = ds.Tables[0];
                
                return dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Intentar Extraer los Datos y Cargarlos","Error Extracion",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                cadenaConexion.Close();
            }
        }


        


    }
}
