using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio.constante
{
    class DataHoraUtil
    {

        public static string formatarHora(string hora)
        {
            string hr = "";
            string min = "";
            
            int i = 0;
            while (i < hora.Length && Char.IsNumber(hora[i]) == true)
            {
                hr = hr + hora[i];
                i++;
            }

            while (i < hora.Length && Char.IsNumber(hora[i]) == false)
            {
                i++;
            }

            while (i < hora.Length && Char.IsNumber(hora[i]) == true)
            {
                min = min + hora[i];
                i++;
            }

            string horaFormatada = hr;
            if (min.Length > 0)
            {
                horaFormatada = horaFormatada + "," + (Convert.ToInt32(min) / 6);
            }
            return horaFormatada;
        }
    }
}
