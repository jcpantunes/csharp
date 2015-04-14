using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.persistencia
{
    class TarefaHistoricoDAO : TarefaBaseDAO
    {
        private string TABELA = "TarefaHistorico";

        public TarefaHistoricoDAO()
        {
            this.Tabela = TABELA;
        }
    }
}
