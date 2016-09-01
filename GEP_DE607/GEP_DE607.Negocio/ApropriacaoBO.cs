using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class ApropriacaoBO
    {
        ApropriacaoDAO apropDAO = new ApropriacaoDAO();

        public List<int> validarListaTarefaInexistente(List<Apropriacao> lista)
        {
            List<int> listaTarefaInexistente = new List<int>();
            if (lista.Count > 0)
            {
                TarefaDAO tarefaDAO = new TarefaDAO();
                List<int> listaID = tarefaDAO.recuperarListaID();
                foreach (Apropriacao aprop in lista)
                {
                    if (!listaTarefaInexistente.Contains(aprop.Tarefa) && listaID.Where(id => id == aprop.Tarefa).Count() == 0)
                    {
                        listaTarefaInexistente.Add(aprop.Tarefa);
                    }
                }
            }
            return listaTarefaInexistente;
        }

        public void incluirLista(List<Apropriacao> lista)
        {
            if (lista.Count > 0)
            {

            }
        }
    }
}
