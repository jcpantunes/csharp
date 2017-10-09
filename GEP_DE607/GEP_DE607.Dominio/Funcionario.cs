using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Funcionario : BaseBean
    {
        public const string LOTACAO = "Lotacao";
        public const string NOME = "Nome";

        public Funcionario()
        {
        }

        public Funcionario(int codigo, string lotacao, string nome)
        {
            this.Codigo = codigo;
            this.Lotacao = lotacao;
            this.Nome = nome;
        }

        public virtual string Lotacao { get; set; }

        public virtual string Nome { get; set; }

        public virtual List<Funcionario> encapsularLista()
        {
            List<Funcionario> lista = new List<Funcionario>();
            lista.Add(this);
            return lista;
        }

    }
}
