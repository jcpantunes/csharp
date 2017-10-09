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
using GEP_DE607.Dominio.Modelo;
using GEP_DE607.Negocio;
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

        private void executarAcao(DataGrid grid, int opcao, bool inteiro)
        {
            if (validarExibicaoTabela())
            {
                DateTime dtInicio = this.txtDtInicio.Text.Length > 0 ? Convert.ToDateTime(this.txtDtInicio.Text) : new DateTime(2014, 01, 01);
                DateTime dtFim = this.txtDtFinal.Text.Length > 0 ? Convert.ToDateTime(this.txtDtFinal.Text) : new DateTime(2020, 12, 31);

                FuncionarioDAO fDAO = new FuncionarioDAO();
                List<Funcionario> listaFuncionario = new List<Funcionario>();
                if (chkTodosFuncionario.IsChecked == true)
                {
                    String lotacao = Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content);
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add(Funcionario.LOTACAO, lotacao);
                    listaFuncionario.AddRange(fDAO.Recuperar(param));
                }
                else
                {
                    foreach (ListBoxItem item in lstFuncionario.SelectedItems)
                    {
                        int codigo = Convert.ToInt32(item.Tag);
                        listaFuncionario.Add(fDAO.Recuperar(codigo));
                    }
                }
                List<string> listaSprint = new List<string>();
                foreach (ListBoxItem item in lstSprint.SelectedItems)
                {
                    listaSprint.Add(Convert.ToString(item.Content));
                }
                DataTable tabela = new DataTable();

                if (opcao == OpcaoIndicadorDados.ITEM_BACKLOG_TRABALHADO)
                {
                    List<ItemBacklog> listaItemBacklog = new List<ItemBacklog>();
                    foreach (Funcionario func in listaFuncionario)
                    {
                        ItemBacklogDAO ibDAO = new ItemBacklogDAO();
                        listaItemBacklog.AddRange(ibDAO.recuperarItensBacklogPorSprintPorResponsavel(listaSprint, func.Codigo));
                    }

                    object[] listaColunas = { "Tipo", "ID", "Título", "Status", "Planejado Para", "Pai", "Data de Modificação", "ID do Projeto", "Valor definido para o Negócio", "Tamanho Estimado", "Complexidade", "PF" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    foreach (ItemBacklog item in listaItemBacklog)
                    {
                        object[] linha = new object[listaColunas.Count()];
                        linha[0] = item.Tipo;
                        linha[1] = item.Id;
                        linha[2] = item.Titulo;
                        linha[3] = item.Status;
                        linha[4] = item.PlanejadoPara;
                        linha[5] = item.Pai;
                        linha[6] = item.DataModificacao;
                        linha[7] = item.Projeto;
                        linha[8] = item.ValorNegocio;
                        linha[9] = item.Tamanho;
                        linha[10] = item.Complexidade;
                        linha[11] = item.Pf;
                        tabela.Rows.Add(linha);
                    }
                }
                else if (opcao == OpcaoIndicadorDados.TAREFA_TRABALHADO)
                {
                    List<Tarefa> listaTarefa = new List<Tarefa>();
                    foreach (Funcionario func in listaFuncionario)
                    {
                        TarefaBO tDAO = new TarefaBO();
                        listaTarefa.AddRange(tDAO.RecuperarTarefasPorSprintPorResponsavel(listaSprint, func.Codigo));
                    }

                    object[] listaColunas = { "ID", "Título", "Responsavel", "Status", "Planejado Para", "Pai", "Data", "Projeto", "Classificação", "Estimativa", "Tempo Gasto" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    foreach (Tarefa item in listaTarefa)
                    {
                        object[] linha = new object[listaColunas.Count()];
                        // linha[0] = item.Tipo;
                        linha[0] = item.Id;
                        linha[1] = item.Titulo;
                        linha[2] = item.Responsavel.Nome;
                        linha[3] = item.Status;
                        linha[4] = item.PlanejadoPara;
                        linha[5] = item.Pai;
                        linha[6] = item.DataModificacao;
                        linha[7] = item.Projeto;
                        linha[8] = item.Classificacao;
                        linha[9] = item.Estimativa;
                        linha[10] = item.TempoGasto;
                        tabela.Rows.Add(linha);
                    }
                }
                baseWindow.preencherGrid(grid, tabela, 80);
            }
        }

        private void itensBacklogTrabalhado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tbltemBacklogTrabalhado, OpcaoIndicadorDados.ITEM_BACKLOG_TRABALHADO, true);
        }

        private void tarefasTrabalhado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblTarefaTrabalhado, OpcaoIndicadorDados.TAREFA_TRABALHADO, true);
        }

    }

    class OpcaoIndicadorDados
    {
        public const int ITEM_BACKLOG_TRABALHADO = 0;

        public const int TAREFA_TRABALHADO = 1;
    }
}
