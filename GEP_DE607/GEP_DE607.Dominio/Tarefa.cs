using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Tarefa : ItemTrabalho
    {

        public const string CLASSIFICACAO = "Classificacao";
        public const string ESTIMATIVA = "Estimativa";
        public const string TEMPO_GASTO = "Tempogasto";

        public Tarefa()
        {
        }

        public Tarefa (int codigo, string tipo, int id, string titulo, Funcionario responsavel, string status,
            string planejadoPara, string pai, DateTime dataModificacao, int projeto,
            string classificacao, decimal estimativa, decimal tempoGasto) : 
                base (codigo, tipo, id, titulo, responsavel, status,
                        planejadoPara, pai, dataModificacao, projeto)
        {
            this.Classificacao = classificacao;
            this.Estimativa = estimativa;
            this.TempoGasto = tempoGasto;
        }

        public virtual string Classificacao { get; set; }

        public virtual decimal Estimativa { get; set; }

        public virtual decimal TempoGasto { get; set; }

        public virtual List<Tarefa> encapsularLista()
        {
            List<Tarefa> lista = new List<Tarefa>();
            lista.Add(this);
            return lista;
        }
    }
}
