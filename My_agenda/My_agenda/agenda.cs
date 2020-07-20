using System;
using System.Data;
using System.Data.SQLite;


namespace My_agenda
{
    class agenda
    {
        private SQLiteConnection cn = null;
        private SQLiteCommand cmd = null;
        private SQLiteDataReader reader = null;
        private DataTable table = null;

        public bool insertar(string nombre, string telefono)
        {
            string query = $"INSERT INTO directorio(nombre,telefono)VALUES('{nombre} ','  {telefono}')";
            return ExecuteNonQuery(query);
        }

        public bool eliminar(int id)
        {
            string query = $"DELETE FROM directorio WHERE id = '{id}'";
            return ExecuteNonQuery(query);
        }

        public bool actualizar(int id, string nombre, string telefono)
        {
            string query = $"UPDATE directorio SET nombre = '{nombre}',telefono ='{telefono}' WHERE id='{id}'";
            return ExecuteNonQuery(query);
        }
        private bool ExecuteNonQuery(string query)
        {
            try
            {
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                int rest = cmd.ExecuteNonQuery();
                if (rest == 1)
                {
                    return true;
                }
            }
            catch (Exception a)
            {
                System.Windows.Forms.MessageBox.Show(a.Message + "" + a.StackTrace);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return false;
        }
        public DataTable consultar()
        {
            string query = $"SELECT * FROM directorio";
            return ExecuteReader(query);


        }

        public DataTable filtrar(string filtro)
        {
            string query = $"SELECT * FROM directorio WHERE nombre LIKE '%{filtro}%' OR telefono LIKE '%{filtro}%'";
            return ExecuteReader(query);
        }
        private DataTable ExecuteReader(string query)
        {
            try
            {
                nombresColumnas();
                cn = conexion.conectar();
                cn.Open();
                cmd = new SQLiteCommand(query, cn);
                reader = cmd.ExecuteReader();

                int z = 0;
                while (reader.Read())
                {
                    z++;
                    object[] fila = new object[] { reader["id"], z, reader["nombre"], reader["telefono"] };
                    table.Rows.Add(fila);
                }
                reader.Close();
                return table;
            }
            catch (Exception m)
            {
                System.Windows.Forms.MessageBox.Show(m.Message + " " + m.StackTrace);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            return table;
        }
        private void nombresColumnas()
        {
                table = new DataTable();
                table.Columns.Add("Id");
                table.Columns.Add("Num.");
                table.Columns.Add("Nombre");
                table.Columns.Add("Telefono");    
        }
    }
}    
