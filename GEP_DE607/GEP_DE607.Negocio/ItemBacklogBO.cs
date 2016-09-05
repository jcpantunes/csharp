using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class ItemBacklogBO
    {

        ItemBacklogDAO itemBacklogDAO = new ItemBacklogDAO();

        public void incluirLista(List<ItemBacklog> lista)
        {
            if (lista.Count > 0)
            {
                List<ItemBacklog> listaBanco = itemBacklogDAO.recuperar();

                List<ItemBacklog> listaItemBacklogInclusao = new List<ItemBacklog>();

                List<ItemBacklog> listaItemBacklogAtualizacao = new List<ItemBacklog>();

                foreach (ItemBacklog ItemBacklog in lista)
                {
                    var ItemBacklogsExistente = listaBanco.Where(t => t.Id.Equals(ItemBacklog.Id));
                    if (ItemBacklogsExistente.Count() == 0)
                    {
                        listaItemBacklogInclusao.Add(ItemBacklog);
                    }
                    else
                    {
                        ItemBacklog.Codigo = ((ItemBacklog)ItemBacklogsExistente.First()).Codigo;
                        listaItemBacklogAtualizacao.Add(ItemBacklog);
                    }
                }

                itemBacklogDAO.incluir(listaItemBacklogInclusao);

                itemBacklogDAO.atualizar(listaItemBacklogAtualizacao);

            }
        }

    }
}
