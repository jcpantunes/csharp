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
        BugDAO bugDAO = null;

        public BugBO(string tipo)
        {
            bugDAO = new BugDAO(tipo);
        }

        public void incluirLista(List<Bug> lista)
        {
            if (lista.Count > 0)
            {
                //BugDAO bugDAO = new BugDAO(lista[0].Tipo);

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
                        bug.Codigo = ((Bug)bugsExistente.First()).Codigo;
                        listaBugAtualizacao.Add(bug);
                    }
                }

                bugDAO.incluir(listaBugInclusao);

                bugDAO.atualizar(listaBugAtualizacao);

            }
        }

        public List<Bug> recuperar(Dictionary<string, string> parametros)
        {
            return bugDAO.recuperar(parametros);
        }

        public List<Bug> recuperarBugsPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add(Bug.RESPONSAVEL, responsavel.ToString());
            parametros.Add(Bug.PLANEJADO_PARA, planejadoPara);
            return bugDAO.recuperar(parametros);
        }

        public int recuperarQtdeItensPorSprintPorCriador(string planejadoPara, string criador)
        {
            return bugDAO.recuperarQtdeItensPorSprintPorCriador(planejadoPara, criador);
        }

        public List<Bug> recuperarItensPorSprintPorCriador(string planejadoPara, string criador)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add(Bug.CRIADO_POR, criador);
            parametros.Add(Bug.PLANEJADO_PARA, planejadoPara);
            return bugDAO.recuperar(parametros);
        }
    }
}
