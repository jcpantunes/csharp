using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Apropriacao : BaseBean
    {
        //Nome	Data	Horas	Tarefa	Macroatividade	Projeto

        public const string NOME = "nome";
        public const string DATA = "data";
        public const string HORA = "hora";
        public const string TAREFA = "tarefa";
        // public const string MACROATIVIDADE = "macroatividade";
        // public const string PROJETO = "projeto";

        public Apropriacao()
        {
        }

        public Apropriacao(int codigo, string nome, DateTime data, decimal hora, int tarefa)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Data = data;
            this.Hora = hora;
            this.Tarefa = tarefa;
        }

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public decimal Hora { get; set; }

        public int Tarefa { get; set; }

        public List<Apropriacao> encapsularLista()
        {
            List<Apropriacao> lista = new List<Apropriacao>();
            lista.Add(this);
            return lista;
        }

    }
}
