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
    public class FuncionarioDAO : BaseDAO
    {
        private string TABELA = "Funcionario";

        public FuncionarioDAO()
        {
        }

        public List<Funcionario> recuperar()
        {
            string query = "SELECT * FROM " + TABELA;
            return executarSelect(query);
        }

        public List<Funcionario> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + TABELA;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Funcionario.CODIGO))
                    {
                        query += Funcionario.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Funcionario.LOTACAO))
                    {
                        query += Funcionario.LOTACAO + " like '%" + parametros[key] + "%' and ";
                    }
                    else if (key.Equals(Funcionario.NOME))
                    {
                        query += Funcionario.NOME + " like '%" + parametros[key] + "%' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        public Funcionario recuperar(int codigo)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE codigo = " + codigo;
            List<Funcionario> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public Funcionario recuperar(string nome)
        {
            string query = "SELECT * FROM " + TABELA + " WHERE nome = '" + nome + "'";
            List<Funcionario> lista = executarSelect(query);
            if (lista.Count > 0)
            {
                return lista[0];
            }
            return null;
        }

        public Funcionario recuperarFuncionarioInCache(List<Funcionario> listaFuncionario, int codigo)
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
                funcionario = recuperar(codigo);
                listaFuncionario.Add(funcionario);
            }
            return funcionario;
        }

        private List<Funcionario> executarSelect(string query)
        {
            List<Funcionario> lista = new List<Funcionario>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                while (reader.Read())
                {
                    Funcionario f = new Funcionario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    lista.Add(f);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Funcionario> lista)
        {
            string queryInsert = "INSERT INTO " + TABELA + " (lotacao, nome) "
                + "values (@lotacao, @nome)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Funcionario> lista)
        {
            string queryUpdate = "UPDATE " + TABELA + " SET lotacao = @lotacao, "
                + "nome = @nome "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Funcionario> lista)
        {
            string query = "DELETE FROM " + TABELA + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Funcionario> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Funcionario f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Funcionario f)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", f.Codigo));
            parametros.Add(new SqlParameter("lotacao", f.Lotacao));
            parametros.Add(new SqlParameter("nome", f.Nome));
            return parametros;
        }
    }
}
