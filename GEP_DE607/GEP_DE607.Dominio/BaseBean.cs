using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class BaseBean
    {
        public const string CODIGO = "codigo";
        public const string FILTRO_DTINICIO = "filtroDtInicio";
        public const string FILTRO_DTFINAL = "filtroDtFinal";

        public int Codigo { get; set; }

        public DateTime FiltroDtInicio { get; set; }

        public DateTime FiltroDtFinal { get; set; }

    }
}
