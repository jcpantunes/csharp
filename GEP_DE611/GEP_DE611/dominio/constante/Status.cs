using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio.constante
{
    class Status
    {
        //TAREFA
        public const string ABERTA = "Aberta";
        public const string FINALIZADA = "Finalizada";
        public const string EM_ANDAMENTO = "Em Andamento";
        public const string PENDENTE = "Pendente";

        public List<string> recuperarListaStatus()
        {
            List<string> lista = new List<string>();
            lista.Add(ABERTA);
            lista.Add(FINALIZADA);
            lista.Add(EM_ANDAMENTO);
            lista.Add(PENDENTE);
            return lista;
        }


    }
}
