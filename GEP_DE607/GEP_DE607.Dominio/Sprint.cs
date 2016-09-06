using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Sprint : BaseBean
    {
        public const string NOME = "nome";
        public const string DTINICIO = "dtInicio";
        public const string DTFINAL = "dtFinal";
        public const string PROJETO = "projeto";

        public Sprint()
        {
        }

        public Sprint(int codigo, string nome, DateTime dtInicio, DateTime dtFinal, int projeto)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.DtInicio = dtInicio;
            this.DtFinal = dtFinal;
            this.Projeto = projeto;
        }

        public string Nome;

        public DateTime DtInicio;

        public DateTime DtFinal;

        public int Projeto;

        public List<Sprint> encapsularLista()
        {
            List<Sprint> lista = new List<Sprint>();
            lista.Add(this);
            return lista;
        }

        public static Dictionary<string, string> criarListaParametros(int codigo)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Sprint.CODIGO, Convert.ToString(codigo));
            return param;
        }

        public static Dictionary<string, string> criarListaParametrosPesquisaPorProjeto(int projeto)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Sprint.PROJETO, Convert.ToString(projeto));
            return param;
        }
    }
}
