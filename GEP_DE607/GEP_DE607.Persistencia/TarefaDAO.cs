using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia.Nhibernate;

namespace GEP_DE607.Persistencia
{
    public class TarefaDAO : NhibernateDAO<Tarefa>
    {

        public TarefaDAO()
        {
        }

        public List<Tarefa> Recuperar(Funcionario responsavel, DateTime dtInicio, DateTime dtFinal)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Tarefa));
                criteria.Add(Restrictions.Ge(Tarefa.DTMODIFICACAO, dtInicio));
                criteria.Add(Restrictions.Lt(Tarefa.DTMODIFICACAO, dtFinal));
                criteria.Add(Restrictions.Lt(Tarefa.RESPONSAVEL, responsavel));
                return criteria.List<Tarefa>().ToList<Tarefa>();
            }
        }
    }
}
