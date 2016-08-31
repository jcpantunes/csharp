using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class SolicitacaoBO
    {

        private SolicitacaoDAO solicpDAO = new SolicitacaoDAO();

        public void salvar(Solicitacao solicitacao)
        {
            if (solicitacao.Codigo == 0)
            {
                solicpDAO.incluir(solicitacao.encapsularLista());
            }
            else
            {
                solicpDAO.atualizar(solicitacao.encapsularLista());
            }
        }

        public List<Solicitacao> recuperar(Dictionary<string, string> parametros)
        {
            return solicpDAO.recuperar(parametros);
        }

        public void excluir(Solicitacao p)
        {
            solicpDAO.excluir(p.encapsularLista());
        }

    }
}
