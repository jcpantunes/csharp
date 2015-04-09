﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEP_DE611.dominio
{
    class Projeto : BaseBean
    {
        public const string CODIGO = "codigo";
        public const string NOME = "nome";
        public const string ID = "id";
        public const string DTINICIO = "dtInicio";
        public const string DTFINAL = "dtFinal";

        public Projeto()
        {
        }

        public Projeto(int codigo, string nome, int id, DateTime dtInicio, DateTime dtFinal)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Id = id;
            this.DtInicio = dtInicio;
            this.DtFinal = dtFinal;
        }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime dtInicio;

        public DateTime DtInicio
        {
            get { return dtInicio; }
            set { dtInicio = value; }
        }

        private DateTime dtFinal;

        public DateTime DtFinal
        {
            get { return dtFinal; }
            set { dtFinal = value; }
        }
        
        public List<Projeto> encapsularLista () 
        {
            List<Projeto> lista = new List<Projeto>();
            lista.Add(this);
            return lista;
        }
    }
}
