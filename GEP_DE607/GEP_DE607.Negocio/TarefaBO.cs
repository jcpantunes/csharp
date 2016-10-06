using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class TarefaBO
    {

        TarefaDAO tarefaDAO = new TarefaDAO();

        public void incluirLista(List<Tarefa> lista)
        {
            if (lista.Count > 0)
            {
                List<Tarefa> listaBanco = tarefaDAO.recuperar();

                List<Tarefa> listaTarefaInclusao = new List<Tarefa>();

                List<Tarefa> listaTarefaAtualizacao = new List<Tarefa>();

                foreach (Tarefa tarefa in lista)
                {
                    var tarefasExistente = listaBanco.Where(t => t.Id.Equals(tarefa.Id));
                    if (tarefasExistente.Count() == 0)
                    {
                        listaTarefaInclusao.Add(tarefa);
                    }
                    else
                    {
                        tarefa.Codigo = ((Tarefa)tarefasExistente.First()).Codigo;
                        listaTarefaAtualizacao.Add(tarefa);
                    }
                }

                tarefaDAO.incluir(listaTarefaInclusao);
                
                tarefaDAO.atualizar(listaTarefaAtualizacao);

            }
        }

        public List<Tarefa> recuperarTarefasPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            List<string> listaPlanejadoPara = new List<string>();
            listaPlanejadoPara.Add(planejadoPara);
            return tarefaDAO.recuperarTarefasPorSprintPorResponsavel(listaPlanejadoPara, responsavel);
        }

        public List<Tarefa> recuperarTarefasPorSprintPorResponsavel(List<string> listaPlanejadoPara, int responsavel)
        {
            return tarefaDAO.recuperarTarefasPorSprintPorResponsavel(listaPlanejadoPara, responsavel);
        }

        public Dictionary<string, int> recuperarQtdeTarefasPorSprints(List<string> listaPlanejadoPara)
        {
            return tarefaDAO.recuperarQtdeTarefasPorSprints(listaPlanejadoPara);
        }
    }
}
