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
        BaseWindow baseWindow = null;

        public VisualizarIndicador()
        {
            InitializeComponent();

            baseWindow = new BaseWindow();

            preencherCombos();
        }

        private void preencherCombos()
        {
            baseWindow.preencherComboLotacao(cmbLotacao, lstFuncionario, 10);

            baseWindow.preencherComboProjeto(cmbProjeto, false);

            if (cmbProjeto.Items.Count > 0)
            {
                cmbProjeto.SelectedIndex = 0;
                int codigo = Convert.ToInt32(((ComboBoxItem)cmbProjeto.SelectedItem).Tag);
                baseWindow.preencherListBoxSprint(lstSprint, codigo);
            }
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            baseWindow.preencherListBoxFuncionario(lstFuncionario, lotacao);

            chkTodosFuncionario.IsChecked = true;
        }

        private void cmbProjeto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int codigo = Convert.ToInt32(((ComboBoxItem)cmbProjeto.SelectedItem).Tag);
            baseWindow.preencherListBoxSprint(lstSprint, codigo);
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            expNumTarefasPorSprint.IsExpanded = false;
            expNumItensPorSprint.IsExpanded = false;
            expComplexidadeItensPorSprint.IsExpanded = false;
            expNumDefeitosPorItemBacklog.IsExpanded = false;
            expNumDefeitosCorrigidosPorSprint.IsExpanded = false;
            expNumTarefasEstimativaMaiorTempoGasto.IsExpanded = false;
            expNumTarefasTempoGastoMaior24.IsExpanded = false;

            expNumTarefasPorSprint.IsExpanded = true;
            expNumItensPorSprint.IsExpanded = true;
            expComplexidadeItensPorSprint.IsExpanded = true;
            expNumDefeitosPorItemBacklog.IsExpanded = true;
            expNumDefeitosCorrigidosPorSprint.IsExpanded = true;
            expNumTarefasEstimativaMaiorTempoGasto.IsExpanded = true;
            expNumTarefasTempoGastoMaior24.IsExpanded = true;
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
            if (cmbProjeto.SelectedIndex >= 0 && lstSprint.SelectedItems.Count > 0 && cmbLotacao.SelectedIndex >= 0 && 
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

        private void prepararTabela(DataTable tabela, List<Funcionario> listaFuncionario, List<string> listaColunas, bool inteiro)
        {
            tabela.Columns.Add("Nome", typeof(string));
            foreach (ListBoxItem item in lstSprint.SelectedItems)
            {
                listaColunas.Add(Convert.ToString(item.Content));
                if (inteiro)
                {
                    tabela.Columns.Add(Convert.ToString(item.Content), typeof(int));
                }
                else
                {
                    tabela.Columns.Add(Convert.ToString(item.Content), typeof(decimal));
                }
            }
            tabela.Columns.Add("Media", typeof(decimal));

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
        }

        private void preencherGrid(DataGrid grid, DataTable tabela)
        {
            if (tabela != null) // table is a DataTable
            {
                int i = 0;
                grid.Columns.Clear();
                foreach (DataColumn col in tabela.Columns)
                {
                    if (i == 0)
                    {
                        grid.Columns.Add(new DataGridTextColumn
                        {
                            Header = col.ColumnName,
                            Width = 180,
                            Binding = new Binding(string.Format("[{0:0.##}]", col.ColumnName))
                        });
                    }
                    else
                    {
                        grid.Columns.Add(new DataGridTextColumn
                        {
                            Header = col.ColumnName,
                            Width = 40,
                            Binding = new Binding(string.Format("[{0:0.##}]", col.ColumnName))
                        });
                    }
                    i++;
                }
                grid.DataContext = tabela;
            }
        }

        private void executarAcao(DataGrid grid, Label lbl, int opcao, bool inteiro)
        {
            if (validarExibicaoTabela())
            {
                DataTable tabela = new DataTable();
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                List<string> listaColunas = new List<string>();
                prepararTabela(tabela, listaFuncionario, listaColunas, inteiro);

                decimal mediaGeral = 0;
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
                        else if (opcao == OpcaoIndicador.NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO)
                        {
                            TarefaDAO tDAO = new TarefaDAO();
                            linha[i + 1] = tDAO.recuperarQtdeTarefasPorSprintTempoGastoMaiorEstimativa(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_TAREFA_TEMPO_GASTO_MAIOR_24)
                        {
                            TarefaDAO tDAO = new TarefaDAO();
                            linha[i + 1] = tDAO.recuperarQtdeTarefasTempoGastoMaior24(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_ITEM_POR_SPRINT)
                        {
                            ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                            linha[i + 1] = ibDAO.recuperarQtdeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.COMPLEXIDADE_ITEM_POR_SPRINT)
                        {
                            // =SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0)*($Backlog.$I$2:$I$200))/SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0))
                            // E = Planejado Para
                            // R = Somatorio numero tarefas do funcionario no item de backlog
                            // I = Complexidade do Item

                            ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                            linha[i + 1] = ibDAO.recuperarComplexidadeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_DEFEITO_POR_ITEM_BACKLOG)
                        {
                            // =SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0)*($Backlog.$M$2:$M$200))/SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0))
                            // M = Quantidade de defeitos do Item

                            DefeitoDAO dDAO = new DefeitoDAO();
                            linha[i + 1] = dDAO.recuperarMediaDefeitosPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_DEFEITO_CORRIGIDO_POR_SPRINT)
                        {
                            // =SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0)*($Backlog.$M$2:$M$200))/SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0))
                            // M = Quantidade de defeitos do Item

                            DefeitoDAO dDAO = new DefeitoDAO();
                            linha[i + 1] = dDAO.recuperarDefeitosCorrigidosResponsavel(listaColunas[i], func.Codigo);
                        }
                        else
                        {
                            linha[i + 1] = i;
                        }
                        media += Convert.ToDecimal(linha[i + 1]);
                    }
                    mediaGeral += Decimal.Round((media / listaColunas.Count), 2);
                    linha[listaColunas.Count + 1] = Decimal.Round((media / listaColunas.Count), 2);
                    tabela.Rows.Add(linha);
                }
                lbl.Content = Decimal.Round((mediaGeral / tabela.Rows.Count), 2); ;
                preencherGrid(grid, tabela);
            }
        }

        private void numTarefasPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumTarefaTrabalhado, lblMediaNumTarefas, OpcaoIndicador.NUM_TAREFA_POR_SPRINT, true);
        }

        private void numItensPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumItemTrabalhado, lblMediaNumItens, OpcaoIndicador.NUM_ITEM_POR_SPRINT, true);
        }

        private void complexidadeItensPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblComplexidadeItemTrabalhado, lblMediaComplexidade, OpcaoIndicador.COMPLEXIDADE_ITEM_POR_SPRINT, false);
        }

        private void numDefeitosPorItemBacklog_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumDefeitosPorItemBacklog, lblMediaNumDefeitos, OpcaoIndicador.NUM_DEFEITO_POR_ITEM_BACKLOG, false);
        }

        private void numDefeitosCorrigidosPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumDefeitosCorrigidos, lblMediaNumDefeitosCorrigidos, OpcaoIndicador.NUM_DEFEITO_CORRIGIDO_POR_SPRINT, true);
        }

        private void numTarefasEstimativaMaiorTempoGasto_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumTarefasEstimativaMaiorTempoGasto, lblMediaNumTarefasEstimativaMaiorTempoGasto, OpcaoIndicador.NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO, true);
        }

        private void numTarefasTempoGastoMaior24_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumTarefasTempoGastoMaior24, lblMediaNumTarefasTempoGastoMaior24, OpcaoIndicador.NUM_TAREFA_TEMPO_GASTO_MAIOR_24, true);
        }

        // Numero de horas apropriadas por sprint
    }

    class OpcaoIndicador
    {
        public const int NUM_TAREFA_POR_SPRINT = 0;

        public const int NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO = 1;

        public const int NUM_TAREFA_TEMPO_GASTO_MAIOR_24 = 2;

        public const int NUM_ITEM_POR_SPRINT = 3;

        public const int COMPLEXIDADE_ITEM_POR_SPRINT = 4;

        public const int NUM_DEFEITO_POR_ITEM_BACKLOG =5;

        public const int NUM_DEFEITO_CORRIGIDO_POR_SPRINT = 6;
    }
}
