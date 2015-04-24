using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio.util
{
    class StatusUtil
    {
        //TAREFA
        public const string ABERTA = "Aberta";
        public const string FINALIZADA = "Finalizada";
        public const string EM_ANDAMENTO = "Em Andamento";
        public const string PENDENTE = "Pendente";
        public const string CANCELADA = "Cancelada";

        //ITEM DE BACKLOG
        public const string NOVO = "Novo";
        public const string PRONTO = "Pronto";
        public const string VALIDADO = "Validado";
        public const string CANCELADO = "Cancelado";

        //DEFEITO
        public const string REPORTADO = "Reportado";
        public const string ACEITO = "Aceito";
        public const string ATRIBUIDO = "Atribuído";
        public const string LIBERADO = "Liberado";
        public const string EM_VERIFICACAO = "Em Verificação";
        public const string FECHADO = "Fechado";

        public static List<string> recuperarListaStatusTarefa()
        {
            List<string> lista = new List<string>();
            lista.Add(ABERTA);
            lista.Add(EM_ANDAMENTO);
            lista.Add(FINALIZADA);
            lista.Add(PENDENTE);
            lista.Add(CANCELADA);
            return lista;
        }

        public static List<string> recuperarListaStatusItemBacklog()
        {
            List<string> lista = new List<string>();
            lista.Add(NOVO);
            lista.Add(EM_ANDAMENTO);
            lista.Add(PRONTO);
            lista.Add(VALIDADO);
            lista.Add(CANCELADO);
            lista.Add(PENDENTE);
            return lista;
        }

        public static List<string> recuperarListaStatusDefeito()
        {
            List<string> lista = new List<string>();
            lista.Add(REPORTADO);
            lista.Add(ACEITO);
            lista.Add(ATRIBUIDO);
            lista.Add(LIBERADO);
            lista.Add(EM_VERIFICACAO);
            lista.Add(FECHADO);
            lista.Add(PENDENTE);
            return lista;
        }
    }
}
