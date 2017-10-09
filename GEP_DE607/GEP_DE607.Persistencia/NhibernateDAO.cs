using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia.Nhibernate;

namespace GEP_DE607.Persistencia
{
    public class NhibernateDAO<T> : INhibernateDAO<T>
    {

        public List<T> Recuperar() 
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                return (List<T>)session.CreateSQLQuery("SELECT * FROM " + typeof(T).Name).AddEntity(typeof(T)).List<T>();
            }
        }

        public T Recuperar(int id)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public List<T> Recuperar(Dictionary<string, object> parametros)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                foreach(KeyValuePair<string, object> kvp in parametros)
                {
                    criteria.Add(Restrictions.Eq(kvp.Key, kvp.Value));
                }
                return criteria.List<T>().ToList<T>();
            }
        }

        public List<int> RecuperarListaID()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                return (List<int>)session
                    .CreateSQLQuery("SELECT Codigo FROM " + typeof(T).Name)
                    //.AddEntity(typeof(T))
                    .List<int>();

                //return session
                //    .QueryOver<T>()
                //    .Select(c => c.Codigo)
                //    .List<int>().ToList();
            }
        }

        public void Incluir(T objeto)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Save(objeto);
            }
        }

        public void Incluir(List<T> listaObjetos)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                foreach (T objeto in listaObjetos)
                {
                    session.Save(objeto);
                }
            }
        }

        public void Atualizar(T objeto)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Update(objeto);
                session.Flush();
            }
        }

        public void Atualizar(List<T> listaObjetos)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                foreach (T objeto in listaObjetos)
                {
                    session.Update(objeto);
                    session.Flush();
                }
            }
        }

        public void Remover(T objeto)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                session.Delete(objeto);
                session.Flush();
            }
        }

        //public ICollection<Product> GetByCategory(string category)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        var products = session
        //            .CreateCriteria(typeof(Product))
        //            .Add(Restrictions.Eq("Category", category))
        //            .List<Product>();
        //        return products;
        //    }
        //}

 
    }
}
