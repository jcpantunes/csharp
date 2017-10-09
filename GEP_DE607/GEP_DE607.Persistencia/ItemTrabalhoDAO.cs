using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using GEP_DE607.Dominio;

namespace GEP_DE607.Persistencia
{
    public abstract class ItemTrabalhoDAO<T> : NhibernateDAO<T>
    {

        protected void RetornarCriteriaExpression(ICriteria criteria, string key, Dictionary<string, string> parametros)
        {
            if (key.Equals(ItemTrabalho.CODIGO))
            {
                criteria.Add(Expression.Eq(Tarefa.CODIGO, Convert.ToInt32(parametros[key])));
            }
            else if (key.Equals(ItemTrabalho.TIPO))
            {
                criteria.Add(Expression.Like(Tarefa.TIPO, "%" + parametros[key] + "%"));
            }
            else if (key.Equals(ItemTrabalho.ID))
            {
                criteria.Add(Expression.Eq(Tarefa.ID, Convert.ToInt32(parametros[key])));
            }
            else if (key.Equals(ItemTrabalho.TITULO))
            {
                criteria.Add(Expression.Like(Tarefa.TITULO, "%" + parametros[key] + "%"));
            }
            else if (key.Equals(ItemTrabalho.RESPONSAVEL))
            {
                criteria.Add(Expression.Eq(Tarefa.RESPONSAVEL, Convert.ToInt32(parametros[key])));
            }
            else if (key.Equals(ItemTrabalho.STATUS))
            {
                criteria.Add(Expression.Eq(Tarefa.STATUS, parametros[key]));
            }
            else if (key.Equals(ItemTrabalho.PLANEJADO_PARA))
            {
                criteria.Add(Expression.Eq(Tarefa.PLANEJADO_PARA, parametros[key]));
            }
            else if (key.Equals(ItemTrabalho.PAI))
            {
                criteria.Add(Expression.Eq(Tarefa.PAI, parametros[key]));
            }
            else if (key.Equals(ItemTrabalho.DTMODIFICACAO))
            {
                criteria.Add(Expression.Eq(Tarefa.DTMODIFICACAO, Convert.ToDateTime(parametros[key])));
            }
            else if (key.Equals(ItemTrabalho.PROJETO))
            {
                criteria.Add(Expression.Eq(Tarefa.PROJETO, Convert.ToInt32(parametros[key])));
            }
        }
    }
}
