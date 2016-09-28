using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GEP_DE607.Dominio;
using GEP_DE607.Dominio.Modelo;

namespace GEP_DE607.Persistencia
{
    public class ApropriacaoDAO : BaseDAO
    {

        public ApropriacaoDAO()
        {
            this.Tabela = "Apropriacao";
        }

        public List<Apropriacao> recuperar()
        {
            string query = "SELECT * FROM " + Tabela;
            return executarSelect(query);
        }

        public List<Apropriacao> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Apropriacao.CODIGO))
                    {
                        query += Apropriacao.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Apropriacao.NOME))
                    {
                        query += Apropriacao.NOME + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Apropriacao.DATA))
                    {
                        query += Apropriacao.DATA + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Apropriacao.HORA))
                    {
                        query += Apropriacao.HORA + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Apropriacao.TAREFA))
                    {
                        query += Apropriacao.TAREFA + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Apropriacao.MACROATIVIDADE))
                    {
                        query += Apropriacao.MACROATIVIDADE + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Apropriacao.MNEMONICO))
                    {
                        query += Apropriacao.MNEMONICO + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Apropriacao.FILTRO_DTINICIO))
                    {
                        query += Apropriacao.DATA + " >= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Apropriacao.FILTRO_DTINICIO))
                    {
                        query += Apropriacao.DATA + " <= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Apropriacao> executarSelect(string query)
        {
            List<Apropriacao> lista = new List<Apropriacao>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Apropriacao f = new Apropriacao(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), 
                        reader.GetDecimal(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7));
                    lista.Add(f);
                }
            }
            desconectar(conn);
            return lista;
        }

        private List<ApropriacaoTarefa> executarSelectApropriacaoTarefa(string query)
        {
            List<ApropriacaoTarefa> lista = new List<ApropriacaoTarefa>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    ApropriacaoTarefa f = new ApropriacaoTarefa(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2),
                        reader.GetDecimal(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7), reader.GetString(8));
                    lista.Add(f);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Apropriacao> lista)
        {
            string queryInsert = "INSERT INTO " + Tabela + " (nome, data, hora, tarefa, macroatividade, mnemonico, projeto) "
                + "values (@nome, @data, @hora, @tarefa, @macroatividade, @mnemonico, @projeto);";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Apropriacao> lista)
        {
            string queryUpdate = "UPDATE " + Tabela + " SET "
                + " nome = @nome, "
                + " data = @data, "
                + " hora = @hora, "
                + " tarefa = @tarefa, "
                + " macroatividade = @macroatividade, "
                + " mnemonico = @mnemonico, "
                + " projeto = @projeto "
                + " WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Apropriacao> lista)
        {
            string query = "DELETE FROM " + Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Apropriacao> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Apropriacao f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        public List<ApropriacaoTarefa> recuperarApropriacaoPorResponsavel(string responsavel, DateTime dtInicio, DateTime dtFinal)
        {
            string query = "SELECT aprop.codigo, aprop.nome, aprop.data, aprop.hora, aprop.tarefa, aprop.macroatividade, aprop.mnemonico, aprop.projeto, tar.titulo"
                + " FROM " + Tabela + " aprop inner join Tarefa tar on aprop.tarefa = tar.id "
                + " WHERE nome = '" + responsavel + "' and data >= '" + Convert.ToDateTime(dtInicio) + "'"
                + " and data <= '" + Convert.ToDateTime(dtFinal) + "' order by aprop.data;";
            return executarSelectApropriacaoTarefa(query);
        }

        private List<SqlParameter> criarListaParametros(Apropriacao f)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", f.Codigo));
            parametros.Add(new SqlParameter("nome", f.Nome));
            parametros.Add(new SqlParameter("data", f.Data));
            parametros.Add(new SqlParameter("hora", f.Hora));
            parametros.Add(new SqlParameter("tarefa", f.Tarefa));
            parametros.Add(new SqlParameter("macroatividade", f.Macroatividade));
            parametros.Add(new SqlParameter("mnemonico", f.Mnemonico));
            parametros.Add(new SqlParameter("projeto", f.Projeto));
            return parametros;
        }
    }
}
