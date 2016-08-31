using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE607.Dominio
{
    public class Solicitacao : BaseBean
    {
        public const string ID = "id";
        public const string DEMANDA = "demanda";
        public const string SISTEMA = "sistema";
        public const string TIPO_DEMANDA = "tipoDemanda";
        public const string ASSUNTO = "assunto";
        public const string DESTINATARIO = "destinatario";
        public const string STATUS = "status";
        public const string DTINICIO = "dtInicio";
        public const string DTENTREGA = "dtEntrega";
        public const string DTFINAL = "dtFinal";

        public Solicitacao()
        {
        }

        public Solicitacao(int codigo, int id, string demanda, string sistema, string tipoDemanda,
            string assunto, string destinatario, string status, DateTime dtInicio, DateTime dtEntrega, DateTime dtFinal)
        {
            this.Codigo = codigo;
            this.Id = id;
            this.Demanda = demanda;
            this.Sistema = sistema;
            this.TipoDemanda = tipoDemanda;
            this.Assunto = assunto;
            this.Destinatario = destinatario;
            this.Status = status;
            this.DtInicio = dtInicio;
            this.DtEntrega = dtEntrega;
            this.DtFinal = dtFinal;
        }

        public int Id { get; set; }

        public string Demanda { get; set; }

        public string Sistema { get; set; }

        public string TipoDemanda { get; set; }

        public string Assunto { get; set; }

        public string Destinatario { get; set; }

        public string Status { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtEntrega { get; set; }

        public DateTime DtFinal { get; set; }

        public List<Solicitacao> encapsularLista()
        {
            List<Solicitacao> lista = new List<Solicitacao>();
            lista.Add(this);
            return lista;
        }
    }
}
