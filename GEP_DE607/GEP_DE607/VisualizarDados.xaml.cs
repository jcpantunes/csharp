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
using GEP_DE607.Componente;
using GEP_DE607.Dominio;
using GEP_DE607.Persistencia;

namespace GEP_DE607
{
    /// <summary>
    /// Interaction logic for VisualizarDados.xaml
    /// </summary>
    public partial class VisualizarDados : Window
    {

        BaseWindow baseWindow = null;

        public VisualizarDados()
        {
            InitializeComponent();

            baseWindow = new BaseWindow();

            preencherCombos();
        }

        private void preencherCombos()
        {
            baseWindow.preencherComboSistema(cmbSistema);

            baseWindow.preencherListBoxProjeto(lstProjeto, "");

            baseWindow.preencherListBoxSprint(lstSprint, 0);

            baseWindow.preencherComboLotacao(cmbLotacao, lstFuncionario, 6);
        }

        private void cmbSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbSistema.SelectedItem;
            string sistema = Convert.ToString(item.Content);
            baseWindow.preencherListBoxProjeto(lstProjeto, sistema);
        }

        private void lstProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int> listaProjeto = new List<int>();
            if (lstProjeto.SelectedItems.Count > 0)
            {
                foreach (ListBoxItem item in lstProjeto.SelectedItems)
                {
                    int idProjeto = Convert.ToInt32(item.Tag);
                    listaProjeto.Add(idProjeto);
                }
                baseWindow.preencherListBoxSprint(lstSprint, listaProjeto);
            }
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            baseWindow.preencherListBoxFuncionario(lstFuncionario, lotacao);
            chkTodosFuncionario.IsChecked = true;
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            //expNumTarefasPorSprint.IsExpanded = false;
            //expNumItensPorSprint.IsExpanded = false;
            //expComplexidadeItensPorSprint.IsExpanded = false;
            //expNumDefeitosPorItemBacklog.IsExpanded = false;
            //expNumDefeitosCorrigidosPorSprint.IsExpanded = false;
            //expNumTarefasEstimativaMaiorTempoGasto.IsExpanded = false;
            //expNumRelatosPorSprint.IsExpanded = false;
        }

        private void lstFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFuncionario.SelectedItems.Count > 0)
            {
                chkTodosFuncionario.IsChecked = false;
            }
            else
            {
                chkTodosFuncionario.IsChecked = true;
            }
        }

        private bool validarExibicaoTabela()
        {
            if (lstProjeto.SelectedIndex >= 0 && lstSprint.SelectedItems.Count > 0 && cmbLotacao.SelectedIndex >= 0 &&
                lstFuncionario.Items.Count > 0 && (chkTodosFuncionario.IsChecked == true || lstFuncionario.SelectedItems.Count > 0))
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

        //private void prepararTabela(DataTable tabela, List<Funcionario> listaFuncionario, List<string> listaColunas, bool inteiro)
        //{
        //    tabela.Columns.Add("Nome", typeof(string));
        //    foreach (ListBoxItem item in lstSprint.SelectedItems)
        //    {
        //        listaColunas.Add(Convert.ToString(item.Content));
        //        if (inteiro)
        //        {
        //            tabela.Columns.Add(Convert.ToString(item.Content), typeof(int));
        //        }
        //        else
        //        {
        //            tabela.Columns.Add(Convert.ToString(item.Content), typeof(decimal));
        //        }
        //    }

        //    if (chkTodosFuncionario.IsChecked == true)
        //    {
        //        FuncionarioDAO fDAO = new FuncionarioDAO();
        //        String lotacao = Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content);

        //        Dictionary<string, string> param = new Dictionary<string, string>();
        //        param.Add(Funcionario.LOTACAO, lotacao);
        //        listaFuncionario.AddRange(fDAO.recuperar(param));
        //    }
        //    else
        //    {
        //        FuncionarioDAO fDAO = new FuncionarioDAO();
        //        foreach (ListBoxItem item in lstFuncionario.SelectedItems)
        //        {
        //            int codigo = Convert.ToInt32(item.Tag);
        //            listaFuncionario.Add(fDAO.recuperar(codigo));
        //        }
        //    }
        //}

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
                        Width = 80,
                        Binding = new Binding(string.Format("[{0:0.##}]", col.ColumnName))
                    });
                }
                grid.DataContext = tabela;
            }
        }

        private void executarAcao(DataGrid grid, int opcao, bool inteiro)
        {
            if (validarExibicaoTabela())
            {
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                if (chkTodosFuncionario.IsChecked == true)
                {
                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    String lotacao = Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content);
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add(Funcionario.LOTACAO, lotacao);
                    listaFuncionario.AddRange(fDAO.recuperar(param));
                }
                else
                {
                    FuncionarioDAO fDAO = new FuncionarioDAO();
                    foreach (ListBoxItem item in lstFuncionario.SelectedItems)
                    {
                        int codigo = Convert.ToInt32(item.Tag);
                        listaFuncionario.Add(fDAO.recuperar(codigo));
                    }
                }
                List<ItemBacklog> listaItemBacklog = new List<ItemBacklog>();
                foreach (Funcionario func in listaFuncionario)
                {
                    List<string> listaSprint = new List<string>();
                    foreach (ListBoxItem item in lstSprint.SelectedItems)
                    {
                        listaSprint.Add(Convert.ToString(item.Content));
                    }
                    ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                    listaItemBacklog = ibDAO.recuperarItensBacklogPorSprintPorResponsavel(listaSprint, func.Codigo);
                }
                
                DataTable tabela = new DataTable();
                
                object[] listaColunas = { "Tipo", "ID", "Título", "Responsável", "Status", "Planejado Para", "Pai", "Data de Modificação", "ID do Projeto", "Valor definido para o Negócio", "Tamanho Estimado", "Complexidade", "PF" };
                foreach(string str in listaColunas)
                {
                    tabela.Columns.Add(Convert.ToString(str));
                }

                foreach (ItemBacklog item in listaItemBacklog)
                {
                    object[] linha = new object[listaColunas.Count()];
                    linha[0] = item.Tipo;
                    linha[1] = item.Id;
                    linha[2] = item.Titulo;
                    linha[3] = item.Responsavel;
                    linha[4] = item.Status;
                    linha[5] = item.PlanejadoPara;
                    linha[6] = item.Pai;
                    linha[7] = item.DataModificacao;
                    linha[8] = item.Projeto;
                    linha[9] = item.ValorNegocio;
                    linha[10] = item.Tamanho;
                    linha[11] = item.Complexidade;
                    linha[12] = item.Pf;
                    tabela.Rows.Add(linha);
                }
                preencherGrid(grid, tabela);
            }
        }

        private void itensBacklogTrabalhado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tbltemBacklogTrabalhado, OpcaoIndicadorDados.ITEM_BACKLOG_TRABALHADO, true);
        }
    }

    class OpcaoIndicadorDados
    {
        public const int ITEM_BACKLOG_TRABALHADO = 0;
    }
}
