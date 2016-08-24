using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    class Tarefa : ItemTrabalho
    {

        public Tarefa()
        {
        }

        public Tarefa(int codigo, string tipo, int id, Funcionario responsavel, String titulo,
            string status, string macroatividade, string planejadoPara, string pai, decimal estimativa, decimal tempoGasto)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
            this.Id = id;
            this.Responsavel = responsavel;
            this.Titulo = titulo;
            this.Status = status;
            this.PlanejadoPara = planejadoPara;
            this.Pai = pai;
            this.Macroatividade = macroatividade;
            this.Estimativa = estimativa;
            this.TempoGasto = tempoGasto;
        }

        public String Macroatividade { get; set; }

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
