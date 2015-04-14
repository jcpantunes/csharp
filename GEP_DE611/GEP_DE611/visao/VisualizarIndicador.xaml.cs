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

            preencherCombo();
        }

        private void preencherCombo()
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

        private void cmbLotacao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem) cmbLotacao.SelectedItem;
            string lotacao = Convert.ToString(item.Content);
            preencherComboFuncionario(lotacao);
        }

        private void btnNumItemTrabalhado_Click(object sender, RoutedEventArgs e)
        {
            if (cmbLotacao.SelectedIndex >= 0 && cmbFuncionario.SelectedIndex >= 0)
            {
                // <DataGridTextColumn Header="Codigo" Width="60" Binding="{Binding Path=Codigo}" />

                List<Funcionario> listaFuncionario = recuperarListaFuncionario(Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content));

                // NOME |   eSocial-281573-1.0.0-CONS-01    |   eSocial-281573-1.0.0-CONS-02    |   Media
                tblNumItemTrabalhado.Columns.Add(recuperarColuna("Nome", 100, 0));
                tblNumItemTrabalhado.Columns.Add(recuperarColuna("eSocial-281573-1.0.0-CONS-01", 200, 1));
                //tblNumItemTrabalhado.Columns.Add(recuperarColuna("eSocial-281573-1.0.0-CONS-02", 200));
                //tblNumItemTrabalhado.Columns.Add(recuperarColuna("Media", 100));

                List<DataGridRow> listaRow = new List<DataGridRow>();
                foreach (Funcionario func in listaFuncionario)
                {
                    listaRow.Add(recuperarLinha(func.Nome, 2, 5));
                }
                tblNumItemTrabalhado.ItemsSource = listaRow;
            }
        }

        private DataGridTextColumn recuperarColuna(string nomeColuna, int tamanho, int coluna)
        {
            return new DataGridTextColumn
            {
                Header = nomeColuna,
                Width = tamanho,
                Binding = new Binding("["+coluna+"]")
            };
        }

        private DataGridRow recuperarLinha(string nome, int t1, int t2)
        {
            List<string> lista = new List<String>();
            lista.Add(nome);
            lista.Add(Convert.ToString(t1));
            //lista.Add(Convert.ToString(t2));
            //int media = (t1 + t2)/2;
            //lista.Add(Convert.ToString(media));

            DataGridRow linha = new DataGridRow();
            linha.DataContext = lista;

            return linha;
        }
    }
}
