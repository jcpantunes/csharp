using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
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

        private string lotacao;

        public string Lotacao
        {
            get { return lotacao; }
            set { lotacao = value; }
        }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        
        public List<Funcionario> encapsularLista () 
        {
            List<Funcionario> lista = new List<Funcionario>();
            lista.Add(this);
            return lista;
        }

    }
}
