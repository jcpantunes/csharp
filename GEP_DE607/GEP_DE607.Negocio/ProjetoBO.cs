using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class ProjetoBO
    {
        private ProjetoDAO pDAO = new ProjetoDAO();

        public void salvar(Projeto p)
        {
            if (p.Codigo == 0)
            {
                pDAO.incluir(p.encapsularLista());
            }
            else
            {
                pDAO.atualizar(p.encapsularLista());
            }
        }

        public void incluirLista(List<Projeto> lista)
        {
            if (lista.Count > 0)
            {
                List<Projeto> listaBanco = pDAO.recuperar();

                List<Projeto> listaProjetoInclusao = new List<Projeto>();

                List<Projeto> listaProjetoAtualizacao = new List<Projeto>();

                foreach (Projeto projeto in lista)
                {
                    var projetosExistente = listaBanco.Where(t => t.Id.Equals(projeto.Id));
                    if (projetosExistente.Count() == 0)
                    {
                        listaProjetoInclusao.Add(projeto);
                    }
                    else
                    {
                        projeto.Codigo = ((Projeto)projetosExistente.First()).Codigo;
                        listaProjetoAtualizacao.Add(projeto);
                    }
                }

                pDAO.incluir(listaProjetoInclusao);

                pDAO.atualizar(listaProjetoAtualizacao);
            }
        }

        public List<Projeto> recuperar(Dictionary<string, string> parametros)
        {
            return pDAO.recuperar(parametros);
        }

        public void excluir(Projeto p)
        {
            pDAO.excluir(p.encapsularLista());
        }

    }
}
