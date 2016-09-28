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
    public class SiscopDAO : BaseDAO
    {

        public SiscopDAO()
        {
            this.Tabela = "Siscop";
        }

        public List<Siscop> recuperar()
        {
            string query = "SELECT * FROM " + this.Tabela;
            return executarSelect(query);
        }

        public List<Siscop> recuperar(Dictionary<string, string> parametros)
        {
            string query = "SELECT * FROM " + this.Tabela;
            if (parametros.Count > 0)
            {
                query += " WHERE ";

                foreach (string key in parametros.Keys)
                {
                    if (key.Equals(Siscop.CODIGO))
                    {
                        query += Siscop.CODIGO + " = " + parametros[key] + " and ";
                    }
                    else if (key.Equals(Siscop.DATA))
                    {
                        query += Siscop.DATA + " = '" + Convert.ToDateTime(parametros[key]) + "' and ";
                    }
                }
                query = query.Substring(0, (query.Length - 4));
            }
            return executarSelect(query);
        }

        public List<Siscop> recuperarSiscopPorResponsavel(int codigoResponsavel, DateTime dtInicio, DateTime dtFinal)
        {
            string query = "SELECT * FROM Siscop sisc WHERE responsavel = " + codigoResponsavel
                + " and data >= '" + Convert.ToDateTime(dtInicio) + "'"
                + " and data <= '" + Convert.ToDateTime(dtFinal) + "' ";
            return executarSelect(query);
        }

        private List<Siscop> executarSelect(string query)
        {
            query += " Order by " + Siscop.DATA;
            List<Siscop> lista = new List<Siscop>();
            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                FuncionarioDAO funcDAO = new FuncionarioDAO();
                List<Funcionario> listaFuncionario = funcDAO.recuperar();

                while (reader.Read())
                {
                    Siscop s = new Siscop();
                    s.Codigo = reader.GetInt32(0);
                    s.Responsavel = listaFuncionario.Where(f => f.Codigo.Equals(reader.GetInt32(1))).First();
                    s.Data = reader.GetDateTime(2);
                    s.Batida = reader.GetString(3);
                    string[] batida = s.Batida.Split('|');
                    s.Entrada1 = batida[0];
                    s.Saida1 = batida[1];
                    s.Entrada2 = batida[2];
                    s.Saida2 = batida[3];
                    s.Extra1 = batida[4];
                    s.Extra2 = batida[5];
                    s.Extra3 = batida[6];
                    s.Extra4 = batida[7];
                    lista.Add(s);
                }
            }
            desconectar(conn);
            return lista;
        }

        public void incluir(List<Siscop> lista)
        {
            string queryInsert = "INSERT INTO " + this.Tabela + " (responsavel, data, batida) "
                + "values (@responsavel, @data, @batida)";
            executarQuery(lista, queryInsert);
        }

        public void atualizar(List<Siscop> lista)
        {
            string queryUpdate = "UPDATE " + this.Tabela + " SET "
                + " responsavel = @responsavel, "
                + " data = @data , "
                + " batida = @batida "
                + "WHERE codigo = @codigo";
            executarQuery(lista, queryUpdate);
        }

        public void excluir(List<Siscop> lista)
        {
            string query = "DELETE FROM " + this.Tabela + " WHERE codigo = @codigo";
            executarQuery(lista, query);
        }

        private void executarQuery(List<Siscop> lista, string query)
        {
            SqlConnection conn = null;
            for (int i = 0; i < lista.Count; i++)
            {
                Siscop f = lista[i];
                save(conn, query, criarListaParametros(f));
            }
            desconectar(conn);
        }

        private List<SqlParameter> criarListaParametros(Siscop p)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("codigo", p.Codigo));
            parametros.Add(new SqlParameter("responsavel", p.Responsavel.Codigo));
            parametros.Add(new SqlParameter("data", p.Data));
            parametros.Add(new SqlParameter("batida", p.Batida));
            return parametros;
        }
    }
}
