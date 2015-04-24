using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
{
    public class BaseBean
    {
        public const string CODIGO = "codigo";

        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

       
    }
}
