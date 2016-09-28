using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class SiscopBO
    {
        SiscopDAO siscopDAO = new SiscopDAO();

        public void incluirLista(List<Siscop> lista)
        {
            if (lista.Count > 0)
            {
                List<Siscop> listaBanco = siscopDAO.recuperar();

                List<Siscop> listaSiscopInclusao = new List<Siscop>();

                List<Siscop> listaSiscopAtualizacao = new List<Siscop>();

                foreach (Siscop siscop in lista)
                {
                    var siscopsExistente = listaBanco.Where(t => t.Responsavel.Nome.ToLower().Equals(siscop.Responsavel.Nome.ToLower())
                                                                    && t.Data.Equals(siscop.Data));

                    if (siscopsExistente.Count() == 0)
                    {
                        listaSiscopInclusao.Add(siscop);
                    }
                    else
                    {
                        siscop.Codigo = ((Siscop)siscopsExistente.First()).Codigo;
                        listaSiscopAtualizacao.Add(siscop);
                    }
                }

                siscopDAO.incluir(listaSiscopInclusao);

                siscopDAO.atualizar(listaSiscopAtualizacao);

            }
        }

        public List<Siscop> recuperarSiscopPorResponsavel(int codigoResponsavel, DateTime dtInicio, DateTime dtFinal)
        {
            return siscopDAO.recuperarSiscopPorResponsavel(codigoResponsavel, dtInicio, dtFinal);
        }
    }
}
