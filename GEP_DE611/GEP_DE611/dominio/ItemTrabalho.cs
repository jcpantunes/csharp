using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE611.dominio;
using GEP_DE611.dominio.constante;

namespace GEP_DE611.dominio
{
    class ItemTrabalho : BaseBean
    {
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

        private Funcionario responsavel;

        public Funcionario Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
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
