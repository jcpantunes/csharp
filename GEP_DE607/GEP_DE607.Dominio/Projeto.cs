using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Projeto : BaseBean
    {
        public const string SS = "ss";
        public const string TIPO = "tipo";
        public const string ID = "id";
        public const string NOME = "nome";
        public const string SISTEMA = "sistema";
        public const string LINGUAGEM = "linguagem";
        public const string PROCESSO = "processo";
        public const string TIPO_PROJETO = "tipoProjeto";
        public const string SITUACAO = "situacao";
        public const string CONCLUSIVIDADE = "conclusividade";
        public const string PF_PREV = "pfprev";
        public const string PF_REAL = "pfreal";
        public const string APROPRIACAO = "apropriacao";
        public const string DTINICIO = "dtInicio";
        public const string DTENTREGA = "dtEntrega";
        public const string DTFINAL = "dtFinal";

        public Projeto()
        {
        }

        public Projeto(int codigo, int ss, string tipo, int id, string nome, string sistema, string linguagem, string processo, string tipoProjeto, 
            string situacao, int conclusividade, decimal pfprev, decimal pfreal, decimal apropriacao, DateTime dtInicio, DateTime dtEntrega, DateTime dtFinal)
        {
            this.Ss = ss;
            this.Codigo = codigo;
            this.Tipo = tipo;
            this.Id = id;
            this.Nome = nome;
            this.Sistema = sistema;
            this.Linguagem = linguagem;
            this.Processo = processo;
            this.TipoProjeto = tipoProjeto;
            this.Situacao = situacao;
            this.Conclusividade = conclusividade;
            this.Pfprev = pfprev;
            this.Pfreal = pfreal;
            this.Apropriacao = apropriacao;
            this.DtInicio = dtInicio;
            this.DtEntrega = dtEntrega;
            this.DtFinal = dtFinal;
        }

        public int Ss { get; set; }

        public string Tipo { get; set; }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sistema { get; set; }

        public string Linguagem { get; set; }

        public string Processo { get; set; }

        public string TipoProjeto { get; set; }

        public string Situacao { get; set; }

        public int Conclusividade { get; set; }

        public decimal Pfprev { get; set; }

        public decimal Pfreal { get; set; }

        public decimal Apropriacao { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtEntrega { get; set; }

        public DateTime DtFinal { get; set; }

        public List<Projeto> encapsularLista()
        {
            List<Projeto> lista = new List<Projeto>();
            lista.Add(this);
            return lista;
        }

        public static Dictionary<string, string> criarListaParametros(int codigo)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Projeto.CODIGO, Convert.ToString(codigo));
            return param;
        }

    }
}
