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
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = s.Nome;
                    item.Tag = s.Codigo;
                    lstSprint.Items.Add(item);
                }
                lstSprint.SelectedIndex = 0;
            }
        }

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem) cmbLotacao.SelectedItem;
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
            if (cmbLotacao.SelectedIndex >= 0 && cmbFuncionario.SelectedIndex >= 0)
            {
                tblNumItemTrabalhado.Columns.Clear();
                // <DataGridTextColumn Header="Codigo" Width="60" Binding="{Binding Path=Codigo}" />

                // NOME |   eSocial-281573-1.0.0-CONS-01    |   eSocial-281573-1.0.0-CONS-02    |   Media

                DataTable table = new DataTable();
                table.Columns.Add("Nome", typeof(string));
                table.Columns.Add("eSocial-281573-1.0.0-CONS-01", typeof(int));
                table.Columns.Add("eSocial-281573-1.0.0-CONS-02", typeof(int));
                table.Columns.Add("Media", typeof(decimal));

                List<Funcionario> listaFuncionario = recuperarListaFuncionario(Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content));
                foreach (Funcionario func in listaFuncionario)
                {
                    decimal media = (3 + 5) / 2;
                    table.Rows.Add(func.Nome, 3, 5, media);
                }

                if (table != null) // table is a DataTable
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        tblNumItemTrabalhado.Columns.Add(new DataGridTextColumn
                        {
                            Header = col.ColumnName,
                            Width = 100,
                            Binding = new Binding(string.Format("[{0}]", col.ColumnName))
                        });
                    }
                    tblNumItemTrabalhado.DataContext = table;
                }
            }
        }

        
    }
}
