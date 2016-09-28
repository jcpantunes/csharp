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
using System.Data;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;
using GEP_DE607.Persistencia;
using GEP_DE607.Util;

namespace GEP_DE607
{
    public class BaseWindow
    {
        public BaseWindow() { }

        public void preencherComboSistema(ComboBox cmb)
        {
            cmb.Items.Add(preencherComboItem(0, "Todos"));
            cmb.Items.Add(preencherComboItem(0, Constantes.SISTEMA_ESOCIAL));
            cmb.Items.Add(preencherComboItem(0, Constantes.SISTEMA_DCTFWEB));
            cmb.SelectedIndex = 0;
        }

        public void preencherComboProjeto(ComboBox cmb, bool todos)
        {
            cmb.Items.Clear();
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
                    cmb.Items.Add(preencherComboItem(p.Id, p.Nome));
                }
                cmb.SelectedIndex = 0;
            }
        }

        public void preencherListBoxProjeto(ListBox lst, string sistema)
        {
            lst.Items.Clear();
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = new List<Projeto>();
            if (sistema.Length == 0 || sistema.Equals("Todos"))
            {
                lista = pDAO.recuperar();
            }
            else
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add(Projeto.SISTEMA, sistema);
                lista = pDAO.recuperar(param);
            }
            if (lista.Count > 0)
            {
                foreach (Projeto p in lista)
                {
                    lst.Items.Add(preencherListItem(p.Id, p.Nome));
                }
            }
        }

        private ComboBoxItem preencherComboItem(int codigo, string nome)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Tag = codigo;
            item.Content = nome;
            return item;
        }

        private ListBoxItem preencherListItem(int codigo, string nome)
        {
            ListBoxItem item = new ListBoxItem();
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
            List<string> lista = Util.Util.retornarListaLotacao();
            if (lista.Count > 0)
            {
                foreach (string lotacao in lista)
                {
                    cmb.Items.Add(preencherComboItem(0, lotacao));
                }
                cmb.SelectedIndex = 0;
                preencherComboFuncionario(cmbFuncionario, lista[0], true);
            }
        }

        public void preencherComboLotacao(ComboBox cmb, ListBox lstFuncionario, int selectedIndex)
        {
            List<string> lista = Util.Util.retornarListaLotacao();
            if (lista.Count > 0)
            {
                foreach (string lotacao in lista)
                {
                    cmb.Items.Add(preencherComboItem(0, lotacao));
                }
                cmb.SelectedIndex = selectedIndex;
                preencherListBoxFuncionario(lstFuncionario, lista[selectedIndex]);
            }
        }

        public void preencherListBoxSprint(ListBox lst, int codigoProjeto)
        {
            lst.Items.Clear();
            SprintDAO sDAO = new SprintDAO();

            List<Sprint> lista = new List<Sprint>();
            if (codigoProjeto == 0)
            {
                lista = sDAO.recuperar();
            }
            else 
            {
                lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            }

            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    lst.Items.Add(preencherListItem(s.Codigo, s.Nome));
                }
            }
        }

        public void preencherListBoxSprint(ListBox lst, List<int> listaProjeto)
        {
            lst.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(listaProjeto);
            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    lst.Items.Add(preencherListItem(s.Codigo, s.Nome));
                }
            }
        }

        public void preencherListBoxFuncionario(ListBox lst, string lotacao)
        {
            lst.Items.Clear();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Funcionario.LOTACAO, lotacao);

            FuncionarioDAO fDAO = new FuncionarioDAO();
            List<Funcionario> lista = fDAO.recuperar(param);

            if (lista.Count > 0)
            {
                foreach (Funcionario f in lista)
                {
                    lst.Items.Add(preencherListItem(f.Codigo, f.Nome));
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
                    cmb.Items.Add(preencherComboItem(f.Codigo, f.Nome));
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
            List<Projeto> lista = pDAO.recuperar(Projeto.criarListaParametros(idProjeto));
            if (lista.Count > 0)
            {
                listaProjeto.Add(lista[0]);
                return lista[0];
            }
            return new Projeto();
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
                f = new Funcionario(0, "DEBHE/DE607", responsavel);
                fDAO.incluir(f.encapsularLista());
                f = fDAO.recuperar(responsavel);
            }
            listaFuncionario.Add(f);
            return f;
        }

        public void preencherGrid(DataGrid grid, DataTable tabela, int width)
        {
            if (tabela != null)
            {
                grid.Columns.Clear();
                foreach (DataColumn col in tabela.Columns)
                {
                    grid.Columns.Add(new DataGridTextColumn
                    {
                        Header = col.ColumnName,
                        Width = width,
                        Binding = new Binding(string.Format("[{0:0.##}]", col.ColumnName))
                    });
                }
                grid.DataContext = tabela;
            }
        }
    }
}
