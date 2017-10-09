using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607.Negocio
{
    public class FuncionarioBO : BaseBO<Funcionario>
    {

        public FuncionarioBO()
        {

        }

        public List<string> RecuperarNomes()
        {
            List<string> retorno = new List<string>();
            List<Funcionario> listaFuncionarios = this.Recuperar();
            foreach (Funcionario f in listaFuncionarios)
            {
                retorno.Add(f.Nome);
            }
            return retorno;
        }

        public Funcionario RecuperarPorNome(string nome)
        {
            FuncionarioDAO dao = new FuncionarioDAO();
            return dao.RecuperarPorNome(nome);
        }
    }
}
