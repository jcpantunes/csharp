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
    /// Interaction logic for VisualizarApropriacao.xaml
    /// </summary>
    public partial class VisualizarApropriacao : Window
    {
        BaseWindow baseWindow = null;

        public VisualizarApropriacao()
        {
            InitializeComponent();

            baseWindow = new BaseWindow();

            preencherCombos();
        }

        private void preencherCombos()
        {
            baseWindow.preencherComboSistema(cmbSistema);

            baseWindow.preencherListBoxProjeto(lstProjeto, "");

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
            if (cmbLotacao.SelectedIndex >= 0 &&
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
                
                DataTable tabela = new DataTable();
                if (opcao == OpcaoIndicadorApropriacao.APROPRIACAO)
                {
                    List<ApropriacaoTarefa> listaApropriacao = new List<ApropriacaoTarefa>();
                    foreach (Funcionario func in listaFuncionario)
                    {
                        ApropriacaoBO tDAO = new ApropriacaoBO();
                        listaApropriacao.AddRange(tDAO.recuperarApropriacaoPorResponsavel(func.Nome, dtInicio, dtFim));
                    }

                    object[] listaColunas = { "Nome", "Data", "Horas", "Tarefa", "Titulo", "Macroatividade", "Mnemonico" };
                    foreach (string str in listaColunas)
                    {
                        tabela.Columns.Add(Convert.ToString(str));
                    }

                    foreach (ApropriacaoTarefa item in listaApropriacao)
                    {
                        object[] linha = new object[listaColunas.Count()];
                        linha[0] = item.Nome;
                        linha[1] = item.Data;
                        linha[2] = item.Hora;
                        linha[3] = item.Tarefa;
                        linha[4] = item.Titulo;
                        linha[5] = item.Macroatividade;
                        linha[6] = item.Mnemonico;
                        tabela.Rows.Add(linha);
                    }
                }
                baseWindow.preencherGrid(grid, tabela, 80);
            }
        }

        private void apropriacaoRealizado_Expanded(object sender, RoutedEventArgs e)
        {
            executarAcao(tblApropriacaoRealizada, OpcaoIndicadorApropriacao.APROPRIACAO, true);
        }
    }

    class OpcaoIndicadorApropriacao
    {

        public const int APROPRIACAO = 2;

    }

}
