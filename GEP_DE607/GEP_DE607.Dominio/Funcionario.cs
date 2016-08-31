using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Funcionario : BaseBean
    {
        public const string LOTACAO = "lotacao";
        public const string NOME = "nome";

        public Funcionario()
        {
        }

        public Funcionario(int codigo, string lotacao, string nome)
        {
            this.Codigo = codigo;
            this.Lotacao = lotacao;
            this.Nome = nome;
        }

        public string Lotacao { get; set; }

        public string Nome { get; set; }

        public List<Funcionario> encapsularLista()
        {
            List<Funcionario> lista = new List<Funcionario>();
            lista.Add(this);
            return lista;
        }

    }
}
