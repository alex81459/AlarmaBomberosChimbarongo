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
            catch (Exception ex)
            {
                MessageBox.Show("Error al Ejecutar el Proceso Solicitado ERROR: "+ex.Message, "Error Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
               //cadenaConexion.Dispose();
                cadenaConexion.Close();
            }   
        }

        public Boolean ExistenciaDatoEnTabla(String SQLConsulta)
        {
            Boolean Resultado = false;
            try
            {
                cadenaConexion.Open();

                SQLiteCommand sqlComando = new SQLiteCommand(SQLConsulta, cadenaConexion);
                SQLiteDataReader result = sqlComando.ExecuteReader();

                if (result.HasRows)
                {
                    Resultado = true;
                }
                else
                {
                    Resultado = false;
                }

                result.Close();
                sqlComando.ExecuteNonQuery();
                cadenaConexion.Close();
                return Resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Ejecutar el Proceso Solicitado ERROR: " + ex.Message, "Error Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
               //cadenaConexion.Dispose();
               cadenaConexion.Close();
               return false;
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

                cadenaConexion.Close();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Intentar Extraer los Datos y Cargarlos ERROR: "+ ex.Message,"Error Extracion",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //cadenaConexion.Dispose();
                cadenaConexion.Close();
                return null;
            }
        }


        


    }
}
