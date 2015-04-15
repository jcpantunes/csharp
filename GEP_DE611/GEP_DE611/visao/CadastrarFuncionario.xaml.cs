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
    /// <summary>
    /// Interaction logic for CadastrarFuncionario.xaml
    /// </summary>
    public partial class CadastrarFuncionario : Window
    {
        public CadastrarFuncionario()
        {
            InitializeComponent();

            preencherLista();

            preencherComboProjeto();

            iniciarCampos();

            iniciarCamposFiltro();
        }

        private void iniciarCampos()
        {
            txtCodigo.Text = "0";
            txtNome.Text = "";
            cmbLotacao.SelectedIndex = 0;
        }

        private void preencherCampos(Funcionario f)
        {
            txtCodigo.Text = Convert.ToString(f.Codigo);
            txtNome.Text = f.Nome;
            foreach (ComboBoxItem item in cmbLotacao.Items)
            {
                if (item.Content.Equals(f.Lotacao))
                {
                    cmbLotacao.SelectedItem = item;
                    break;
                }
            }
        }

        private void preencherLista()
        {
            try
            {
                FuncionarioDAO pDAO = new FuncionarioDAO();
                tblFuncionario.ItemsSource = pDAO.recuperar();
            }
            catch (Exception ex)
            {
                Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                alerta.Show();

                this.Close();
            }
        }

        private void preencherComboProjeto()
        {
            ComboBoxItem itemTodos = new ComboBoxItem();
            itemTodos.Content = "Todos";
            itemTodos.Tag = 0;
            cmbFiltroLotacao.Items.Add(itemTodos);

            List<string> lista = Util.retornarListaLotacao();
            foreach (string l in lista)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = l;
                cmbLotacao.Items.Add(item);

                ComboBoxItem itemFiltro = new ComboBoxItem();
                itemFiltro.Content = l;
                cmbFiltroLotacao.Items.Add(itemFiltro);
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length == 0 || cmbLotacao.SelectedIndex < 0)
            {
                Alerta alerta = new Alerta("Favor preencher todos os campos");
                alerta.Show();
            }
            else
            {
                string lotacao = Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content);
                Funcionario f = new Funcionario(Convert.ToInt32(txtCodigo.Text), lotacao, txtNome.Text);

                FuncionarioDAO sDAO = new FuncionarioDAO();
                if (f.Codigo == 0)
                {
                    sDAO.incluir(f.encapsularLista());
                }
                else
                {
                    sDAO.atualizar(f.encapsularLista());
                }

                Alerta alerta = new Alerta("Salvo com sucesso.");
                alerta.Show();

                iniciarCampos();

                preencherLista();
            }
        }

        private void tblFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int linha = tblFuncionario.SelectedIndex;
            if (linha >= 0 && tblFuncionario.SelectedItem is Funcionario)
            {
                Funcionario f = (Funcionario)tblFuncionario.SelectedItem;
                preencherCampos(f);
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCampos();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtCodigo.Text) > 0 && txtNome.Text.Length != 0 && cmbLotacao.SelectedIndex >= 0)
            {
                string lotacao = Convert.ToString(((ComboBoxItem)cmbLotacao.SelectedItem).Content);
                Funcionario s = new Funcionario(Convert.ToInt32(txtCodigo.Text), lotacao, txtNome.Text);

                FuncionarioDAO sDAO = new FuncionarioDAO();
                try
                {
                    sDAO.excluir(s.encapsularLista());
                    Alerta alerta = new Alerta("Excluido com sucesso.");
                    alerta.Show();
                }
                catch (Exception ex)
                {
                    Alerta alerta = new Alerta("Um erro inesperado ocorreu." + '\n' + ex.Message);
                    alerta.Show();
                }
            }
            else
            {
                Alerta alerta = new Alerta("Projeto não existente ou os dados do projeto foram alterados. Favor selecionar o projeto novamente.");
                alerta.Show();
            }

            iniciarCampos();
            preencherLista();
        }


        private void iniciarCamposFiltro()
        {
            txtFiltroNome.Text = "";
            cmbFiltroLotacao.SelectedIndex = 0;
        }

        private void btnFiltroLimpar_Click(object sender, RoutedEventArgs e)
        {
            iniciarCamposFiltro();
            preencherLista();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            if (txtFiltroNome.Text.Length > 0)
            {
                param.Add(Funcionario.NOME, txtFiltroNome.Text);
            }
            if (cmbFiltroLotacao.SelectedIndex > 0)
            {
                param.Add(Funcionario.LOTACAO, Convert.ToString(((ComboBoxItem)cmbFiltroLotacao.SelectedItem).Content));
            }

            if (param.Count > 0)
            {
                try
                {
                    FuncionarioDAO pDAO = new FuncionarioDAO();
                    tblFuncionario.ItemsSource = pDAO.recuperar(param);
                }
                catch (Exception ex)
                {
                    Alerta alerta = new Alerta("Problema ao tentar acessar o banco de dados: \n" + ex.Message);
                    alerta.Show();

                    this.Close();
                }
            }
            else
            {
                preencherLista();
            }
        }
    }
}
