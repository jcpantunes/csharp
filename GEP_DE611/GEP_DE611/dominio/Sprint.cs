using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
{
    class Sprint : BaseBean
    {
        public const string CODIGO = "codigo";
        public const string NOME = "nome";
        public const string DTINICIO = "dtInicio";
        public const string DTFINAL = "dtFinal";
        public const string PROJETO = "projeto";

        public Sprint()
        {
        }

        public Sprint(int codigo, string nome, DateTime dtInicio, DateTime dtFinal, Projeto projeto)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.DtInicio = dtInicio;
            this.DtFinal = dtFinal;
            this.Projeto = projeto;
        }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private DateTime dtInicio;

        public DateTime DtInicio
        {
            get { return dtInicio; }
            set { dtInicio = value; }
        }

        private DateTime dtFinal;

        public DateTime DtFinal
        {
            get { return dtFinal; }
            set { dtFinal = value; }
        }

        private Projeto projeto;

        public Projeto Projeto
        {
            get { return projeto; }
            set { projeto = value; }
        }
        
        public List<Sprint> encapsularLista () 
        {
            List<Sprint> lista = new List<Sprint>();
            lista.Add(this);
            return lista;
        }
    }
}
