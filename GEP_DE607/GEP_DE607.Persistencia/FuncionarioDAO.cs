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
using GEP_DE607.Persistencia.Nhibernate;
using GEP_DE607.Dominio;

namespace GEP_DE607.Persistencia
{
    public class FuncionarioDAO : NhibernateDAO<Funcionario>
    {

        public FuncionarioDAO()
        {

        }

        public Funcionario RecuperarPorNome(string nome)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(Funcionario))
                    .Add(Expression.Like(Funcionario.NOME, "%" + nome + "%"))
                    .List<Funcionario>().FirstOrDefault<Funcionario>();
            }
        }
    }
}
