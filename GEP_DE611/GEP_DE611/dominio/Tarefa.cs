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
        public Tarefa()
        {
        }

        public Tarefa(int codigo, string tipo, int id, Funcionario responsavel, String titulo, 
            string status, string planejadoPara, DateTime dataColeta, string pai, string estimativa, 
            string estimaticaCorrigida, string tempoGasto)
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

        private string estimativa;

        public string Estimativa
        {
            get { return estimativa; }
            set { estimativa = value; }
        }

        private string estimaticaCorrigida;

        public string EstimaticaCorrigida
        {
            get { return estimaticaCorrigida; }
            set { estimaticaCorrigida = value; }
        }

        private string tempoGasto;

        public string TempoGasto
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
