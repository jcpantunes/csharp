using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;

namespace GEP_DE611.persistencia
{
    class DefeitoDAO : BaseDAO
    {

        private string TABELA = "Defeito";

        public DefeitoDAO()
        {
        }
        
        public List<Defeito> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public Defeito recuperar(int codigo)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE codigo = " + codigo;
            List<Defeito> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<Defeito> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    query += retornarPesquisaWhere(key, parametros);

                    if (key.Equals(Defeito.ENCONTRADO_PROJETO))
                    {
                        query += Defeito.ENCONTRADO_PROJETO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Defeito.TIPO_RELATO))
                    {
                        query += Defeito.TIPO_RELATO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Defeito.RESOLUCAO))
                    {
                        query += Defeito.RESOLUCAO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Defeito.PROJETO))
                    {
                        query += ItemBacklog.PROJETO + " = " + parametros[key] + " and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Defeito> executarSelect(string query)
        {
            List<Defeito> lista = new List<Defeito>();
            List<Funcionario> listaFuncionario = new List<Funcionario>();
            
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Defeito d = new Defeito();
                    d.Codigo = reader.GetInt32(0);
                    d.Tipo = reader.GetString(1);
                    d.Id = reader.GetInt32(2);
                    d.Titulo = reader.GetString(3);
                    d.Status = reader.GetString(4);
                    d.PlanejadoPara = reader.GetString(5);
                    d.DataColeta = reader.GetDateTime(6);
                    d.EncontradoProjeto = reader.GetString(7);
                    d.TipoRelato = reader.GetString(8);
                    d.Resolucao = reader.GetString(9);
                    d.Pai = reader.GetString(10);
                    d.Projeto = reader.GetInt32(11);
                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    d.Responsavel = fDAO.recuperarFuncionarioInCache(listaFuncionario, reader.GetInt32(12));
                    lista.Add(d);
                }
            }
            desconectar(conn);
            return lista;
        }

        public decimal recuperarMediaDefeitosPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            string query = "SELECT ROUND(AVG(CAST(total AS DECIMAL)), 2) FROM ( "
                + "SELECT id, count(codigo) as total FROM "
                    + "(SELECT distinct item.id as id, d.codigo as codigo FROM ItemBacklog AS item "
                        + "LEFT JOIN Defeito AS d ON d.pai = item.id LEFT JOIN Tarefa AS t ON item.id = t.pai "
                        + "WHERE t.planejadoPara = '" + planejadoPara + "' and t.responsavel = " + responsavel + ") as defeitosPorItem "
                    + "GROUP BY id) as mediaDefeitos";
            return retornarSelectValorDecimal(query);
        }

        public void incluir (List<Defeito> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA
                + " (tipo, id, titulo, status, planejadoPara, dataColeta, "
                + " encontradoProjeto, tipoRelato, resolucao, pai, codigoProjeto, responsavel) "
                + " VALUES (@tipo, @id, @titulo, @status, @planejadoPara, @dataColeta, "
                + " @encontradoProjeto, @tipoRelato, @resolucao, @pai, @codigoProjeto, @responsavel)";
            executarQuery(lista, queryInsert);
        }

        private string retornarQueryAtualizar(string campo)
        {
            string queryUpdate = "UPDATE " + TABELA 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "encontradoProjeto = @encontradoProjeto, "
                + "tipoRelato = @tipoRelato, "
                + "resolucao = @resolucao, "
                + "pai = @pai, "
                + "codigoProjeto = @codigoProjeto, "
                + "responsavel = @responsavel "
                + "WHERE " + campo + " = @" + campo;
            return queryUpdate;
        }

        public void atualizar(List<Defeito> lista)
        {
            string queryUpdate = retornarQueryAtualizar("codigo");
            executarQuery(lista, queryUpdate);
        }

        public void atualizarPorId(List<Defeito> lista)
        {
            string queryUpdate = retornarQueryAtualizar("id");
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Defeito> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Defeito> lista, string query)
        {
            SqlConnection conn = null;
            conn = conectar(conn);
            for (int i = 0; i < lista.Count; i++)
            {
                Defeito item = lista[i];
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

        private List<SqlParameter> criarListaParametros(Defeito d) 
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", d.Codigo));
            parametros.Add(new SqlParameter("tipo", d.Tipo));
            parametros.Add(new SqlParameter("id", d.Id));
            parametros.Add(new SqlParameter("titulo", d.Titulo));
            parametros.Add(new SqlParameter("status", d.Status));
            parametros.Add(new SqlParameter("planejadoPara", d.PlanejadoPara));
            parametros.Add(new SqlParameter("dataColeta", d.DataColeta));
            parametros.Add(new SqlParameter("encontradoProjeto", d.EncontradoProjeto));
            parametros.Add(new SqlParameter("tipoRelato", d.TipoRelato));
            parametros.Add(new SqlParameter("resolucao", d.Resolucao));
            parametros.Add(new SqlParameter("pai", d.Pai));
            parametros.Add(new SqlParameter("codigoProjeto", d.Projeto));
            parametros.Add(new SqlParameter("responsavel", d.Responsavel.Codigo));
            return parametros;
        }

    }
}
