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
    public class TarefaDAO : BaseDAO
    {
        private string TABELA = "Tarefa";

        public TarefaDAO()
        {
        }
        
        public List<Tarefa> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public Tarefa recuperar(int codigo)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE codigo = " + codigo;
            List<Tarefa> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<Tarefa> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    query += retornarPesquisaWhere(key, parametros);
                    if (key.Equals(Tarefa.CLASSIFICACAO))
                    {
                        query += Tarefa.CLASSIFICACAO + " = '" + parametros[key] + "' and ";
                    }
                    else if (key.Equals(Tarefa.ESTIMATIVA))
                    {
                        query += Tarefa.ESTIMATIVA + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Tarefa.TEMPO_GASTO))
                    {
                        query += Tarefa.TEMPO_GASTO + " = " + parametros[key] + " and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        private List<Tarefa> executarSelect(string query)
        {
            List<Tarefa> lista = new List<Tarefa>();
            
            SqlConnection conn = null;

            FuncionarioDAO funcDAO = new FuncionarioDAO();
            List<Funcionario> listaFuncionario = funcDAO.recuperar();

            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Tarefa t = new Tarefa();
                    t.Codigo = reader.GetInt32(0);
                    t.Tipo = reader.GetString(1);
                    t.Id = reader.GetInt32(2);
                    t.Titulo = reader.GetString(3);
                    t.Responsavel = listaFuncionario.Where(f => f.Codigo.Equals(reader.GetInt32(4))).First(); 
                    t.Status = reader.GetString(5);
                    t.PlanejadoPara = reader.GetString(6);
                    t.Pai = reader.GetString(7);
                    t.DataModificacao = reader.GetDateTime(8);
                    t.Projeto = reader.GetInt32(9);
                    t.Classificacao = reader.GetString(10);
                    t.Estimativa = reader.GetDecimal(11);
                    t.TempoGasto = reader.GetDecimal(12);

                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir (List<Tarefa> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA
                + " (tipo, id, titulo, responsavel, status, planejadoPara, pai, dataModificacao, projeto, "
                + " classificacao, estimativa, tempoGasto) "
                + " VALUES (@tipo, @id, @titulo, @responsavel, @status, @planejadoPara, @pai, @dataModificacao, @projeto, "
                + " @classificacao, @estimativa, @tempoGasto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<Tarefa> lista)
        {
            string queryUpdate = "UPDATE " + TABELA 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "titulo = @titulo, "
                + "responsavel = @responsavel, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "pai = @pai, "
                + "dataModificacao = @dataModificacao, "
                + "projeto = @projeto, "
                + "classificacao = @classificacao, "
                + "estimativa = @estimativa, "
                + "tempoGasto = @tempoGasto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Tarefa> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Tarefa> lista, string query)
        {
            SqlConnection conn = null;
            conn = conectar(conn);
            for (int i = 0; i < lista.Count; i++)
            {
                Tarefa f = lista[i];
                SqlCommand cmd = new SqlCommand(query, conn);
                List<SqlParameter> listaParametros = criarListaParametros(f);
                foreach (SqlParameter parametro in listaParametros)
                {
                    cmd.Parameters.Add(parametro);
                }
                cmd.ExecuteNonQuery();
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Tarefa t) 
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
            parametros.Add(new SqlParameter("classificacao", t.Classificacao));
            parametros.Add(new SqlParameter("estimativa", t.Estimativa));
            parametros.Add(new SqlParameter("tempoGasto", t.TempoGasto));
            return parametros;
        }
    }
}
