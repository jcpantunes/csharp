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

    class TarefaDAO : TarefaBaseDAO
    {
        private string TABELA = "Tarefa";

        public TarefaDAO()
        {
            this.Tabela = TABELA;
        }

        public int recuperarQtdeTarefasPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            string query = "SELECT Count(id) FROM " + Tabela
                + " WHERE planejadoPara = '" + planejadoPara + "' and responsavel = " + responsavel;

            int quantidade = 0;

            SqlConnection conn = null;
            SqlDataReader reader = select(conn, query);
            if (reader != null)
            {
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    quantidade = reader.GetInt32(0);
                }
            }
            desconectar(conn);

            return quantidade;
        }
    }
}
