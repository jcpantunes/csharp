using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class BaseBO<T> : IBaseBO<T>
    {

        public List<T> Recuperar()
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            return dao.Recuperar();
        }

        public T Recuperar(int id)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            return dao.Recuperar(id);
        }

        public virtual List<T> Recuperar(Dictionary<string, object> parametros)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            return dao.Recuperar(parametros);
        }

        public List<int> RecuperarListaID()
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            return dao.RecuperarListaID();
        }

        public void Incluir(T objeto)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            dao.Incluir(objeto);
        }

        public void Incluir(List<T> listaObjetos)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            dao.Incluir(listaObjetos);
        }

        public void Atualizar(T objeto)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            dao.Atualizar(objeto);
        }

        public void Atualizar(List<T> listaObjetos)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            dao.Atualizar(listaObjetos);
        }

        public void Remover(T objeto)
        {
            INhibernateDAO<T> dao = new NhibernateDAO<T>();
            dao.Remover(objeto);
        }

    }
}