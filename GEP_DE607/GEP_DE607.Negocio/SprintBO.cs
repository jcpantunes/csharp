using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class SprintBO
    {

        SprintDAO sprintDAO = new SprintDAO();

        public void incluirLista(List<Sprint> lista)
        {
            if (lista.Count > 0)
            {
                List<Sprint> listaBanco = sprintDAO.recuperar();

                List<Sprint> listaSprintInclusao = new List<Sprint>();

                List<Sprint> listaSprintAtualizacao = new List<Sprint>();

                foreach (Sprint Sprint in lista)
                {
                    var SprintsExistente = listaBanco.Where(t => t.Nome.Equals(Sprint.Nome));
                    if (SprintsExistente.Count() == 0)
                    {
                        listaSprintInclusao.Add(Sprint);
                    }
                    else
                    {
                        Sprint.Codigo = ((Sprint)SprintsExistente.First()).Codigo;
                        listaSprintAtualizacao.Add(Sprint);
                    }
                }

                sprintDAO.incluir(listaSprintInclusao);

                sprintDAO.atualizar(listaSprintAtualizacao);

            }
        }

    }
}
