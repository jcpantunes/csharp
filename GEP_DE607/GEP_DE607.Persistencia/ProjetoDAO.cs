using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using System.Data.SqlClient;

namespace GEP_DE607.Persistencia
{
    class ProjetoDAO : BaseDAO
    {
        private string TABELA = "Projeto";

        public ProjetoDAO()
        {
        }

        public List<Projeto> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public List<Projeto> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Projeto.CODIGO))
                    {
                        query += Projeto.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Projeto.NOME))
                    {
                        query += Projeto.NOME + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Projeto.ID))
                    {
                        query += Projeto.ID + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Projeto.DTINICIO))
                    {
                        query += Projeto.DTINICIO + " >= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Projeto.DTFINAL))
                    {
                        query += Projeto.DTFINAL + " <= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Projeto> executarSelect(string query)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Projeto p = new Projeto(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5),
                        reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetInt32(10), reader.GetDecimal(11),
                        reader.GetDecimal(12), reader.GetDecimal(13), reader.GetDateTime(14), reader.GetDateTime(15), reader.GetDateTime(16));
                    lista.Add(p);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Projeto> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA + " (nome, id, dtInicio, dtFinal) "
                + "values (@nome, @id, @dtInicio, @dtFinal)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Projeto> lista)
        {
            string queryUpdate = "UPDATE " + TABELA + " SET "
                + " nome = @nome, "
                + " id = @id, "
                + " dtInicio = @dtInicio, "
                + " dtFinal = @dtFinal "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Projeto> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Projeto> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Projeto f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Projeto p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", p.Codigo));
            parametros.Add(new SqlParameter("nome", p.Nome));
            parametros.Add(new SqlParameter("id", p.Id));
            parametros.Add(new SqlParameter("dtInicio", p.DtInicio));
            parametros.Add(new SqlParameter("dtFinal", p.DtFinal));
            return parametros;
        }
    }
}
