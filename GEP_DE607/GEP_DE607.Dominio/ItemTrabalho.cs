using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class ItemTrabalho : BaseBean
    {
        public const string TIPO = "tipo";
        public const string TITULO = "titulo";
        public const string ID = "id";
        public const string STATUS = "status";
        public const string PLANEJADO_PARA = "planejadoPara";
        public const string PAI = "pai";
        public const string DTINICIAL = "dataInicial";
        public const string DTFINAL = "dataFinal";
        public const string RESPONSAVEL = "responsavel";
        public const string PROJETO = "codigoProjeto";

        public string Tipo { get; set; }

        public int Id { get; set; }

        public String Titulo { get; set; }
        
        public string Status { get; set; }

        public string PlanejadoPara { get; set; }

        public string Pai { get; set; }

        public Funcionario Responsavel { get; set; }

        public int Projeto { get; set; }

    }
}
