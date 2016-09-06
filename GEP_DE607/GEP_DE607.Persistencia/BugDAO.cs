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
    public class BugDAO : BaseDAO
    {

        public static string TIPO_DEFEITO = "Defeito";
        public static string TIPO_RELATO = "Relato";

        public BugDAO(string tipo)
        {
            this.Tabela = tipo;
        }

        public List<Bug> recuperar()
        {
            string query = "SELECT * FROM " + Tabela;
            return executarSelect(query);
        }

        public Bug recuperar(int codigo)
        {
            string query = "SELECT * FROM " + Tabela + " WHERE codigo = " + codigo;
            List<Bug> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<Bug> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    query += retornarPesquisaWhere(key, parametros);
                    if (key.Equals(Bug.CRIADO_POR))
                    {
                        query += Bug.CRIADO_POR + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Bug.ENCONTRADO_PROJETO))
                    {
                        query += Bug.ENCONTRADO_PROJETO + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Bug.TIPO_RELATO))
                    {
                        query += Bug.TIPO_RELATO + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Bug.RESOLUCAO))
                    {
                        query += Bug.RESOLUCAO + " = '" + parametros[key] + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Bug> executarSelect(string query)
        {
            List<Bug> lista = new List<Bug>();

            SqlConnection conn = null;

            FuncionarioDAO funcDAO = new FuncionarioDAO();
            List<Funcionario> listaFuncionario = funcDAO.recuperar();

            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Bug bug = new Bug();
                    bug.Codigo = reader.GetInt32(0);
                    bug.Tipo = reader.GetString(1);
                    bug.Id = reader.GetInt32(2);
                    bug.Titulo = reader.GetString(3);
                    bug.Responsavel = listaFuncionario.Where(f => f.Codigo.Equals(reader.GetInt32(4))).First();
                    bug.Status = reader.GetString(5);
                    bug.PlanejadoPara = reader.GetString(6);
                    bug.Pai = reader.GetString(7);
                    bug.DataModificacao = reader.GetDateTime(8);
                    bug.Projeto = reader.GetInt32(9);
                    bug.CriadoPor = reader.GetString(10);
                    bug.EncontradoProjeto = reader.GetString(11);
                    bug.TipoRelato = reader.GetString(12);
                    bug.Resolucao = reader.GetString(13);

                    lista.Add(bug);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Bug> lista)
        {
            string queryInsert = "INSERT INTO " + Tabela
                + " (tipo, id, titulo, responsavel, status, planejadoPara, pai, dataModificacao, projeto, "
                + " criadoPor, encontradoProjeto, tipoRelato, resolucao) "
                + " VALUES (@tipo, @id, @titulo, @responsavel, @status, @planejadoPara, @pai, @dataModificacao, @projeto, "
                + " @criadoPor, @encontradoProjeto, @tipoRelato, @resolucao)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Bug> lista)
        {
            string queryUpdate = "UPDATE " + Tabela
                + " SET tipo = @tipo, "
                + " id = @id, "
                + " titulo = @titulo, "
                + " responsavel = @responsavel, "
                + " status = @status, "
                + " planejadoPara = @planejadoPara, "
                + " pai = @pai, "
                + " dataModificacao = @dataModificacao, "
                + " projeto = @projeto, "
                + " criadoPor = @criadoPor, "
                + " encontradoProjeto = @encontradoProjeto, "
                + " tipoRelato = @tipoRelato, "
                + " resolucao = @resolucao "
                + " WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Bug> lista)
        {
            string query = "DELETE FROM " + Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Bug> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Bug f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Bug bug)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", bug.Codigo));
            parametros.Add(new SqlParameter("tipo", bug.Tipo));
            parametros.Add(new SqlParameter("id", bug.Id));
            parametros.Add(new SqlParameter("titulo", bug.Titulo));
            parametros.Add(new SqlParameter("responsavel", bug.Responsavel.Codigo));
            parametros.Add(new SqlParameter("status", bug.Status));
            parametros.Add(new SqlParameter("planejadoPara", bug.PlanejadoPara));
            parametros.Add(new SqlParameter("pai", bug.Pai));
            parametros.Add(new SqlParameter("dataModificacao", bug.DataModificacao));
            parametros.Add(new SqlParameter("projeto", bug.Projeto));
            parametros.Add(new SqlParameter("criadoPor", bug.CriadoPor));
            parametros.Add(new SqlParameter("encontradoProjeto", bug.EncontradoProjeto));
            parametros.Add(new SqlParameter("tipoRelato", bug.TipoRelato));
            parametros.Add(new SqlParameter("resolucao", bug.Resolucao));
            return parametros;
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

        public int recuperarDefeitosCorrigidosResponsavel(string planejadoPara, int responsavel)
        {
            string query = "SELECT COUNT(id) FROM Defeito d "
                + "WHERE d.planejadoPara = '" + planejadoPara + "' and d.tipoRelato = 'Erro' and d.responsavel = " + responsavel;
            return retornarSelectValorInt(query);
        }
    }
}
