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
        public BaseWindow() {}

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
                // cmb.SelectedIndex = 0;
            }
        }

        private ComboBoxItem preencherComboItem(int codigo, string nome)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Tag = codigo; 
            item.Content = nome;
            return item;
        }

        public void preencherComboSprint(int codigoProjeto, ComboBox cmb, bool todos)
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

        public void preencherComboStatus(ComboBox cmb, List<string> listaStatus, bool todos)
        {
            if (listaStatus.Count > 0)
            {
                if (todos)
                {
                    cmb.Items.Add(preencherComboItem(0, "Todos"));
                }

                for (int i = 0; i < listaStatus.Count; i++)
                {
                    cmb.Items.Add(preencherComboItem(i + 1, listaStatus[i]));
                }
                cmb.SelectedIndex = 0;
            }
        }

        public void preencherComboLotacao(ComboBox cmb, ComboBox cmbFuncionario)
        {
            List<string> lista = Util.retornarListaLotacao();
            if (lista.Count > 0)
            {
                foreach (string lotacao in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = lotacao;
                    cmb.Items.Add(item);
                }
                cmb.SelectedIndex = 0;
                preencherComboFuncionario(cmbFuncionario, lista[0], true);
            }
        }

        public void preencherListBoxSprint(ListBox lst, int codigoProjeto)
        {
            lst.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    lst.Items.Add(item);
                }
            }
        }

        public void preencherComboFuncionario(ComboBox cmb, string lotacao, bool todos)
        {
            cmb.Items.Clear();

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Funcionario.LOTACAO, lotacao);

            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> lista = fDAO.recuperar(param);

            if (lista.Count > 0)
            {
                if (todos)
                {
                    cmb.Items.Add(preencherComboItem(0, "Todos"));
                }

                foreach (Funcionario f in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = f.Nome;
                    item.Tag = f.Codigo;
                    cmb.Items.Add(item);
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
            return new Projeto(codProjeto, nomeProjeto, idProjeto, DateTime.Now, DateTime.Now);
        }

        public Funcionario recuperarFuncionarioInCache(List<Funcionario> listaFuncionario, string responsavel)
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
