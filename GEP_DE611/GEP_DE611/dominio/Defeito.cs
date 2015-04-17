using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
{
    class Defeito : ItemTrabalho
    {
        public const string ENCONTRADO_PROJETO = "encontradoProjeto";

        public const string TIPO_RELATO = "tipoRelato";

        public const string RESOLUCAO = "resolucao";

        public Defeito()
        {
        }

        public string EncontradoProjeto { get; set;}

        public string TipoRelato {get; set;}

        public string Resolucao {get; set;}

        public List<Defeito> encapsularLista()
        {
            List<Defeito> lista = new List<Defeito>();
            lista.Add(this);
            return lista;
        }
    }
}
