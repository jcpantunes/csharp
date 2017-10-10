using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class TarefaBO : BaseBO<Tarefa>
    {

        public TarefaBO()
        {

        }

        public List<Tarefa> Recuperar(Funcionario responsavel, DateTime dtInicio, DateTime dtFinal)
        {
            TarefaDAO dao = new TarefaDAO();
            return dao.Recuperar(responsavel, dtInicio, dtFinal);
        }

        public List<Tarefa> RecuperarTarefasPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add(Tarefa.PLANEJADO_PARA, planejadoPara);
            parametros.Add(Tarefa.RESPONSAVEL, responsavel.ToString());
            return this.Recuperar(parametros);
        }

        public List<Tarefa> RecuperarTarefasPorSprintPorResponsavel(List<string> listaPlanejadoPara, int responsavel)
        {
            List<Tarefa> listaTarefas = new List<Tarefa>();
            foreach(string planejadoPara in listaPlanejadoPara)
            {
                listaTarefas.AddRange(RecuperarTarefasPorSprintPorResponsavel(planejadoPara, responsavel));
            }
            return listaTarefas;
        }

        public Dictionary<string, int> RecuperarQtdeTarefasPorSprints(List<string> listaPlanejadoPara)
        {
            return new Dictionary<string, int>();
        }

        public int RecuperarQtdeItensPorSprintPorResponsavel(string planejadoPara, int responsavel)
        {
            return 1;
        }
    }
}
