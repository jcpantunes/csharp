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
using GEP_DE607.Util;
using GEP_DE607.Componente;
using GEP_DE607.Dominio;
using GEP_DE607.Negocio;
using GEP_DE607.Persistencia;

namespace GEP_DE607
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
                foreach(ListBoxItem item in lstProjeto.SelectedItems)
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
            expNumTarefasPorSprint.IsExpanded = false;
            expNumItensPorSprint.IsExpanded = false;
            expComplexidadeItensPorSprint.IsExpanded = false;
            expNumDefeitosCorrigidosPorSprint.IsExpanded = false;
            expNumRelatosCorrigidosPorSprint.IsExpanded = false;

            //expNumDefeitosPorItemBacklog.IsExpanded = false;
            //expNumTarefasEstimativaMaiorTempoGasto.IsExpanded = false;
            //expNumTarefasTempoGastoMaior24.IsExpanded = false;

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
                listaFuncionario.AddRange(fDAO.Recuperar(param));
            }
            else
            {
                FuncionarioDAO fDAO = new FuncionarioDAO();
                foreach (ListBoxItem item in lstFuncionario.SelectedItems)
                {
                    int codigo = Convert.ToInt32(item.Tag);
                    listaFuncionario.Add(fDAO.Recuperar(codigo));
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
                            Width = 60,
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
                            linha[i + 1] = tDAO.recuperarQtdeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_RELATO_CORRIGIDO_POR_SPRINT)
                        {
                            BugDAO rDAO = new BugDAO(Constantes.RELATO);
                            linha[i + 1] = rDAO.recuperarQtdeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_ITEM_POR_SPRINT)
                        {
                            ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                            linha[i + 1] = ibDAO.recuperarQtdeItensBacklogPorSprintPorResponsavel(listaColunas[i], func.Codigo);
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
                        else if (opcao == OpcaoIndicador.NUM_DEFEITO_CORRIGIDO_POR_SPRINT)
                        {
                            // =SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0)*($Backlog.$M$2:$M$200))/SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0))
                            // M = Quantidade de defeitos do Item

                            BugDAO dDAO = new BugDAO(Constantes.DEFEITO);
                            linha[i + 1] = dDAO.recuperarQtdeItensPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        }
                        else if (opcao == OpcaoIndicador.NUM_DEFEITO_CRIADO_POR_SPRINT)
                        {
                            BugBO dBO = new BugBO(Constantes.DEFEITO);
                            linha[i + 1] = dBO.recuperarQtdeItensPorSprintPorCriador(listaColunas[i], func.Nome);
                        }
                        //else if (opcao == OpcaoIndicador.NUM_DEFEITO_POR_ITEM_BACKLOG)
                        //{
                        //    // =SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0)*($Backlog.$M$2:$M$200))/SOMARPRODUTO(($Backlog.$E$2:$E$200=B$2)*($Backlog.$R$2:$R$200>0))
                        //    // M = Quantidade de defeitos do Item

                        //    BugDAO dDAO = new BugDAO(Constantes.DEFEITO);
                        //    linha[i + 1] = dDAO.recuperarMediaDefeitosPorSprintPorResponsavel(listaColunas[i], func.Codigo);
                        //}
                        //else if (opcao == OpcaoIndicador.NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO)
                        //{
                        //    TarefaDAO tDAO = new TarefaDAO();
                        //    linha[i + 1] = tDAO.recuperarQtdeTarefasPorSprintTempoGastoMaiorEstimativa(listaColunas[i], func.Codigo);
                        //}
                        //else if (opcao == OpcaoIndicador.NUM_TAREFA_TEMPO_GASTO_MAIOR_24)
                        //{
                        //    TarefaDAO tDAO = new TarefaDAO();
                        //    linha[i + 1] = tDAO.recuperarQtdeTarefasTempoGastoMaior24(listaColunas[i], func.Codigo);
                        //}
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

        private void numDefeitosCorrigidosPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumDefeitosCorrigidos, lblMediaNumDefeitosCorrigidos, OpcaoIndicador.NUM_DEFEITO_CORRIGIDO_POR_SPRINT, true);
        }

        private void numRelatosCorrigidosPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumRelatosCorrigidosPorSprint, lblMediaNumRelatosCorrigidosPorSprint, OpcaoIndicador.NUM_RELATO_CORRIGIDO_POR_SPRINT, true);
        }

        private void numNumDefeitosCriadosPorSprint_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblNumDefeitosCriadosPorSprint, lblMediaNumDefeitosCriadosPorSprint, OpcaoIndicador.NUM_DEFEITO_CRIADO_POR_SPRINT, true);
        }

        //private void numDefeitosPorItemBacklog_Expanded(object sender, RoutedEventArgs e)
        //{
        //    executarAcao(tblNumDefeitosPorItemBacklog, lblMediaNumDefeitos, OpcaoIndicador.NUM_DEFEITO_POR_ITEM_BACKLOG, false);
        //}

        //private void numTarefasEstimativaMaiorTempoGasto_Expanded(object sender, RoutedEventArgs e)
        //{
        //    executarAcao(tblNumTarefasEstimativaMaiorTempoGasto, lblMediaNumTarefasEstimativaMaiorTempoGasto, OpcaoIndicador.NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO, true);
        //}

        //private void numTarefasTempoGastoMaior24_Expanded(object sender, RoutedEventArgs e)
        //{
        //    executarAcao(tblNumTarefasTempoGastoMaior24, lblMediaNumTarefasTempoGastoMaior24, OpcaoIndicador.NUM_TAREFA_TEMPO_GASTO_MAIOR_24, true);
        //}

        private void sprintsComTarefa_Expanded(object sender, RoutedEventArgs e)
        {
            DataTable tabela = new DataTable();

            List<string> listaSprint = new List<string>();
            foreach (ListBoxItem item in lstSprint.SelectedItems)
            {
                listaSprint.Add(Convert.ToString(item.Content));
            }

            TarefaBO tarefaBO = new TarefaBO();
            Dictionary<string, int> resultado = tarefaBO.RecuperarQtdeTarefasPorSprints(listaSprint);
            object[] listaColunas = { "Planejado Para", "Quantidade" };
            foreach (string str in listaColunas)
            {
                tabela.Columns.Add(Convert.ToString(str));
            }

            foreach (string key in resultado.Keys)
            {
                object[] linha = new object[listaColunas.Count()];
                linha[0] = key;
                linha[1] = resultado[key];
                tabela.Rows.Add(linha);
            }
            baseWindow.preencherGrid(tblSprintsComTarefa, tabela, 80);
        }

        private bool validarConsultaDados(DataGrid grid, ref string linhaSelecionada, ref string colunaSelecionada)
        {
            bool isValid = false;
            if (grid.SelectedCells.Count > 0)
            {
                int indexColuna = grid.CurrentCell.Column.DisplayIndex;
                if (indexColuna > 0 && indexColuna < (grid.SelectedCells.Count - 1))
                {
                    DataRow linha = (grid.SelectedItem as DataRowView).Row;
                    linhaSelecionada = linha[0] as string;

                    DataColumn coluna = linha.Table.Columns[indexColuna];
                    colunaSelecionada = coluna.ColumnName;

                    isValid = true;
                }
            }
            return isValid;
        }

        private void tblNumItensPorSprint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            string linhaSelecionada = "";
            string colunaSelecionada = "";
            if (validarConsultaDados(grid, ref linhaSelecionada, ref colunaSelecionada))
            {
                FuncionarioBO funcBO = new FuncionarioBO();
                int codigo = funcBO.Recuperar(Funcionario.GerarParametros(Funcionario.NOME, linhaSelecionada)).FirstOrDefault().Codigo;

                ItemBacklogBO itemBO = new ItemBacklogBO();
                List<ItemBacklog> listaBacklog = itemBO.recuperarItensBacklogPorSprintPorResponsavel(colunaSelecionada, codigo);

                DataTable tabela = new DataTable();
                int[] listaTamColunas = { 80, 300, 80, 80 };
                object[] listaColunas = { ItemBacklog.PROJETO, ItemBacklog.TITULO, ItemBacklog.STATUS, ItemBacklog.COMPLEXIDADE };

                List<object[]> listaLinhas = new List<object[]>();
                foreach (ItemBacklog item in listaBacklog)
                {
                    object[] linha = { item.Projeto, item.Titulo, item.Status, item.Complexidade };
                    listaLinhas.Add(linha);
                }
                ConsultarDados tela = new ConsultarDados();
                string titulo = String.Format("Consulta Itens Backlog trabalhados no sprint {0} por {1}", colunaSelecionada, linhaSelecionada);
                tela.preencherTabela(titulo, tabela, listaTamColunas, listaColunas, listaLinhas);
                tela.Show();
            }
        }

        private void tblNumTarefaTrabalhado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            string linhaSelecionada = "";
            string colunaSelecionada = "";
            if (validarConsultaDados(grid, ref linhaSelecionada, ref colunaSelecionada))
            {
                FuncionarioBO funcBO = new FuncionarioBO();
                int codigo = funcBO.Recuperar(Funcionario.GerarParametros(Funcionario.NOME, linhaSelecionada)).FirstOrDefault().Codigo;

                TarefaBO itemBO = new TarefaBO();
                List<Tarefa> listaTarefa = itemBO.RecuperarTarefasPorSprintPorResponsavel(colunaSelecionada, codigo);

                DataTable tabela = new DataTable();
                int[] listaTamColunas = { 80, 80, 300, 80, 80 };
                object[] listaColunas = { Tarefa.ID, Tarefa.PROJETO, Tarefa.TITULO, Tarefa.STATUS, Tarefa.TEMPO_GASTO };

                List<object[]> listaLinhas = new List<object[]>();
                foreach (Tarefa item in listaTarefa)
                {
                    object[] linha = { item.Id, item.Projeto, item.Titulo, item.Status, item.TempoGasto };
                    listaLinhas.Add(linha);
                }
                ConsultarDados tela = new ConsultarDados();
                string titulo = String.Format("Consulta Tarefas trabalhadas no sprint {0} por {1}", colunaSelecionada, linhaSelecionada);
                tela.preencherTabela(titulo, tabela, listaTamColunas, listaColunas, listaLinhas);
                tela.Show();
            }
        }

        private void recuperarDefeitoOuRelato(DataGrid grid, string tipo)
        {
            string linhaSelecionada = "";
            string colunaSelecionada = "";
            if (validarConsultaDados(grid, ref linhaSelecionada, ref colunaSelecionada))
            {
                FuncionarioBO funcBO = new FuncionarioBO();
                int codigo = funcBO.Recuperar(Funcionario.GerarParametros(Funcionario.NOME, linhaSelecionada)).FirstOrDefault().Codigo;

                BugBO itemBO = new BugBO(tipo);
                List<Bug> listaBug = itemBO.recuperarBugsPorSprintPorResponsavel(colunaSelecionada, codigo);

                DataTable tabela = new DataTable();
                int[] listaTamColunas = { 80, 80, 300, 80, 80 };
                object[] listaColunas = { Bug.ID, Bug.PROJETO, Bug.TITULO, Bug.STATUS, Bug.RESOLUCAO };

                List<object[]> listaLinhas = new List<object[]>();
                foreach (Bug item in listaBug)
                {
                    object[] linha = { item.Id, item.Projeto, item.Titulo, item.Status, item.Resolucao };
                    listaLinhas.Add(linha);
                }
                ConsultarDados tela = new ConsultarDados();
                string titulo = String.Format("Consulta " + tipo + "s trabalhadas no sprint {0} por {1}", colunaSelecionada, linhaSelecionada);
                tela.preencherTabela(titulo, tabela, listaTamColunas, listaColunas, listaLinhas);
                tela.Show();
            }
        }

        private void tblNumDefeitosCorrigidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            recuperarDefeitoOuRelato(grid, Constantes.DEFEITO);
        }

        private void tblNumRelatosCorrigidosPorSprint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            recuperarDefeitoOuRelato(grid, Constantes.RELATO);
        }

        private void tblNumDefeitosCriadosPorSprint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            string linhaSelecionada = "";
            string colunaSelecionada = "";
            if (validarConsultaDados(grid, ref linhaSelecionada, ref colunaSelecionada))
            {
                BugBO itemBO = new BugBO(Constantes.DEFEITO);
                List<Bug> listaBug = itemBO.recuperarItensPorSprintPorCriador(colunaSelecionada, linhaSelecionada);

                DataTable tabela = new DataTable();
                int[] listaTamColunas = { 80, 80, 300, 80, 80 };
                object[] listaColunas = { Bug.ID, Bug.PROJETO, Bug.TITULO, Bug.STATUS, Bug.RESOLUCAO };

                List<object[]> listaLinhas = new List<object[]>();
                foreach (Bug item in listaBug)
                {
                    object[] linha = { item.Id, item.Projeto, item.Titulo, item.Status, item.Resolucao };
                    listaLinhas.Add(linha);
                }
                ConsultarDados tela = new ConsultarDados();
                string titulo = String.Format("Consulta " + Constantes.DEFEITO + "s criados no sprint {0} por {1}", colunaSelecionada, linhaSelecionada);
                tela.preencherTabela(titulo, tabela, listaTamColunas, listaColunas, listaLinhas);
                tela.Show();
            }
        }
    }

    class OpcaoIndicador
    {
        public const int NUM_SPRINTS_COM_TAREFA = 1;

        public const int NUM_ITEM_POR_SPRINT = 3;

        public const int COMPLEXIDADE_ITEM_POR_SPRINT = 5;

        public const int NUM_TAREFA_POR_SPRINT = 6;

        public const int NUM_DEFEITO_CORRIGIDO_POR_SPRINT = 7;

        public const int NUM_RELATO_CORRIGIDO_POR_SPRINT = 9;

        public const int NUM_DEFEITO_CRIADO_POR_SPRINT = 10;

        public const int NUM_DEFEITO_POR_ITEM_BACKLOG = 11;

        public const int NUM_TAREFA_ESTIMATIVA_MAIOR_TEMPO_GASTO = 13;

        public const int NUM_TAREFA_TEMPO_GASTO_MAIOR_24 = 15;

    }
}
