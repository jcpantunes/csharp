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

        public static List<string> retornarListaLotacao()
        {
            List<string> lista = new List<string>();
            lista.Add("DEBHE/DE601");
            lista.Add("DEBHE/DE602");
            lista.Add("DEBHE/DE603");
            lista.Add("DEBHE/DE604");
            lista.Add("DEBHE/DE605");
            lista.Add("DEBHE/DE606");
            lista.Add("DEBHE/DE607");
            lista.Add("DEBHE/DE608");
            lista.Add("DEBHE/DE609");
            lista.Add("DEBHE/DE610");
            lista.Add("DEBHE/DE611");
            lista.Add("DEBHE/DE612");
            lista.Add("DEBHE/DE613");
            lista.Add("DEBHE/DE614");
            lista.Add("DEBHE/DE615");
            lista.Add("DEBHE/DE616");
            lista.Add("DEBHE/DE617");
            lista.Add("DEBHE/DE6ED");
            lista.Add("DEBHE/DE6CT");

            return lista;
        }
    }
}
