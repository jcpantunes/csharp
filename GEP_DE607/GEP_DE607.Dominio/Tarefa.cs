using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Tarefa : ItemTrabalho
    {

        public const string CLASSIFICACAO = "classificacao";
        public const string ESTIMATIVA = "estimativa";
        public const string TEMPO_GASTO = "tempogasto";

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

        public string Classificacao { get; set; }

        public decimal Estimativa { get; set; }

        public decimal TempoGasto { get; set; }

        public List<Tarefa> encapsularLista()
        {
            List<Tarefa> lista = new List<Tarefa>();
            lista.Add(this);
            return lista;
        }
    }
}
