using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
{
    class ItemBacklog : ItemTrabalho
    {

        public const string VALOR_NEGOCIO = "valorNegocio";
        public const string TAMANHO = "tamanho";
        public const string COMPLEXIDADE = "complexidade";
        public const string PF = "pf";
        public const string PROJETO = "codigoProjeto";

        public ItemBacklog()
        {
        }

        private int valorNegocio;

        public int ValorNegocio
        {
            get { return valorNegocio; }
            set { valorNegocio = value; }
        }

        private int tamanho;

        public int Tamanho
        {
            get { return tamanho; }
            set { tamanho = value; }
        }

        private int complexidade;

        public int Complexidade
        {
            get { return complexidade; }
            set { complexidade = value; }
        }

        private decimal pf;

        public decimal Pf
        {
            get { return pf; }
            set { pf = value; }
        }

        private int projeto;

        public int Projeto
        {
            get { return projeto; }
            set { projeto = value; }
        }

        public List<ItemBacklog> encapsularLista()
        {
            List<ItemBacklog> lista = new List<ItemBacklog>();
            lista.Add(this);
            return lista;
        }
    }
}
