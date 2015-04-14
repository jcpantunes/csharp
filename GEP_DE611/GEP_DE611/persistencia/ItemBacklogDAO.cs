using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;

namespace GEP_DE611.persistencia
{
    class ItemBacklogDAO : BaseDAO
    {
        private string TABELA = "ItemBacklog";

        public ItemBacklogDAO()
        {
        }
        
        public List<ItemBacklog> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public ItemBacklog recuperar(int codigo)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE codigo = " + codigo;
            List<ItemBacklog> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<ItemBacklog> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
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
                    t.Status = reader.GetString(4);
                    t.PlanejadoPara = reader.GetString(5);
                    t.DataColeta = reader.GetDateTime(6);
                    t.ValorNegocio = reader.GetInt32(7);
                    t.Tamanho = reader.GetInt32(8);
                    t.Complexidade = reader.GetInt32(9);
                    t.Pf = reader.GetDecimal(10);
                    t.Projeto = reader.GetInt32(11);
                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir (List<ItemBacklog> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA
                + " (tipo, id, titulo, status, planejadoPara, dataColeta, "
                + " valorNegocio, tamanho, complexidade, pf, codigoProjeto) "
                + " VALUES (@tipo, @id, @titulo, @status, @planejadoPara, @dataColeta, "
                + " @valorNegocio, @tamanho, @complexidade, @pf, @codigoProjeto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<ItemBacklog> lista)
        {
            string queryUpdate = "UPDATE " + TABELA 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "valorNegocio = @valorNegocio, "
                + "tamanho = @tamanho, "
                + "complexidade = @complexidade, "
                + "pf = @pf, "
                + "codigoProjeto = @codigoProjeto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void atualizarPorId(List<ItemBacklog> lista)
        {
            string queryUpdate = "UPDATE " + TABELA
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "valorNegocio = @valorNegocio, "
                + "tamanho = @tamanho, "
                + "complexidade = @complexidade, "
                + "pf = @pf, "
                + "codigoProjeto = @codigoProjeto "
                + "WHERE id = @id";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<ItemBacklog> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<ItemBacklog> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                ItemBacklog f = lista[i];
                save(conn, query, criarListaParametros(f));
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
            parametros.Add(new SqlParameter("status", t.Status));
            parametros.Add(new SqlParameter("planejadoPara", t.PlanejadoPara));
            parametros.Add(new SqlParameter("dataColeta", t.DataColeta));
            parametros.Add(new SqlParameter("valorNegocio", t.ValorNegocio));
            parametros.Add(new SqlParameter("tamanho", t.Tamanho));
            parametros.Add(new SqlParameter("complexidade", t.Complexidade));
            parametros.Add(new SqlParameter("pf", t.Pf));
            parametros.Add(new SqlParameter("codigoProjeto", t.Projeto));
            return parametros;
        }
    }
}
