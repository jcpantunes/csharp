﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class FuncionarioBO
    {
        
        FuncionarioDAO funcDAO = new FuncionarioDAO();

        public void incluirLista(List<Funcionario> lista)
        {
            if (lista.Count > 0)
            {
                List<Funcionario> listaBanco = funcDAO.recuperar();

                List<Funcionario> listaFuncionario = new List<Funcionario>();
                foreach (Funcionario funcionario in lista)
                {
                    var funcionarioExistente = listaBanco.Where(f => f.Nome.Equals(funcionario.Nome));
                    if (funcionarioExistente.Count() == 0)
                    {
                        listaFuncionario.Add(funcionario);
                    }
                }

                funcDAO.incluir(listaFuncionario);

            }
        }

    }
}
