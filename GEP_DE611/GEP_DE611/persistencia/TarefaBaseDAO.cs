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
    abstract class TarefaBaseDAO : BaseDAO
    {
        private string tabela;
        public string Tabela
        {
            get { return tabela; }
            set { tabela = value; }
        }

        public TarefaBaseDAO()
        {
        }
        
        public List<Tarefa> recuperar()
        {
            string query = "SELECT * FROM " + Tabela;
            return executarSelect(query);
        }

        public Tarefa recuperar(int codigo)
        {
            string query = "SELECT * FROM " + Tabela + " WHERE codigo = " + codigo;
            List<Tarefa> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public List<Tarefa> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    query += retornarPesquisaWhere(key, parametros);
                    if (key.Equals(Tarefa.RESPONSAVEL))
                    {
                        query += Tarefa.RESPONSAVEL + " = " + parametros[key] + " and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        public decimal recuperarEstimativaTotalPorSprint(string planejadoPara)
        {
            string query = "SELECT SUM(estimativa) FROM " + Tabela + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and estimativaCorrigida = 0 and dataColeta in (SELECT distinct MAX (dataColeta) "
				+ " FROM " + Tabela + " WHERE planejadoPara = '" + planejadoPara + "') "
                + " union "
                + " SELECT SUM(estimativaCorrigida) FROM " + Tabela + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and estimativaCorrigida > 0 and dataColeta in (SELECT distinct MAX (dataColeta) "
				+ " FROM " + Tabela + " WHERE planejadoPara = '" + planejadoPara + "') ";

            decimal estimativaTotal = 0;
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        estimativaTotal += reader.GetDecimal(0);
                    }
                }
            }
            desconectar(conn);
            return estimativaTotal;
        }

        public List<DateTime> recuperarListaDatasPorString(string planejadoPara)
        {
            string query = "SELECT distinct (dataColeta) FROM " + Tabela
                + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and tempoGasto <> 0 "
                + " ORDER BY dataColeta ASC ";

            List<DateTime> datas = new List<DateTime>();

            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    datas.Add(reader.GetDateTime(0));
                }
            }
            desconectar(conn);

            return datas;
        }

        public List<KeyValuePair<string, decimal>> recuperarTempoGastoTotalPorData(string planejadoPara)
        {
            List<KeyValuePair<string, decimal>> tempoGastoPorData = new List<KeyValuePair<string, decimal>>();
            List<DateTime> datas = recuperarListaDatasPorString(planejadoPara);

            string whereData = "";
            foreach (DateTime data in datas)
            {
                whereData += " dataColeta = '" + data + "' or ";
            }
            if (whereData.Length > 0)
            {
                string query = "SELECT dataColeta, SUM(tempoGasto) FROM " + Tabela
                    + " WHERE planejadoPara = '" + planejadoPara + "' "
                    + " and tempoGasto > 0 and " + whereData.Substring(0, (whereData.Length - 3))
                    + " GROUP BY dataColeta ORDER BY dataColeta ASC ";

                SqlConnection conn = null;
                SqlDataReader reader = select(conn, query);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        DateTime data  = reader.GetDateTime(0);
                        decimal tempoGasto = reader.GetDecimal(1);
                        tempoGastoPorData.Add(new KeyValuePair<string, decimal>(data.ToShortDateString(), tempoGasto));
                    }
                }
                desconectar(conn);
            }
            
            return tempoGastoPorData;
        }

        private List<Tarefa> executarSelect(string query)
        {
            List<Tarefa> lista = new List<Tarefa>();
            
            SqlConnection conn = null;

            List<Funcionario> listaFuncionario = new List<Funcionario>();

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
                    t.Status = reader.GetString(4);
                    t.PlanejadoPara = reader.GetString(5);
                    t.Pai = reader.GetString(6);
                    t.DataColeta = reader.GetDateTime(7);
                    t.Estimativa = reader.GetDecimal(8);
                    t.EstimativaCorrigida = reader.GetDecimal(9);
                    t.TempoGasto = reader.GetDecimal(10);
                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    t.Responsavel = fDAO.recuperarFuncionarioInCache(listaFuncionario, reader.GetInt32(11));

                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir (List<Tarefa> lista)
        {
            string queryInsert = "INSERT INTO " + Tabela
                + " (tipo, id, responsavel, titulo, status, planejadoPara, dataColeta, pai, "
                + "estimativa, estimativaCorrigida, tempoGasto) " 
                + " VALUES (@tipo, @id, @responsavel, @titulo, @status, @planejadoPara, @dataColeta, "
	            + "@pai, @estimativa, @estimativaCorrigida, @tempoGasto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<Tarefa> lista)
        {
            string queryUpdate = "UPDATE " + Tabela 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "responsavel = @responsavel, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "pai = @pai, "
                + "estimativa = @estimativa, "
                + "estimativaCorrigida = @estimativaCorrigida, "
                + "tempoGasto = @tempoGasto "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Tarefa> lista)
        {
            string query = "DELETE FROM " + Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        public void excluirPorSprintPorData(string planejadoPara, string data)
        {
            string query = "DELETE FROM " + Tabela
                    + " WHERE planejadoPara = '" + planejadoPara + "' "
                    + " and dataColeta = '" + data + "' ";
            
            SqlConnection conn = null;
            save(conn, query);
            desconectar(conn);
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
            parametros.Add(new SqlParameter("responsavel", t.Responsavel.Codigo));
            parametros.Add(new SqlParameter("titulo", t.Titulo));
            parametros.Add(new SqlParameter("status", t.Status));
            parametros.Add(new SqlParameter("planejadoPara", t.PlanejadoPara));
            parametros.Add(new SqlParameter("dataColeta", t.DataColeta));
            parametros.Add(new SqlParameter("pai", t.Pai));
            parametros.Add(new SqlParameter("estimativa", t.Estimativa));
            parametros.Add(new SqlParameter("estimativaCorrigida", t.EstimativaCorrigida));
            parametros.Add(new SqlParameter("tempoGasto", t.TempoGasto));
            return parametros;
        }
    }
}
