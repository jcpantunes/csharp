using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Util
{
    class Constantes
    {
        public const string TAREFA = "Tarefa";
        public const string FUNCIONARIO = "Funcionario";

        public static List<string> recuperarDominioTipoCarga()
        { 
            List<string> listaTipoCarga = new List<string>();
            listaTipoCarga.Add(TAREFA);
            listaTipoCarga.Add(FUNCIONARIO);
            return listaTipoCarga;
        }


    }
}
