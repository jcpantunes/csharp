using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Persistencia
{
    public interface INhibernateDAO<T>
    {
        List<T> Recuperar();

        T Recuperar(int id);

        List<T> Recuperar(Dictionary<string, object> parametros);

        List<int> RecuperarListaID();

        void Incluir(T objeto);

        void Incluir(List<T> listaObjetos);

        void Atualizar(T objeto);

        void Atualizar(List<T> listaObjetos);

        void Remover(T objeto);

    }
}