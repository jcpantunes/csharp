using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class BugBO
    {

        public void incluirLista(List<Bug> lista)
        {
            if (lista.Count > 0)
            {
                BugDAO bugDAO = new BugDAO(lista[0].Tipo);

                List<Bug> listaBanco = bugDAO.recuperar();

                List<Bug> listaBugInclusao = new List<Bug>();

                List<Bug> listaBugAtualizacao = new List<Bug>();

                foreach (Bug bug in lista)
                {
                    var bugsExistente = listaBanco.Where(t => t.Id.Equals(bug.Id));
                    if (bugsExistente.Count() == 0)
                    {
                        listaBugInclusao.Add(bug);
                    }
                    else
                    {
                        listaBugAtualizacao.Add(bug);
                    }
                }

                bugDAO.incluir(listaBugInclusao);

                bugDAO.atualizar(listaBugAtualizacao);

            }
        }

    }
}
