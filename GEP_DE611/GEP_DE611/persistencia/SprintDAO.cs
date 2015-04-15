using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE611.dominio;
using System.Data.SqlClient;

namespace GEP_DE611.persistencia
{
    class SprintDAO : BaseDAO
    {
        private string TABELA = "Sprint";

        public SprintDAO()
        {
        }

        public List<Sprint> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public List<Sprint> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
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
                query = query.Substring(0, (query.Length-4));
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
                    s.Projeto = recuperarProjeto(reader.GetInt32(4));
                    lista.Add(s);
                }
            }
            desconectar(conn);
            return lista;
        }

        private Projeto recuperarProjeto(int codigo)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Projeto.CODIGO, Convert.ToString(codigo));

            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar(param);
            
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return new Projeto(0, "NaoEncontrado", 100099, DateTime.Now, DateTime.Now);
        }

        public void incluir (List<Sprint> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA + " (nome, dtInicio, dtFinal, codigoProjeto) "
                + "values (@nome, @dtInicio, @dtFinal, @codigoProjeto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<Sprint> lista)
        {
            string queryUpdate = "UPDATE " + TABELA + " SET " 
                + " nome = @nome, "
                + " dtInicio = @dtInicio, "
                + " dtFinal = @dtFinal, " 
                + " codigoProjeto = @codigoProjeto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Sprint> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
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
            parametros.Add(new SqlParameter("codigoProjeto", p.Projeto.Codigo));
            return parametros;
        }
    }
}
