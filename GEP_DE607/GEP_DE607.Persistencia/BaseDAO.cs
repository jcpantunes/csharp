using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using System.Data;
using System.Data.SqlClient;
using GEP_DE607.Dominio;

namespace GEP_DE607.Persistencia
{
    public class BaseDAO
    {
        protected const int INSERT = 1;
        protected const int UPDATE = 2;
        protected const int DELETE = 3;

        // string connectionString = @"Data Source=DESKTOP-H3JA6SK\SQLEXPRESS;Initial Catalog=DBD_GEP;"
        string connectionString = @"Data Source=SERPRO1540297V1\SQLEXPRESS;Initial Catalog=DBD_GEP;"
            + "Integrated Security=True;Min Pool Size=5;Max Pool Size=250;Connect Timeout=20";

        protected string Tabela { get; set; }

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

        protected int retornarSelectValorInt(string query)
        {
            int quantidade = 0;
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    quantidade = reader.GetInt32(0);
                }
            }
            desconectar(conn);
            return quantidade;
        }

        protected decimal retornarSelectValorDecimal(string query)
        {
            decimal quantidade = 0;
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    quantidade = reader.GetDecimal(0);
                    if (quantidade > 0)
                    {
                        quantidade = Decimal.Round(quantidade, 2);
                    }
                    else
                    {
                        quantidade = 0;
                    }
                }
            }
            desconectar(conn);
            return quantidade;
        }

        protected string retornarPesquisaWhere(string key, Dictionary<string, string> parametros)
        {
            string query = "";
            if (key.Equals(ItemTrabalho.CODIGO))
            {
                query += ItemTrabalho.CODIGO + " = " + parametros[key] + " and ";
            }
            else if (key.Equals(ItemTrabalho.TIPO))
            {
                query += ItemTrabalho.TIPO + " like '%" + parametros[key] + "%' and ";
            }
            else if (key.Equals(ItemTrabalho.ID))
            {
                query += ItemTrabalho.ID + " = '" + parametros[key] + "' and ";
            }
            else if (key.Equals(ItemTrabalho.TITULO))
            {
                query += ItemTrabalho.TITULO + " like '%" + parametros[key] + "%' and ";
            }
            else if (key.Equals(ItemTrabalho.RESPONSAVEL))
            {
                query += ItemTrabalho.RESPONSAVEL + " = " + parametros[key] + " and ";
            }
            else if (key.Equals(ItemTrabalho.STATUS))
            {
                query += ItemTrabalho.STATUS + " = '" + parametros[key] + "' and ";
            }
            else if (key.Equals(ItemTrabalho.PLANEJADO_PARA))
            {
                query += ItemTrabalho.PLANEJADO_PARA + " = '" + parametros[key] + "' and ";
            }
            else if (key.Equals(ItemTrabalho.PAI))
            {
                query += ItemTrabalho.PAI + " = '" + parametros[key] + "' and ";
            }
            else if (key.Equals(ItemTrabalho.DTMODIFICACAO))
            {
                query += ItemTrabalho.DTMODIFICACAO + " = '" + Convert.ToDateTime(parametros[key]) + "' and ";
            }
            else if (key.Equals(ItemTrabalho.PROJETO))
            {
                query += ItemTrabalho.PROJETO + " = " + parametros[key] + " and ";
            }
            return query;
        }

        public List<int> recuperarListaID()
        {
            List<int> listaID = new List<int>();
            string query = "SELECT DISTINCT id FROM " + Tabela;
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    listaID.Add(reader.GetInt32(0));
                }
            }
            desconectar(conn);
            return listaID;
        }


    }
}
