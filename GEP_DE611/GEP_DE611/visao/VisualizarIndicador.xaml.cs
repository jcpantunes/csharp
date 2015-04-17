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
using GEP_DE611.componente;
using GEP_DE611.dominio;
using GEP_DE611.dominio.util;
using GEP_DE611.persistencia;


namespace GEP_DE611.visao
{
    /// <summary>
    /// Interaction logic for VisualizarIndicador.xaml
    /// </summary>
    public partial class VisualizarIndicador : Window
    {
        public VisualizarIndicador()
        {
            InitializeComponent();

            preencherCombos();
        }

        private void preencherCombos()
        {
            preencherComboLotacao();

            preencherComboProjeto();
        }

        private void preencherComboLotacao()
        {
            List<string> lista = Util.retornarListaLotacao();
            if (lista.Count > 0)
            {
                foreach (string lotacao in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = lotacao;
                    cmbLotacao.Items.Add(item);
                }
                cmbLotacao.SelectedIndex = 0;
                preencherComboFuncionario(lista[0]);
            }
        }

        private void preencherComboFuncionario(string lotacao)
        {
            cmbFuncionario.Items.Clear();
            List<Funcionario> lista = recuperarListaFuncionario(lotacao);

            if (lista.Count > 0)
            {
                ComboBoxItem itemTodos = new ComboBoxItem();
                itemTodos.Content = "Todos";
                itemTodos.Tag = 0;
                cmbFuncionario.Items.Add(itemTodos);
                cmbFuncionario.SelectedIndex = 0;

                foreach (Funcionario f in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = f.Nome;
                    item.Tag = f.Codigo;
                    cmbFuncionario.Items.Add(item);
                }
                cmbFuncionario.SelectedIndex = 0;
            }
        }

        private List<Funcionario> recuperarListaFuncionario(string lotacao)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(Funcionario.LOTACAO, lotacao);

            FuncionarioDAO fDAO = new FuncionarioDAO();
            return fDAO.recuperar(param);
        }

        private void preencherComboProjeto()
        {
            ProjetoDAO pDAO = new ProjetoDAO();
            List<Projeto> lista = pDAO.recuperar();
            if (lista.Count > 0)
            {
                foreach (Projeto p in lista)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = p.Nome;
                    item.Tag = p.Codigo;
                    cmbProjeto.Items.Add(item);
                }
                cmbProjeto.SelectedIndex = 0;
                preencherListBoxSprint(lista[0].Codigo);
            }
        }

        private void preencherListBoxSprint(int codigoProjeto)
        {
            lstSprint.Items.Clear();
            SprintDAO sDAO = new SprintDAO();
            List<Sprint> lista = sDAO.recuperar(Sprint.criarListaParametrosPesquisaPorProjeto(codigoProjeto));
            if (lista.Count > 0)
            {
                foreach (Sprint s in lista)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    lstSprint.Items.Add(item);
                }
            }
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            preencherComboFuncionario(lotacao);
        }

        private void cmbProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbProjeto.SelectedItem;
            int codigo = Convert.ToInt32(item.Tag);
            preencherListBoxSprint(codigo);
        }

        private void btnNumItemTrabalhado_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool validarExibicaoTabela()
        {
            if (cmbLotacao.SelectedIndex >= 0 && cmbFuncionario.SelectedIndex >= 0
                && cmbProjeto.SelectedIndex >= 0 && lstSprint.SelectedItems.Count > 0)
            {
                return true;
            }
            else 
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            return false;
        }

        private void prepararTabela(DataTable tabela, List<Funcionario> listaFuncionario, List<string> listaColunas)
        {
            tabela.Columns.Add("Nome", typeof(string));
            foreach (ListBoxItem item in lstSprint.SelectedItems)
            {
                listaColunas.Add(Convert.ToString(item.Content));
                tabela.Columns.Add(Convert.ToString(item.Content), typeof(int));
            }
            tabela.Columns.Add("Media", typeof(decimal));

            if (cmbFuncionario.SelectedIndex == 0)
            {
                listaFuncionario.AddRange(recuperarListaFuncionario(Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content)));
            }
            else
            {
                FuncionarioDAO fDAO = new FuncionarioDAO();
                listaFuncionario.Add(fDAO.recuperar(Convert.ToInt32(((ComboBoxItem)cmbFuncionario.SelectedItem).Tag)));
            }
        }

        private void preencherGrid(DataGrid grid, DataTable tabela)
        {
            if (tabela != null) // table is a DataTable
            {
                grid.Columns.Clear();
                foreach (DataColumn col in tabela.Columns)
                {
                    grid.Columns.Add(new DataGridTextColumn
                    {
                        Header = col.ColumnName,
                        Width = 70,
                        Binding = new Binding(string.Format("[{0}]", col.ColumnName))
                    });
                }
                grid.DataContext = tabela;
            }
        }

        private void executarAcao(DataGrid grid, int opcao)
        {
            if (validarExibicaoTabela())
            {
                DataTable tabela = new DataTable();
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                List<string> listaColunas = new List<string>();
                prepararTabela(tabela, listaFuncionario, listaColunas);

                foreach (Funcionario func in listaFuncionario)
                {
                    object[] linha = new object[listaColunas.Count + 2]; // +2 por causa das colunas nome e media
                    linha[0] = func.Nome;
                    decimal media = 0;
                    for (int i = 0; i < listaColunas.Count; i++)
                    {
                        if (opcao == OpcaoIndicador.NUM_TAREFA_POR_SPRINT)
                        {
                            TarefaDAO tDAO = new TarefaDAO();
                            linha[i + 1] = tDAO.recuperarQtdeTarefasPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_ITEM_POR_SPRINT)
                        {
                            ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                            linha[i + 1] = ibDAO.recuperarQtdeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.COMPLEXIDADE_ITEM_POR_SPRINT)
                        {
                            ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                            linha[i + 1] = ibDAO.recuperarComplexidadeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else
                        {
                            linha[i + 1] = i;
                        }
                        media += Convert.ToInt32(linha[i + 1]);
                    }
                    linha[listaColunas.Count + 1] = (media / listaColunas.Count);
                    tabela.Rows.Add(linha);
                }
                preencherGrid(grid, tabela);
            }
        }

        private void numTarefasPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumTarefaTrabalhado, OpcaoIndicador.NUM_TAREFA_POR_SPRINT);
        }

        private void numItensPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumItemTrabalhado, OpcaoIndicador.NUM_ITEM_POR_SPRINT);
        }

        private void complexidadeItensPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblComplexidadeItemTrabalhado, OpcaoIndicador.COMPLEXIDADE_ITEM_POR_SPRINT);
        }
    }

    class OpcaoIndicador
    {
        public const int NUM_TAREFA_POR_SPRINT = 0;

        public const int NUM_ITEM_POR_SPRINT = 1;

        public const int COMPLEXIDADE_ITEM_POR_SPRINT = 2;
    }
}
