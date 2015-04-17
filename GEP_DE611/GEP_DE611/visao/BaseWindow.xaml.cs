using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;
using GEP_DE611.persistencia;
using GEP_DE611.componente;

namespace GEP_DE611.visao
{
    public class BaseWindow
    {
        public BaseWindow()
        {

        }

        public void preencherComboProjeto(ComboBox cmb, bool todos)
        {
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
            if (lista.Count > 0)
            {
                if (todos)
                {
                    cmb.Items.Add(preencherComboItem(0, "Todos"));
                }

                foreach (Projeto p in lista)
                {
                    cmb.Items.Add(preencherComboItem(p.Codigo, p.Nome));
                }
                cmb.SelectedIndex = 0;
            }
        }

        private ComboBoxItem preencherComboItem(int codigo, string nome)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Tag = codigo; 
            item.Content = nome;
            return item;
        }

        private void preencherComboSprint(int codigoProjeto, ComboBox cmb, bool todos)
        {
            cmb.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                if (todos)
                {
                    cmb.Items.Add(preencherComboItem(0, "Todos"));
                }

                foreach (Sprint p in lista)
                {
                    cmb.Items.Add(preencherComboItem(p.Codigo, p.Nome));
                }
                cmb.SelectedIndex = 0;
            }
        }

        public void preencherComboStatus(ComboBox cmb, bool todos)
        {
            List<string> lista = StatusUtil.recuperarListaStatus();
            if (lista.Count > 0)
            {
                if (todos)
                {
                    cmb.Items.Add(preencherComboItem(0, "Todos"));
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    cmb.Items.Add(preencherComboItem(i+1, lista[i]));
                }
                cmb.SelectedIndex = 0;
            }
        }

        public Projeto recuperarProjetoInCache(List<Projeto> listaProjeto, int idProjeto, int codProjeto, string nomeProjeto)
        {
            foreach (Projeto p in listaProjeto)
            {
                if (p.Id.Equals(idProjeto))
                {
                    return p;
                }
            }
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar(Projeto.criarListaParametrosId(idProjeto));
            if (lista.Count > 0)
            {
                listaProjeto.Add(lista[0]);
                return lista[0];
            }
            else
            {
                return new Projeto(codProjeto, nomeProjeto, idProjeto, DateTime.Now, DateTime.Now);
            }
        }

        private Funcionario recuperarFuncionarioInCache(List<Funcionario> listaFuncionario, string responsavel)
        {
            foreach (Funcionario func in listaFuncionario)
            {
                if (func.Nome.Equals(responsavel))
                {
                    return func;
                }
            }
            FuncionarioDAO fDAO = new FuncionarioDAO();
            Funcionario f = fDAO.recuperar(responsavel);
            if (f == null)
            {
                f = new Funcionario(0, "DEBHE/DE611", responsavel);
                fDAO.incluir(f.encapsularLista());
                f = fDAO.recuperar(responsavel);
            }
            listaFuncionario.Add(f);
            return f;
        }

        

        
    }
}
