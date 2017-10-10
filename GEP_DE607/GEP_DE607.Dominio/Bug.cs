using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Bug : ItemTrabalho
    {

        public const string CRIADO_POR = "criadoPor";
        public const string ENCONTRADO_PROJETO = "encontradoProjeto";
        public const string TIPO_RELATO = "tipoRelato";
        public const string RESOLUCAO = "resolucao";

        public Bug()
        {
        }

        public Bug(int codigo, string tipo, int id, string titulo, Funcionario responsavel, string status,
            string planejadoPara, string pai, DateTime dataModificacao, int projeto,
            string criadoPor, string encontradoProjeto, string tipoRelato, string resolucao) : 
                base (codigo, tipo, id, titulo, responsavel, status,
                        planejadoPara, pai, dataModificacao, projeto)
        {
            this.EncontradoProjeto = encontradoProjeto;
            this.TipoRelato = tipoRelato;
            this.Resolucao = resolucao;
        }

        public string EncontradoProjeto { get; set; }

        public string TipoRelato { get; set; }

        public string Resolucao { get; set; }

    }
}
