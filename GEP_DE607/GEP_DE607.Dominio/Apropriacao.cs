using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Apropriacao : BaseBean
    {

        public const string NOME = "nome";
        public const string DATA = "data";
        public const string HORA = "hora";
        public const string TAREFA = "tarefa";
        public const string MACROATIVIDADE = "macroatividade";
        public const string MNEMONICO = "mnemonico";
        public const string PROJETO = "projeto";

        public Apropriacao()
        {
        }

        public Apropriacao(int codigo, string nome, DateTime data, decimal hora, 
            int tarefa, string macroatividade, string mnemonico, int projeto)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Data = data;
            this.Hora = hora;
            this.Tarefa = tarefa;
            this.Macroatividade = macroatividade;
            this.Mnemonico = mnemonico;
            this.Projeto = projeto;
        }

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public decimal Hora { get; set; }

        public int Tarefa { get; set; }

        public string Macroatividade { get; set; }

        public string Mnemonico { get; set; }

        public int Projeto { get; set; }

        public List<Apropriacao> encapsularLista()
        {
            List<Apropriacao> lista = new List<Apropriacao>();
            lista.Add(this);
            return lista;
        }

    }
}
