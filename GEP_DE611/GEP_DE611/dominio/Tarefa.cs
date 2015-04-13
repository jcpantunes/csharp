using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE611.dominio.util;

namespace GEP_DE611.dominio
{
    class Tarefa : ItemTrabalho
    {
        public const string RESPONSAVEL = "responsavel";
        
        public Tarefa()
        {
        }

        public Tarefa(int codigo, string tipo, int id, Funcionario responsavel, String titulo,
            string status, string planejadoPara, DateTime dataColeta, string pai, decimal estimativa,
            decimal estimativaCorrigida, decimal tempoGasto)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
            this.Id = id;
            this.Responsavel = responsavel;
            this.Titulo = titulo;
            this.Status = status;
            this.PlanejadoPara = planejadoPara;
            this.DataColeta = dataColeta;
            this.Pai = pai;
            this.Estimativa = estimativa;
            this.EstimativaCorrigida = estimativaCorrigida;
            this.TempoGasto = tempoGasto;
        }

        private Funcionario responsavel;

        public Funcionario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        private decimal estimativa;

        public decimal Estimativa
        {
            get { return estimativa; }
            set { estimativa = value; }
        }

        private decimal estimativaCorrigida;

        public decimal EstimativaCorrigida
        {
            get { return estimativaCorrigida; }
            set { estimativaCorrigida = value; }
        }

        private decimal tempoGasto;

        public decimal TempoGasto
        {
            get { return tempoGasto; }
            set { tempoGasto = value; }
        }

        public List<Tarefa> encapsularLista()
        {
            List<Tarefa> lista = new List<Tarefa>();
            lista.Add(this);
            return lista;
        }
    }
}
