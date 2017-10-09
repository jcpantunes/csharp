using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class ItemTrabalho : BaseBean
    {
        public const string TIPO = "Tipo";
        public const string ID = "Id";
        public const string TITULO = "Titulo";
        public const string RESPONSAVEL = "Responsavel";
        public const string STATUS = "Status";
        public const string PLANEJADO_PARA = "PlanejadoPara";
        public const string PAI = "Pai";
        public const string DTMODIFICACAO = "DataModificacao";
        public const string PROJETO = "Projeto";

        public virtual string Tipo { get; set; }

        public virtual int Id { get; set; }

        public virtual string Titulo { get; set; }

        public virtual Funcionario Responsavel { get; set; }

        public virtual string Status { get; set; }

        public virtual string PlanejadoPara { get; set; }

        public virtual string Pai { get; set; }

        public virtual DateTime DataModificacao { get; set; }

        public virtual int Projeto { get; set; }

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
