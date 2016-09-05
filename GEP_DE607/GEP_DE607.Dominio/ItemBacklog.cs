using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class ItemBacklog : ItemTrabalho
    {

        public const string VALOR_NEGOCIO = "valorNegocio";

        public const string TAMANHO = "tamanho";

        public const string COMPLEXIDADE = "complexidade";

        public const string PF = "pf";

        public ItemBacklog()
        {
        }

        public ItemBacklog(int codigo, string tipo, int id, string titulo, Funcionario responsavel, string status,
            string planejadoPara, string pai, DateTime dataModificacao, int projeto,
            int valorNegocio, int tamanho, int complexidade, decimal pf) :
            base(codigo, tipo, id, titulo, responsavel, status,
                        planejadoPara, pai, dataModificacao, projeto)
        {
            this.ValorNegocio = valorNegocio;
            this.Tamanho = tamanho;
            this.Complexidade = complexidade;
            this.Pf = pf;
        }

        public int ValorNegocio { get; set; }

        public int Tamanho { get; set; }

        public int Complexidade { get; set; }

        public decimal Pf { get; set; }

        public List<ItemBacklog> encapsularLista()
        {
            List<ItemBacklog> lista = new List<ItemBacklog>();
            lista.Add(this);
            return lista;
        }

    }
}
