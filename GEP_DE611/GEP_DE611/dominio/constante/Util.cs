using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio.constante
{
    class Util
    {

        public static bool verificarStringNumero(string s)
        {
            char[] vetor = s.ToCharArray();
            foreach (char c in vetor)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
