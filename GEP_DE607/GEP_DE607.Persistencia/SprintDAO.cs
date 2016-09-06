using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GEP_DE607.Dominio;

namespace GEP_DE607.Persistencia
{
    public class SprintDAO : BaseDAO
    {
        

        public SprintDAO()
        {
            this.Tabela = "Sprint";
        }

        public List<Sprint> recuperar()
        {
            string query = "SELECT * FROM " + this.Tabela;
            return executarSelect(query);
        }

        public List<Sprint> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + this.Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Sprint.CODIGO))
                    {
                        query += Sprint.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Sprint.NOME))
                    {
                        query += Sprint.NOME + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Sprint.DTINICIO))
                    {
                        query += Sprint.DTINICIO + " >= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Sprint.DTFINAL))
                    {
                        query += Sprint.DTFINAL + " <= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Sprint.PROJETO))
                    {
                        query += Sprint.PROJETO + " = " + parametros[key] + " and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Sprint> executarSelect(string query)
        {
            query += " Order by " + Sprint.NOME;
            List<Sprint> lista = new List<Sprint>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Sprint s = new Sprint();
                    s.Codigo = reader.GetInt32(0);
                    s.Nome = reader.GetString(1);
                    s.DtInicio = reader.GetDateTime(2);
                    s.DtFinal = reader.GetDateTime(3);
                    s.Projeto = reader.GetInt32(4);
                    lista.Add(s);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Sprint> lista)
        {
            string queryInsert = "INSERT INTO " + this.Tabela + " (nome, dtInicio, dtFinal, projeto) "
                + "values (@nome, @dtInicio, @dtFinal, @projeto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Sprint> lista)
        {
            string queryUpdate = "UPDATE " + this.Tabela + " SET "
                + " nome = @nome, "
                + " dtInicio = @dtInicio, "
                + " dtFinal = @dtFinal, "
                + " projeto = @projeto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Sprint> lista)
        {
            string query = "DELETE FROM " + this.Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Sprint> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Sprint f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Sprint p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", p.Codigo));
            parametros.Add(new SqlParameter("nome", p.Nome));
            parametros.Add(new SqlParameter("dtInicio", p.DtInicio));
            parametros.Add(new SqlParameter("dtFinal", p.DtFinal));
            parametros.Add(new SqlParameter("projeto", p.Projeto));
            return parametros;
        }
    }
}
