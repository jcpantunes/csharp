using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE611.dominio.constante;

namespace GEP_DE611.dominio
{
    class Tarefa : ItemTrabalho
    {
        public const string CODIGO = "codigo";
        public const string TIPO = "tipo";
        public const string ID = "id";
        public const string RESPONSAVEL = "responsavel";
        public const string TITULO = "titulo";
        public const string STATUS = "status";
        public const string PLANEJADO_PARA = "planejadoPara";
        public const string DATA_COLETA = "dataColeta";
        public const string DTINICIO = "dataInicio";
        public const string DTFINAL = "dataFinal";
        public const string PAI = "pai";

        public Tarefa()
        {
        }

        public Tarefa(int codigo, string tipo, int id, Funcionario responsavel, String titulo,
            string status, string planejadoPara, DateTime dataColeta, string pai, decimal estimativa,
            decimal estimaticaCorrigida, decimal tempoGasto)
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
            this.EstimaticaCorrigida = estimaticaCorrigida;
            this.TempoGasto = tempoGasto;
        }

        private decimal estimativa;

        public decimal Estimativa
        {
            get { return estimativa; }
            set { estimativa = value; }
        }

        private decimal estimaticaCorrigida;

        public decimal EstimaticaCorrigida
        {
            get { return estimaticaCorrigida; }
            set { estimaticaCorrigida = value; }
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
