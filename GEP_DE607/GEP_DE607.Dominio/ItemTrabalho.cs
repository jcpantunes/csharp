using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class ItemTrabalho : BaseBean
    {
        public const string TIPO = "tipo";
        public const string ID = "id";
        public const string TITULO = "titulo";
        public const string RESPONSAVEL = "responsavel";
        public const string STATUS = "status";
        public const string PLANEJADO_PARA = "planejadoPara";
        public const string PAI = "pai";
        public const string DTMODIFICACAO = "dataModificacao";
        public const string PROJETO = "projeto";

        public string Tipo { get; set; }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public Funcionario Responsavel { get; set; }

        public string Status { get; set; }

        public string PlanejadoPara { get; set; }

        public string Pai { get; set; }

        public DateTime DataModificacao { get; set; }

        public int Projeto { get; set; }

        public ItemTrabalho()
        {
        }

        public ItemTrabalho(int codigo, string tipo, int id, string titulo, Funcionario responsavel, string status,
            string planejadoPara, string pai, DateTime dataModificacao, int projeto)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
            this.Id = id;
            this.Titulo = titulo;
            this.Responsavel = responsavel; 
            this.Status = status;
            this.PlanejadoPara = planejadoPara;
            this.Pai = pai;
            this.DataModificacao = dataModificacao;
            this.Projeto = projeto;
        }
    }
}
