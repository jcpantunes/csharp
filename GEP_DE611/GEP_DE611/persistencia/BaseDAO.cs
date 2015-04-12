using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using System.Data;
using System.Data.SqlClient;

namespace GEP_DE611.persistencia
{
    class BaseDAO
    {
        public static int INSERT = 1;

        public static int UPDATE = 2;

        public static int DELETE = 3;

        string connectionString = @"Data Source=JULIO-PC\SQLEXPRESS;Initial Catalog=DBD_GEP;"
        // string connectionString = @"Data Source=SERPRO1540297V1\SQLEXPRESS;Initial Catalog=DBD_GEP;"
            + "Integrated Security=True;Min Pool Size=5;Max Pool Size=250;Connect Timeout=5";

        protected SqlConnection conectar(SqlConnection conn)
        {
            conn = new SqlConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        protected SqlConnection desconectar(SqlConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return conn;
        }

        protected SqlDataReader select(SqlConnection conn, string query)
        {
            SqlDataReader reader = null;
            conn = conectar(conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            return reader;
        }

        protected void save(SqlConnection conn, string query)
        {
            conn = conectar(conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        protected void save(SqlConnection conn, string query, List<SqlParameter> listaParametros)
        {
            conn = conectar(conn);
            SqlCommand cmd = new SqlCommand(query, conn);
            foreach (SqlParameter parametro in listaParametros)
            {
                cmd.Parameters.Add(parametro);
            }
            cmd.ExecuteNonQuery();
        }
    }

}
