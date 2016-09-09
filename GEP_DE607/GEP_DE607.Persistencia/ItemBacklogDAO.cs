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
    public class ItemBacklogDAO : BaseDAO
    {
        public ItemBacklogDAO()
        {
            this.Tabela = "ItemBacklog";
        }

        public List<ItemBacklog> recuperar()
        {
            string query = "SELECT * FROM " + Tabela;
            return executarSelect(query);
        }

        public ItemBacklog recuperar(int codigo)
        {
            string query = "SELECT * FROM " + Tabela + " WHERE codigo = " + codigo;
            List<ItemBacklog> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<ItemBacklog> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    query += retornarPesquisaWhere(key, parametros);

                    if (key.Equals(ItemBacklog.VALOR_NEGOCIO))
                    {
                        query += ItemBacklog.VALOR_NEGOCIO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(ItemBacklog.TAMANHO))
                    {
                        query += ItemBacklog.TAMANHO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(ItemBacklog.COMPLEXIDADE))
                    {
                        query += ItemBacklog.COMPLEXIDADE + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(ItemBacklog.PF))
                    {
                        query += ItemBacklog.PF + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(ItemBacklog.PROJETO))
                    {
                        query += ItemBacklog.PROJETO + " = " + parametros[key] + " and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<ItemBacklog> executarSelect(string query)
        {
            List<ItemBacklog> lista = new List<ItemBacklog>();

            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    ItemBacklog t = new ItemBacklog();
                    t.Codigo = reader.GetInt32(0);
                    t.Tipo = reader.GetString(1);
                    t.Id = reader.GetInt32(2);
                    t.Titulo = reader.GetString(3);
                    // t.Responsavel = reader.GetInt32(4);
                    t.Status = reader.GetString(5);
                    t.PlanejadoPara = reader.GetString(6);
                    t.Pai = reader.GetString(7);
                    t.DataModificacao = reader.GetDateTime(8);
                    t.Projeto = reader.GetInt32(9);
                    t.ValorNegocio = reader.GetInt32(10);
                    t.Tamanho = reader.GetInt32(11);
                    t.Complexidade = reader.GetInt32(12);
                    t.Pf = reader.GetDecimal(13);
                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        public int recuperarQtdeItensBacklogPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            string query = "SELECT Count(id) FROM " + Tabela
                + " WHERE planejadoPara = '" + planejadoPara + "' and id in "
                    + " (SELECT distinct pai FROM Tarefa "
                    + " WHERE planejadoPara = '" + planejadoPara + "' and responsavel = " + responsavel + ")";
            return retornarSelectValorInt(query);
        }

        public int recuperarComplexidadeItensPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            string query = "SELECT avg(complexidade) FROM " + Tabela
                + " WHERE id in (SELECT distinct pai FROM Tarefa "
                    + " WHERE planejadoPara = '" + planejadoPara + "' and responsavel = " + responsavel + ")";
            return retornarSelectValorInt(query);
        }

        public void incluir(List<ItemBacklog> lista)
        {
            string queryInsert = "INSERT INTO " + Tabela
                + " (tipo, id, titulo, responsavel, status, planejadoPara, pai, dataModificacao, projeto, "
                + " valorNegocio, tamanho, complexidade, pf) "
                + " VALUES (@tipo, @id, @titulo, @responsavel, @status, @planejadoPara, @pai, @dataModificacao, @projeto, "
                + " @valorNegocio, @tamanho, @complexidade, @pf)";
            executarQuery(lista, queryInsert);
        }

        private string retornarQueryAtualizar(string campo)
        {
            string queryUpdate = "UPDATE " + Tabela
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "titulo = @titulo, "
                + "responsavel = @responsavel, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "pai = @pai, "
                + "dataModificacao = @dataModificacao, "
                + "projeto = @projeto, "
                + "valorNegocio = @valorNegocio, "
                + "tamanho = @tamanho, "
                + "complexidade = @complexidade, "
                + "pf = @pf "
                + "WHERE " + campo + " = @" + campo;
            return queryUpdate;
        }

        public void atualizar(List<ItemBacklog> lista)
        {
            string queryUpdate = retornarQueryAtualizar("codigo");
            executarQuery(lista, queryUpdate);
        }

        public void atualizarPorId(List<ItemBacklog> lista)
        {
            string queryUpdate = retornarQueryAtualizar("id");
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<ItemBacklog> lista)
        {
            string query = "DELETE FROM " + Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<ItemBacklog> lista, string query)
        {
            SqlConnection conn = null;
            conn = conectar(conn);
            for (int i = 0; i < lista.Count; i++)
            {
                ItemBacklog item = lista[i];
                SqlCommand cmd = new SqlCommand(query, conn);
                List<SqlParameter> listaParametros = criarListaParametros(item);
                foreach (SqlParameter parametro in listaParametros)
                {
                    cmd.Parameters.Add(parametro);
                }
                cmd.ExecuteNonQuery();
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(ItemBacklog t)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", t.Codigo));
            parametros.Add(new SqlParameter("tipo", t.Tipo));
            parametros.Add(new SqlParameter("id", t.Id));
            parametros.Add(new SqlParameter("titulo", t.Titulo));
            parametros.Add(new SqlParameter("responsavel", t.Responsavel.Codigo));
            parametros.Add(new SqlParameter("status", t.Status));
            parametros.Add(new SqlParameter("planejadoPara", t.PlanejadoPara));
            parametros.Add(new SqlParameter("pai", t.Pai));
            parametros.Add(new SqlParameter("dataModificacao", t.DataModificacao));
            parametros.Add(new SqlParameter("projeto", t.Projeto));
            parametros.Add(new SqlParameter("valorNegocio", t.ValorNegocio));
            parametros.Add(new SqlParameter("tamanho", t.Tamanho));
            parametros.Add(new SqlParameter("complexidade", t.Complexidade));
            parametros.Add(new SqlParameter("pf", t.Pf));
            return parametros;
        }

    }
}
