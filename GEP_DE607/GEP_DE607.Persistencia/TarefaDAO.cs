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
    public class TarefaDAO : NhibernateDAO<Tarefa>
    {

        public TarefaDAO()
        {
        }
    }
}
