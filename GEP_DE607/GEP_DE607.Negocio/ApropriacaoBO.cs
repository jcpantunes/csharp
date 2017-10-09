using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Dominio.Modelo;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class ApropriacaoBO
    {
        ApropriacaoDAO apropDAO = new ApropriacaoDAO();

        public List<int> validarListaApropriacaoInexistente(List<Apropriacao> lista)
        {
            List<int> listaApropriacaoInexistente = new List<int>();
            if (lista.Count > 0)
            {
                TarefaDAO tarefaDAO = new TarefaDAO();
                List<int> listaID = tarefaDAO.RecuperarListaID();
                foreach (Apropriacao aprop in lista)
                {
                    if (!listaApropriacaoInexistente.Contains(aprop.Tarefa) && listaID.Where(id => id == aprop.Tarefa).Count() == 0)
                    {
                        listaApropriacaoInexistente.Add(aprop.Tarefa);
                    }
                }
            }
            return listaApropriacaoInexistente;
        }

        public void incluirLista(List<Apropriacao> lista)
        {
            if (lista.Count > 0)
            {
                List<Apropriacao> listaBanco = apropDAO.recuperar();

                List<Apropriacao> listaApropriacaoInclusao = new List<Apropriacao>();

                List<Apropriacao> listaApropriacaoAtualizacao = new List<Apropriacao>();

                foreach (Apropriacao apropriacao in lista)
                {
                    var apropriacaoExistente = listaBanco.Where(t => t.Nome.Equals(apropriacao.Nome)
                                                                        && t.Data.Equals(apropriacao.Data)
                                                                        && t.Tarefa.Equals(apropriacao.Tarefa));
                    if (apropriacaoExistente.Count() == 0)
                    {
                        listaApropriacaoInclusao.Add(apropriacao);
                    }
                    else
                    {
                        apropriacao.Codigo = ((Apropriacao)apropriacaoExistente.First()).Codigo;
                        listaApropriacaoAtualizacao.Add(apropriacao);
                    }
                }

                apropDAO.incluir(listaApropriacaoInclusao);

                apropDAO.atualizar(listaApropriacaoAtualizacao);
            }
        }

        public List<ApropriacaoTarefa> recuperarApropriacaoPorResponsavel(string responsavel, DateTime dtInicio, DateTime dtFinal)
        {
            return apropDAO.recuperarApropriacaoPorResponsavel(responsavel, dtInicio, dtFinal);
        }

        public Dictionary<DateTime, decimal> recuperarApropriacaoPorResponsavelPorDia(string responsavel, DateTime dtInicio, DateTime dtFinal)
        {
            return apropDAO.recuperarApropriacaoPorResponsavelPorDia(responsavel, dtInicio, dtFinal);
        }
    }
}
