using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio.Modelo
{
    public class ApropriacaoTarefa : Apropriacao
    {

        public const string TIITULO = "titulo";

        public ApropriacaoTarefa()
        { }

        public ApropriacaoTarefa(int codigo, string nome, DateTime data, decimal hora,
            int tarefa, string macroatividade, string mnemonico, int projeto, string titulo) :
            base (codigo, nome, data, hora, tarefa, macroatividade, mnemonico, projeto)
        {
            this.Titulo = titulo;
        }

        public string Titulo { get; set; }

    }
}
