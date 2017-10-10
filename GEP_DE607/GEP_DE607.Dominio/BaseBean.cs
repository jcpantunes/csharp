using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class BaseBean
    {
        public const string CODIGO = "Codigo";
        public const string FILTRO_DTINICIO = "FiltroDtInicio";
        public const string FILTRO_DTFINAL = "FiltroDtFinal";

        public virtual int Codigo { get; set; }

        public virtual DateTime FiltroDtInicio { get; set; }

        public virtual DateTime FiltroDtFinal { get; set; }

        public static Dictionary<string, object> GerarParametros(string key, string value)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add(key, value);
            return param;
        }

    }

}
