using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Util
{
    class DataHoraUtil
    {
        public static decimal formatarHora(string hora)
        {
            string hr = "";
            string min = "";

            if (hora.Length == 0)
            {
                return 0;
            }
            else
            {
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

                decimal horaFormatada = Convert.ToDecimal(hr);
                if (min.Length > 0)
                {
                    horaFormatada = Convert.ToDecimal(horaFormatada + "," + (Convert.ToInt32(min) / 6));
                }
                return horaFormatada;
            }
        }
    }
}
