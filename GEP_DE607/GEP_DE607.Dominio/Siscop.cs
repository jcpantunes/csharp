using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Siscop : BaseBean
    {

        public const string RESPONSAVEL = "responsavel";
        public const string DATA = "data";
        public const string BATIDA = "batida";

        public Siscop() {}

        public Funcionario Responsavel;

        public DateTime Data;

        public string Batida { get; set; }

        public string Entrada1 { get; set; }
        public string Saida1 { get; set; }
        public string Entrada2 { get; set; }
        public string Saida2 { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
        public string Extra4 { get; set; }

        public List<Siscop> encapsularLista()
        {
            List<Siscop> lista = new List<Siscop>();
            lista.Add(this);
            return lista;
        }

    }
}
