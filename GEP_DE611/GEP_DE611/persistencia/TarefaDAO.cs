using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;

namespace GEP_DE611.persistencia
{

    class TarefaDAO : BaseDAO
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

        public decimal recuperarEstimativaTotalPorSprint(string planejadoPara)
        {
            string query = "SELECT * FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and dataColeta in (SELECT distinct MAX (dataColeta) "
                + " FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "')";

            List<Tarefa> lista = executarSelect(query);
            decimal estimativaTotal = 0;
            foreach (Tarefa t in lista)
            {
                estimativaTotal += t.Estimativa.Length > 0 ? Convert.ToDecimal(t.Estimativa) : 0;
            }
            return estimativaTotal;
        }

        public List<KeyValuePair<string, decimal>> recuperarTempoGastoTotalPorData(string planejadoPara)
        {
            List<KeyValuePair<string, decimal>> tempoGastoPorData = new List<KeyValuePair<string, decimal>>();

            string query = "SELECT distinct (dataColeta) FROM " + TABELA
                + " WHERE planejadoPara = '" + planejadoPara + "'"
                + " and tempoGasto <> '' "
                + " ORDER BY dataColeta ASC ";

            List<string> datas = new List<string>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    datas.Add(reader.GetDateTime(0).ToString());
                }
            }
            desconectar(conn);

            foreach (string data in datas)
            {
                query = "SELECT * FROM " + TABELA
                    + " WHERE planejadoPara = '" + planejadoPara + "' "
                    + " and tempoGasto <> '' "
                    + " and dataColeta = '" + data + "' "
                    + " ORDER BY dataColeta ASC ";

                List<Tarefa> lista = executarSelect(query);
                decimal tempoGasto = 0;
                foreach (Tarefa t in lista)
                {
                    tempoGasto += t.TempoGasto.Length > 0 ? Convert.ToDecimal(t.TempoGasto) : 0;
                }
                tempoGastoPorData.Add(new KeyValuePair<string, decimal>(data, tempoGasto));
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
                    t.Estimativa = reader.GetString(8);
                    t.EstimaticaCorrigida = reader.GetString(9);
                    t.TempoGasto = reader.GetString(10);
                    t.Responsavel = recuperarFuncionario(listaFuncionario, reader.GetInt32(11));

                    lista.Add(t);
                }
            }
            desconectar(conn);
            return lista;
        }

        private Funcionario recuperarFuncionario(List<Funcionario> listaFuncionario, int codigo)
        {
            Funcionario funcionario = null;
            foreach (Funcionario f in listaFuncionario)
            {
                if (f.Codigo == codigo)
                {
                    funcionario = f;
                    break;
                }
            }
            if (funcionario == null)
            {
                FuncionarioDAO dao = new FuncionarioDAO();
                funcionario = dao.recuperar(codigo);
                listaFuncionario.Add(funcionario);
            }
            return funcionario;
        }

        public void incluir (List<Tarefa> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA
                + " (tipo, id, responsavel, titulo, status, planejadoPara, dataColeta, pai, "
                + "estimativa, estimaticaCorrigida, tempoGasto) " 
                + " VALUES (@tipo, @id, @responsavel, @titulo, @status, @planejadoPara, @dataColeta, "
	            + "@pai, @estimativa, @estimaticaCorrigida, @tempoGasto)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar (List<Tarefa> lista)
        {
            string queryUpdate = "UPDATE " + TABELA 
                + " SET tipo = @tipo, "
                + "id = @id, "
                + "reponsavel = @responsavel, "
                + "titulo = @titulo, "
                + "status = @status, "
                + "planejadoPara = @planejadoPara, "
                + "dataColeta = @dataColeta, "
                + "pai = @pai, "
                + "estimativa = @estimativa, "
                + "estimaticaCorrigida = @estimaticaCorrigida, "
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
            for (int i = 0; i < lista.Count; i++)
            {
                Tarefa f = lista[i];
                save(conn, query, criarListaParametros(f));
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
            parametros.Add(new SqlParameter("estimaticaCorrigida", t.EstimaticaCorrigida));
            parametros.Add(new SqlParameter("tempoGasto", t.TempoGasto));
            return parametros;
        }
    }
}
