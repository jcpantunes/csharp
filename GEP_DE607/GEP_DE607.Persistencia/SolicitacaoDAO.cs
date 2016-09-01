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
    public class SolicitacaoDAO : BaseDAO
    {

        public SolicitacaoDAO()
        {
            this.Tabela = "Solicitacao";
        }

        public List<Solicitacao> recuperar()
        {
            string query = "SELECT * FROM " + Tabela;
            return executarSelect(query);
        }

        public List<Solicitacao> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Solicitacao.CODIGO))
                    {
                        query += Solicitacao.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Solicitacao.ID))
                    {
                        query += Solicitacao.ID + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Solicitacao.DESTINATARIO))
                    {
                        query += Solicitacao.DESTINATARIO + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Solicitacao.ASSUNTO))
                    {
                        query += Solicitacao.ASSUNTO + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Solicitacao.DTINICIO))
                    {
                        query += Solicitacao.DTINICIO + " >= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                    else if (key.Equals(Solicitacao.DTFINAL))
                    {
                        query += Solicitacao.DTFINAL + " <= '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Solicitacao> executarSelect(string query)
        {
            List<Solicitacao> lista = new List<Solicitacao>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Solicitacao p = new Solicitacao(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3),
                        reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), 
                        reader.GetDateTime(8), reader.GetDateTime(9), reader.GetDateTime(10));
                    lista.Add(p);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Solicitacao> lista)
        {
            string queryInsert = "INSERT INTO " + Tabela + " (id, demanda, sistema, tipoDemanda, assunto, destinatario, status, "
                + " dtInicio, dtEntrega, dtFinal) "
                + " values (@id, @demanda, @sistema, @tipoDemanda, @assunto, @destinatario, @status, @dtInicio, @dtEntrega, @dtFinal);";

            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Solicitacao> lista)
        {
            string queryUpdate = "UPDATE " + Tabela + " SET "
                + " id = @id, "
                + " demanda = @demanda, "
                + " sistema = @sistema, "
                + " tipoDemanda = @tipoDemanda, "
                + " assunto = @assunto, "
                + " destinatario = @destinatario, "
                + " status = @status, "
                + " dtInicio = @dtInicio, "
                + " dtEntrega = @dtEntrega, "
                + " dtFinal = @dtFinal "
                + " WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Solicitacao> lista)
        {
            string query = "DELETE FROM " + Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Solicitacao> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Solicitacao f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Solicitacao p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", p.Codigo));
            parametros.Add(new SqlParameter("id", p.Id));
            parametros.Add(new SqlParameter("demanda", p.Demanda));
            parametros.Add(new SqlParameter("sistema", p.Sistema));
            parametros.Add(new SqlParameter("tipoDemanda", p.TipoDemanda));
            parametros.Add(new SqlParameter("assunto", p.Assunto));
            parametros.Add(new SqlParameter("destinatario", p.Destinatario));
            parametros.Add(new SqlParameter("status", p.Status));
            parametros.Add(new SqlParameter("dtInicio", p.DtInicio));
            parametros.Add(new SqlParameter("dtEntrega", p.DtEntrega));
            parametros.Add(new SqlParameter("dtFinal", p.DtFinal));
            return parametros;
        }

    }
}
