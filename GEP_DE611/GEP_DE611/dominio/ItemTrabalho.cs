using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;

namespace GEP_DE611.dominio
{
    class ItemTrabalho : BaseBean
    {
        public const string TIPO = "tipo";
        public const string TITULO = "titulo";
        public const string ID = "id";
        public const string STATUS = "status";
        public const string PLANEJADO_PARA = "planejadoPara";
        public const string PAI = "pai";
        public const string DATA_COLETA = "dataColeta";

        public const string DTINICIO = "dataInicio";
        public const string DTFINAL = "dataFinal";

        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String titulo;

        public String Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string planejadoPara;

        public string PlanejadoPara
        {
            get { return planejadoPara; }
            set { planejadoPara = value; }
        }
        
        private string pai;

        public string Pai
        {
            get { return pai; }
            set { pai = value; }
        }

        private DateTime dataColeta;

        public DateTime DataColeta
        {
            get { return dataColeta; }
            set { dataColeta = value; }
        }


    }
}
