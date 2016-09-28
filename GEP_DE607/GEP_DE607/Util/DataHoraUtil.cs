using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Util
{
    public class DataHoraUtil
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


        public static TimeSpan recuperarHora(string horario, string[] linhas, string nome, int opcao)
        {
            string[] linha = { "", "08:00", "12:00", "13:00", "17:00" };
            string[] hr = linha[opcao].Split(':');
            int hora = Convert.ToInt32(hr[0]);
            int min = Convert.ToInt32(hr[1]);
            int codigo = 0;

            if (horario.Length == 5 || horario.Length == 7)
            {
                hr = horario.Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 7 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            else if (horario.StartsWith("#") && (horario.Length == 6 || horario.Length == 8))
            {
                hr = horario.Replace("#", "").Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 8 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            if (codigo == 21 || codigo == 99)
            {
                if (linhas.Length > 1)
                {
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        linha = linhas[i].Replace("\"", "").Split('\t');
                        if (linha[0].ToUpper().Equals(nome.ToUpper()))
                        {
                            hr = linha[opcao].Split(':');
                            hora = Convert.ToInt32(hr[0]);
                            min = Convert.ToInt32(hr[1]);
                        }
                    }
                }
            }
            return new TimeSpan(hora, min, 0);
        }

        public static int recuperarDiaFinalMes(int ano, int mes)
        {
            int dia = 30;
            if (mes == 2)
            {
                dia = ano % 4 == 0 ? 28 : 29;
            }
            else if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)
            {
                dia = 31;
            }
            return dia;
        }

        public static TimeSpan recuperarHorarioFormatado(string horario, ref int codigo)
        {
            int hora = 0;
            int min = 0;

            if (horario.Length == 5 || horario.Length == 7)
            {
                string[] hr = horario.Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 7 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            else if (horario.StartsWith("#") && (horario.Length == 6 || horario.Length == 8))
            {
                string[] hr = horario.Replace("#", "").Split(':');
                hora = Convert.ToInt32(hr[0]);
                min = Convert.ToInt32(hr[1].Substring(0, 2));
                codigo = horario.Length == 8 ? Convert.ToInt32(hr[1].Substring(2, 2)) : 0;
            }
            return new TimeSpan(hora, min, 0);
        }

        public static string calcularTotalDia(string horario1, string horario2)
        {
            if (horario1.Length == 0 && horario2.Length == 0)
            {
                return "";
            }
            else if (horario1.Length == 0 || horario2.Length == 0)
            {
                return horario1.Length == 0 ? horario2 : horario1;
            }
            else
            {
                int hora1 = Convert.ToInt32(horario1.Split(':')[0]);
                int min1 = Convert.ToInt32(horario1.Split(':')[1]);
                int hora2 = Convert.ToInt32(horario2.Split(':')[0]);
                int min2 = Convert.ToInt32(horario2.Split(':')[1]);
                return (new TimeSpan(hora1, min1, 0)).Add(new TimeSpan(hora2, min2, 0)).ToString().Substring(0, 5);
            }
        }
    }
}
